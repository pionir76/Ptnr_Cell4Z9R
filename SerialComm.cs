using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;
using System.Threading;
using System.Windows.Forms;
using System.Security.Cryptography;

namespace Ptnr
{
    public delegate void UpdateCtrlStatusEventHandler(int addr, string strSts);

    public class Command
    {
        public string StrCmd { get; set; }
        public int ErrCnt { get; set; }

        public Command(string str)
        { 
            StrCmd = str;
            ErrCnt = 0;
        }
    }

    public class SerialComm
    {
        private SerialPort _port;
        private int _portNo;
        private CommConfig _commCfg;

        public bool IsRun { get; private set; }
        public List<Command> WriteBuff { get; private set; }

        public event UpdateCtrlStatusEventHandler UpdateCtrlStatus;
        public event EventHandler CommClose;

        public SerialComm()
        {
            IsRun = false;
            WriteBuff = new List<Command>();
        }

        ~SerialComm()
        {
            CommStop();

        }

        public bool CommStart(CommConfig commCfg, int port)
        {
            if(port == 15)
            {
                return false;
            }

            if(PortOpen(commCfg, port))
            {
                WriteBuff.Clear();
                IsRun = true;

                Thread t = new Thread(() => CommLoop());
                t.Start();
            }
            else
            {
                return false;
            }

            return true;
        }

        public void CommStop()
        {
            IsRun = false;
        }

        private void PortClose()
        {
            if(_port != null)
            {
                if (_port.IsOpen)
                {
                    _port.Close();
                    _port.Dispose();
                }
            }
        }

        private bool PortOpen(CommConfig _commCfg, int _portNo)
        {
            if (_port == null)
            {
                _port = new SerialPort();
            }

            if(!_port.IsOpen)
            {
                _port.PortName = string.Format("COM{0}", _portNo);
                _port.BaudRate = _commCfg.BaudRate;
                _port.Parity = _commCfg.Parity;
                _port.DataBits = _commCfg.DataBit;
                _port.StopBits = _commCfg.StopBits;

                try
                {
                    _port.Open();
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    _port.Close();
                }

            }
            return _port.IsOpen;
        }

        private void CommLoop()
        {
            while (IsRun)
            {
                if (!_port.IsOpen)
                {
                    Thread.Sleep(1000);
                    return;
                }
                
                if (WriteBuff.Count > 0)
                {
                    string cmd = WriteBuff[0].StrCmd;

                    List<byte> bytes = Encoding.ASCII.GetBytes(cmd).ToList();
                    SysDefs.makePcLinkSum(bytes);

                    string rcv = Send(bytes);
                    bool bSuccess = false;

                    //Console.WriteLine(">> Snd Command :" + cmd);
                    //Console.WriteLine(">> Rcv Command :" + rcv);

                    if (rcv != null && SysDefs.checkPcLinkSum(rcv))
                    {
                        if (rcv.IndexOf("OK") >= 0)
                        {
                            WriteBuff.RemoveAt(0);
                            bSuccess = true;
                        }
                    }

                    // Timeout : We have to remove failed command from the list.
                    if (bSuccess == false)
                    {
                        WriteBuff[0].ErrCnt++;
                        if (WriteBuff[0].ErrCnt > 15)
                        {
                            WriteBuff.RemoveAt(0);
                        }
                        Thread.Sleep(200);
                    }
                }

                //|================================================|
                //|                       |                        |
                //|  Chamber #1 (Addr1)   |   SDR100 #1 (Addr3)    |
                //|                       |                        |
                //|=================================================
                //|================================================|
                //|                       |                        |
                //|  Chamber #2 (Addr2)   |   SDR100 #2 (Addr6)    |
                //|                       |                        |
                //|=================================================
                //|================================================|
                //|                       |                        |
                //|  Chamber #3 (Addr4)   |   SDR100 #3 (Addr7)    |
                //|                       |                        |
                //|=================================================
                //|================================================|
                //|                       |                        |
                //|  Chamber #4 (Addr5)   |   SDR100 #4 (Addr8)    |
                //|                       |                        |
                //|================================================|

                UpdateCtrlSts(SysDefs.ADDR_CHAMBER1);
                UpdateCtrlSts(SysDefs.ADDR_RECORDER1);

                UpdateCtrlSts(SysDefs.ADDR_CHAMBER2);
                UpdateCtrlSts(SysDefs.ADDR_RECORDER2);

                UpdateCtrlSts(SysDefs.ADDR_CHAMBER3);
                UpdateCtrlSts(SysDefs.ADDR_RECORDER3);

                UpdateCtrlSts(SysDefs.ADDR_CHAMBER4);
                UpdateCtrlSts(SysDefs.ADDR_RECORDER4);

                Thread.Sleep(500);
            }
            PortClose();

            if(CommClose != null)
            {
                CommClose(this, EventArgs.Empty);
            }
        }

        public void UpdateCtrlSts(int addr)
        {
            string cmd = "";

            //--------------------------------------------------------------------------//
            // Read Chamber#1 Status
            // - TEMI(TPV:1, TSP:2, HPV:5, HSP:6, NOWSTS:10)
            // - TEMP(PV:1, SP:3, NOWSTS:10)
            //--------------------------------------------------------------------------//
            if (addr == SysDefs.ADDR_CHAMBER1 || addr == SysDefs.ADDR_CHAMBER2 || 
                addr == SysDefs.ADDR_CHAMBER3 || addr == SysDefs.ADDR_CHAMBER4)
            {
                if (addr == SysDefs.ADDR_CHAMBER1) cmd = string.Format("01RRD,06,0001,0002,0003,0005,0006,0010");
                if (addr == SysDefs.ADDR_CHAMBER2) cmd = string.Format("02RRD,06,0001,0002,0003,0005,0006,0010");

                if (addr == SysDefs.ADDR_CHAMBER3) cmd = string.Format("04RRD,06,0001,0002,0003,0005,0006,0010");
                if (addr == SysDefs.ADDR_CHAMBER4) cmd = string.Format("05RRD,06,0001,0002,0003,0005,0006,0010");
            }

            //--------------------------------------------------------------------------//
            // Read Recorder#1 Status (NPV1~NPV9)
            //--------------------------------------------------------------------------//
            else if (addr == SysDefs.ADDR_RECORDER1 || addr == SysDefs.ADDR_RECORDER2 ||
                     addr == SysDefs.ADDR_RECORDER3 || addr == SysDefs.ADDR_RECORDER4 )
            {
                if (addr == SysDefs.ADDR_RECORDER1) cmd = string.Format("03RRD,10,0001,0002,0003,0004,0005,0006,0007,0008,0009,0016");
                if (addr == SysDefs.ADDR_RECORDER2) cmd = string.Format("06RRD,10,0001,0002,0003,0004,0005,0006,0007,0008,0009,0016");
                if (addr == SysDefs.ADDR_RECORDER3) cmd = string.Format("07RRD,10,0001,0002,0003,0004,0005,0006,0007,0008,0009,0016");
                if (addr == SysDefs.ADDR_RECORDER4) cmd = string.Format("08RRD,10,0001,0002,0003,0004,0005,0006,0007,0008,0009,0016");
            }

            List<byte> dTx = Encoding.ASCII.GetBytes(cmd).ToList();
            SysDefs.makePcLinkSum(dTx);
            string rcv = Send(dTx);

            if (rcv != null)
            {
                //Console.WriteLine("SND> "+cmd);
                //Console.WriteLine("RCV> " + rcv);

                if (SysDefs.checkPcLinkSum(rcv))
                {
                    UpdateCtrlStatus(addr, rcv);
                }
            }
        }

        public void Write(string str)
        {
            Command c = new Command(str);
            WriteBuff.Add(c);
        }

        public void Write(int addr, ushort reg, short val)
        {
            string str = string.Format("{0:D2}WRD,01,{1:D4},{2:X4}", addr, reg, val);

            Command c = new Command(str);
            WriteBuff.Add(c);
        }

        public string Send(List<byte> dTx)
        {
            if (!_port.IsOpen)
            {
                return "";
            }

            Thread.Sleep(100);
            string strRes = "";

            try
            {
                _port.Write(dTx.ToArray(), 0, dTx.Count);
                Thread.Sleep(200);

                _port.ReadTimeout = 2000;
                int nRecv = _port.BytesToRead;

                if (nRecv>0)
                {
                    byte[] rBuff = new byte[nRecv];
                    if (_port.IsOpen)
                    {
                        _port.Read(rBuff, 0, nRecv);
                    }
                    
                    strRes = Encoding.ASCII.GetString(rBuff, 0, nRecv);
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

            return strRes;
        }
    }
}

## 一、ModbusRTU简介

ModbusRTU的全程是Modbus Remote Terminal Uint 远程终端单元，它的通讯链路主要是依赖串口通信（RS232/485）。通讯结构是主从模式，一个主站对应多个从站，只允许主站发送请求，从站根据主站发送的请求返回相应的响应。

<img width="919" height="480" alt="image" src="https://github.com/user-attachments/assets/7ea23d07-fe32-4a33-bb86-bb46cbab5c86" />

支持Modbus的设备一般会有以下寄存器:

- 线圈寄存器：控制电机的启动停止、灯光开关等，外部设备对它可读可写
- 离散输入寄存器：检查开关是否被触发、故障信号输入等，外部设备对它只读
- 保持寄存器：以word为单位(16bit)，主要是设置目标参数、例如目标温度、设备运行参数等，外部设备对它可读可写
- 输入寄存器：以word为单位(16bit)，主要是读取各种传感器的值，外部设备对它只读

根据这些寄存器的特性，对这些寄存器的操作就可以排列组合汇总一下，用功能码(FuncCode)来区分不同的操作：

- funcCode == 0x01 读线圈
- funcCode == 0x02 读离散输入
- funcCode == 0x03 读保持寄存器
- funcCode == 0x04 读输入寄存器
- funcCode == 0x05 写单个线圈
- funcCode == 0x06 写单个保持寄存器
- funcCode == 0x0F 写多个线圈
- funcCode == 0x10 写多个保持寄存器

请求数据帧中除了包含功能码(FuncCode)，还包括从站地址、数据、校验码。


<img width="600" height="165" alt="image" src="https://github.com/user-attachments/assets/3ff45ac4-4f41-4aec-8d35-93bedf36aff5" />

各种操作的详细报文图示如下：

<img width="1374" height="1080" alt="image" src="https://github.com/user-attachments/assets/8aa29871-f688-4ed5-9ad6-7a44157aed3b" />


## 二、ModbusRTU的操作图示

### 1、假设主站要从01号从站获取地址1,2,3这3个线圈的值

根据格式填入01(从站地址)01(功能码)00 01(线圈起始地址)00 03(线圈数量)2D CB(CRC校验码)，然后广播发送给所有的设备


<img width="2003" height="1037" alt="image" src="https://github.com/user-attachments/assets/52bfcc1b-e61f-4fcd-8a39-06762efc028d" />


各个从站接受到后和从站地址与自己相对比，如果是自己就接受该数据帧，否则就丢弃


<img width="1907" height="1049" alt="image" src="https://github.com/user-attachments/assets/f6f6ff76-717a-452d-b3e8-45160a5cdc6a" />


<img width="1897" height="1047" alt="image" src="https://github.com/user-attachments/assets/58713790-92d8-4250-95ee-6299b9e63b26" />


01从站对这个数据进行CRC校验，确认无误后就处理该请求


<img width="1896" height="1058" alt="image" src="https://github.com/user-attachments/assets/abe5a051-d641-45ad-af28-220342054030" />


从站组装报文 01(从站地址) 01(功能码) 01(线圈值001高位补0后得00000001再转为16进制得01) 01(数据长度1字节) 90 48(CRC校验码)，后广播发送给主站：

<img width="1895" height="1068" alt="image" src="https://github.com/user-attachments/assets/8b65800c-e7d6-4573-aea5-adcc1249cc7c" />


### 2、假设主站要从03号从站获得地址1、2这2个保持寄存器的值

根据格式填入03(从站地址)03(功能码)00 01(保持寄存器起始地址)00 02(保持寄存器数量)2D CB(CRC校验码)，然后广播发送给所有的设备，仅03从站接收


<img width="1899" height="1062" alt="image" src="https://github.com/user-attachments/assets/dd82c51e-075a-4775-b859-152e06ad4fe4" />


03从站对这个数据进行CRC校验，确认无误后就处理该请求.<br>


从站组装报文 03(从站地址) 03(功能码) 04(数据位总字节) 00 E9(233转换为2进制后再转为16进制) 00 8B(139转换为2进制后再转为16进制) 48 60(CRC校验码)，后广播发送给主站：


<img width="1899" height="1056" alt="image" src="https://github.com/user-attachments/assets/ba1fc2ef-d7d6-40a5-a91e-6cf8514aaa61" />


### 3、假设主站要改02号从站地址为1的线圈值为1


根据格式填入02(从站地址)05(功能码)00 01(线圈地址)FF 00(线圈值FF 00 表示1)DD C9(CRC校验码)，然后广播发送给所有的设备，仅02从站接收


<img width="1899" height="1066" alt="image" src="https://github.com/user-attachments/assets/aedec3cf-5d32-430a-8a72-d838a92ddbac" />


修改成功后从站将报文原样返回，主站对比接收报文和发送报文是否一样即可


<img width="1909" height="1069" alt="image" src="https://github.com/user-attachments/assets/9e64dd2d-01ae-4cc4-93dc-08c71165a0c4" />



### 4、假设主站要改03号从站地址为1的保持寄存器值为123


根据格式填入03(从站地址)06(功能码)00 01(保持寄存器地址)00 7B(值123)99 CB(CRC校验码)，然后广播发送给所有的设备，仅03从站接收


<img width="1913" height="1067" alt="image" src="https://github.com/user-attachments/assets/b39412ec-60e0-4f26-ab43-1222ee183a50" />


修改成功后从站将报文原样返回，主站对比接收报文和发送报文是否一样即可


<img width="1903" height="1060" alt="image" src="https://github.com/user-attachments/assets/dafe46e8-a746-4273-8a07-31e99e39e4e2" />





## 三、封装ModbusRTU类

首先需要SerialInfo类来作为串口的数据暂存

```c#
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.Communication
{
    public class SerialInfo
    {
        public string PortName { get; set; } = "COM1";
        public int BaudRate { get; set; } = 9600;
        public int DataBit { get; set; } = 8;
        public Parity Parity { get; set; } = Parity.None;
        public StopBits StopBits { get; set; } = StopBits.One;
    }
}
```

然后定义一个RTU类来封装所有通信的操作

```c#
using System;
using System.Collections.Generic;
using System.IO.Ports;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication.Modbus
{
    public class RTU
    {
        public Action<int, List<byte>> ResponseData;

        private static RTU _instance;
        private static SerialInfo _serialInfo;

        SerialPort _serialPort;
        bool _isBusing = false;


        int _currentSlave;  //当前请求的从站地址
        int _funcCode;      //当前请求的功能码
        int _wordLen;       //当前请求的字长
        int _startAddr;     //当前请求的起始地址


        /// <summary>
        /// 使用单例模式来构造：只第一次传入 serialInfo 有效，后续调用即使传不同参数也不会重建。
        /// </summary>
        private RTU(SerialInfo serialInfo)
        {
            _serialPort = new SerialPort();
            _serialInfo = serialInfo;
        }

        public static RTU GetInstance(SerialInfo serialInfo)
        {
            lock ("rtu")
            {
                if (_instance == null)
                    _instance = new RTU(serialInfo);
                return _instance;
            }
        }


        /// <summary>
        /// 串口操作
        /// </summary>
        public bool Connection()   //打开串口
        {
            try
            {
                if (_serialPort.IsOpen)
                    _serialPort.Close();

                _serialPort.PortName = _serialInfo.PortName;
                _serialPort.BaudRate = _serialInfo.BaudRate;
                _serialPort.DataBits = _serialInfo.DataBit;
                _serialPort.Parity = _serialInfo.Parity;
                _serialPort.StopBits = _serialInfo.StopBits;

                _serialPort.ReceivedBytesThreshold = 1;
                _serialPort.DataReceived += _serialPort_DataReceived;

                _serialPort.Open();
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void Dispose()  //关闭串口
        {
            if (_serialPort.IsOpen)
            {
                _serialPort.Close();
                _serialPort.Dispose();
                _serialPort = null;
            }
        }



        /// <summary>
        /// RTU接收数据帧
        /// </summary>
        int _receiveByteCount = 0;
        byte[] _byteBuffer = new byte[512];
        private void _serialPort_DataReceived(object sender, SerialDataReceivedEventArgs e) //接收数据帧
        {
            //读取数据帧
            byte _receiveBytes;
            while (_serialPort.BytesToRead > 0)
            {
                _receiveBytes = (byte)_serialPort.ReadByte();
                _byteBuffer[_receiveByteCount] = _receiveBytes;
                _receiveByteCount++;
                if (_receiveByteCount >= 512)
                {
                    _receiveByteCount = 0;
                    //清除输入缓冲区
                    _serialPort.DiscardInBuffer();
                    return;
                }
            }
            //校验数据帧
            if (_byteBuffer[0] == (byte)_currentSlave && _byteBuffer[1] == _funcCode && _receiveByteCount >= _wordLen + 5)
            {
                // 检查crc
                ResponseData?.Invoke(_startAddr, new List<byte>(SubByteArray(_byteBuffer, 0, _wordLen + 3)));
                _serialPort.DiscardInBuffer();
            }
        }
        private byte[] SubByteArray(byte[] byteArr, int start, int len) //工具方法
        {
            byte[] Res = new byte[len];
            if (byteArr != null && byteArr.Length > len)
            {
                for (int i = 0; i < len; i++)
                {
                    Res[i] = byteArr[i + start];
                }
            }
            return Res;
        }



        /// <summary>
        /// RTU发送数据帧
        /// </summary>
        public async Task<bool> Send(int slaveAddr, byte funcCode, int startAddr, int len)  //发送数据帧
        {
            _currentSlave = slaveAddr;
            _funcCode = funcCode;
            _startAddr = startAddr;

            if (funcCode == 0x01)  //读线圈
                _wordLen = len / 8 + ((len % 8 > 0) ? 1 : 0);
            if (funcCode == 0x03)  //读保持寄存器
                _wordLen = len * 2;


            //拼接8字节的请求数据帧：[从站地址] [功能码] [起始地址高位] [起始地址低位] [长度高位] [长度低位] [CRC低位] [CRC高位]
            List<byte> sendBuffer = new List<byte>();
            sendBuffer.Add((byte)slaveAddr);
            sendBuffer.Add(funcCode);
            sendBuffer.Add((byte)(startAddr / 256));
            sendBuffer.Add((byte)(startAddr % 256));
            sendBuffer.Add((byte)(len / 256));
            sendBuffer.Add((byte)(len % 256));
            byte[] crc = Crc16(sendBuffer.ToArray(), 6);
            sendBuffer.AddRange(crc);

            
            try
            {
                while (_isBusing) { }  //串口已经被占用则自旋等待

                _isBusing = true;
                _serialPort.Write(sendBuffer.ToArray(), 0, 8);
                _isBusing = false;

                await Task.Delay(1000); //写完需要延迟来等待下位机返回的确认帧
            }
            catch
            {
                return false;
            }
                _receiveByteCount = 0;
            return true;
        }

        
        /// <summary>
        /// CRC校验
        /// </summary>
        #region  CRC校验
        private static readonly byte[] aucCRCHi = {
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x00, 0xC1, 0x81, 0x40,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40, 0x01, 0xC0, 0x80, 0x41, 0x01, 0xC0, 0x80, 0x41,
             0x00, 0xC1, 0x81, 0x40
         };
        private static readonly byte[] aucCRCLo = {
             0x00, 0xC0, 0xC1, 0x01, 0xC3, 0x03, 0x02, 0xC2, 0xC6, 0x06, 0x07, 0xC7,
             0x05, 0xC5, 0xC4, 0x04, 0xCC, 0x0C, 0x0D, 0xCD, 0x0F, 0xCF, 0xCE, 0x0E,
             0x0A, 0xCA, 0xCB, 0x0B, 0xC9, 0x09, 0x08, 0xC8, 0xD8, 0x18, 0x19, 0xD9,
             0x1B, 0xDB, 0xDA, 0x1A, 0x1E, 0xDE, 0xDF, 0x1F, 0xDD, 0x1D, 0x1C, 0xDC,
             0x14, 0xD4, 0xD5, 0x15, 0xD7, 0x17, 0x16, 0xD6, 0xD2, 0x12, 0x13, 0xD3,
             0x11, 0xD1, 0xD0, 0x10, 0xF0, 0x30, 0x31, 0xF1, 0x33, 0xF3, 0xF2, 0x32,
             0x36, 0xF6, 0xF7, 0x37, 0xF5, 0x35, 0x34, 0xF4, 0x3C, 0xFC, 0xFD, 0x3D,
             0xFF, 0x3F, 0x3E, 0xFE, 0xFA, 0x3A, 0x3B, 0xFB, 0x39, 0xF9, 0xF8, 0x38,
             0x28, 0xE8, 0xE9, 0x29, 0xEB, 0x2B, 0x2A, 0xEA, 0xEE, 0x2E, 0x2F, 0xEF,
             0x2D, 0xED, 0xEC, 0x2C, 0xE4, 0x24, 0x25, 0xE5, 0x27, 0xE7, 0xE6, 0x26,
             0x22, 0xE2, 0xE3, 0x23, 0xE1, 0x21, 0x20, 0xE0, 0xA0, 0x60, 0x61, 0xA1,
             0x63, 0xA3, 0xA2, 0x62, 0x66, 0xA6, 0xA7, 0x67, 0xA5, 0x65, 0x64, 0xA4,
             0x6C, 0xAC, 0xAD, 0x6D, 0xAF, 0x6F, 0x6E, 0xAE, 0xAA, 0x6A, 0x6B, 0xAB,
             0x69, 0xA9, 0xA8, 0x68, 0x78, 0xB8, 0xB9, 0x79, 0xBB, 0x7B, 0x7A, 0xBA,
             0xBE, 0x7E, 0x7F, 0xBF, 0x7D, 0xBD, 0xBC, 0x7C, 0xB4, 0x74, 0x75, 0xB5,
             0x77, 0xB7, 0xB6, 0x76, 0x72, 0xB2, 0xB3, 0x73, 0xB1, 0x71, 0x70, 0xB0,
             0x50, 0x90, 0x91, 0x51, 0x93, 0x53, 0x52, 0x92, 0x96, 0x56, 0x57, 0x97,
             0x55, 0x95, 0x94, 0x54, 0x9C, 0x5C, 0x5D, 0x9D, 0x5F, 0x9F, 0x9E, 0x5E,
             0x5A, 0x9A, 0x9B, 0x5B, 0x99, 0x59, 0x58, 0x98, 0x88, 0x48, 0x49, 0x89,
             0x4B, 0x8B, 0x8A, 0x4A, 0x4E, 0x8E, 0x8F, 0x4F, 0x8D, 0x4D, 0x4C, 0x8C,
             0x44, 0x84, 0x85, 0x45, 0x87, 0x47, 0x46, 0x86, 0x82, 0x42, 0x43, 0x83,
             0x41, 0x81, 0x80, 0x40
         };
        private byte[] Crc16(byte[] pucFrame, int usLen)
        {
            int i = 0;
            byte crcHi = 0xFF;
            byte crcLo = 0xFF;
            UInt16 iIndex = 0x0000;

            while (usLen-- > 0)
            {
                iIndex = (UInt16)(crcLo ^ pucFrame[i++]);
                crcLo = (byte)(crcHi ^ aucCRCHi[iIndex]);
                crcHi = aucCRCLo[iIndex];
            }

            return new byte[] { crcLo, crcHi };
        }


        #endregion
    }
}
```









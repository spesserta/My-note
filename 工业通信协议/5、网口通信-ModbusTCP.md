### 一、基本概念

ModbusTCP是基于TCP/IP协议实现的，通信链路是依赖以太网通信。在ModbusTCP中主站是TCP客户端，从站是TCP服务器！

<img width="943" height="495" alt="image" src="https://github.com/user-attachments/assets/840f560a-6bed-4c0c-a3b6-13198811d597" />


ModbusTCP的功能码和ModbusRTU是相同的，在此不多说。由于ModbusTCP是基于TCP实现的，TCP的数据链路层里面已经有差错校验了，因此无需校验码字段了，并且从站ID也更换成了MBAP报头（包含事务处理、协议、长度、单元）。

- 事务处理标识符：占用2字节，类似于报文分段后的序号，客户端产生新的事务符，然后服务器返回相同的事务符，以此来进行匹配
- 协议标识符：占用2字节，固定为0，0表示Modbus协议
- 长度标识符：占用2字节，表示长度之后的字节总数
- 单元标识符：占用1字节，一般设置为FF或者00，相当于RTU的从站地址，一般用不上

其他的数据部分和ModbusRTU是一样的。

<img width="1769" height="1074" alt="image" src="https://github.com/user-attachments/assets/edca245e-1f97-48cc-bf6e-317ca15a977d" />



### 二、执行过程


##### 1、假设要读取从站01的地址1、2、3这3个线圈的值

生成的报文结构为：00 01(事务处理标识符) 00 00(协议标识符)00 06(字节长度) FF(单位标识符) 01(功能码01) 00 01(线圈起始地址) 00 03(线圈数量) <br>

生成完毕后注意不是广播的形式发布了而是IP地址+端口号精确定位从站的地址


<img width="1895" height="1063" alt="image" src="https://github.com/user-attachments/assets/fc6f1202-5875-40d6-8ef1-e0cbdaa17bc5" />


从站收到请求后根据报文格式解析该请求，并且返回00 01(事务处理标识符) 00 00(协议标识符)00 04(字节长度) FF(单位标识符) 01(功能码01) 01(返回线圈的长度为1字节) 01(返回线圈值001高位补0后转换为16进制得01)


<img width="1898" height="1058" alt="image" src="https://github.com/user-attachments/assets/3b56cdc0-4a90-42bf-ba38-d70f4c5b11ce" />


<img width="1892" height="1060" alt="image" src="https://github.com/user-attachments/assets/a0903b88-a550-4c49-9f9d-7c3d5cf167b9" />


主站收到后确认无误后就得到了想要的数据


<img width="1895" height="1058" alt="image" src="https://github.com/user-attachments/assets/02b66af6-feb7-40f9-8b37-6c094c972db2" />



##### 其他操作可以根据图中来写，都大差不差的


<img width="1844" height="1077" alt="image" src="https://github.com/user-attachments/assets/4c54b827-8ae6-4ecb-adc3-a917012a12ad" />




### 三、封装TCP类

首先需要一个TCPInfo类来建立TCP的模型（IP地址+端口号），类似于封装Serial类

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Communication
{
    public class TcpInfo
    {
        public string IpAddress { get; set; } = "127.0.0.1";
        public int Port { get; set; } = 502;
    }
}
```

接下来就是封装TCP了

```c#
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Zhaoxi.Communication.Modbus
{
    public class TCP
    {
        public Action<int, List<byte>> ResponseData;

        private static TCP _instance;
        private static TcpInfo _tcpInfo;

        TcpClient _tcpClient;
        NetworkStream _stream;
        bool _isBusing = false;

        int _currentUnitId;
        int _funcCode;
        int _wordLen;
        int _startAddr;
        ushort _transactionId = 0;

        private TCP(TcpInfo tcpInfo)
        {
            _tcpClient = new TcpClient();
            _tcpInfo = tcpInfo;
        }

        public static TCP GetInstance(TcpInfo tcpInfo)
        {
            lock ("tcp")
            {
                if (_instance == null)
                    _instance = new TCP(tcpInfo);
                return _instance;
            }
        }

        public bool Connection()    //建立连接
        {
            try
            {
                if (_tcpClient.Connected)
                    _tcpClient.Close();

                _tcpClient.Connect(_tcpInfo.IpAddress, _tcpInfo.Port);
                _stream = _tcpClient.GetStream();

                // 开启后台线程监听接收数据
                Task.Run(ReceiveLoop);
            }
            catch
            {
                return false;
            }

            return true;
        }

        public void Dispose()  //关闭连接
        {
            if (_stream != null)
            {
                _stream.Close();
                _stream.Dispose();
                _stream = null;
            }

            if (_tcpClient.Connected)
            {
                _tcpClient.Close();
                _tcpClient.Dispose();
                _tcpClient = null;
            }
        }

        byte[] _receiveBuffer = new byte[1024];
        int _receiveByteCount = 0;

        private void ReceiveLoop()    //读取数据
        {
            try
            {
                while (_tcpClient.Connected)
                {
                    if (_stream.DataAvailable)
                    {
                        int bytesRead = _stream.Read(_receiveBuffer, _receiveByteCount,
                            _receiveBuffer.Length - _receiveByteCount);
                        _receiveByteCount += bytesRead;

                        // 判断是否收完一整帧：MBAP头里第4-5字节是长度
                        if (_receiveByteCount >= 6)
                        {
                            int frameLength = (_receiveBuffer[4] << 8) | _receiveBuffer[5];
                            frameLength += 6; // 加上MBAP头自身

                            if (_receiveByteCount >= frameLength)
                            {
                                // 检查单元标识符和功能码
                                if (_receiveBuffer[6] == (byte)_currentUnitId
                                    && _receiveBuffer[7] == _funcCode)
                                {
                                    ResponseData?.Invoke(_startAddr,
                                        new List<byte>(SubByteArray(_receiveBuffer, 7, frameLength - 7)));
                                }

                                _receiveByteCount = 0;
                                Array.Clear(_receiveBuffer, 0, _receiveBuffer.Length);
                            }
                        }
                    }

                    Task.Delay(10).Wait();
                }
            }
            catch { }
        }

        public async Task<bool> Send(int unitId, byte funcCode, int startAddr, int len)   //发送数据
        {
            _currentUnitId = unitId;
            _funcCode = funcCode;
            _startAddr = startAddr;

            if (funcCode == 0x01)
                _wordLen = len / 8 + ((len % 8 > 0) ? 1 : 0);
            if (funcCode == 0x03)
                _wordLen = len * 2;

            // 构建 PDU（协议数据单元：功能码 + 数据）
            List<byte> pdu = new List<byte>();
            pdu.Add(funcCode);
            pdu.Add((byte)(startAddr / 256));
            pdu.Add((byte)(startAddr % 256));
            pdu.Add((byte)(len / 256));
            pdu.Add((byte)(len % 256));

            // 构建 MBAP 头（7字节）
            _transactionId++;
            List<byte> sendBuffer = new List<byte>();
            sendBuffer.Add((byte)(_transactionId >> 8));   // 事务标识符高字节
            sendBuffer.Add((byte)(_transactionId & 0xFF));  // 事务标识符低字节
            sendBuffer.Add(0x00);                            // 协议标识符高字节（固定0）
            sendBuffer.Add(0x00);                            // 协议标识符低字节（固定0）
            sendBuffer.Add((byte)((pdu.Count + 1) >> 8));   // 长度高字节（单元标识符1字节 + PDU）
            sendBuffer.Add((byte)((pdu.Count + 1) & 0xFF)); // 长度低字节
            sendBuffer.Add((byte)unitId);                    // 单元标识符

            sendBuffer.AddRange(pdu);

            try
            {
                while (_isBusing) { }

                _isBusing = true;
                _stream.Write(sendBuffer.ToArray(), 0, sendBuffer.Count);
                _isBusing = false;

                await Task.Delay(1000);
            }
            catch
            {
                return false;
            }

            _receiveByteCount = 0;
            return true;
        }

        private byte[] SubByteArray(byte[] byteArr, int start, int len)  //工具类
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
    }
}
```





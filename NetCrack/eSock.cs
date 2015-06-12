using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;


/// <summary>
/// Made By BahNahNah
/// uid=2388291
/// </summary>
public static class eSocket
{
    public class Client
    {
        public delegate void DataRetrievedDelegate(eSocket.Client c, PacketStructure packet);
        public delegate void DisconnectDelegate(eSocket.Client c, SocketError SE);

        #region " Global Variables "

        Socket _cSock;
        byte[] _buffer;
        public event DataRetrievedDelegate DataRetrievedCallback;
        public event DisconnectDelegate DisconnectCallback;

        #endregion

        #region " Properties "
        public Socket NetworkSocket
        {
            get { return _cSock; }
        }

        #endregion

        #region " Initilisation "

        /// <summary>
        /// Initilise client with a default buffer size of 1024
        /// </summary>
        public Client()
        {
            _buffer = new byte[1024];
            _cSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
        }

        /// <summary>
        /// Initilise client
        /// </summary>
        /// <param name="bufferSize">Largest packet size the client can retrieve</param>
        public Client(int bufferSize)
        {
            _cSock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _buffer = new byte[bufferSize];
        }

        #endregion

        #region " Client Sub-Classes "


        #endregion

        #region " Connection "

        /// <summary>
        /// Connect to a server
        /// </summary>
        /// <param name="IP">the ip of the server</param>
        /// <param name="port">the port to connect to</param>
        /// <returns></returns>
        public bool Connect(string IP, int port)
        {
            try
            {
                _cSock.Connect(IP, port);
                return BeginCallbacks();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Connect to a server
        /// </summary>
        /// <param name="ip">ip of the server</param>
        /// <param name="port">port to connect to</param>
        /// <returns></returns>
        public bool Connect(IPAddress ip, int port)
        {
            try
            {
                _cSock.Connect(ip, port);
                return BeginCallbacks();
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Connect to a server
        /// </summary>
        /// <param name="ipPort">Endpoint of server to connect to</param>
        /// <returns></returns>
        public bool Connect(IPEndPoint ipPort)
        {
            try
            {
                _cSock.Connect(ipPort);
                return BeginCallbacks();
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region " Functions "

        public bool Send(PacketStructure packet)
        {
            try
            {
                _cSock.Send(packet.PacketBuffer);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region " Callbacks "
        private bool BeginCallbacks()
        {
            SocketError SE;
            _cSock.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, out SE, RetrieveCallback, null);
            return SE == SocketError.Success;
        }
        private void RetrieveCallback(IAsyncResult AR)
        {
            try
            {
                _cSock.EndReceive(AR);
                ushort PacketLength = PacketStructure.PacketLength(_buffer);
                byte[] _Packet = new byte[PacketLength];
                Buffer.BlockCopy(_buffer, 0, _Packet, 0, PacketLength);
                if (DataRetrievedCallback != null)
                    DataRetrievedCallback(this, new PacketStructure(_Packet));
                SocketError SE;
                _cSock.BeginReceive(_buffer, 0, _buffer.Length, SocketFlags.None, out SE, RetrieveCallback, null);
                if (SE != SocketError.Success)
                {
                    if (DisconnectCallback != null)
                        DisconnectCallback(null, SE);
                }
            }
            catch
            {
                if (DisconnectCallback != null)
                    DisconnectCallback(null, SocketError.NotConnected);
            }
        }

        #endregion
    }
    public class Server
    {
        public delegate void DataRetrievedDelegate(eSockClient c, PacketStructure packet, PacketError er);
        public delegate void DataRetrievedNotStructuredDelegate(eSockClient c, byte[] packet);
        public delegate void DisconnectDelegate(eSockClient c, SocketError SE);
        public delegate void ConnectionDelegate(eSockClient c);

        #region " Global Variables "

        Socket _Sock;
        int clientBufferSize = 1024;
        int _port;
        bool _structured = true;
        public event DataRetrievedDelegate DataRetrievedCallback;
        public event DisconnectDelegate ClientDisconnectCallback;
        public event ConnectionDelegate ClientConnectCallback;
        public event DataRetrievedNotStructuredDelegate DataRetrievedNotStructured;

        #endregion

        #region " Properties "
        public Socket NetworkSocket
        {
            get { return _Sock; }
        }

        #endregion

        #region " Initilisation "
        /// <summary>
        /// Start a server with a buffer size of 1024 and structured packets
        /// </summary>
        /// <param name="port">Port to listen on</param>
        public Server(int port)
        {
            clientBufferSize = 1024;
            _Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _port = port;
            _structured = true;
        }
        /// <summary>
        /// Start a server
        /// </summary>
        /// <param name="port">Port to listen on</param>
        /// <param name="bufferSize">Buffer size of server</param>
        /// <param name="structured">Use structured packets</param>
        public Server(int port, int bufferSize, bool structured)
        {
            clientBufferSize = bufferSize;
            _Sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _port = port;
            _structured = structured;
        }

        #endregion

        #region " Server Sub-Classes "

        public class eSockClient
        {
            private Socket cSock;
            private byte[] _buffer;
            private object _tag;

            /// <summary>
            /// Any extra data that is needed for the client can be stored in this object
            /// </summary>
            public object Tag
            {
                get { return _tag; }
                set { _tag = value; }
            }

            /// <summary>
            /// Change the size of the packetBuffer for this client
            /// </summary>
            /// <param name="size">new buffer size</param>
            public void ResizeBuffer(int size)
            {
                _buffer = new byte[size];
            }

            public byte[] PacketBuffer
            {
                get { return _buffer; }
                set { _buffer = value; }
            }
            public eSockClient(Socket c)
            {
                cSock = c;
            }
            public Socket NetworkSocket
            {
                get { return cSock; }
            }

            public bool Send(PacketStructure packet)
            {
                try
                {
                    cSock.Send(packet.PacketBuffer);
                    return true;
                }
                catch
                {
                    return false;
                }
            }
        }

        public enum PacketError
        {
            Success,
            SizeIncorrect
        }

        #endregion

        #region " Listening "

        /// <summary>
        /// Start server listening with a default backlog of 5
        /// </summary>
        /// <returns></returns>
        public bool Start()
        {
            try
            {
                _Sock.Bind(new IPEndPoint(IPAddress.Any, _port));
                _Sock.Listen(5);
                _Sock.BeginAccept(AcceptCallback, null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// Start server listening with a custom backlog
        /// </summary>
        /// <param name="Backlog">Server backlog</param>
        /// <returns></returns>
        public bool Start(int Backlog)
        {
            try
            {
                _Sock.Bind(new IPEndPoint(IPAddress.Any, _port));
                _Sock.Listen(Backlog);
                _Sock.BeginAccept(AcceptCallback, null);
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region " Functions "



        #endregion

        #region " Callback "

        private void AcceptCallback(IAsyncResult AR)
        {
            Socket cSock = _Sock.EndAccept(AR);

            if (ClientConnectCallback != null)
                ClientConnectCallback(new eSockClient(cSock));
            SocketError SE;
            eSockClient client = new eSockClient(cSock);
            client.ResizeBuffer(clientBufferSize);
            cSock.BeginReceive(client.PacketBuffer, 0, client.PacketBuffer.Length, SocketFlags.None, out SE, RetrieveCallback, client);
            if (SE != SocketError.Success)
            {
                if (ClientDisconnectCallback != null)
                    ClientDisconnectCallback(new eSockClient(cSock), SE);
            }
            _Sock.BeginAccept(AcceptCallback, null);
        }

        private void RetrieveCallback(IAsyncResult AR)
        {
            SocketError SE = SocketError.NotConnected;
            eSockClient client = (eSockClient)AR.AsyncState;
            try
            {
                client.NetworkSocket.EndReceive(AR, out SE);
                if (SE != SocketError.Success)
                    throw new Exception("Socket Error");

                if (_structured)
                {
                    try
                    {
                        ushort packSize = PacketStructure.PacketLength(client.PacketBuffer);
                        byte[] _packet = new byte[packSize];
                        Buffer.BlockCopy(client.PacketBuffer, 0, _packet, 0, _packet.Length);
                        PacketStructure ps = new PacketStructure(_packet);
                        if (DataRetrievedCallback != null)
                            DataRetrievedCallback(client, ps, PacketError.Success);
                    }
                    catch
                    {
                        if (DataRetrievedCallback != null)
                            DataRetrievedCallback(client, new PacketStructure(4, 0), PacketError.SizeIncorrect);
                    }
                }
                else
                {
                    if (DataRetrievedNotStructured != null)
                        DataRetrievedNotStructured(client, client.PacketBuffer);
                }

                client.NetworkSocket.BeginReceive(client.PacketBuffer, 0, client.PacketBuffer.Length, SocketFlags.None, out SE, RetrieveCallback, client);
                if (SE != SocketError.Success)
                    throw new Exception("Socket Error");
            }
            catch
            {
                if (ClientDisconnectCallback != null)
                    ClientDisconnectCallback(client, SE);
            }
        }

        #endregion

    }

    #region " Packet "

    /// <summary>
    /// Writes packets.
    /// </summary>
    public abstract class PacketWriter
    {
        #region " Initilisation "

        private byte[] _packetBuffer;
        /// <summary>
        /// Creates an empty packet
        /// </summary>
        /// <param name="size">size of buffer</param>
        public PacketWriter(int size)
        {
            _packetBuffer = new byte[size];
        }
        public PacketWriter(byte[] pack)
        {
            _packetBuffer = pack;
        }

        #endregion

        #region " Properties "
        /// <summary>
        /// Current buffer length
        /// </summary>
        public int Length
        {
            get { return _packetBuffer.Length; }
        }
        /// <summary>
        /// Current buffer bytes
        /// </summary>
        public byte[] PacketBuffer
        {
            get { return _packetBuffer; }
            private set { _packetBuffer = value; }
        }

        #endregion

        #region " uShort "
        /// <summary>
        /// Read a ushort (2 bytes) from the packetBuffer
        /// </summary>
        /// <param name="offset">Byte offset to read value</param>
        /// <returns></returns>
        public ushort ReadUShort(int offset)
        {
            return BitConverter.ToUInt16(_packetBuffer, offset);
        }
        /// <summary>
        /// Write a ushort (2 bytes) into the packetBuffer
        /// </summary>
        /// <param name="value">value to write into the buffer</param>
        /// <param name="offset">byte offset to write the value</param>
        public void WriteUShort(int offset, ushort value)
        {
            byte[] tBuff = BitConverter.GetBytes(value);
            Buffer.BlockCopy(tBuff, 0, _packetBuffer, offset, sizeof(ushort));
        }

        #endregion

        #region " Integer "
        /// <summary>
        /// Read an integer (4 bytes) from the packetBuffer
        /// </summary>
        /// <param name="offset">Byte offset to start reading</param>
        /// <returns></returns>
        public int ReadInteger(int offset)
        {
            return BitConverter.ToInt32(_packetBuffer, offset);
        }
        /// <summary>
        /// Write an integer (4 bytes) to the packetBuffer
        /// </summary>
        /// <param name="offset">Byte offset to start writing</param>
        /// <param name="value">Value to write to the buffer</param>
        public void WriteInteger(int offset, int value)
        {
            byte[] tBuff = BitConverter.GetBytes(value);
            Buffer.BlockCopy(tBuff, 0, _packetBuffer, offset, sizeof(int));
        }

        #endregion

        #region " String "
        /// <summary>
        /// Read a string from the packetBuffer
        /// </summary>
        /// <param name="offset">Byte offset to start reading the string</param>
        /// <param name="length">Number of bytes to read</param>
        /// <param name="enc">Encoding of the string</param>
        /// <returns></returns>
        public string ReadString(int offset, int length, Encoding enc)
        {
            return enc.GetString(_packetBuffer, offset, length);
        }
        /// <summary>
        /// Write a string to the packetBuffer
        /// </summary>
        /// <param name="offset">Byte offset to start writing the value</param>
        /// <param name="value">String to write</param>
        /// <param name="enc">Encoding to use</param>
        public void WriteString(int offset, string value, Encoding enc)
        {
            byte[] tBuff = enc.GetBytes(value);
            Buffer.BlockCopy(tBuff, 0, _packetBuffer, offset, tBuff.Length);
        }

        #endregion

        #region " Byte Array "
        /// <summary>
        /// Read a byte array from the packetPuffer
        /// </summary>
        /// <param name="offset">Byte offset to begin reading</param>
        /// <param name="length">Number of bytes to read</param>
        /// <returns></returns>
        public byte[] ReadBytes(int offset, int length)
        {
            byte[] tBuff = new byte[length];
            Buffer.BlockCopy(_packetBuffer, offset, tBuff, 0, tBuff.Length);
            return tBuff;
        }
        /// <summary>
        /// Write a byte array to the packetBuffer
        /// </summary>
        /// <param name="offset">Byte offset to start writing</param>
        /// <param name="value">Bytes to write to the buffer</param>
        public void WriteBytes(int offset, byte[] value)
        {
            Buffer.BlockCopy(value, 0, _packetBuffer, offset, value.Length);
        }
        #endregion
    }

    /// <summary>
    /// The base class for packets;
    /// The 4 first bytes are reserved;
    /// 0-2 - Length of the packet;
    /// 2-4 - Packet id
    /// </summary>
    public class PacketStructure : PacketWriter
    {
        #region " Initilisation "

        /// <summary>
        /// Initilise an empty packet
        /// </summary>
        /// <param name="length">length of the packet (Excluding headers), So the atcual size will be length + 4</param>
        /// <param name="type">the packet id</param>
        public PacketStructure(int length, ushort type)
            : base(length + 4)
        {
            WriteUShort(0, (ushort)Length);
            WriteUShort(2, type);
        }

        /// <summary>
        /// Load a packet into a packet structure
        /// </summary>
        /// <param name="packet"></param>
        public PacketStructure(byte[] packet)
            : base(packet)
        {

        }

        #endregion

        #region " Properties "

        public ushort Type
        {
            get { return ReadUShort(2); }
        }

        #endregion

        #region " Functions "

        /// <summary>
        /// Writes the string to the PacketStructure
        /// </summary>
        /// <param name="value">value to write</param>
        /// <param name="enc">Encoding to use on the string</param>
        public void WriteStringStructured(string value, Encoding enc)
        {
            WriteString(4, value, enc);
        }

        /// <summary>
        /// Reads all the data in the packet as a string
        /// </summary>
        /// <param name="enc">Encoding to read string as</param>
        /// <returns></returns>
        public string ReadStringStructured(Encoding enc)
        {
            return ReadString(4, Length - 4, enc);
        }

        /// <summary>
        /// Write bytes to a packetStructure
        /// </summary>
        /// <param name="pl"></param>
        public void WriteBytesStructured(byte[] pl)
        {
            WriteBytes(4, pl);
        }

        /// <summary>
        /// Reads all packetStructure data as bytes
        /// </summary>
        /// <returns></returns>
        public byte[] ReadBytesStructured()
        {
            return ReadBytes(4, Length - 4);
        }
        #endregion

        #region " Static Functions "
        /// <summary>
        /// Must be a packet structure
        /// Reads the packet header and returns the length
        /// </summary>
        /// <param name="Packet">PacketStructure packet to read</param>
        /// <returns></returns>
        public static ushort PacketLength(byte[] Packet)
        {
            return BitConverter.ToUInt16(Packet, 0);
        }
        /// <summary>
        /// Must be a packet structure
        /// Reads the packet header and returns the packet id
        /// </summary>
        /// <param name="Packet"></param>
        /// <returns></returns>
        public static ushort PacketType(byte[] Packet)
        {
            return BitConverter.ToUInt16(Packet, 4);
        }
        #endregion
    }

    public class EmptyPacket : PacketWriter
    {
        /// <summary>
        /// Creates an empty packet
        /// </summary>
        /// <param name="length">Length of the packet</param>
        public EmptyPacket(int length)
            : base(length)
        {

        }
    }

    #endregion
}
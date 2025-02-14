//#define FTP_DEBUG
using System;
using System.IO;
using System.Security.AccessControl;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections;
using System.Configuration;
using System.Web;
using System.Web.UI.WebControls;
//using System.Web.UI.HtmlControls;
using System.Web.Security;
using System.Web.Caching;
//using System.Data;
// using System.Drawing;
using System.Collections.Specialized;
using System.Web.Mail;
using System.Xml;

namespace FTPLib
{
	public class FTP
    {
        #region Private Variables

        private string messages; // server messages
        private string responseStr; // server response if the user wants it.
        private bool passive_mode;		// #######################################
        private long bytes_total; // upload/download info if the user wants it.
        private long file_size; // gets set when an upload or download takes place
        private Socket main_sock;
        private IPEndPoint main_ipEndPoint;
        private Socket listening_sock;
        private Socket data_sock;
        private IPEndPoint data_ipEndPoint;
        private FileStream file;
        private int response;
        private string bucket;

        #endregion        
        #region ftp methods
        /// <summary>
        /// forces disconnecting from the ftp server
        /// </summary>
        private void Fail()
        {
            Disconnect();
        }


        private void SetBinaryMode(bool mode)
        {
            if (mode)
                SendCommand("TYPE I");
            else
                SendCommand("TYPE A");

            ReadResponse();
            if (response != 200)
                Fail();
        }

        /// <summary>
        /// sends a command to the ftp server
        /// </summary>
        /// <param name="command"></param>
        private void SendCommand(string command)
        {
            Byte[] cmd = Encoding.ASCII.GetBytes((command + "\r\n").ToCharArray());

            try
            {
                main_sock.Send(cmd, cmd.Length, 0);
            }
            catch (Exception ex)
            {
                // here an error is thrown
                throw ex;
            }
        }

        /// <summary>
        /// 
        /// </summary>
        private void FillBucket()
        {
            try
            {
                Byte[] bytes = new Byte[1024]; // 1024
                long bytesgot;
                int msecs_passed = 0;		// #######################################

                // <-- infinite loop here
                while (main_sock.Available > 0)
                {
                    bytesgot = main_sock.Receive(bytes, 1024, 0);
                    bucket += Encoding.ASCII.GetString(bytes, 0, (int)bytesgot);
                }
                // infinite loop here -->
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// gets message lines from the ftp server on connect
        /// </summary>
        /// <returns></returns>
        private string GetLineFromBucketOnConnect()
        {
            int i;
            string buf = "";

            if (((i = bucket.IndexOf('\n')) < 0))
            {
                // used to test the number of loops
                int loopCounter = 0;
                while (i < 0) // a test to force exit the loop
                {
                    // <-- infinite loop might appear here
                    FillBucket();
                    // infinite loop might appear here -->
                    i = bucket.IndexOf('\n');
                }
            }

            buf = bucket.Substring(0, i);
            bucket = bucket.Substring(i + 1);

            return buf;
        }

        /// <summary>
        /// gets message lines from the ftp server
        /// </summary>
        /// <returns></returns>
        private string GetLineFromBucket()
        {
            int i;
            string buf = "";

            if (((i = bucket.IndexOf('\n')) < 0))
            {
                // used to test the number of loops
                int loopCounter = 0;
                
                // while ((i < 0) && (loopCounter < 10000))
                while ((i < 0) && (loopCounter < 10000)) // here should be a test to force exit the loop
                {
                    FillBucket();
                    i = bucket.IndexOf('\n');
                    loopCounter++;
                }
                // HttpContext.Current.Response.Write("<br />" + loopCounter);
            }
            try
            {
                buf = bucket.Substring(0, i);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                buf = "";
            }
            try
            {
                bucket = bucket.Substring(i + 1);
            }
            catch (ArgumentOutOfRangeException ex)
            {
                bucket = "";
            }
            return buf;
        }

        /// <summary>
        /// gets the response from the server. The variable responseStr holds the entire string and
        /// the variable response holds the response number.
        /// </summary>
        private void ReadResponseOnConnect()
        {
            string buf; messages = "";
            // here might be the problem

            // while (true)
            for (int tempResponseCounter = 0; ; tempResponseCounter++)
            {
                //buf = GetLineFromBucket();

                // <-- here is the problem with aspnet_wp 99%

                buf = GetLineFromBucketOnConnect();

                // here is the problem with aspnet_wp 99% ->>

                // the server will respond with "000-Foo bar" on multi line responses
                // "000 Foo bar" would be the last line it sent for that response.
                // Better example:
                // "000-This is a multiline response"
                // "000-Foo bar"
                // "000 This is the end of the response"
                if (Regex.Match(buf, "^[0-9]+ ").Success)
                {
                    responseStr = buf;
                    response = int.Parse(buf.Substring(0, 3));
                    break;
                }
                else
                    messages += Regex.Replace(buf, "^[0-9]+-", "") + "\n";
            }
        }

        /// <summary>
        /// gets the response from the server. The variable responseStr holds the entire string and
        /// the variable response holds the response number.
        /// </summary>
        private void ReadResponse()
        {
            string buf; messages = "";
            // here might be the problem
            
            // while (true)
            for (int tempResponseCounter = 0;;tempResponseCounter++)
            {
                //buf = GetLineFromBucket();
                
                // <-- here is the problem with aspnet_wp 99%
                
                buf = GetLineFromBucket();
                
                // here is the problem with aspnet_wp 99% ->>

                // the server will respond with "000-Foo bar" on multi line responses
                // "000 Foo bar" would be the last line it sent for that response.
                // Better example:
                // "000-This is a multiline response"
                // "000-Foo bar"
                // "000 This is the end of the response"
                if (Regex.Match(buf, "^[0-9]+ ").Success)
                {
                    responseStr = buf;
                    response = int.Parse(buf.Substring(0, 3));
                    break;
                }
                else
                    messages += Regex.Replace(buf, "^[0-9]+-", "") + "\n";
            }
        }


        /// <summary>
        /// opens a socket for communicating with the ftp server
        /// </summary>
        private void OpenDataSocket()
        {
            if (passive_mode)		// #######################################
            {
                string[] pasv;
                string server;
                int port;

                if (!IsConnected)
                    Connect();
                SendCommand("PASV");
                ReadResponse();
                if (response != 227)
                    Fail();

                // if you add code that needs a data socket, i.e. a PASV or PORT command required,
                // call this function to do the dirty work. It sends the PASV or PORT command,
                // parses out the port and ip info and opens the appropriate data socket
                // for you. The socket variable is private Socket data_socket. Once you
                // are done with it, be sure to call CloseDataSocket()
                try
                {
                    int i1, i2;

                    i1 = responseStr.IndexOf('(') + 1;
                    i2 = responseStr.IndexOf(')') - i1;
                    pasv = responseStr.Substring(i1, i2).Split(',');
                }
                catch (Exception)
                {
                    Disconnect();
                    throw new Exception("Malformed PASV response: " + responseStr);
                }

                if (pasv.Length < 6)
                {
                    Disconnect();
                    throw new Exception("Malformed PASV response: " + responseStr);
                }

                server = String.Format("{0}.{1}.{2}.{3}", pasv[0], pasv[1], pasv[2], pasv[3]);
                port = (int.Parse(pasv[4]) << 8) + int.Parse(pasv[5]);

                try
                {
                    CloseDataSocket();
                    data_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                    data_ipEndPoint = new IPEndPoint(Dns.GetHostByName(server).AddressList[0], port);

                    data_sock.Connect(data_ipEndPoint);
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to connect for data transfer: " + ex.Message);
                }
            }
            else		// #######################################
            {
                if (!IsConnected)
                    Connect();

                try
                {
                    CloseDataSocket();
                    listening_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                    // for the PORT command we need to send our IP address; let's extract it
                    // from the LocalEndPoint of the main socket, that's already connected
                    string sLocAddr = main_sock.LocalEndPoint.ToString();
                    int ix = sLocAddr.IndexOf(':');
                    if (ix < 0)
                    {
                        throw new Exception("Failed to parse the local address: " + sLocAddr);
                    }
                    string sIPAddr = sLocAddr.Substring(0, ix);
                    // let the system automatically assign a port number (setting port = 0)
                    System.Net.IPEndPoint localEP = new IPEndPoint(IPAddress.Parse(sIPAddr), 0);

                    listening_sock.Bind(localEP);
                    sLocAddr = listening_sock.LocalEndPoint.ToString();
                    ix = sLocAddr.IndexOf(':');
                    if (ix < 0)
                    {
                        throw new Exception("Failed to parse the local address: " + sLocAddr);
                    }
                    int nPort = int.Parse(sLocAddr.Substring(ix + 1));

                    // start to listen for a connection request from the host (note that
                    // Listen is not blocking) and send the PORT command
                    listening_sock.Listen(1);
                    string sPortCmd = string.Format("PORT {0},{1},{2}",
                                                    sIPAddr.Replace('.', ','),
                                                    nPort / 256, nPort % 256);
                    SendCommand(sPortCmd);
                    ReadResponse();
                    if (response != 200)
                        Fail();
                }
                catch (Exception ex)
                {
                    throw new Exception("Failed to connect for data transfer: " + ex.Message);
                }
            }
        }


        private void ConnectDataSocket()		// #######################################
        {
            if (data_sock != null)		// already connected (always so if passive mode)
                return;

            try
            {
                data_sock = listening_sock.Accept();	// Accept is blocking
                listening_sock.Close();
                listening_sock = null;

                if (data_sock == null)
                {
                    throw new Exception("Winsock error: " +
                        Convert.ToString(System.Runtime.InteropServices.Marshal.GetLastWin32Error()));
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Failed to connect for data transfer: " + ex.Message);
            }
        }


        /// <summary>
        /// closes the socket used to communicate with the ftp server
        /// </summary>
        private void CloseDataSocket()
        {
            if (data_sock != null)
            {
                if (data_sock.Connected)
                {
                    data_sock.Close();
                }
                data_sock = null;
            }

            data_ipEndPoint = null;
        }
        /// <summary>
        /// Closes all connections to the ftp server
        /// </summary>
        public void Disconnect()
        {
            CloseDataSocket();

            if (main_sock != null)
            {
                if (main_sock.Connected)
                {
                    SendCommand("QUIT");
                    main_sock.Close();
                }
                main_sock = null;
            }

            if (file != null)
                file.Close();

            main_ipEndPoint = null;
            file = null;
        }
        /// <summary>
        /// Connects to a ftp server
        /// </summary>
        /// <param name="server">IP or hostname of the server to connect to</param>
        /// <param name="port">Port number the server is listening on</param>
        /// <param name="user">Account name to login as</param>
        /// <param name="pass">Password for the account specified</param>
        public void Connect(string server, int port, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.port = port;

            Connect();
        }
        /// <summary>
        /// Connect to a ftp server
        /// </summary>
        /// <param name="server">IP or hostname of the server to connect to</param>
        /// <param name="user">Account name to login as</param>
        /// <param name="pass">Password for the account specified</param>
        public void Connect(string server, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;

            Connect();
        }
        /// <summary>
        /// Connect to an ftp server
        /// </summary>
        public void Connect()
        {
            if (server == null)
                throw new Exception("No server has been set.");
            if (user == null)
                throw new Exception("No username has been set.");

            /*
            if (main_sock != null)
                if (main_sock.Connected)
                    return;
            */
            main_sock = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            main_ipEndPoint = new IPEndPoint(Dns.GetHostByName(server).AddressList[0], port);

            try
            {
                main_sock.Connect(main_ipEndPoint);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

            ReadResponseOnConnect();
            if (response != 220)
                Fail();

            SendCommand("USER " + user);
            ReadResponseOnConnect();

            switch (response)
            {
                case 331:
                    if (pass == null)
                    {
                        Disconnect();
                        throw new Exception("No password has been set.");
                    }
                    SendCommand("PASS " + pass);
                    ReadResponseOnConnect();

                    // if the credentials are wrong
                    try
                    {
                        if (response != 230) throw new Exception();
                    }
                    catch (Exception ex)
                    {
                        throw ex;
                    }

                    // if (response != 230) Fail();
                    break;
                case 230:
                    break;
            }

            return;
        }
        #endregion ftp methods
        #region Constructors
        /// <summary>
        /// initializes a connection with the default parameters
        /// </summary>
        public FTP()
        {
            server = null;
            user = null;
            pass = null;
            port = 21;
            passive_mode = true;		// #######################################
            main_sock = null;
            main_ipEndPoint = null;
            listening_sock = null;
            data_sock = null;
            data_ipEndPoint = null;
            file = null;
            bucket = "";
            bytes_total = 0;
            timeout = 1000000;	// 1000 seconds
            messages = "";
        }
        /// <summary>
        /// initializes a connection with specific parameters. The default port 21 is used
        /// </summary>
        /// <param name="server">Server to connect to</param>
        /// <param name="user">Account to login as</param>
        /// <param name="pass">Account password</param>
        public FTP(string server, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            port = 21;
            passive_mode = true;		// #######################################
            main_sock = null;
            main_ipEndPoint = null;
            listening_sock = null;
            data_sock = null;
            data_ipEndPoint = null;
            file = null;
            bucket = "";
            bytes_total = 0;
            timeout = 1000000;	// 1000 seconds
            messages = "";
        }
        /// <summary>
        /// initializes a connection with specific parameters. Used to connect through a different port
        /// </summary>
        /// <param name="server">Server to connect to</param>
        /// <param name="port">Port server is listening on</param>
        /// <param name="user">Account to login as</param>
        /// <param name="pass">Account password</param>
        public FTP(string server, int port, string user, string pass)
        {
            this.server = server;
            this.user = user;
            this.pass = pass;
            this.port = port;
            passive_mode = true;		// #######################################
            main_sock = null;
            main_ipEndPoint = null;
            listening_sock = null;
            data_sock = null;
            data_ipEndPoint = null;
            file = null;
            bucket = "";
            bytes_total = 0;
            timeout = 1000000;	// 1000 seconds
            messages = "";
        }

        #endregion
        
        #region handle files

        /// <summary>
        /// gets the content of the specified directory
        /// </summary>
        /// <param name="directoryName"></param>
        /// <returns></returns>
        public string[] GetFilesAndDirectoriesList(string directoryName)
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                if (directoryName.StartsWith(GetPathSeparator().ToString()))
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + server + directoryName + GetPathSeparator() + "*.*"));
                else
                    reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + server + GetPathSeparator().ToString() + directoryName + GetPathSeparator()) + "*.*");
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(user, pass);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;

                WebResponse response = reqFTP.GetResponse();
                // an error is thrown here
                StreamReader reader = new StreamReader(response.GetResponseStream());

                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }
                result.Remove(result.ToString().LastIndexOf('\n'), 1);
                reader.Close();
                response.Close();
                //MessageBox.Show(response.StatusDescription);
            }
            catch (Exception ex)
            {
                return null;
            }
            return result.ToString().Split('\n');
        }

        /// <summary>
        /// Returns the 'Raw' DateInformation in ftp format. (YYYYMMDDhhmmss). Use GetFileDate to return a DateTime object as a better option.
        /// </summary>
        /// <param name="fileName">Remote FileName to Query</param>
        /// <returns>Returns the 'Raw' DateInformation in ftp format</returns>
        public string GetFileDateRaw(string fileName)
        {
            if (!IsConnected)
                Connect();
            SendCommand("MDTM " + fileName);
            ReadResponse();
            if (response != 213)
            {
                throw new Exception(responseStr);
            }

            return (this.responseStr.Substring(4));
        }

        /// <summary>
        /// GetFileDate will query the ftp server for the date of the remote file.
        /// </summary>
        /// <param name="fileName">Remote FileName to Query</param>
        /// <returns>DateTime of the Input FileName</returns>
        public DateTime GetFileDate(string fileName)
        {
            return ConvertFTPDateToDateTime(GetFileDateRaw(fileName));
        }

        private DateTime ConvertFTPDateToDateTime(string input)
        {
            if (input.Length < 14)
                throw new ArgumentException("Input Value for ConvertFTPDateToDateTime method was too short.");

            //YYYYMMDDhhmmss": 
            int year = Convert.ToInt16(input.Substring(0, 4));
            int month = Convert.ToInt16(input.Substring(4, 2));
            int day = Convert.ToInt16(input.Substring(6, 2));
            int hour = Convert.ToInt16(input.Substring(8, 2));
            int min = Convert.ToInt16(input.Substring(10, 2));
            int sec = Convert.ToInt16(input.Substring(12, 2));

            return new DateTime(year, month, day, hour, min, sec);
        }

        /// <summary>
        /// returns the path separator for the ftp server
        /// </summary>
        /// <returns></returns>
        public char GetPathSeparator()
        {
            string strWorkingDirectory = GetWorkingDirectory();
            char[] pathSeparators = { '/', '\\' };
            // iterate through the list of possible path separators
            for (int tempIntSeparatorCounters = 0; tempIntSeparatorCounters < pathSeparators.Length; tempIntSeparatorCounters++)
                // if any of the path separators is found in the working directory, return it
                if (strWorkingDirectory.IndexOf(pathSeparators[tempIntSeparatorCounters]) > -1)
                    return pathSeparators[tempIntSeparatorCounters];
            
            return pathSeparators[0];
        }

        /// <summary>
        /// Gets the working directory on the ftp server
        /// </summary>
        /// <returns>The working directory</returns>
        public string GetWorkingDirectory()
        {
            //PWD - print working directory
            if (!IsConnected)
                Connect();
            SendCommand("PWD");
            ReadResponse();

            // here is an error 421 No-transfer-time exceeded
            if ((response != 257) && (response != 421))
                throw new Exception(responseStr);

            string pwd;
            try
            {
                pwd = responseStr.Substring(responseStr.IndexOf("\"", 0) + 1);//5);
                pwd = pwd.Substring(0, pwd.LastIndexOf("\""));
                pwd = pwd.Replace("\"\"", "\""); // directories with quotes in the name come out as "" from the server
            }
            catch (Exception ex)
            {
                throw new Exception("Uhandled PWD response: " + ex.Message);
            }

            return pwd;
        }

        /// <summary>
        /// Changes to another directory on the ftp server
        /// </summary>
        /// <param name="path">Directory to change to</param>
        public void ChangeDir(string path)
        {
                if (!IsConnected)
                    Connect();
                SendCommand("CWD " + path);
            
                // <-- here is the problem with aspnet_wp 99%
                ReadResponse();
                // here is the problem with aspnet_wp 99% -->

                // 250, 257, 550
                
                // possible messages:
                // 250 CWD Command successful
                // 550 No such file of directory
                // 550 permission denied
                if (response != 250)
                {
                    throw new Exception(responseStr);
                }
        }

        /// <summary>
        /// Changes to another directory on the ftp server
        /// </summary>
        /// <param name="path">Directory to change to</param>
        public void ChangeDirLongPathName(string path)
        {
            ChangeDir(GetPathSeparator().ToString());
            // changes the working directory if the path has the template dir1/dir2/dir3/dir4/dir5
            string[] strDirectoryList = path.Split(GetPathSeparator());
            for (int tempCounter = 0; tempCounter < strDirectoryList.Length; tempCounter++)
                // iterate through the list of directories
                // eg: we get five directories: dir1, dir2, dir3, dir4 and dir 5
                if (strDirectoryList[tempCounter] != String.Empty)
                {
                    try
                    {
                        ChangeDir(strDirectoryList[tempCounter]);
                    }

                    catch (Exception ex)
                    {
                        throw ex;
                    }
                }
        }

        /// <summary>
        /// Create a directory on the ftp server
        /// </summary>
        /// <param name="dir">Directory to create</param>
        public void MakeDir(string dir)
        {
            try
            {
                if (!IsConnected)
                    Connect();
                SendCommand("MKD " + dir);

                ReadResponse();

                switch (response)
                {
                    // possible messages:
                    // 257 MKD commnad successful
                    // 550 No such file of directory
                    // 550 permission denied
                    case 257:
                    case 250:
                        break;
                    default:
                        throw new Exception(responseStr);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Creates a directory on the ftp server
        /// </summary>
        /// <param name="dir">Directory to create</param>
        public void MakeDirLongPathName(string dir)
        {
            // makes a dir if the path has the template dir1/dir2/dir3/dir4/dir5
            string[] strDirectoryList = dir.Split(GetPathSeparator());
            for (int tempCounter = 0; tempCounter < strDirectoryList.Length; tempCounter++)
                // iterate through the list of directories
                // eg: we get five directories: dir1, dir2, dir3, dir4 and dir 5
                if (strDirectoryList[tempCounter] != String.Empty)
                {
                    try
                    {
                        MakeDir(strDirectoryList[tempCounter]);
                        ChangeDir(strDirectoryList[tempCounter]);
                    }
                    
                    catch (Exception ex)
                    {
                        ChangeDir(strDirectoryList[tempCounter]);
                    }
                }
        }

        /// <summary>
        /// Removes a directory from the ftp server
        /// </summary>
        /// <param name="dir">Name of directory to remove</param>
        public void RemoveDir(string dir)
        {
            if (!IsConnected)
                Connect();
            SendCommand("RMD " + dir);
            ReadResponse();
            if (response != 250)
            {
                // possible messages:
                // 257 MKD commnad successful
                // 550 No such file of directory
                // 550 permission denied
                throw new Exception(responseStr);
            }
        }
        /// <summary>
        /// Removes a file from the ftp server
        /// </summary>
        /// <param name="filename">Name of the file to delete</param>
        public void RemoveFile(string filename)
        {
            if (!IsConnected)
                Connect();
            SendCommand("DELE " + filename);
            ReadResponse();
            if (response != 250)
            {
                throw new Exception(responseStr);
            }
        }
        /// <summary>
        /// Renames a file on the ftp server
        /// </summary>
        /// <param name="oldfilename">Old file name</param>
        /// <param name="newfilename">New file name</param>
        public void RenameFile(string oldfilename, string newfilename)		// #######################################
        {
            if (!IsConnected)
                Connect();
            SendCommand("RNFR " + oldfilename);
            ReadResponse();
            if (response != 350)
            {
                throw new Exception(responseStr);
            }
            else
            {
                SendCommand("RNTO " + newfilename);
                ReadResponse();
                if (response != 250)
                {
                    throw new Exception(responseStr);
                }
            }
        }
        /// <summary>
        /// Gets the size of a file (Provided the ftp server supports it)
        /// </summary>
        /// <param name="filename">Name of file</param>
        /// <returns>The size of the file specified by filename</returns>
        public long GetFileSize(string filename)
        {
            if (!IsConnected)
                Connect();
            SendCommand("SIZE " + filename);
            ReadResponse();
            if (response != 213)
            {
                throw new Exception(responseStr);
            }

            return Int64.Parse(responseStr.Substring(4));
        }
        /// <summary>
        /// Open an upload with no resume if it already exists
        /// </summary>
        /// <param name="filename">File to upload</param>
        public void OpenUpload(string filename)
        {
            OpenUpload(filename, filename, false);
        }
        /// <summary>
        /// Opens an upload with no resume if it already exists
        /// </summary>
        /// <param name="filename">Local file to upload (Can include path to file)</param>
        /// <param name="remotefilename">Filename to store file as on ftp server</param>
        public void OpenUpload(string filename, string remotefilename)
        {
            OpenUpload(filename, remotefilename, false);
        }
        /// <summary>
        /// Opens an upload with resume support
        /// </summary>
        /// <param name="filename">Local file to upload (Can include path to file)</param>
        /// <param name="resume">Attempt resume if exists</param>
        public void OpenUpload(string filename, bool resume)
        {
            OpenUpload(filename, filename, resume);
        }
        /// <summary>
        /// Opens an upload with resume support
        /// </summary>
        /// <param name="filename">Local file to upload (Can include path to file)</param>
        /// <param name="remote_filename">Filename to store file as on ftp server</param>
        /// <param name="resume">Attempt resume if exists</param>
        public void OpenUpload(string filename, string remote_filename, bool resume)
        {
            try
            {
                if (!IsConnected)
                    Connect();
                SetBinaryMode(true);
                OpenDataSocket();

                bytes_total = 0;

                try
                {
                    file = new FileStream(filename, FileMode.Open);
                }
                catch (Exception ex)
                {
                    file = null;
                    throw new Exception(ex.Message);
                }

                file_size = file.Length;

                if (resume)
                {
                    long size = GetFileSize(remote_filename);
                    SendCommand("REST " + size);
                    ReadResponse();
                    if (response == 350)
                        file.Seek(size, SeekOrigin.Begin);
                }

                SendCommand("STOR " + remote_filename);
                ReadResponse();

                switch (response)
                {
                    case 125:
                    case 150:
                        break;
                    default:
                        file.Close();
                        file = null;
                        throw new Exception(responseStr);
                }
                ConnectDataSocket();		// #######################################	
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return;
        }
        /// <summary>
        /// Downloads a file with no resume
        /// </summary>
        /// <param name="filename">Remote file name</param>
        public void OpenDownload(string filename)
        {
            OpenDownload(filename, filename, false);
        }
        /// <summary>
        /// Download a file with optional resume
        /// </summary>
        /// <param name="filename">Remote file name</param>
        /// <param name="resume">Attempt resume if file exists</param>
        public void OpenDownload(string filename, bool resume)
        {
            OpenDownload(filename, filename, resume);
        }
        /// <summary>
        /// Download a file with no attempt to resume
        /// </summary>
        /// <param name="filename">Remote filename</param>
        /// <param name="localfilename">Local filename (Can include path to file)</param>
        public void OpenDownload(string filename, string localfilename)
        {
            OpenDownload(filename, localfilename, false);
        }
        /// <summary>
        /// Opens a file for download
        /// </summary>
        /// <param name="remote_filename">The name of the file on the FTP server</param>
        /// <param name="local_filename">The name of the file to save as (Can include path to file)</param>
        /// <param name="resume">Attempt resume if file exists</param>
        public void OpenDownload(string remote_filename, string local_filename, bool resume)
        {
            if (!IsConnected)
                Connect();
            SetBinaryMode(true);

            bytes_total = 0;

            try
            {
                file_size = GetFileSize(remote_filename);
            }
            catch
            {
                file_size = 0;
            }

            if (resume && File.Exists(local_filename))
            {
                try
                {
                    file = new FileStream(local_filename, FileMode.Open);
                }
                catch (Exception ex)
                {
                    file = null;
                    throw new Exception(ex.Message);
                }
                SendCommand("REST " + file.Length);
                ReadResponse();
                if (response != 350)
                    throw new Exception(responseStr);
                file.Seek(file.Length, SeekOrigin.Begin);
                bytes_total = file.Length;
            }
            else
            {
                try
                {
                    // the downloaded files are written in to the temporary download folder of the application
                    // which is \temp_downloads
                    file = new FileStream(HttpContext.Current.Server.MapPath(GetXmlValue("TemporaryDownloadsFolder")) + "\\" + local_filename, FileMode.Create); // http://www.google.com
                }
                catch (Exception ex)
                {
                    file = null;
                    throw new Exception(ex.Message);
                }
            }

            OpenDataSocket();
            SendCommand("RETR " + remote_filename);
            ReadResponse();

            switch (response)
            {
                case 125:
                case 150:
                    break;
                default:
                    file.Close();
                    file = null;
                    throw new Exception(responseStr);
            }
            ConnectDataSocket();		// #######################################
            return;
        }
        /// <summary>
        /// Uploads the file, to be used in a loop until file is completely uploaded
        /// </summary>
        /// <returns>Bytes sent</returns>
        public long DoUpload()
        {
            Byte[] bytes = new Byte[1024]; // 1024
            long bytes_got;

            try
            {
                bytes_got = file.Read(bytes, 0, bytes.Length);
                bytes_total += bytes_got;
                data_sock.Send(bytes, (int)bytes_got, 0);

                if (bytes_got <= 0)
                {
                    // the upload is complete or an error occured
                    file.Close();
                    file = null;

                    CloseDataSocket();
                    ReadResponse();
                    switch (response)
                    {
                        case 226:
                        case 250:
                            break;
                        default:
                            throw new Exception(responseStr);
                    }

                    SetBinaryMode(false);
                }
            }
            catch (Exception ex)
            {
                file.Close();
                file = null;
                CloseDataSocket();
                ReadResponse();
                SetBinaryMode(false);
                throw ex;
            }

            return bytes_got;
        }
        /// <summary>
        /// Downloads a file, to be used in a loop until the file is completely downloaded
        /// </summary>
        /// <returns>Number of bytes recieved</returns>
        public long DoDownload()
        {
            Byte[] bytes = new Byte[1024]; //1024
            long bytes_got;

            try
            {
                bytes_got = data_sock.Receive(bytes, bytes.Length, 0);

                if (bytes_got <= 0)
                {
                    // the download is done or an error occured
                    CloseDataSocket();
                    file.Close();
                    file = null;

                    ReadResponse();
                    switch (response)
                    {
                        case 226:
                        case 250:
                            break;
                        default:
                            throw new Exception(responseStr);
                    }

                    SetBinaryMode(false);

                    return bytes_got;
                }

                file.Write(bytes, 0, (int)bytes_got);
                bytes_total += bytes_got;
            }
            catch (Exception ex)
            {
                CloseDataSocket();
                file.Close();
                file = null;
                ReadResponse();
                SetBinaryMode(false);
                throw ex;
            }

            return bytes_got;
        }
        #endregion handle files

        #region xml
        
        private static XmlDocument _xmlDoc;
        /// <summary>
        /// iterates through nodes collection and extract an array of node/value
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public XmlNode GetXmlNode(string path, string strXmlFile)
        {
            string pathXmlFile = HttpContext.Current.Server.MapPath("~/xml/" + strXmlFile); // Gets Physical path of the "Config.xml" on server
            Cache cache = HttpContext.Current.Cache;

            try
            {
                _xmlDoc = (XmlDocument)cache[pathXmlFile];
                if (_xmlDoc == null)
                {
                    _xmlDoc = new XmlDocument();
                    _xmlDoc.Load(pathXmlFile);  // loads "ConfigSite.xml file 
                    cache.Add(pathXmlFile, _xmlDoc, new CacheDependency(pathXmlFile), DateTime.Now.AddHours(6), TimeSpan.Zero, CacheItemPriority.High, null);
                }
                XmlNode root = _xmlDoc.DocumentElement;
                return root.SelectSingleNode(path);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        //

        /// <summary>
        /// gets an xml value for a corresponding node
        /// </summary>
        /// <param name="path"></param>
        /// <returns></returns>
        public string GetXmlValue(string path)
        {
            try {
                return GetXmlNode(path, "Config.xml").Attributes["value"].Value;
            }
            catch {
                return "";
            }
        }   

        /// <summary>
        /// gets a collection of nodes with (name, value) from the specified xml file
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public ArrayList GetNodes(string node)
        {
            ArrayList h = new ArrayList();
            // iterates through the collection
            foreach (XmlNode n in GetXmlNode(node, "alertconfig.xml"))
            {
                // for each node in the collection add the corresponding value in the array
                h.Add(n.Attributes["value"].Value);
                // h.Add(n.Attributes["name"].Value);
            }
            return h;
        }

        /// <summary>
        /// gets all available color types
        /// </summary>
        /// <returns></returns>
        public ArrayList GetAlertColors()
        {
            ArrayList h = new ArrayList();
            // iterates through the collection
            foreach (XmlNode n in GetXmlNode("Alerts", "alertconfig.xml"))
            {
                // for each node in the collection add the corresponding value in the array
                h.Add(n.Attributes["color"].Value);
                // h.Add(n.Attributes["name"].Value);
            }
            return h;
        }

        /// <summary>
        /// gets the alert's color from the corresponding row <Alerts><Alert value="alertName" color="alertColor" /></Alerts>
        /// this color is used to represent different types of alerts
        /// </summary>
        /// <param name="alertType"></param>
        /// <returns></returns>
        public string GetAlertColorByType(string alertType)
        {
            ArrayList arrAlertNames = new ArrayList(); ArrayList arrAlertColors = new ArrayList();
            // iterates through the collection
            foreach (XmlNode n in GetXmlNode("Alerts", "alertconfig.xml"))
            {
                // for each node in the collection add the corresponding value in the array
                arrAlertNames.Add(n.Attributes["value"].Value);
                // h.Add(n.Attributes["name"].Value);
            }

            foreach (XmlNode n in GetXmlNode("Alerts", "alertconfig.xml"))
            {
                // for each node in the collection add the corresponding value in the array
                arrAlertColors.Add(n.Attributes["color"].Value);
                // h.Add(n.Attributes["name"].Value);
            }
            for (int tempCounter = 0; tempCounter < arrAlertNames.Count; tempCounter++)
                if (arrAlertNames[tempCounter].ToString() == alertType)
                    return arrAlertColors[tempCounter].ToString();
            return String.Empty;
        }

        /// <summary>
        /// gets te distribution list for user which have to be notfied of an alert being uploaded
        /// </summary>
        /// <returns></returns>
        public ArrayList GetDistributionListForAlerts()
        {
            ArrayList arrAlertNames = new ArrayList(); ArrayList arrAlertColors = new ArrayList();
            // iterates through the collection
            foreach (XmlNode n in GetXmlNode("Notifications", "Config.xml"))
            {
                // for each node in the collection add the corresponding value in the array
                arrAlertNames.Add(n.Attributes["value"].Value);
                // h.Add(n.Attributes["name"].Value);
            }
            return arrAlertNames;
        }
        #endregion xml
        #region error handling
        /// <summary>
        /// sends an e-mail regarding an error
        /// </summary>
        /// <param name="page"></param>
        /// <param name="exc"></param>
        /// <param name="v"></param>
        public void SendMail(string page, string exc, NameValueCollection v)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = "Web Application FTP Transfer";
                mailMessage.To = "a.mihailescu@ag.com;";
                mailMessage.Subject = "Web Application FTP Transfer crashed";
                mailMessage.BodyFormat = MailFormat.Text;
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.Body = String.Format("Unexpected exception in WebApplicationFTP\\{0}.aspx - Error : {1}\n", page, exc);
                for (int i = 0; i < v.Count; i++)
                    mailMessage.Body += String.Format("{0} - {1}", v[i], v.GetValues(v[i]));
                SmtpMail.SmtpServer = GetXmlValue("SmtpServer"); // for resting purposes use smtp.ines.ro
                SmtpMail.Send(mailMessage);
            }
            catch (Exception ex)
            {

            }
        }

        /// <summary>
        /// Notifies all users from the distribution list of an alert being uploaded
        /// </summary>
        /// <param name="strAlertType">type of alert</param>
        /// <param name="strAlertDate">date of alert</param>
        /// <param name="strUser">user to be notified</param>
        /// <param name="strMessage">message to be shown to the users</param>
        public void NotifyUserAtAlert(string strUser, string strMessage)
        {
            try
            {
                MailMessage mailMessage = new MailMessage();
                mailMessage.From = GetXmlValue("SendMailFromUser");
                mailMessage.To = strUser;
                mailMessage.Subject = GetXmlValue("MailNotifyNewFileUploaded");
                mailMessage.BodyEncoding = System.Text.Encoding.UTF8;
                mailMessage.Body = strMessage;
                mailMessage.BodyFormat = MailFormat.Html;

                // SmtpMail.SmtpServer = "smtp.ines.ro"; // for resting purposes use smtp.ines.ro // default 192.168.0.71
                SmtpMail.SmtpServer = GetXmlValue("SmtpServer");
                SmtpMail.Send(mailMessage);
            }
            catch (Exception ex)
            {
            }
        }
        #endregion error handling

        #region messages
        /// <summary>
        /// returns a general message with an icon
        /// </summary>
        /// <param name="icon">icon's name to be displayed with the message</param>
        /// <param name="message">message to be displayed</param>
        /// <param name="color">the color message to be displayed</param>
        /// <returns></returns>
        public string ShowGeneralMessage(string icon, string message, string color)
        {
            string strTempMessageToDisplay = String.Empty;
            strTempMessageToDisplay += "<table border=\"0\"><tr>";
            strTempMessageToDisplay += "<td valign=\"middle\">";
            strTempMessageToDisplay += "<img src=\"images/" + icon + "\" border=\"0\" />";
            strTempMessageToDisplay += "</td>";
            strTempMessageToDisplay += "<td valign=\"middle\"><font color=\"" + color + "\">" + message + "</font></td></tr></table>";

            return strTempMessageToDisplay;
        }

        /// <summary>
        /// returns text to display a link with an icon and a message. The link will be displayed in a table
        /// </summary>
        /// <param name="icon">icon to be displayed with the link</param>
        /// <param name="message">link's message</param>
        /// <param name="navigationLink">link's url</param>
        /// <returns></returns>
        public string ShowLinkWithIcon(string icon, string message, string navigationLink, string target)
        {
            string strTempMessageToDisplay = String.Empty;
            strTempMessageToDisplay += "<table border=\"0\"><tr>";
            strTempMessageToDisplay += "<td valign=\"middle\">";
            strTempMessageToDisplay += "<a target=\"" + target + "\" href=\"" + navigationLink + "\"><img src=\"images/" + icon + "\" border=\"0\" /></a>";
            strTempMessageToDisplay += "</td><td valign=\"middle\">";
            strTempMessageToDisplay += "<a target=\"" + target + "\" href=\"" + navigationLink + "\">" + message + "</a>" + "</td></tr></table>";

            return strTempMessageToDisplay;
        }

        /// <summary>
        /// shows an error message
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="message"></param>
        public void ShowErrorMessage(Label lbl, string message)
        {
            lbl.Text += ShowGeneralMessage("error.jpg", message, "red");
        }

        /// <summary>
        /// shows an information message
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="message"></param>
        public void ShowInformationMessage(Label lbl, string message)
        {
            lbl.Text += ShowGeneralMessage("information.jpg", message, "black");
        }

        /// <summary>
        /// shows a warning message
        /// </summary>
        /// <param name="lbl"></param>
        /// <param name="message"></param>
        public void ShowWarningMessage(Label lbl, string message)
        {
            lbl.Text += ShowGeneralMessage("warning.jpg", message, "black");
        }

        #endregion messages

        #region Public Variables

        /// <summary>
		/// IP address or hostname to connect to
		/// </summary>
		public string server;
		/// <summary>
		/// Username to login as
		/// </summary>
		public string user;
		/// <summary>
		/// Password for account
		/// </summary>
		public string pass;
		/// <summary>
		/// Port number the FTP server is listening on
		/// </summary>
		public int port;

        /// <summary>
		/// The timeout (miliseconds) for waiting on data to arrive
		/// </summary>
		public int timeout;
		#endregion		

        #region ftp messages

        /// <summary>
		/// Connection status to the server
		/// </summary>
		public bool IsConnected
		{
			get
			{
				if (main_sock != null)
					return main_sock.Connected;
				return false;
			}
		}
		/// <summary>
		/// Returns true if the message buffer has data in it
		/// </summary>
		public bool MessagesAvailable
		{
			get
			{
				if(messages.Length > 0)
					return true;
				return false;
			}
		}
		/// <summary>
		/// Server messages if any, buffer is cleared after you access this property
		/// </summary>
		public string Messages
		{
			get
			{
				string tmp = messages;
				messages = "";
				return tmp;
			}
		}
		/// <summary>
		/// The response string from the last issued command
		/// </summary>
		public string ResponseString
		{
			get
			{
				return responseStr;
			}
		}
		/// <summary>
		/// The total number of bytes sent/recieved in a transfer
		/// </summary>
		public long BytesTotal		// #######################################
		{
			get
			{
				return bytes_total;
			}
		}
		/// <summary>
		/// The size of the file being downloaded/uploaded (Can possibly be 0 if no size is available)
		/// </summary>
		public long FileSize		// #######################################
		{
			get
			{
				return file_size;
			}
		}
		/// <summary>
		/// True:  Passive mode [default]
		/// False: Active Mode
		/// </summary>
		public bool PassiveMode		// #######################################
		{
			get
			{
				return passive_mode;
			}
			set
			{
				passive_mode = value;
			}
        }
        #endregion ftp messages       
        
	}
}
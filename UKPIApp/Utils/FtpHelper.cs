using System;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Net;


namespace UKPI.Utils
{
    public class FtpHelper
    {
        private string m_FtpServerIP;
        private string m_FtpUserID;
        private string m_FtpPassword;

        public string FtpServerIP
        {
            get { return m_FtpServerIP; }
            set { m_FtpServerIP = value; }
        }

        public string FtpUserID
        {
            get { return m_FtpUserID; }
            set { m_FtpUserID = value; }
        }

        public string FtpPassword
        {
            get { return m_FtpPassword; }
            set { m_FtpPassword = value; }
        }


        /// <summary>
        /// Upload file to FTP server
        /// </summary>
        /// <param name="fileName">Full path of source file</param>
        /// <param name="ftpServerIP">Server IP</param>
        /// <param name="ftpUserID">User name to login FTP Server</param>
        /// <param name="ftpPassword">Password to login FTP Server</param>
        /// <remarks>
        /// Creator: PhongNTT
        /// Created date: 2010-03-16
        /// TESTED
        /// </remarks>
        public static void Upload(string fileName, string ftpServerIP, string ftpUserID, string ftpPassword)
        {
            FileInfo fileInfo = new FileInfo(fileName);

            Upload(fileName, fileInfo.Name, ftpServerIP, ftpUserID, ftpPassword);
        }

        /// <summary>
        /// Upload file to FTP server
        /// </summary>
        /// <param name="sourceFilename">Full path of source file</param>
        /// <param name="desFilename">Path on FTP server. Not need full. exp: /ABC/file001.txt</param>
        /// <param name="ftpServerIP">Server IP</param>
        /// <param name="ftpUserID">User name to login FTP Server</param>
        /// <param name="ftpPassword">Password to login FTP Server</param>
        /// <remarks>
        /// Creator: PhongNTT
        /// Created date: 2010-03-16
        /// TESTED
        /// </remarks>
        public static void Upload(string sourceFilename, string desFilename, string ftpServerIP, string ftpUserID, string ftpPassword)
        {
            FileInfo fileInf = new FileInfo(sourceFilename);
            //string uri = "ftp://" + ftpServerIP + "/" + fileInf.Name;
            string uri = "ftp://" + ftpServerIP + "/" + desFilename;
            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create
                     (new Uri(uri));

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential(ftpUserID, ftpPassword);

            // By default KeepAlive is true, where the control connection
            // is not closed after a command is executed.
            reqFTP.KeepAlive = false;

            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.
            reqFTP.UseBinary = true;

            // Notify the server about the size of the uploaded file
            reqFTP.ContentLength = fileInf.Length;

            // The buffer size is set to 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            // Opens a file stream (System.IO.FileStream) to read the file
            // to be uploaded
            FileStream fs = fileInf.OpenRead();

            try
            {
                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload
                    // Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                fs.Close();
                //MessageBox.Show(ex.Message, "Upload Error");
                throw ex;
            }
        }

        /// <summary>
        /// Upload file to FTP server
        /// </summary>
        /// <remarks>
        /// Creator: PhongNTT
        /// Created date: 2010-03-16
        /// TESTED
        /// </remarks>
        public void Upload(string filename)
        {
            Upload(filename, filename);
        }

        /// <summary>
        /// Upload file to FTP server
        /// </summary>
        /// <param name="sourceFilename">Full path of source file</param>
        /// <param name="desFilename">Path on FTP server. Not need full. exp: /ABC/file001.txt</param>
        /// <remarks>
        /// Creator: PhongNTT
        /// Created date: 2010-03-16
        /// TESTED
        /// </remarks>
        public void Upload(string sourceFilename, string desFilename)
        {
            FileInfo fileInf = new FileInfo(sourceFilename);
            string uri = "ftp://" + m_FtpServerIP + "/" + desFilename;
            FtpWebRequest reqFTP;

            // Create FtpWebRequest object from the Uri provided
            reqFTP = (FtpWebRequest)FtpWebRequest.Create
                     (new Uri(uri));

            // Provide the WebPermission Credintials
            reqFTP.Credentials = new NetworkCredential(m_FtpUserID, m_FtpPassword);

            // By default KeepAlive is true, where the control connection
            // is not closed after a command is executed.
            reqFTP.KeepAlive = false;

            // Specify the command to be executed.
            reqFTP.Method = WebRequestMethods.Ftp.UploadFile;

            // Specify the data transfer type.
            reqFTP.UseBinary = true;

            // Notify the server about the size of the uploaded file
            reqFTP.ContentLength = fileInf.Length;

            // The buffer size is set to 2kb
            int buffLength = 2048;
            byte[] buff = new byte[buffLength];
            int contentLen;

            // Opens a file stream (System.IO.FileStream) to read the file
            // to be uploaded
            FileStream fs = fileInf.OpenRead();

            try
            {
                // Stream to which the file to be upload is written
                Stream strm = reqFTP.GetRequestStream();

                // Read from the file stream 2kb at a time
                contentLen = fs.Read(buff, 0, buffLength);

                // Till Stream content ends
                while (contentLen != 0)
                {
                    // Write Content from the file stream to the FTP Upload
                    // Stream
                    strm.Write(buff, 0, contentLen);
                    contentLen = fs.Read(buff, 0, buffLength);
                }

                // Close the file stream and the Request Stream
                strm.Close();
                fs.Close();
            }
            catch (Exception ex)
            {
                fs.Close();
                throw ex;
                //MessageBox.Show(ex.Message, "Upload Error");
            }
        }

        public void Delete(string fileName)
        {
            try
            {
                string uri = "ftp://" + m_FtpServerIP + "/" + fileName;
                FtpWebRequest reqFTP;
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + m_FtpServerIP + "/" + fileName));

                reqFTP.Credentials = new NetworkCredential(m_FtpUserID, m_FtpPassword);
                reqFTP.KeepAlive = false;
                reqFTP.Method = WebRequestMethods.Ftp.DeleteFile;

                string result = String.Empty;
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                long size = response.ContentLength;
                Stream datastream = response.GetResponseStream();
                StreamReader sr = new StreamReader(datastream);
                result = sr.ReadToEnd();
                sr.Close();
                datastream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show(ex.Message, "FTP 2.0 Delete");
            }
        }

        public string[] GetFilesDetailList()
        {
            string[] downloadFiles;
            try
            {
                StringBuilder result = new StringBuilder();
                FtpWebRequest ftp;
                ftp = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + m_FtpServerIP + "/"));
                ftp.Credentials = new NetworkCredential(m_FtpUserID, m_FtpPassword);
                ftp.Method = WebRequestMethods.Ftp.ListDirectoryDetails;
                WebResponse response = ftp.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                string line = reader.ReadLine();
                while (line != null)
                {
                    result.Append(line);
                    result.Append("\n");
                    line = reader.ReadLine();
                }

                result.Remove(result.ToString().LastIndexOf("\n"), 1);
                reader.Close();
                response.Close();
                return result.ToString().Split('\n');
                //MessageBox.Show(result.ToString().Split('\n'));
            }
            catch (Exception ex)
            {
                throw ex;
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                downloadFiles = null;
                return downloadFiles;
            }
        }

        public string[] GetFileList()
        {
            string[] downloadFiles;
            StringBuilder result = new StringBuilder();
            FtpWebRequest reqFTP;
            try
            {
                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + m_FtpServerIP + "/"));
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(m_FtpUserID, m_FtpPassword);
                reqFTP.Method = WebRequestMethods.Ftp.ListDirectory;
                WebResponse response = reqFTP.GetResponse();
                StreamReader reader = new StreamReader(response.GetResponseStream());
                //MessageBox.Show(reader.ReadToEnd());
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
                return result.ToString().Split('\n');
            }
            catch (Exception ex)
            {
                throw ex;
                //System.Windows.Forms.MessageBox.Show(ex.Message);
                downloadFiles = null;
                return downloadFiles;
            }
        }

        public void Download(string filePath, string fileName)
        {
            FtpWebRequest reqFTP;
            try
            {
                //filePath = <<The full path where the file is to be created.>>, 
                //fileName = <<Name of the file to be created(Need not be the name of the file on FTP server).>>
                FileStream outputStream = new FileStream(filePath + "\\" + fileName, FileMode.Create);

                reqFTP = (FtpWebRequest)FtpWebRequest.Create(new Uri("ftp://" + m_FtpServerIP + "/" + fileName));
                reqFTP.Method = WebRequestMethods.Ftp.DownloadFile;
                reqFTP.UseBinary = true;
                reqFTP.Credentials = new NetworkCredential(m_FtpUserID, m_FtpPassword);
                FtpWebResponse response = (FtpWebResponse)reqFTP.GetResponse();
                Stream ftpStream = response.GetResponseStream();
                long cl = response.ContentLength;
                int bufferSize = 2048;
                int readCount;
                byte[] buffer = new byte[bufferSize];

                readCount = ftpStream.Read(buffer, 0, bufferSize);
                while (readCount > 0)
                {
                    outputStream.Write(buffer, 0, readCount);
                    readCount = ftpStream.Read(buffer, 0, bufferSize);
                }

                ftpStream.Close();
                outputStream.Close();
                response.Close();
            }
            catch (Exception ex)
            {
                throw ex;
                //MessageBox.Show(ex.Message);
            }
        }

        public void CreateFolder(string dirPath)
        {
            string uri = "ftp://" + m_FtpServerIP + "/" + dirPath;

            WebRequest request = WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential(m_FtpUserID, m_FtpPassword);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream ftpStream = response.GetResponseStream();

            ftpStream.Close();
            response.Close();
        }

        public bool TryCreateFolder(string dirPath)
        {
            if (IsFolderExists(dirPath))
            {
                return false;
            }

            string uri = "ftp://" + m_FtpServerIP + "/" + dirPath;

            WebRequest request = WebRequest.Create(uri);
            request.Method = WebRequestMethods.Ftp.MakeDirectory;
            request.Credentials = new NetworkCredential(m_FtpUserID, m_FtpPassword);
            FtpWebResponse response = (FtpWebResponse)request.GetResponse();
            Stream ftpStream = response.GetResponseStream();

            ftpStream.Close();
            response.Close();

            return true;
        }

        public bool IsFolderExists(string dirPath)
        {
            string uri = "ftp://" + m_FtpServerIP + "/" + dirPath;

            try
            {
                FtpWebRequest request = (FtpWebRequest)WebRequest.Create(uri);
                request.Method = WebRequestMethods.Ftp.ListDirectory;
                request.Credentials = new NetworkCredential(m_FtpUserID, m_FtpPassword);
                using (FtpWebResponse response = (FtpWebResponse)request.GetResponse())
                {
                    return true;
                }
            }
            catch (WebException ex)
            {
                if (ex.Response != null)
                {
                    FtpWebResponse response = (FtpWebResponse)ex.Response;
                    if (response.StatusCode == FtpStatusCode.ActionNotTakenFileUnavailable)
                    {
                        return false;
                    }
                }
            }
            return false; 
        }

        public void UploadFolder(string localFolderPath, string folderOnServer, bool createFolder)
        {
            string ftpBasePath = "ftp://" + m_FtpServerIP + "/" + folderOnServer;

            string serverFolder = folderOnServer;
            
            if(createFolder)
            {
                DirectoryInfo dirInfo = new DirectoryInfo(localFolderPath);
                string folderToCreate = serverFolder + "/" + dirInfo.Name;

                TryCreateFolder(folderToCreate);

                serverFolder = serverFolder + "/" + dirInfo.Name;
            }

            UploadAllFileInFolder(localFolderPath, serverFolder);

            UploadAllSubFolders(localFolderPath, serverFolder);
        }

        /// <summary>
        /// Upload all files in folder to FTP server
        /// </summary>
        public void UploadAllFileInFolder(string localFolderPath, string folderOnServer)
        {
            string[] allFiles = Directory.GetFiles(localFolderPath);
            string ftpBasePath = folderOnServer;

            foreach(string file in allFiles)
            {
                FileInfo fileInfo = new FileInfo(file);
                string fileOnServer = ftpBasePath + "/" + fileInfo.Name;

                Upload(file, fileOnServer);
            }
        }

        public void UploadAllSubFolders(string localFolderPath, string folderOnServer)
        {
            string[] allSubFolder = Directory.GetDirectories(localFolderPath);
            string ftpBasePath = folderOnServer;

            foreach (string folder in allSubFolder)
            {
                UploadFolder(folder, folderOnServer, true);
            }
        }

    }
}

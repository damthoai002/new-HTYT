using System;
using System.IO;
using System.Text;
using log4net;
using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip.Compression;
using System.Collections.Generic;

namespace UKPI.Core
{
    public class Zipper
    {
        private static readonly ILog logger = LogManager.GetLogger(typeof(Zipper));
        /// <summary>
        /// Zip a set of files
        /// </summary>
        /// <remarks>
        /// Author			: Vu Thanh Tuan - FSOFT G3
        /// Modifications	: Created 24-Apr-2006
        /// </remarks>
        /// <param name="fileNames">Name of files are not equal</param>
        /// <param name="zipFileName"></param>
        /// <param name="password">null if do not use password</param>
        public static void ZipFiles(string[] fileNames, string zipFileName, string password)
        {
            Crc32 crc = new Crc32();
            ZipOutputStream s = new ZipOutputStream(File.Create(zipFileName));

            try
            {
                s.Password = password;
                s.SetLevel(6); // 0 - store only to 9 - means best compression

                foreach (string file in fileNames)
                {
                    FileStream fs = File.OpenRead(file);
                    byte[] buffer;
                    ZipEntry entry;

                    try
                    {
                        buffer = new byte[fs.Length];
                        fs.Read(buffer, 0, buffer.Length);
                        entry = new ZipEntry(Path.GetFileName(file));

                        entry.DateTime = DateTime.Now;

                        // set Size and the crc, because the information
                        // about the size and crc should be stored in the header
                        // if it is not set it is automatically written in the footer.
                        // (in this case size == crc == -1 in the header)
                        // Some ZIP programs have problems with zip files that don't store
                        // the size and crc in the header.
                        entry.Size = fs.Length;
                        fs.Close();
                    }
                    catch (Exception ex)
                    {
                        if (fs != null)
                        {
                            fs.Close();
                        }
                        throw ex;
                    }

                    crc.Reset();
                    crc.Update(buffer);

                    entry.Crc = crc.Value;

                    s.PutNextEntry(entry);

                    s.Write(buffer, 0, buffer.Length);

                }

                s.Finish();
                s.Close();
            }
            catch (Exception ex)
            {
                if (s != null)
                {
                    s.Close();
                }
                throw ex;
            }
        }

        /// <summary>
        /// Zip a file
        /// </summary>
        /// <remarks>
        /// Author			: Pham Duong Vu - FSOFT G3
        /// Modifications	: Created 25-Apr-2006
        /// </remarks>
        /// <param name="strFileName">File name need to zip</param>
        /// <param name="strZipFileName">File name to zip ext: .zip </param>
        /// <param name="strPassword">null if do not use password</param>
        public static void ZipFiles(string strFileName, string strZipFileName, string strPassword)
        {
            Crc32 crc = new Crc32();
            ZipOutputStream s = new ZipOutputStream(File.Create(strZipFileName));

            try
            {
                s.Password = strPassword;
                s.SetLevel(6); // 0 - store only to 9 - means best compression


                FileStream fs = File.OpenRead(strFileName);
                byte[] buffer;
                ZipEntry entry;

                try
                {
                    buffer = new byte[fs.Length];
                    fs.Read(buffer, 0, buffer.Length);
                    entry = new ZipEntry(Path.GetFileName(strFileName));

                    entry.DateTime = DateTime.Now;

                    // set Size and the crc, because the information
                    // about the size and crc should be stored in the header
                    // if it is not set it is automatically written in the footer.
                    // (in this case size == crc == -1 in the header)
                    // Some ZIP programs have problems with zip files that don't store
                    // the size and crc in the header.
                    entry.Size = fs.Length;
                    fs.Close();
                }
                catch (Exception ex)
                {
                    if (fs != null)
                    {
                        fs.Close();
                    }
                    throw ex;
                }

                crc.Reset();
                crc.Update(buffer);

                entry.Crc = crc.Value;

                s.PutNextEntry(entry);

                s.Write(buffer, 0, buffer.Length);

                s.Finish();
                s.Close();
            }
            catch (Exception ex)
            {
                if (s != null)
                {
                    s.Close();
                }
                throw ex;
            }
        }

        public static void ZipFiles(MemoryStream fileStream, string fileName, string strZipFileName, string strPassword)
        {
            Crc32 crc = new Crc32();
            ZipOutputStream s = new ZipOutputStream(File.Create(strZipFileName));

            try
            {
                s.Password = strPassword;
                s.SetLevel(6); // 0 - store only to 9 - means best compression

                byte[] buffer;
                ZipEntry entry;

                try
                {
                    fileStream.Flush();
                    buffer = fileStream.ToArray();
                    entry = new ZipEntry(Path.GetFileName(fileName));

                    entry.DateTime = DateTime.Now;

                    // set Size and the crc, because the information
                    // about the size and crc should be stored in the header
                    // if it is not set it is automatically written in the footer.
                    // (in this case size == crc == -1 in the header)
                    // Some ZIP programs have problems with zip files that don't store
                    // the size and crc in the header.
                    entry.Size = fileStream.Length;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

                crc.Reset();
                crc.Update(buffer);

                entry.Crc = crc.Value;

                s.PutNextEntry(entry);

                s.Write(buffer, 0, buffer.Length);

                s.Finish();
                s.Close();
            }
            catch (Exception ex)
            {
                if (s != null)
                {
                    s.Close();
                }
                throw ex;
            }
        }

        public static List<KeyValuePair<string, byte[]>> UnZipFile(string zipFilePath, string password)
        {
            List<KeyValuePair<string, byte[]>> result = new List<KeyValuePair<string, byte[]>>();
            ZipInputStream s = new ZipInputStream(File.OpenRead(zipFilePath));

            try
            {
                ZipEntry theEntry;
                s.Password = password;

                while ((theEntry = s.GetNextEntry()) != null)
                {
                    MemoryStream ms = new MemoryStream();
                    try
                    {
                        int size = 2048;
                        byte[] data = new byte[2048];
                        while (true)
                        {
                            size = s.Read(data, 0, data.Length);
                            if (size > 0)
                            {
                                ms.Write(data, 0, size);
                            }
                            else
                            {
                                break;
                            }
                        }
                        ms.Flush();

                        result.Add(new KeyValuePair<string,byte[]>(theEntry.Name, ms.ToArray()));
                        ms.Close();
                    }
                    catch (Exception ex)
                    {
                        if (ms != null)
                        {
                            ms.Close();
                        }
                        throw ex;
                    }
                }
                s.Close();
                return result;
            }
            catch (Exception ex)
            {
                if (s != null)
                {
                    s.Close();
                }
                throw ex;
            }
        }

        /// <summary>
        /// Unzip a file
        /// </summary>
        /// <remarks>
        /// Author			: Vu Thanh Tuan - FSOFT G3
        /// Modifications	: Created 24-Apr-2006
        /// </remarks>
        /// <param name="zipFileName">Name of files are not equal</param>
        /// <param name="path"></param>
        /// <param name="password">null if do not use password</param>
        public static void UnZipFile(string zipFileName, string path, string password)
        {
            ZipInputStream s = new ZipInputStream(File.OpenRead(zipFileName));

            try
            {
                ZipEntry theEntry;
                s.Password = password;

                while ((theEntry = s.GetNextEntry()) != null)
                {
                    string fileName = RemoveEndBackSlash(path) + @"\" + Path.GetFileName(theEntry.Name);

                    if (fileName != String.Empty)
                    {
                        FileStream streamWriter = File.Create(fileName);

                        try
                        {
                            int size = 2048;
                            byte[] data = new byte[2048];
                            while (true)
                            {
                                size = s.Read(data, 0, data.Length);
                                if (size > 0)
                                {
                                    streamWriter.Write(data, 0, size);
                                }
                                else
                                {
                                    break;
                                }
                            }

                            streamWriter.Close();
                        }
                        catch (Exception ex)
                        {
                            if (streamWriter != null)
                            {
                                streamWriter.Close();
                            }
                            throw ex;
                        }
                    }
                }
                s.Close();
            }
            catch (Exception ex)
            {
                if (s != null)
                {
                    s.Close();
                }
                throw ex;
            }
        }

        /// <summary>
        /// Check zip file is encrypted or not
        /// </summary>
        /// <param name="zipFileName"></param>
        /// <returns></returns>
        public static bool IsEncrypt(string zipFileName)
        {
            bool blnReturn;
            ZipInputStream s = new ZipInputStream(File.OpenRead(zipFileName));

            try
            {
                ZipEntry theEntry = s.GetNextEntry();
                blnReturn = theEntry.IsCrypted;
                s.Close();
                return blnReturn;
            }
            catch (Exception ex)
            {
                if (s != null)
                {
                    s.Close();
                }
                throw ex;
            }
        }

        /// <summary>
        /// Remove end back slash in path string
        /// </summary>
        /// <remarks>
        /// Author			: Vu Thanh Tuan - FSOFT G3
        /// Modifications	: Created 19-Apr-2006
        /// </remarks>
        /// <param name="path"></param>
        /// <returns></returns>
        public static string RemoveEndBackSlash(string path)
        {
            if (path.Substring(path.Length - 1, 1) == "\\")
                return path.Substring(0, path.Length - 1);
            else
                return path;
        }
    }

    public class Cryptography
    {
        #region - Variant -

        private static readonly ILog log = LogManager.GetLogger(typeof(Cryptography));
        private int fillchar = (int)'=';
        private string cvt = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
        + "abcdefghijklmnopqrstuvwxyz" + "0123456789+/";
        #endregion - Variant -

        public Cryptography()
        {

        }

        #region  - Encode, Decode -

        /// <summary>
        /// Encode a string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public String Encode(String data)
        {
            if (data == null)
                return null;
            return (GetString(Encode(GetBinaryBytes(data))));
        }

        /// <summary>
        /// Encode data function
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public byte[] Encode(byte[] data)
        {
            int c;
            int len = data.Length;
            StringBuilder ret = new StringBuilder(((len / 3) + 1) * 4);
            for (int i = 0; i < len; ++i)
            {
                c = (data[i] >> 2) & 0x3f;
                ret.Append(cvt[c]);
                c = (data[i] << 4) & 0x3f;
                if (++i < len)
                    c |= (data[i] >> 4) & 0x0f;

                ret.Append(cvt[c]);
                if (i < len)
                {
                    c = (data[i] << 2) & 0x3f;
                    if (++i < len)
                        c |= (data[i] >> 6) & 0x03;

                    ret.Append(cvt[c]);
                }
                else
                {
                    ++i;
                    ret.Append((char)fillchar);
                }

                if (i < len)
                {
                    c = data[i] & 0x3f;
                    ret.Append(cvt[c]);
                }
                else
                {
                    ret.Append((char)fillchar);
                }
            }

            return (GetBinaryBytes(ret.ToString()));
        }

        /// <summary>
        /// Decode a string
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public String Decode(String data)
        {
            if (data == null)
                return null;

            return (GetString(Decode(GetBinaryBytes(data))));
        }

        public byte[] Decode(byte[] data)
        {
            int c;
            int c1;
            int len = data.Length;
            StringBuilder ret = new StringBuilder((len * 3) / 4);
            for (int i = 0; i < len; ++i)
            {
                c = cvt.IndexOf((char)data[i]);
                ++i;
                c1 = cvt.IndexOf((char)data[i]);
                c = ((c << 2) | ((c1 >> 4) & 0x3));
                ret.Append((char)c);
                if (++i < len)
                {
                    c = data[i];
                    if (fillchar == c)
                        break;

                    c = cvt.IndexOf((char)c);
                    c1 = ((c1 << 4) & 0xf0) | ((c >> 2) & 0xf);
                    ret.Append((char)c1);
                }

                if (++i < len)
                {
                    c1 = data[i];
                    if (fillchar == c1)
                        break;

                    c1 = cvt.IndexOf((char)c1);
                    c = ((c << 6) & 0xc0) | c1;
                    ret.Append((char)c);
                }
            }

            return (GetBinaryBytes(ret.ToString()));
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="arr"></param>
        /// <returns></returns>
        private string GetString(byte[] arr)
        {
            StringBuilder buf = new StringBuilder();
            for (int i = 0; i < arr.Length; ++i)
                buf.Append((char)arr[i]);

            return (buf.ToString());
        }

        /// <summary>
        /// Get binary byte function
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private byte[] GetBinaryBytes(String str)
        {
            byte[] b = new byte[str.Length];
            for (int i = 0; i < b.Length; ++i)
                b[i] = (byte)str[i];

            return (b);
        }

        #endregion  - Encode, Decode -

        #region Generate password by filename by the traditional way of Unilever

        /// <summary>
        /// Auto generate one script to decrypt
        /// </summary>
        /// <remarks>
        /// Author			: HungNT - G3 FPT Software
        /// Created date	: 2005/04/01
        /// Convert to CS	: DucNM - G3 FPT Software
        /// Modified date	: 2006/04/07
        /// </remarks>
        /// <param name="a_strFilename">myStr - The string get from distributor code and week</param>
        /// <returns> The string gennerate after script</returns>
        public string GenPWDByFilename(string a_strFilename, int maxLen, string key)
        {
            //m_MaxLenFile = 
            if (a_strFilename == null || a_strFilename.Length == 0)
                return "";

            try
            {
                a_strFilename = System.IO.Path.GetFileNameWithoutExtension(a_strFilename.Trim());

                if (a_strFilename.Length >= maxLen)
                    a_strFilename = a_strFilename.Substring(a_strFilename.Length - maxLen);

                string returnValue = GenPassword(a_strFilename, key).Replace("\"", "");
                returnValue = returnValue.Replace("'", "");
                return returnValue;
            }
            catch (Exception ex)
            {
                //log4net
                log.Error(ex.Message + ex.StackTrace);
                return "";
            }
        }

        /// <summary>
        /// Auto generate one script to decrypt
        /// </summary>
        /// <param name="a_strToken">The string get from distributor code and week</param>
        /// <returns>The string gennerate after script</returns>
        public string GenPassword(string token, string key)
        {
            if (token == null || token.Length == 0)
                return "";

            char chr;
            string returnValue = "";
            int j;
            int k;

            try
            {
                //Initial data before generate
                j = 1;

                //Generate password by mCode string and myStr
                for (int i = 0; i < token.Length; i++)
                {
                    chr = key[j - 1];
                    j = (j == key.Length) ? 1 : j + 1;

                    k = token[i] ^ chr;

                    if (k < 33 || k > 126)
                    {
                        k = 93;
                    }

                    returnValue = returnValue + (char)k;
                }
                return returnValue;
            }
            catch (Exception ex)
            {
                //log4net
                log.Error(ex.Message + ex.StackTrace);
                return "";
            }
        }
        #endregion Generate password by filename
    }
}

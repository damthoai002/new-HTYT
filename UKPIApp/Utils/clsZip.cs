using System;
using System.Collections;
using System.IO;

using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
using ICSharpCode.SharpZipLib.BZip2;
using ICSharpCode.SharpZipLib.Zip.Compression;

namespace UKPI.Utils
{
	/// <summary>
	/// Summary description for clsZip.
	/// </summary>
	public class clsZip
	{
		public clsZip()
		{
			//
			// TODO: Add constructor logic here
			//
		}

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
					catch(Exception ex)
					{
						if(fs != null)
						{
							fs.Close();
						}
						throw ex;
					}
			
					crc.Reset();
					crc.Update(buffer);
			
					entry.Crc  = crc.Value;
			
					s.PutNextEntry(entry);
			
					s.Write(buffer, 0, buffer.Length);
			
				}
		
				s.Finish();
				s.Close();
			}
			catch(Exception ex)
			{
				if(s != null)
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
				catch(Exception ex)
				{
					if(fs != null)
					{
						fs.Close();
					}
					throw ex;
				}
		
				crc.Reset();
				crc.Update(buffer);
		
				entry.Crc  = crc.Value;
		
				s.PutNextEntry(entry);
		
				s.Write(buffer, 0, buffer.Length);
		
				s.Finish();
				s.Close();
			}
			catch(Exception ex)
			{
				if(s != null)
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
		public static void UnzipFile(string zipFileName, string path, string password)
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
						catch(Exception ex)
						{
							if(streamWriter != null)
							{
								streamWriter.Close();
							}
							throw ex;
						}
					}
				}
				s.Close();
			}
			catch(Exception ex)
			{
				if(s != null)
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
				blnReturn =  theEntry.IsCrypted;
				s.Close();			
				return blnReturn;
			}
			catch(Exception ex)
			{
				if(s != null)
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
			if(path.Substring(path.Length - 1, 1)== "\\")
				return path.Substring(0, path.Length - 1);
			else
				return path;
        }

        #region Folder Zip

        /// <summary>
        /// Zip folder
        /// </summary>
        /// <param name="inputFolderPath">Path to zip. If end with '\' then zip all files anf sub-folders. Not end with '\' then zip folder.</param>
        /// <param name="outputPathAndFile">File name as ZIP result</param>
        /// <param name="password">Password for zip</param>
        public static void ZipFolder(string inputFolderPath, string outputPathAndFile, string password)
        {
            ArrayList ar = GenerateFileList(inputFolderPath); // generate file list
            int TrimLength = (Directory.GetParent(inputFolderPath)).ToString().Length;
            // find number of chars to remove     // from orginal file path
            //TrimLength += 1; //remove '\'
            FileStream ostream;
            byte[] obuffer;
            string outPath = outputPathAndFile;
            ZipOutputStream oZipStream = new ZipOutputStream(File.Create(outPath)); // create zip stream
            if (password != null && password != String.Empty)
                oZipStream.Password = password;
            oZipStream.SetLevel(6); // maximum compression
            ZipEntry oZipEntry;
            foreach (string Fil in ar) // for each file, generate a zipentry
            {
                oZipEntry = new ZipEntry(Fil.Remove(0, TrimLength));
                oZipStream.PutNextEntry(oZipEntry);

                if (!Fil.EndsWith(@"/")) // if a file ends with '/' its a directory
                {
                    ostream = File.OpenRead(Fil);
                    obuffer = new byte[ostream.Length];
                    ostream.Read(obuffer, 0, obuffer.Length);
                    oZipStream.Write(obuffer, 0, obuffer.Length);
                    ostream.Close();
                }
            }
            oZipStream.Finish();
            oZipStream.Close();
        }


        private static ArrayList GenerateFileList(string Dir)
        {
            ArrayList fils = new ArrayList();
            bool Empty = true;
            foreach (string file in Directory.GetFiles(Dir)) // add each file in directory
            {
                fils.Add(file);
                Empty = false;
            }

            if (Empty)
            {
                if (Directory.GetDirectories(Dir).Length == 0)
                // if directory is completely empty, add it
                {
                    fils.Add(Dir + @"/");
                }
            }

            foreach (string dirs in Directory.GetDirectories(Dir)) // recursive
            {
                foreach (object obj in GenerateFileList(dirs))
                {
                    fils.Add(obj);
                }
            }
            return fils; // return file list
        }


        public static void UnZipFolder(string zipPathAndFile, string outputFolder, string password, bool deleteZipFile)
        {
            ZipInputStream s = new ZipInputStream(File.OpenRead(zipPathAndFile));
            if (password != null && password != String.Empty)
                s.Password = password;
            ZipEntry theEntry;
            string tmpEntry = String.Empty;
            while ((theEntry = s.GetNextEntry()) != null)
            {
                string directoryName = outputFolder;
                string fileName = Path.GetFileName(theEntry.Name);
                // create directory 
                if (directoryName != "")
                {
                    Directory.CreateDirectory(directoryName);
                }
                if (fileName != String.Empty)
                {
                    if (theEntry.Name.IndexOf(".ini") < 0)
                    {
                        string fullPath = directoryName + "\\" + theEntry.Name;
                        fullPath = fullPath.Replace("\\ ", "\\");
                        string fullDirPath = Path.GetDirectoryName(fullPath);
                        if (!Directory.Exists(fullDirPath)) Directory.CreateDirectory(fullDirPath);
                        FileStream streamWriter = File.Create(fullPath);
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
                }
            }
            s.Close();
            if (deleteZipFile)
                File.Delete(zipPathAndFile);
        }

        #endregion Folder Zip
    }
}

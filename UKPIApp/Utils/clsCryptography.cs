using System;
using System.Security.Cryptography;
using System.Text;

namespace UKPI.Utils
{
	public class clsCryptography
	{
		public clsCryptography(){}

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
					ret.Append((char) fillchar);
				}

				if (i < len) 
				{
					c = data[i] & 0x3f;
					ret.Append(cvt[c]);
				} 
				else 
				{
					ret.Append((char) fillchar);
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
				ret.Append((char) c);
				if (++i < len) 
				{
					c = data[i];
					if (fillchar == c)
						break;

					c = cvt.IndexOf((char) c);
					c1 = ((c1 << 4) & 0xf0) | ((c >> 2) & 0xf);
					ret.Append((char) c1);
				}

				if (++i < len) 
				{
					c1 = data[i];
					if (fillchar == c1)
						break;

					c1 = cvt.IndexOf((char) c1);
					c = ((c << 6) & 0xc0) | c1;
					ret.Append((char) c);
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
				buf.Append((char) arr[i]);

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
				b[i] = (byte) str[i];

			return (b);
		}

		private int fillchar = (int)'=';

		private string cvt = "ABCDEFGHIJKLMNOPQRSTUVWXYZ"
		+ "abcdefghijklmnopqrstuvwxyz" + "0123456789+/";

		#region Generate password by filename by the traditional way of Unilever
		private string m_strPasswordToken = "Unilever";

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
		public string GenPWDByFilename(string a_strFilename)
		{
			if(a_strFilename == null || a_strFilename.Length == 0)
				return "";

			try
			{
				a_strFilename = System.IO.Path.GetFileNameWithoutExtension(a_strFilename.Trim());

				if(a_strFilename.Length >= 25)
					a_strFilename = a_strFilename.Substring(a_strFilename.Length - 25);

				string returnValue = GenPassword(a_strFilename).Replace("\"", "");
				returnValue = returnValue.Replace("'", "");
				return returnValue;
			}
			catch(Exception)
			{
				//log4net
				return "";
			}
		}

        /// <summary>
        /// Add by KienTNT
        /// </summary>
        /// <param name="a_strFilename">Full file name without extention</param>
        /// <returns>password</returns>
        public string GenPWDByFullFilename(string a_strFilename)
        {
            if (a_strFilename == null || a_strFilename.Length == 0)
                return "";

            try
            {
                a_strFilename = System.IO.Path.GetFileNameWithoutExtension(a_strFilename.Trim());

                //if (a_strFilename.Length >= 25)
                //    a_strFilename = a_strFilename.Substring(a_strFilename.Length - 25);

                string returnValue = GenPassword(a_strFilename).Replace("\"", "");
                returnValue = returnValue.Replace("'", "");
                return returnValue;
            }
            catch (Exception)
            {
                //log4net
                return "";
            }
        }

		/// <summary>
		/// Auto generate one script to decrypt
		/// </summary>
		/// <param name="a_strToken">The string get from distributor code and week</param>
		/// <returns>The string gennerate after script</returns>
		public string GenPassword(string a_strToken)
		{	
			if(a_strToken == null || a_strToken.Length == 0)
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
				for(int i = 0; i < a_strToken.Length; i ++)
				{
					chr = m_strPasswordToken[j-1];
					j = (j == m_strPasswordToken.Length) ? 1 : j + 1;

					k = a_strToken[i] ^ chr;

					if (k < 33 || k > 126)
					{
						k = 93;
					}
					
					returnValue = returnValue + (char)k;
				}
				return returnValue;
			}
			catch (Exception)
			{
				//log4net
				return "";
			}
		}
		#endregion Generate password by filename
	}
}

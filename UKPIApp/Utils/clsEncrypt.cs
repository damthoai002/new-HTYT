using System.Diagnostics;
using Microsoft.VisualBasic;
using System;
using System.Data;
using System.Collections;

namespace UKPI.Utils
{
    public class clsEncrypt
    {
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
            if (a_strFilename == null || a_strFilename.Length == 0)
                return "";

            try
            {
                a_strFilename = GetFileNameWithoutExt(a_strFilename.Trim());

                if (a_strFilename.Length >= 25)
                    a_strFilename = a_strFilename.Substring(a_strFilename.Length - 25);

                string returnValue = GenPassword(a_strFilename).Replace("\"", "");
                returnValue = returnValue.Replace("'", "");
                return returnValue;
            }
            catch
            {
                //log4net
                return "";
            }
        }

        /// <summary>
        /// Get file name by full name
        /// </summary>
        /// <remarks>
        /// Author			: DucNM - G3 FPT Software
        /// Created date	: 2006/04/07
        /// </remarks>
        /// <param name="fullname"></param>
        /// <returns></returns>
        public string GetFileName(string fullname)
        {
            string filename = "";
            int i = fullname.LastIndexOf("\\");

            if (i >= 0)
                filename = fullname.Substring(i + 1);

            return filename;
        }

        /// <summary>
        /// Get file name from full name without extention
        /// </summary>
        /// <remarks>
        /// Author			: DucNM - G3 FPT Software
        /// Created date	: 2006/04/07
        /// </remarks>
        /// <param name="fullname"></param>
        /// <returns></returns>
        public string GetFileNameWithoutExt(string fullname)
        {
            string filename = GetFileName(fullname);
            int i = filename.LastIndexOf(".");
            if (i >= 0)
                filename = filename.Substring(0, i);

            return filename;
        }

        /// <summary>
        /// Auto generate one script to decrypt
        /// </summary>
        /// <param name="a_strToken">The string get from distributor code and week</param>
        /// <returns>The string gennerate after script</returns>
        public string GenPassword(string a_strToken)
        {
            if (a_strToken == null || a_strToken.Length == 0)
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
                for (int i = 0; i < a_strToken.Length; i++)
                {
                    chr = m_strPasswordToken[j - 1];
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

    }
}


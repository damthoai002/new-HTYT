using System;
using System.Windows.Forms;
using System.IO;

using ICSharpCode.SharpZipLib.Checksums;
using ICSharpCode.SharpZipLib.Zip;
using ICSharpCode.SharpZipLib.GZip;
namespace UVMI.Utils
{
	/// <summary>
	/// Summary description for DateTimeUtil.
	/// </summary>
	public class clsUtil
	{
	//private EncodePass.Encrypt encrypt = new EncodePass.Encrypt();

		public clsUtil()
		{
			
		}
		/// <summary>
		/// Change DateTime value to String format "YYYYMMDD"
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	21-Apr-2006
		/// </remarks>
		/// <param name="m_date">date value in DateTime Type</param>
		/// <returns>string</returns>
		public string GetDateString(DateTime m_date)
		{
			string s_date;
			string s_day,s_month,s_year;
			
			if(m_date.Month <10)
				s_month="0" + m_date.Month.ToString();
			else
				s_month=m_date.Month.ToString();
			
			if(m_date.Day<10)
				s_day="0" + m_date.Day.ToString();
			else
				s_day=m_date.Day.ToString(); 

			s_year=m_date.Year.ToString();
 
			s_date=s_year+s_month+s_day;
			
			return s_date;
		}
		/// <summary>
		/// Change string date format "YYYYMMDD" to format "DD/MM/YYYY"
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	17-Apr-2006
		/// </remarks>
		/// <param name="date"></param>
		/// <returns></returns>
		public string GetDateDMY(string date)
		{
			if (date.Length == 8)
			{
				string m_year = date.Substring(0,4);
				string m_month = date.Substring(4,2);
				string m_day = date.Substring(6,2);
				return m_day+ "/" + m_month + "/" + m_year;
			}
			else return "";
			
		}

		/// <summary>
		/// Change string date format "DD/MM/YYYY" to format "YYYYMMDD"
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	17-Apr-2006
		/// </remarks>
		/// <param name="date"></param>
		/// <returns></returns>
		public string GetDateYMD(string date)
		{
			if (date.Length == 10)
			{
				string m_year = date.Substring(6,4);
				string m_month = date.Substring(3,2);
				string m_day = date.Substring(0,2);
				return m_year+m_month+m_day;
			}
			else return "";
			
		}
	
		/// <summary>
		/// Change date String format "YYYYMMDD" to DateTime type
		/// </summary>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	17-Apr-2006
		/// </remarks>
		/// <param name="strDate">string date in string Type</param>
		/// <returns>DateTime</returns>
		public DateTime GetDateType(string strDate)
		{
			int intYear = Int32.Parse(strDate.Substring(0,4));
			int intMonth =Int32.Parse(strDate.Substring(4,2));
			int intDay = Int32.Parse(strDate.Substring(6,2));

			DateTime dtm = new DateTime(intYear,intMonth,intDay);

			return dtm;
		}

		/// <summary>
		/// Validate FromDate , ToDateBind data to cboDistributor when user
		/// change value of cboRegion
		/// </summary>
		/// <param name="dtmFromDate">From date</param>
		/// <param name="dtmToDate">To date</param>
		/// <returns>bool</returns>
		public bool ValidateDate(DateTime dtmFromDate, DateTime dtmToDate)
		{
			return (dtmFromDate <= dtmToDate );
		}
		
		/// <summary>
		/// Validate number
		/// </summary>
		/// <remarks>
		/// Creator:		Nguyen Bao Nguyen - Developer, G3, FPT-SOFT
		/// Modifiation:	27-Mar-2006 Created
		/// </remarks>
		/// <param name="strNumber">string number</param>
		/// <returns>bool</returns>
		public bool ValidateNumber(string strNumber)
		{
			bool blnRetVal = true;
			try 
			{
				int intTemp = System.Int32.Parse(strNumber);
				if ( intTemp <= 0 )
				{
					MessageBox.Show("Average day or Safety day must be positive number","Warning",MessageBoxButtons.OK,MessageBoxIcon.Warning);
				}
				return blnRetVal;
			}
			catch (System.Exception ex)
			{
				blnRetVal = false;
				return blnRetVal;
			}
		}
		/// <summary>
		/// Replace ' by ''
		/// </summary>
		/// <param name="value"></param>
		/// <returns></returns>
		public string EncodeString(string strValue)
		{
			if(strValue == null || strValue.Length == 0)
				return strValue;
			strValue = strValue.Replace("'","''");
			strValue = strValue.Replace("*","%");
			strValue = strValue.Replace("?","_");

			return strValue;
		}
		/// <summary>
		/// Zip file
		/// </summary>
		/// <param name="strPath">Path contains file to zip</param>
		/// <param name="strFilename">Name of file to zip</param>
		/// <param name="strLocation">Location where locate zipped file</param>
//		private void ZipFile(string strPath,string strFilename,string strLocation)
//		{
//			string m_filename =strPath + "\\" + strFilename;
//		
//			Crc32 crc = new Crc32();
//			string m_zipfilename = strLocation +"\\"+strFilename+".zip";
//			ZipOutputStream s = new ZipOutputStream(File.Create(m_zipfilename));
//		
//			s.SetLevel(6); // 0 - store only to 9 - means best compression
//			s.Password = encrypt.GenPWDByFilename(m_zipfilename);
//			FileStream fs = File.OpenRead(m_filename);
//			
//			byte[] buffer = new byte[fs.Length];
//			fs.Read(buffer, 0, buffer.Length);
//			ZipEntry entry = new ZipEntry(m_filename);
//			
//			entry.DateTime = DateTime.Now;
//			
//			// set Size and the crc, because the information
//			// about the size and crc should be stored in the header
//			// if it is not set it is automatically written in the footer.
//			// (in this case size == crc == -1 in the header)
//			// Some ZIP programs have problems with zip files that don't store
//			// the size and crc in the header.
//			entry.Size = fs.Length;
//			fs.Close();
//			
//			crc.Reset();
//			crc.Update(buffer);
//			
//			entry.Crc  = crc.Value;
//			
//			s.PutNextEntry(entry);
//			
//			s.Write(buffer, 0, buffer.Length);
//		
//		
//			s.Finish();
//			s.Close();
//		}
	}
}

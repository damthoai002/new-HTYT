using System;
using System.Reflection;
using System.Diagnostics;
using System.Text.RegularExpressions;
using System.Data;
//using Microsoft.Office.Interop.Excel;
using Excel;

namespace UKPI.Utils
{
	public enum MyColor
	{
		Blue,
		Red,
		Yellow,
		White,
		PaleTurquoise,
		LightGray,
		LightYellow,
		LightTurquoise,
		Black,
		SalMon,
		Grey,
        Brown,
        black,
        Gold
	}

	/// <summary>
	/// Summary description for clsExcelBase.
	/// </summary>
	public class clsExcelBase
	{
		public MyColor color;
		
		static		object			m_objMissing		= Missing.Value;

		public clsExcelBase()
		{
			//
			// TODO: Add constructor logic here
			//
		}
		public MyColor MyColorExcel
		{
			get { return color;}
		}

		/// <summary>
		/// Get list of column in excel
		/// </summary>
		/// <returns></returns>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	21-Nov-2006
		/// </remarks>
		public string[] GetColArr()
		{
			string[]						strColArr			= new string[256];
			int intCount;

			strColArr[0] = "A";
			strColArr[1] = "B";
			strColArr[2] = "C";
			strColArr[3] = "D";
			strColArr[4] = "E";
			strColArr[5] = "F";
			strColArr[6] = "G";
			strColArr[7] = "H";
			strColArr[8] = "I";
			strColArr[9] = "J";
			strColArr[10] = "K";
			strColArr[11] = "L";
			strColArr[12] = "M";
			strColArr[13] = "N";
			strColArr[14] = "O";
			strColArr[15] = "P";
			strColArr[16] = "Q";
			strColArr[17] = "R";
			strColArr[18] = "S";
			strColArr[19] = "T";
			strColArr[20] = "U";
			strColArr[21] = "V";
			strColArr[22] = "W";
			strColArr[23] = "X";
			strColArr[24] = "Y";
			strColArr[25] = "Z";

			intCount = 26;
			for (int intTmp = 0; intTmp<= 8; intTmp++) 
			{
				for (int intTmp1 = 0; intTmp1 <=25; intTmp1++)
				{
					if (intCount<=255)
					{
						strColArr[intCount] = strColArr[intTmp] + strColArr[intTmp1];
						intCount++;
					}
				}
			}
			return strColArr;
		}

		/// <summary>
		/// Format excel range
		/// </summary>
		/// <param name="excelRange"></param>
		/// <param name="blnCenter"></param>
		/// <param name="colorBackGround"></param>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	21-Nov-2006
		/// </remarks>
		public void FormatRange(Excel.Range excelRange,bool blnCenter, MyColor colorBackGround)
		{
			switch (colorBackGround)
			{
				case MyColor.Blue:
					excelRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Blue);
					break;
				case MyColor.Red:
					excelRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
					break;
				case MyColor.Yellow:
					excelRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
					break;
				case MyColor.White:
					excelRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
					break;
                case MyColor.Brown:
                    excelRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Brown);
                    break;
                case MyColor.black:
                    excelRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
                    break;
                case MyColor.Gold:
                    excelRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Gold);
                    break;
				case MyColor.PaleTurquoise:
					excelRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleTurquoise);
					break;
				case MyColor.LightGray:
					excelRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
					break;
				case MyColor.LightYellow:
					excelRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Khaki);
					break;
				case MyColor.LightTurquoise:
					excelRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.PaleTurquoise);
					break;
				case MyColor.SalMon:
					excelRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightPink);
					break;
				case MyColor.Grey:
					excelRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.LightGray);
					break;
				default:
					excelRange.Interior.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
					break;
			}
			if (blnCenter)
			{
				excelRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
				excelRange.HorizontalAlignment = XlVAlign.xlVAlignCenter;
			}
		}

		/// <summary>
		/// Format excel range
		/// </summary>
		/// <param name="excelRange"></param>
		/// <param name="blnCenter"></param>
		/// <param name="colorBackground"></param>
		/// <param name="colorForeGround"></param>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	21-Nov-2006
		/// </remarks>
		public void FormatRange(Excel.Range excelRange ,bool blnCenter, MyColor colorBackground, MyColor colorForeGround)
		{
			FormatRange(excelRange, blnCenter, colorBackground);
			switch (colorForeGround)
			{
				case MyColor.Blue:
					excelRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.SkyBlue);
					break;
				case MyColor.Red:
					excelRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Red);
					break;
				case MyColor.Yellow:
					excelRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Yellow);
					break;
				case MyColor.White:
					excelRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.White);
					break;
				case MyColor.Black:
					excelRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
					break;
				default:
					excelRange.Font.Color = System.Drawing.ColorTranslator.ToOle(System.Drawing.Color.Black);
					break;
			}
		}

		/// <summary>
		/// Format excel range
		/// </summary>
		/// <param name="excelRange"></param>
		/// <param name="blnCenter"></param>
		/// <param name="colorBackground"></param>
		/// <param name="colorForeGround"></param>
		/// <param name="blnBorder"></param>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	21-Nov-2006
		/// </remarks>
		public void FormatRange(Excel.Range excelRange ,bool blnCenter, MyColor colorBackground, MyColor colorForeGround ,bool blnBorder)
		{
			FormatRange(excelRange , blnCenter,colorBackground,colorForeGround);
			if (blnBorder)
			{
				excelRange.BorderAround(m_objMissing,XlBorderWeight.xlThin,XlColorIndex.xlColorIndexAutomatic,m_objMissing);
			}
		}

		/// <summary>
		/// Format excel range
		/// </summary>
		/// <param name="excelRange"></param>
		/// <param name="blnCenter"></param>
		/// <param name="colorBackground"></param>
		/// <param name="colorForeGround"></param>
		/// <param name="blnBorder"></param>
		/// <param name="blnBold"></param>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	21-Nov-2006
		/// </remarks>
		public void FormatRange(Excel.Range excelRange ,bool blnCenter, MyColor colorBackground, MyColor colorForeGround ,bool blnBorder, bool blnBold )
		{
			FormatRange(excelRange , blnCenter,colorBackground,colorForeGround);
			excelRange.Font.Bold = blnBold;
			if (blnBorder)
			{
				excelRange.BorderAround(m_objMissing,XlBorderWeight.xlThin,XlColorIndex.xlColorIndexAutomatic,m_objMissing);
			}
		}

		/// <summary>
		/// Format excel range
		/// </summary>
		/// <param name="excelRange"></param>
		/// <param name="blnCenter"></param>
		/// <param name="colorBackground"></param>
		/// <param name="colorForeGround"></param>
		/// <param name="blnBorder"></param>
		/// <param name="blnBold"></param>
		/// <param name="intSize"></param>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	21-Nov-2006
		/// </remarks>
		public void FormatRange(Excel.Range excelRange ,bool blnCenter, MyColor colorBackground, MyColor colorForeGround ,bool blnBorder, bool blnBold , int intSize )
		{
			FormatRange(excelRange , blnCenter,colorBackground,colorForeGround);
			excelRange.Font.Bold = blnBold;
			excelRange.Font.Size = intSize;
			if (blnBorder)
			{
				excelRange.BorderAround(m_objMissing,XlBorderWeight.xlThin,XlColorIndex.xlColorIndexAutomatic,m_objMissing);
			}
		}

		/// <summary>
		/// Format excel range
		/// </summary>
		/// <param name="excelRange"></param>
		/// <param name="blnCenter"></param>
		/// <param name="colorBackGround"></param>
		/// <param name="blnBorder"></param>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	21-Nov-2006
		/// </remarks>
		public void FormatRange(Excel.Range excelRange ,bool blnCenter, MyColor colorBackGround,bool blnBorder)
		{
			FormatRange(excelRange , blnCenter ,colorBackGround);
			if (blnBorder)
			{
				excelRange.BorderAround(m_objMissing,XlBorderWeight.xlThin,XlColorIndex.xlColorIndexAutomatic,m_objMissing);
			}
		}

		/// <summary>
		/// Format excel range
		/// </summary>
		/// <param name="excelRange"></param>
		/// <param name="blnCenter"></param>
		/// <param name="colorBackGround"></param>
		/// <param name="blnBorder"></param>
		/// <param name="blnBold"></param>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	21-Nov-2006
		/// </remarks>
		public void FormatRange(Excel.Range excelRange ,bool blnCenter, MyColor colorBackGround ,bool blnBorder , bool blnBold )
		{
			FormatRange(excelRange , blnCenter ,colorBackGround);
			excelRange.Font.Bold = blnBold;
			if (blnBorder)
			{
				excelRange.BorderAround(m_objMissing,XlBorderWeight.xlThin,XlColorIndex.xlColorIndexAutomatic,m_objMissing);
			}
				
		}

		/// <summary>
		/// Format excel range
		/// </summary>
		/// <param name="excelRange"></param>
		/// <param name="blnCenter"></param>
		/// <param name="colorBackGround"></param>
		/// <param name="blnBorder"></param>
		/// <param name="blnBold"></param>
		/// <param name="intSize"></param>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	21-Nov-2006
		/// </remarks>
		public void FormatRange(Excel.Range excelRange ,bool blnCenter, MyColor colorBackGround ,bool blnBorder , bool blnBold, int intSize )
		{
			FormatRange(excelRange , blnCenter ,colorBackGround);
			excelRange.Font.Bold = blnBold;
			excelRange.Font.Size = intSize;
			if (blnBorder)
			{
				excelRange.BorderAround(m_objMissing,XlBorderWeight.xlThin,XlColorIndex.xlColorIndexAutomatic,m_objMissing);
			}
				
		}

		/// <summary>
		/// Format excel range
		/// </summary>
		/// <param name="excelRange"></param>
		/// <param name="blnCenter"></param>
		/// <param name="blnBorder"></param>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	21-Nov-2006
		/// </remarks>
		public void FormatRange(Excel.Range excelRange ,bool blnCenter, bool blnBorder )
		{
			if (blnBorder)
			{
				excelRange.BorderAround(m_objMissing,XlBorderWeight.xlThin,XlColorIndex.xlColorIndexAutomatic,m_objMissing);
			}
			if (blnCenter)
			{
				excelRange.VerticalAlignment = XlVAlign.xlVAlignCenter;
				excelRange.HorizontalAlignment = XlVAlign.xlVAlignCenter;
			}
		}

		/// <summary>
		/// Format excel range
		/// </summary>
		/// <param name="excelRange"></param>
		/// <param name="blnCenter"></param>
		/// <param name="blnBorder"></param>
		/// <param name="blnBold"></param>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	21-Nov-2006
		/// </remarks>
		public void FormatRange(Excel.Range excelRange ,bool blnCenter ,bool blnBorder, bool blnBold )
		{
			FormatRange(excelRange,blnCenter,blnBorder);
			excelRange.Font.Bold = blnBold;
		}

		/// <summary>
		/// Format excel range
		/// </summary>
		/// <param name="excelRange"></param>
		/// <param name="blnCenter"></param>
		/// <param name="blnBorder"></param>
		/// <param name="blnBold"></param>
		/// <param name="intFontSize"></param>
		public void FormatRange(Excel.Range excelRange ,bool blnCenter ,bool blnBorder, bool blnBold, int intFontSize )
		{
			FormatRange(excelRange, blnCenter, blnBorder, blnBold);
			excelRange.Font.Size = intFontSize;
		}

		/// <summary>
		/// Kill excel process
		/// </summary>
		/// <param name="preExcelProcesses"></param>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	21-Nov-2006
		/// </remarks>
		public void KillProcess(Process[]preExcelProcesses)
		{
			Process[] excelProcesses = Process.GetProcessesByName("EXCEL");
			foreach(Process process in excelProcesses)
			{
				if(!Contain(preExcelProcesses, process))
				{
					process.Kill();
				}
			}
		}

		/// <summary>
		/// Check contain excel process in process array or not
		/// </summary>
		/// <param name="processes"></param>
		/// <param name="process"></param>
		/// <returns></returns>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	21-Nov-2006
		/// </remarks>
		private bool Contain(Process[] processes, Process process)
		{
			foreach(Process p in processes)
			{
				if (p.Id == process.Id)
					return true;
			}
			return false;
		}

		/// <summary>
		/// Validate number
		/// </summary>
		/// <param name="strNumber"></param>
		/// <param name="intLen"></param>
		/// <returns></returns>
		/// <remarks>
		/// Author:		Nguyen Bao Nguyen G3
		/// Modified:	21-Nov-2006
		/// </remarks>
		public bool ValidateNumber(string strNumber,int intLen)
		{
			bool blnValid = true;

			Regex regex = new Regex("^[0-9]*$");
			if (!regex.IsMatch(strNumber))
			{
				blnValid = false;
			}
			if (strNumber.Length > intLen)
			{
				blnValid = false;
			}
			return blnValid;
		}
	}
}

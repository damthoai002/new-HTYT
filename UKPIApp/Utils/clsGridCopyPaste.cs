using System;
using System.Windows.Forms;
using System.Data;
using UKPI.Controls;

namespace UKPI.Utils
{
	/// <summary>
	/// Summary description for clsGridCopyPaste.
	/// </summary>
	/// <remarks>
	/// Author:			Nguyen Minh Duc. G3.
	/// Created date:	14/05/2006
	/// </remarks>
	public class clsGridCopyPaste
	{
		public clsGridCopyPaste()
		{
			//
			// TODO: Add constructor logic here
			//
		}

		#region Copy Paste From Excel Trick. Author: Nguyen Minh Duc. G3.
		private static TextBox tempText = new TextBox();

		/// <summary>
		/// 
		/// </summary>
		/// <param name="ctrl"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void RegCopyPaste(Control ctrl)
		{
			DataGrid grd = ctrl as DataGrid;
			if(grd != null)
			{
				RegCopyPaste(grd);
			}
			else
			{
				foreach(Control sub in ctrl.Controls)
				{
					RegCopyPaste(sub);
				}
			}
		}

		/// <summary>
		/// 
		/// </summary>
		/// <param name="grd"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void RegCopyPaste(DataGrid grd)
		{
			foreach(DataGridTableStyle grdStyle in grd.TableStyles)
			{
				foreach(DataGridColumnStyle col in grdStyle.GridColumnStyles)
				{
					DataGridTextBoxColumn txtCol = col as DataGridTextBoxColumn;
					if(txtCol != null)
						RegCopyPaste(txtCol);
				}
			}
		}
		/// <summary>
		/// Register Paste data from Excel
		/// </summary>
		/// <param name="col"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void RegCopyPaste(DataGridTextBoxColumn col)
		{
			col.TextBox.Tag = col;
			//col.TextBox.KeyUp -=new KeyEventHandler(TextBox_KeyUp);
			//col.TextBox.KeyUp +=new KeyEventHandler(TextBox_KeyUp);
			col.TextBox.KeyDown -= new KeyEventHandler(TextBox_KeyUp);
			col.TextBox.KeyDown += new KeyEventHandler(TextBox_KeyUp);
		}

		/// <summary>
		/// Handle Paste data from Excel
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		private static void TextBox_KeyUp(object sender, KeyEventArgs e)
		{
			if(e.KeyCode == Keys.V && e.Control)
				e.Handled = false;
			else
				return;

			TextBox text = (TextBox)sender;
			DataGridTextBoxColumn col = text.Tag as DataGridTextBoxColumn;

			if(col == null)
				return;

			Paste(col.DataGridTableStyle);

		}

		/// <summary>
		/// Paste data from Clipboard to DataGridTableStyle
		/// </summary>
		/// <param name="grdStyle"></param>
		/// <remarks>
		/// Author:			Nguyen Minh Duc. G3.
		/// Created date:	14/05/2006
		/// </remarks>
		public static void Paste(DataGridTableStyle grdStyle)
		{
			if( grdStyle == null || grdStyle.ReadOnly)
				return;

			DataView view = null;
			object datasoucre = grdStyle.DataGrid.DataSource;
			if(datasoucre.GetType() == typeof(DataTable))
				view = ((DataTable)datasoucre).DefaultView;
			else if(datasoucre.GetType() == typeof(DataView))
				view = ((DataView)datasoucre);
			else
				return;

			GridColumnStylesCollection cols = grdStyle.GridColumnStyles;
			if(grdStyle.DataGrid.ReadOnly)
				return;
			DataGridCell grdcell = grdStyle.DataGrid.CurrentCell;

			int startCol = grdcell.ColumnNumber;
			if(startCol < 0)
				return;
			int endCol = grdStyle.GridColumnStyles.Count - 1;

			int startRow = grdcell.RowNumber;

			char tab = '\t';

				
			if(!tempText.Multiline)
				tempText.Multiline = true;
			tempText.Clear();
			tempText.Paste();
			string value = tempText.Text;
			tempText.Clear();

			value = value.Replace("\r\n", "\n");
			if(value.EndsWith("\n"))
				value = value.Substring(0, value.Length - 1);

			string []lines = value.Split('\n');

			int minRow = view.Count - startRow;;

			grdStyle.DataGrid.BeginInit();
			view.Table.BeginInit();

			if(minRow > lines.Length)
				minRow = lines.Length;
			for(int i = 0; i < minRow; i ++)
			{
				string []cells = lines[i].Split(tab);
				int minCol = endCol - startCol + 1;
				if(minCol > cells.Length)
					minCol = cells.Length;

				for(int j = 0; j < minCol; j ++)
				{
					Type grdColType = cols[j + startCol].GetType();
                    if (!cols[j + startCol].ReadOnly && (grdColType == typeof(DataGridTextBoxColumn) || grdColType == typeof(System.Windows.Forms.DataGridBoolColumn) || grdColType == typeof(DataGridTextAutoHilight)))
					{
						try
						{
							DataColumn dcol = view.Table.Columns[cols[j + startCol].MappingName];
							if(dcol == null)
								continue;
							Type type = dcol.DataType;
							System.Windows.Forms.DataGridBoolColumn blnCol = cols[j + startCol] as System.Windows.Forms.DataGridBoolColumn;
							if(type == typeof(string))
							{
								if(blnCol == null)
								{
									view[i + startRow][cols[j + startCol].MappingName] = cells[j];
								}
								else
								{
									if(cells[j].Equals(blnCol.TrueValue) || cells[j].Equals(blnCol.FalseValue))
									{
										view[i + startRow][cols[j + startCol].MappingName] = cells[j];
									}
								}
							}
							else
							{
								if(cells[j].Length == 0)
								{
									if(dcol.AllowDBNull)
										view[i + startRow][cols[j + startCol].MappingName] = DBNull.Value;

								}
								else
								{
									object obj = Convert.ChangeType(cells[j], view.Table.Columns[cols[j + startCol].MappingName].DataType);
									if(blnCol == null)
									{
										view[i + startRow][cols[j + startCol].MappingName] = obj;
									}
									else
									{
										if(obj != null && (obj.Equals(blnCol.TrueValue) || obj.Equals(blnCol.FalseValue)))
										{
											view[i + startRow][cols[j + startCol].MappingName] = obj;
										}
									}
								}
							}
						}
						catch{}
					}
				}
			}

			view.Table.EndInit();
			grdStyle.DataGrid.EndInit();
			grdStyle.DataGrid.Refresh();
		}
		#endregion Copy Paste From Excel Trick
	}
}

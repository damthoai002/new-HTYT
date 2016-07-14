using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace UKPI.Controls
{
	/// <summary>
	/*
	* Creater by : Nguyen Thanh Lap G 3
	* Creater day: 01 - 08 - 2008
	*/
	/// </summary>
	

	public class DataGridRadioColumn  : DataGridBoolColumn
	{
		//public event DataGridSourceChangeEventHandler SourceChange;
		private Bitmap _RadioNoChecked;
		private Bitmap _RadioChecked;
		private ArrayList _arrGroupColumn;
		public DataGridRadioColumn()
		{
			try
			{
				_arrGroupColumn = new ArrayList();
				System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(DataGridRadioColumn));
				this._RadioChecked  = new Bitmap((((System.Drawing.Image)(resources.GetObject("RadioChecked.Image")))));
				this._RadioNoChecked  = new Bitmap((((System.Drawing.Image)(resources.GetObject("RadioNoChecked.Image")))));
			}
			catch(Exception ex)
			{
				MessageBox.Show(ex.Message);
			}
			
		}
		protected override void Edit(System.Windows.Forms.CurrencyManager source, int rowNum, System.Drawing.Rectangle bounds, bool readOnly, string instantText, bool cellIsVisible) 
		{ 
			// dont call the baseclass so no editing done...
			//	base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible); 
		} 
		//public void HandleMouseDown(object sender, MouseEventArgs e)
//		public void MouseDown(MouseEventArgs e)
//		{
//			try
//			{
//				object valueCell;
//				DataGrid dg = this.DataGridTableStyle.DataGrid;
//				DataGrid.HitTestInfo hti = dg.HitTest(new Point(e.X, e.Y));
//				bool isClickInCell = ( hti.Row > -1 && hti.Column > -1 && this.DataGridTableStyle.GridColumnStyles[hti.Column ].MappingName == this.MappingName);
//				if(isClickInCell)
//				{
//					DataTable dtSource = this.DataGridTableStyle.DataGrid.DataSource as DataTable;
//					valueCell = dtSource.Rows[hti.Row][hti.Column] == this.FalseValue?this.TrueValue : this.FalseValue ; 
//					dtSource.Rows[hti.Row][hti.Column] = valueCell; 
//					dtSource.AcceptChanges();
//					if(_arrGroupColumn != null)
//					{
//						foreach(string MappingNameGr in _arrGroupColumn)
//						{
//							dtSource.Rows[hti.Row][MappingNameGr] = valueCell == this.FalseValue? ((DataGridRadioColumn)this.DataGridTableStyle.GridColumnStyles[MappingNameGr]).TrueValue : ((DataGridRadioColumn)this.DataGridTableStyle.GridColumnStyles[MappingNameGr]).FalseValue ;
//						}
//					}
//					dtSource.AcceptChanges();
//					dg.Refresh();
//				} 
//			}
//			catch(Exception ex)
//			{
//				MessageBox.Show("DataGridRadioColumn:HandleMouseDown ->" + ex.Message);  
//			}
//			
//		}
		protected override void Paint(System.Drawing.Graphics g, System.Drawing.Rectangle bounds, System.Windows.Forms.CurrencyManager source, int rowNum, System.Drawing.Brush backBrush, System.Drawing.Brush foreBrush, bool alignToRight)
		{
			base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
			try
			{
				Bitmap bm = this._RadioNoChecked;
				object gridSource = this.DataGridTableStyle.DataGrid.DataSource ;
				if(gridSource == null  )
				{}
				else if(gridSource.GetType().Name.Equals("DataTable"))
				{
					bm = (gridSource as DataTable).Rows[rowNum][this.MappingName].ToString() == this.FalseValue.ToString() ? this._RadioNoChecked : this._RadioChecked ;
					
				}
				else if(gridSource.GetType().Name.Equals("DataView"))
				{
					bm = (gridSource as DataView).Table.Rows[rowNum][this.MappingName].ToString() == this.FalseValue.ToString() ? this._RadioNoChecked : this._RadioChecked ;
				}
				g.DrawImage(bm, bounds, 0, 0, bm.Width, bm.Height,GraphicsUnit.Pixel);
			}
			catch(Exception ex)
			{
				MessageBox.Show("DataGridRadioColumn:Paint ->" + ex.Message);  
			}
		}
//		public  bool DeleteGroupColumn(string MappingName)
//		{
//			bool result = false;
//			try
//			{
//				if(_arrGroupColumn.Contains(MappingName))
//				{
//					_arrGroupColumn.Remove(MappingName);
//					result = true;
//				}
//				foreach(string MappingNameGB in _arrGroupColumn)
//				{
//					((DataGridRadioColumn)this.DataGridTableStyle.GridColumnStyles[MappingNameGB]).SetGroupArray(_arrGroupColumn);
//				}
//			}
//			catch(Exception ex)
//			{
//				MessageBox.Show("DataGridRadioColumn:DeleteGroupColumn ->" + ex.Message);  
//			}
//			return result;
//		}
//		public bool SetGroupColumn(string MappingName)	
//		{
//			bool result = false;
//			try
//			{
//				foreach(DataGridColumnStyle grColumnStyle in this.DataGridTableStyle.GridColumnStyles)
//				{
//					if(grColumnStyle.MappingName == MappingName && grColumnStyle.GetType() == typeof(DataGridRadioColumn))
//					{
//						if(	!_arrGroupColumn.Contains(MappingName))
//							_arrGroupColumn.Add(MappingName);
//						result = true;
//						break;
//					}
//				}
//				_arrGroupColumn.Add(this.MappingName);
//				foreach(string MappingNameGB in _arrGroupColumn)
//				{
//					((DataGridRadioColumn)this.DataGridTableStyle.GridColumnStyles[MappingNameGB]).SetGroupArray(_arrGroupColumn);
//				}
//				_arrGroupColumn.Remove(this.MappingName);
//			}
//			catch(Exception ex)
//			{
//				MessageBox.Show("DataGridRadioColumn:SetGroupColumn ->" + ex.Message);  
//			}
//			return result;
//		}
//		public void RemoveAllGroupColumn()	
//		{
//			
//			_arrGroupColumn.Clear();
//			foreach(string MappingNameGB in _arrGroupColumn)
//			{
//				((DataGridRadioColumn)this.DataGridTableStyle.GridColumnStyles[MappingNameGB]).RemoveAllGroupColumn();
//			}
//		}
//		protected internal void SetGroupArray(ArrayList arr)	
//		{
//			//Bien tap lai ham nay
//			_arrGroupColumn = (ArrayList)arr.Clone() ;
//			if(_arrGroupColumn.Contains(this.MappingName))
//				_arrGroupColumn.Remove(this.MappingName);
//		}
	}
//	public delegate void DataGridSourceChangeEventHandler(object sender, DataGridSourceChangeEventArgs e);
//	public class DataGridSourceChangeEventArgs : EventArgs
//	{
//		private int _row;
//		private int _col;
//		private bool _isNew;
//
//		public DataGridSourceChangeEventArgs(int row, int col, bool isNew)
//		{
//			_row = row;
//			_col = col;
//			_isNew = isNew;
//		}
//		public int RowIndex	{get{return _row;}}
//		public int ColIndex	{get{return _col;}}
//		public bool isNew	{get{return _isNew;}}
//	}


}

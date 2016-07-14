using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;

namespace UKPI.Controls
{
	/*
	 * Creater by : Nguyen Thanh Lap G 3
	 * Creater day: 05 - 08 - 2008
	 */
	public delegate void DataGridCheckErrorCellHandler(object sender, DataGridCheckErrorCellEventArgs e);
	public delegate void DataGridSetDataSourceCellHandler(object sender, DataGridSetDataSourceCellEventArgs e);

	#region - DataGridSetFormatCellEventArgs -
	public class DataGridSetDataSourceCellEventArgs : EventArgs
	{
		private int _column;
		private int _row;
		private object _dataSource;
		private string _ValueMember;
		private string _DisplayMember;
		private string _SelectedValue;
		public DataGridSetDataSourceCellEventArgs(int row, int col)
		{
			_row = row;
			_column = col;
			_dataSource = null;
			_DisplayMember = string.Empty;
			_ValueMember = string.Empty;
		}
		public int Column
		{
			get {return _column;}
		}
		public int Row
		{
			get {return _row;}
		}
		public object DataSource
		{
			get {return _dataSource;}
			set {_dataSource = value;}
		}
		public string ValueMember
		{
			get {return _ValueMember;}
			set {_ValueMember = value;}
		}
		public string DisplayMember
		{
			get {return _DisplayMember;}
			set {_DisplayMember = value;}
		}
		public string SelectedValue
		{
			get {return _SelectedValue;}
			set {_SelectedValue = value;}
		}
	}
	#endregion - DataGridSetFormatCellEventArgs -
    #region - DataGridCheckErrorCellEventArgs -
	public class DataGridCheckErrorCellEventArgs : EventArgs
	{
		private object _value;
		private int _row;
		private bool _CellError;
		private string _errorMsg;
		public DataGridCheckErrorCellEventArgs(int Row, object Value,bool CellError)
		{
			_row = Row;
			_value = Value;
			_CellError = CellError;
			ErrorMessage = "";
		}
		public object Value
		{
			get {return _value;}
			set {_value = value;}
		}
		public int Row
		{
			get {return _row;}
			set {_row = value;}
		}
		public bool CellError
		{
			get {return _CellError;}
			set {_CellError = value;}
		}
		public string ErrorMessage
		{
			get {return _errorMsg;}
			set {_errorMsg = value;}
		}
	}
	#endregion - DataGridCheckErrorCellEventArgs -
	public class DataGridTextAutoHilight : DataGridTextBoxColumn
	{
		public event DataGridSetDataSourceCellHandler DataGridSetDataSourceCell;
		public event DataGridCheckErrorCellHandler DataGridCheckErrorCell;
		private int _col;
		private Hashtable _HasError;
		private Hashtable _HasNoInput;
		private Brush _errorBackBrush;
		private Brush _normalBackBrush;
		private CurrencyManager _cmanager;
		private int iCurrentRowEdit = -1;
		public ComboBox _comboBox ;
		#region -initiation-
		public DataGridTextAutoHilight(Brush ErrorBackBrush,Brush _normalBackBrush,bool isCombobox)
		{
			_HasError = new Hashtable();
			_HasNoInput = new Hashtable();
			this._errorBackBrush = ErrorBackBrush;
			this._normalBackBrush = _normalBackBrush;
			this._col = -1;
			if(isCombobox)
			{
				this._comboBox = new ComboBox();
				this._comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
				this._comboBox.Leave += new EventHandler(comboBox_Leave);
			}
			else
				this._comboBox = null;
		}
		public DataGridTextAutoHilight(bool isCombobox)
		{
			_HasError = new Hashtable();
			_HasNoInput = new Hashtable();
			_errorBackBrush = null;
			_normalBackBrush = null;
			this._col = -1;
			if(isCombobox)
			{
				this._comboBox = new ComboBox();
				this._comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
				this._comboBox.Leave += new EventHandler(comboBox_Leave);
			}
			else
				this._comboBox = null;
		}
		#endregion --initiation-
		#region -override base class-
		protected override void Paint(
			System.Drawing.Graphics g,
			System.Drawing.Rectangle bounds,
			System.Windows.Forms.CurrencyManager source,
			int rowNum,
			System.Drawing.Brush backBrush,
			System.Drawing.Brush foreBrush,
			bool alignToRight)
		{
			string strValue = string.Empty;
			if(_HasError.ContainsKey(rowNum))
			{
				backBrush = _errorBackBrush != null?_errorBackBrush : backBrush ;
			}
			else
			{
				if(_HasNoInput.Count > 0 && _HasNoInput.ContainsKey(rowNum)
					&& _HasNoInput.ContainsKey(rowNum)? (bool)_HasNoInput[rowNum] : false)
				{
					// add color if you want for cell read only
				}
				else
				{
					backBrush = _normalBackBrush != null?_normalBackBrush : backBrush;//Brushes.Moccasin;
				}
			}
			base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
		}
		protected override void Edit(
			System.Windows.Forms.CurrencyManager source,
			int rowNum,
			System.Drawing.Rectangle bounds,
			bool readOnly,
			string instantText,
			bool cellIsVisible)
		{		
			DataGridSetDataSourceCellEventArgs e = null;
			if (DataGridSetDataSourceCell != null)
			{
				e = new DataGridSetDataSourceCellEventArgs(rowNum, colNum);	
				DataGridSetDataSourceCell(this, e);
			}
			base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);
			if(_comboBox != null && DataGridTableStyle.DataGrid.CurrentCell.ColumnNumber == colNum && e.DataSource != null)
			{
				iCurrentRowEdit = rowNum;
				_cmanager = source;
				DataGridTableStyle.DataGrid.Scroll += new EventHandler(DataGrid_Scroll);
				_comboBox.Parent = TextBox.Parent;
				Rectangle rect = DataGridTableStyle.DataGrid.GetCurrentCellBounds();
				_comboBox.Location = rect.Location;
				_comboBox.Size = new Size(TextBox.Size.Width + 4, _comboBox.Size.Height);
				_comboBox.DataSource = e.DataSource;
				_comboBox.DisplayMember = e.DisplayMember ;
				_comboBox.ValueMember = e.ValueMember;
				if(e.SelectedValue != null)
					_comboBox.SelectedValue = e.SelectedValue;
				_comboBox.Show();
				_comboBox.BringToFront();
				_comboBox.Focus();
			}
		}
		protected  override void SetColumnValueAtRow(CurrencyManager source, int rowNum, object value)
		{
			int intCount;
			DataGridCheckErrorCellEventArgs e = null;
			if(DataGridCheckErrorCell != null)
			{
				e = new DataGridCheckErrorCellEventArgs(rowNum,value,_HasError.ContainsKey(rowNum));
				DataGridCheckErrorCell(this,e);
				if(e.CellError)
					setErrorCell(rowNum,e.ErrorMessage);
				else
					clearErrorCell(rowNum);
			}
			intCount = (DataGridTableStyle.DataGrid.DataSource as DataTable).Rows.Count;
			if(intCount < rowNum + 1)
			{
				DataRow dr = (DataGridTableStyle.DataGrid.DataSource as DataTable).NewRow();
				(DataGridTableStyle.DataGrid.DataSource as DataTable).Rows.Add(dr); 
			}
			 
			base.SetColumnValueAtRow (source, rowNum, e != null? e.Value : value);
			//base.SetColumnValueAtRow (source, rowNum, value);
		}
		#endregion -override base class-
		#region - genaral function - 
		public void setErrorCell(int indexError,string ErrorMsg)
		{
			if(ErrorMsg != null && ErrorMsg != "")
			{
				if(_HasError.ContainsKey(indexError))
				{
					_HasError.Remove(indexError);
				}
				_HasError.Add(indexError,ErrorMsg);
			}
		}
		public void SetStateRow(int Row, bool ReadOnly)
		{
			if(_HasNoInput.ContainsKey(Row))
			{
				_HasNoInput.Remove(Row);
			}
			_HasNoInput.Add(Row,ReadOnly); 
		}
		public bool isReadOnly(int Row)
		{
			bool blnReadOnly = false;
			if(_HasNoInput.ContainsKey(Row))
			{
				blnReadOnly = (bool)_HasNoInput[Row];
			}
			return blnReadOnly;
		}
		public void ClearAllStateRow()
		{
			_HasNoInput.Clear(); 
		}
		public void clearErrorCell(int indexError)
		{
			if(_HasError.ContainsKey(indexError))
				_HasError.Remove(indexError);
		}
		public void clearAllErrorCell()
		{
			_HasError.Clear(); 
		}
		public void SetCoboboxDataSource(object DataSource,string ValueMember,string DisplayMember)
		{
			if(DataSource!= null) _comboBox.DataSource = DataSource;
			if(ValueMember!= "") _comboBox.ValueMember = ValueMember;
			if(DisplayMember!= "") _comboBox.DisplayMember = DisplayMember;
		}
		public bool isCellError(int row)
		{
			return _HasError.ContainsKey(row);
		}
		public void DeleteCell(int Row)
		{
			// remove and re-range prior error
			Hashtable _Tmp = new Hashtable();
			Hashtable _Tmp1 = new Hashtable();
			foreach(int i in _HasError.Keys)
			{
				if( i > Row)
				{
					_Tmp.Add(i-1,_HasError[i]);
				}
				else if(i < Row)
				{
					_Tmp.Add(i,_HasError[i]);
				}
			}
			_HasError.Clear();
			_HasError = _Tmp;
			// remove and re-range state of cell
			foreach(int i in _HasNoInput.Keys)
			{
				if( i > Row)
				{
					_Tmp1.Add(i-1,_HasNoInput[i]);
				}
				else if(i < Row)
				{
					_Tmp1.Add(i,_HasNoInput[i]);
				}
			}
			_HasNoInput.Clear();
			_HasNoInput = _Tmp1;
		}
		public string GetMessageError(int Row)
		{
			string strMsg = string.Empty;
			if(_HasError.ContainsKey(Row))
				strMsg = _HasError[Row].ToString();
			return strMsg;
		}
		#endregion - genaral function - 
		#region - Property - 
		public bool isError
		{
			get
			{
				if(_HasError.Count > 0)
					return true;
				else
					return false;
			}
		}
		public int colNum
		{
			get
			{
				if(_col == -1)
				{
					foreach(DataGridColumnStyle colStyle in DataGridTableStyle.GridColumnStyles)
					{
						if(colStyle.MappingName == MappingName)
							_col = DataGridTableStyle.GridColumnStyles.IndexOf(colStyle);
					}
				}
				return _col;
			}
		}
		#endregion - Property - 
		#region - Event -
		private void DataGrid_Scroll(object sender, EventArgs e)
		{
			this._comboBox.Hide();
		}
		private void comboBox_Leave(object sender, EventArgs e)
		{
			try
			{ 
				SetColumnValueAtRow(_cmanager, iCurrentRowEdit, _comboBox.SelectedValue);
				this._comboBox.Hide();
				this.DataGridTableStyle.DataGrid.Scroll -= new EventHandler(DataGrid_Scroll);   
				Invalidate();
			}
			catch
			{
				this._comboBox.Hide();
			}
		}
		#endregion - Event -

		#region - Is Numeric -
		private bool _isNumericOnly;
		[Browsable(true), DefaultValue(false)]
		public bool IsNumericOnly
		{
			get{return _isNumericOnly;}
			set{_isNumericOnly = value;}
		}

		public DataGridTextAutoHilight()
		{
			this.TextBox.KeyPress += new KeyPressEventHandler(NumericTextBox_KeyPress);
		}

		private void NumericTextBox_KeyPress(object sender, KeyPressEventArgs e)
		{
			if(IsNumericOnly)
			{
				char chr = e.KeyChar;
				if (!(chr >= '0' && chr <= '9' || chr == 8 || chr == 13))
					e.Handled = true;
			}
		}
		#endregion - End Is Numeric - 
	}
}

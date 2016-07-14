using System;
using System.ComponentModel;
using System.Collections;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace UKPI.Controls
{
	public delegate void DataGridDisableCellHandler(object sender, DataGridDisableCellEventArgs e);
	#region - DataGridDisableCell -
	// ====================== Our Publisher ================================= 
	// This Class is our second Paramter for our Event, it must derived
	// from EventArgs. We publish the Properties: Column, Row and EnableValue
	// to our Subscribers.
	public class DataGridDisableCellEventArgs : EventArgs
	{
		private int _column;
		private int _row;
		private bool _enablevalue = true;

		// Constructor, set private values row, col.
		public DataGridDisableCellEventArgs(int row, int col)
		{
			_row = row;
			_column = col;
		}

		// Published Property Column
		public int Column
		{
			get {return _column;}
			set {_column = value;}
		}

		// Published Property Row
		public int Row
		{
			get {return _row;}
			set {_row = value;}
		}

		// Published Property EnableValue
		public bool EnableValue
		{
			get {return _enablevalue;}
			set {_enablevalue = value;}
		}
	}

	// This is our own, customized DataGrid TextBox Column. Here
	// we draw the Cell Background and disable / enable the Cell
	// according to the EnableValue in the defined Event.
	public class DataGridDisableTextBox : DataGridTextBoxColumn
	{
		// Declare the Event for the defined Delegate.
		public event DataGridDisableCellHandler DataGridDisableCell;
		
		// Save the column number
		private int _col;

		// Our own Constructor, which must NOT conform the Constructor
		// in the Base Class (Constructors are not derived)
		public DataGridDisableTextBox(int column)
		{
			_col = column;
		}

		// Here is the trick for the Background / Foreground Color
		// of the Cell - override the Paint method, with our
		// own functionality.
		protected override void Paint(
			System.Drawing.Graphics g,
			System.Drawing.Rectangle bounds,
			System.Windows.Forms.CurrencyManager source,
			int rowNum,  // Here is the Row Number
			System.Drawing.Brush backBrush,
			System.Drawing.Brush foreBrush,
			bool alignToRight)
		{
			// Do we have Subscribers - notify them if we have
			if (DataGridDisableCell != null)
			{
				// Initialize our Event with the current Row and Column Number
				DataGridDisableCellEventArgs e = new DataGridDisableCellEventArgs(rowNum, _col);
								
				// Notify Subscribers to call their EventHandlers - where they
				// can do whatever they want. After this we check the EnableValue
				// Flag, which may be set / unset by a Subscriber.
				DataGridDisableCell(this, e);

				// Set the Foreground / Back Color according to our Subscribers
				if (e.EnableValue) 
				{
					backBrush = Brushes.Moccasin;
					foreBrush = Brushes.DarkBlue;
				}
			}
			
			// In any case (enabled or disabled) draw the Column using the Base Method
			base.Paint(g, bounds, source, rowNum, backBrush, foreBrush, alignToRight);
		}

		// Here is the trick to emable / disable the TextBox
		// of the Cell - override the Edit method, with our
		// own functionality.
		protected override void Edit(
			System.Windows.Forms.CurrencyManager source,
			int rowNum,
			System.Drawing.Rectangle bounds,
			bool readOnly,
			string instantText,
			bool cellIsVisible)
		{		
			// Do we have Subscribers - notify them if we have
			if (DataGridDisableCell != null)
			{
				// Initialize our Event with the current Row and Column Number
				DataGridDisableCellEventArgs e = new DataGridDisableCellEventArgs(rowNum, _col);

				// Notify Subscribers to call their EventHandlers - where they
				// can do whatever they want. After this we check the EnableValue
				// Flag, which may be set / unset by a Subscriber.			
				DataGridDisableCell(this, e);

				readOnly = !e.EnableValue;
			}

			// Only call the Edit Method (which enables the TextBox in the DataGrid)
			// when the Enable Flag has been set by the Subscriber
			base.Edit(source, rowNum, bounds, readOnly, instantText, cellIsVisible);
		}
	}
	#endregion
}
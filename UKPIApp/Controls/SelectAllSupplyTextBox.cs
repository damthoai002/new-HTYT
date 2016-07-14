using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;

namespace UKPI.Controls
{
    public class SelectAllSupplyTextBox : TextBox
    {
        protected override void OnKeyDown(System.Windows.Forms.KeyEventArgs e)
        {
            if (e.Control && (e.KeyCode == System.Windows.Forms.Keys.A))
            {
                this.SelectAll();
                e.SuppressKeyPress = true;
                e.Handled = true;
            }
            else
                base.OnKeyDown(e);
        }

    }
}

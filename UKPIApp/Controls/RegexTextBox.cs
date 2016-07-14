using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace UKPI.Controls
{
    public class RegexTextBox : TextBox
    {
        private string regexPattern = string.Empty;
        public string RegexPattern
        {
            get
            {
                return regexPattern;
            }
            set { regexPattern = value; }
        }

        protected override void OnKeyPress(KeyPressEventArgs e)
        {
            base.OnKeyPress(e);

            System.Globalization.NumberFormatInfo numberFormatInfo = System.Globalization.CultureInfo.CurrentCulture.NumberFormat;
            string decimalSeparator = numberFormatInfo.NumberDecimalSeparator;
            string groupSeparator = numberFormatInfo.NumberGroupSeparator;
            string negativeSign = numberFormatInfo.NegativeSign;

            string keyInput = e.KeyChar.ToString();
            Regex regex = new Regex(regexPattern);
            string tmpValue = this.Text.Remove(this.SelectionStart, this.SelectionLength).Insert(this.SelectionStart, keyInput);
            if (e.KeyChar != '\b' && !regex.IsMatch(tmpValue))
            {
                // Consume this invalid key and beep
                e.Handled = true;
                //    MessageBeep();
            }
        }

        public int IntValue
        {
            get
            {
                int result;

                if (int.TryParse(this.Text, out result))
                {
                    return result;
                }

                return 0;
            }
        }

        public decimal DecimalValue
        {
            get
            {
                Decimal result;

                if (Decimal.TryParse(this.Text, out result))
                {
                    return result;
                }

                return 0;
            }
        }
    }
}

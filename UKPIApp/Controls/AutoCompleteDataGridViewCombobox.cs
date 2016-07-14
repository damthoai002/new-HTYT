using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace UKPI.Controls
{
    public partial class AutoCompleteDataGridViewCombobox : DataGridViewComboBoxColumn
    {
        public event System.ComponentModel.CancelEventHandler NotInList;

        private bool _limitToList = true;
        private bool _inEditMode = false;

        public AutoCompleteDataGridViewCombobox()
        {
            InitializeComponent();
        }

        public AutoCompleteDataGridViewCombobox(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }

        [Category("Behavior")]
        public bool LimitToList
        {
            get { return _limitToList; }
            set { _limitToList = value; }
        }

        protected virtual void
            OnNotInList(System.ComponentModel.CancelEventArgs e)
        {
            if (NotInList != null)
            {
                NotInList(this, e);
            }
        }

       


    }
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.IO;
using System.Collections;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;
using Excel;
using FPT.Component.ExcelPlus;
using log4net.Util;
using UKPI.BusinessObject;
using UKPI.Presentation.ApproveTSLookup;
using UKPI.ValueObject;
using UKPI.Utils;
using UKPI.DataAccessObject;
using DataTable = System.Data.DataTable;

namespace UKPI.Presentation
{
    public partial class FrmArchiveDataFromProwatch : Form
    {
        #region Private fields

        private static readonly log4net.ILog Log = log4net.LogManager.GetLogger(typeof(FrmArchiveDataFromProwatch));

        private readonly clsCommon _common = new clsCommon();

        #endregion

        #region Constructors

        public FrmArchiveDataFromProwatch()
        {

            InitializeComponent();
            clsTitleManager.InitTitle(this);

        }

 

        #endregion

    





    }
}
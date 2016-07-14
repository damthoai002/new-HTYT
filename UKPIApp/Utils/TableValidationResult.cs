using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace UKPI.Utils
{
    public class TableValidationResult
    {
        public const string DEFAULT_ERROR_COLUMN = "ValidationResult";

        public string ResultColumn { get; set; }
        public DataTable Table { get; set; }
        public bool IsSuccess { get; set; }

        public TableValidationResult()
        {
            Table = null;
            IsSuccess = false;
            ResultColumn = DEFAULT_ERROR_COLUMN;
        }
    }
}

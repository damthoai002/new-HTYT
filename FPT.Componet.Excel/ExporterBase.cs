using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPT.Component.ExcelPlus
{
    /// <summary>
    /// Please inherit from Exporter insteads.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public abstract class ExporterBase<T>
        where T : IErrorObject
    {
        public const string DEFAULT_TABLE_NAME = "Export";
        #region Properties
        protected Dictionary<string, System.Data.DataTable> currentTables;
        protected string currentFile;
        #endregion Properties

        #region Abstract Methods
        protected abstract IResult<T> TransformTable();
        protected abstract IResult<T> CreateHeading();
        protected abstract IResult<T> RenderStyle();
        protected abstract IResult<T> WriteToWorkBook();
        protected abstract IResult<T> SaveAndClose();
        #endregion Abstract Methods
        public ExporterBase()
        {
        }

        public IResult<T> Export(IList<System.Data.DataTable> data, string filePath)
        {
            IResult<T> result = new ResultBase<T>();
            currentTables = new Dictionary<string, System.Data.DataTable>();
            currentFile = filePath;

            foreach (var item in data)
            {
                if (string.IsNullOrEmpty(item.TableName))
                {
                    item.TableName = DEFAULT_TABLE_NAME;
                }
                if (currentTables.ContainsKey(item.TableName))
                {
                    currentTables[item.TableName] = item;
                }
                else
                {
                    currentTables.Add(item.TableName, item);
                }
            }

            IResult<T> prepare = PrepareTable();
            result.Update(prepare);
            if (result.ForceStop && !result.IsSuccess)
            {
                return result;
            }

            IResult<T> write = WriteToWorkBook();
            result.Update(write);
            if (result.ForceStop && !result.IsSuccess)
            {
                return result;
            }

            IResult<T> style = RenderStyle();
            result.Update(style);
            if (result.ForceStop && !result.IsSuccess)
            {
                return result;
            }

            IResult<T> save = SaveAndClose();
            result.Update(save);

            return result;
        }



        protected virtual IResult<T> PrepareTable()
        {
            IResult<T> result = TransformTable();

            if (result.ForceStop && !result.IsSuccess)
            {
                return result;
            }

            IResult<T> heading = CreateHeading();
            result.Update(heading);

            return result;
        }
    }
}

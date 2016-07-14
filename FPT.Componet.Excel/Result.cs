using System.Collections.Generic;

namespace FPT.Component.ExcelPlus
{
    public class ResultBase<T> : IResult<T>
        where T : IErrorObject
    {
        public List<T> Errors { get; set; }

        public bool ForceStop { get; set; }

        public bool IsSuccess { get { return Errors.Count <= 0; } }

        public ResultBase()
        {
            Errors = new List<T>();
            ForceStop = false;
        }

        public ResultBase(T arg)
        {
            Errors = new List<T>();
            Errors.Add(arg);
            ForceStop = false;
        }

        public void Update(IResult<T> other)
        {
            if (!other.IsSuccess)
            {
                Errors.AddRange(other.Errors.ToArray());
            }
            ForceStop = ForceStop || other.ForceStop;
        }

        public void AddException(System.Exception ex)
        {
            T obj = System.Activator.CreateInstance<T>();
            obj.CopyFrom(ex);
            Errors.Add(obj);
        }

    }
}

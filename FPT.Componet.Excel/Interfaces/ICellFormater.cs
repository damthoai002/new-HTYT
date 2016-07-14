using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FPT.Component.ExcelPlus.Interfaces
{
    public interface IRangeFormater<T>
        where T : IErrorObject
    {
        IResult<T> Merge(FCell fromCell, FCell toCell);
    }
}

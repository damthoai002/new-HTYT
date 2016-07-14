
namespace FPT.Component.ExcelPlus
{
    public interface IErrorResult<T>
        where T : IErrorObject
    {
        IResult<T> GetError();
    }
}

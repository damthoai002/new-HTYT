
namespace FPT.Component.ExcelPlus
{
    public interface IRange
    {
        string this[int row, int column] { get; set; }
        string this[int row, string column] { get; set; }
        string this[string cellAddress] { get; set; }
        int EndRow { get; }
        int EndColumn { get; }
        int StartRow { get; }
        int StartColumn { get; }
    }
}

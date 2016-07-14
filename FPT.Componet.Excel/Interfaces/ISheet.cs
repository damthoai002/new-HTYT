
namespace FPT.Component.ExcelPlus
{
    public interface ISheet
    {
        IRange Cells { get; set; }

        int SheetNumber { get; set; }
        string SheetName { get; set; }
    }
}

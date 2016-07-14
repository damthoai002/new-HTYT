
namespace FPT.Component.ExcelPlus
{
    public delegate ResultState WorkBookHandler(object sender, IWorkbook workbook) ;
    public delegate ResultState WorkSheetHandler(object sender, ISheet sheet);
    public delegate void SheetNameHandler(object sender, string sheetName);

}

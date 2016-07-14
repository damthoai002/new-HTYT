
namespace FPT.Component.ExcelPlus
{
    public class ExcelFactory
    {
        public static IExcelReader CreateExcelReader(ExcelVersion version, bool useCOM)
        {
            IExcelReader result = null;
            if (useCOM)
            {
                result = new ExcelCOMReader();
            }
            else
            {
                switch (version)
                {
                    case ExcelVersion.Excel2003:
                        result = new NpoiReader();
                        break;
                    case ExcelVersion.Excel2007:
                        result = new EpplusReader();
                        break;
                    default:
                        break;
                }
            }
            return result;
        }

        public static IExcelWriter CreateExcelWriter(ExcelVersion version)
        {
            IExcelWriter result = null;
            switch (version)
            {
                case ExcelVersion.Excel2003:
                    result = new NpoiWriter();
                    break;
                case ExcelVersion.Excel2007:
                    result = new EpplusWriter();
                    break;
                default:
                    break;
            }
            return result;
        }
    }
}

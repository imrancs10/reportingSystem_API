using System.Data;

namespace ReportingSystem.API.Utility
{
    public interface IExcelReader
    {
        DataTable ReadExcelasDataTable(string excelFilePath);
    }
}

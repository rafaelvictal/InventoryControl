using InventoryControl.Core.DTO;

namespace InventoryControl.Core.Results;

public class ReportResult
{
    public bool IsSucess { get; set; }
    public string Message { get; set; }
    public List<StockReportDTO> StockReports { get; set; }

    public ReportResult(bool isSuccess, List<StockReportDTO> stockReports, string message)
    {
        IsSucess = isSuccess;
        StockReports = stockReports;
        Message = message;
    }

    public static ReportResult Sucess(List<StockReportDTO> stockReport) =>
        new ReportResult(true, stockReport, "Report generated succesfully.");

    public static ReportResult Fail(string message) =>
        new ReportResult(false, new List<StockReportDTO>(), message);

}

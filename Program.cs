using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Storage;
using System;
namespace IntelligencePipeline;
class Program
{
    private static void DisplayReport(Report report) { }

    private static void DisplayValidatedReports(ReportRepository repository) { }

    private static void DisplayRejectedReports(RejectedReportRepository repository) { }
    public static void Main(string[] args) {
        DateTime time = new DateTime(2021,1,1);
        DroneReport Something = new DroneReport(1,time,32.5000,35.0000,"This is only for testing",350,58);
    } 
}
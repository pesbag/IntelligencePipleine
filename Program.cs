using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Storage;
using IntelligencePipeline.Pipeline;
using IntelligencePipeline.Validation;
using IntelligencePipeline.Calculators;

using System;
namespace IntelligencePipeline;
class Program
{
    private static void DisplayReport(Report report) {
        ReportPipeline reportPipeline = new ReportPipeline();
        reportPipeline.DisplayStatistics();
    }

    private static void DisplayValidatedReports(ReportRepository repository) {
        ReportRepository reportPipeline = new ReportRepository();
        reportPipeline.GetAll();
    }

    private static void DisplayRejectedReports(RejectedReportRepository repository) {
        RejectedReportRepository reportPipeline = new RejectedReportRepository();
        reportPipeline.GetAll();
    }
    public static void Main(string[] args) {
        ReportPipeline reportPipeline = new ReportPipeline();
        DateTime time = new DateTime(2021,1,1);
        DroneReport Something = new DroneReport(1,time,32.5000,35.0000,"This is only for testing",350,58);
        reportPipeline.ProcessReport(Something);
        DisplayReport(Something);
    } 
}
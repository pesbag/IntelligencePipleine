using IntelligencePipeline.Calculators;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Pipeline;
using IntelligencePipeline.Storage;
using IntelligencePipeline.Validation;
using System;
namespace IntelligencePipeline;
class Program
{
    private static void DisplayReport(Report report) {
        Console.WriteLine($"--- Report Details ---");
        Console.WriteLine($"ID: {report.ReportId}");
        Console.WriteLine($"Status: {report.Status}");
        Console.WriteLine($"Priority: {report.Priority}");
        Console.WriteLine($"Classification: {report.Classification}");
        Console.WriteLine($"Description: {report.Description}");

        if (report.Status == ReportStatus.Rejected && !string.IsNullOrEmpty(report.RejectionReason))
        {
            Console.WriteLine($"Rejection Reason: {report.RejectionReason}");
        }
    }

    private static void DisplayValidatedReports(ReportRepository repository) {
        List<Report> listOfReport = repository.GetAll();
        for (int i=0; i < listOfReport.Count; i++)
        {
            DisplayReport(listOfReport[i]);
        }
    }

    private static void DisplayRejectedReports(RejectedReportRepository repository) {
        List<Report> listOfRejectedReport = repository.GetAll();
        for (int i = 0; i < listOfRejectedReport.Count; i++)
        {
            DisplayReport(listOfRejectedReport[i]);
        }
    }
    public static void Main(string[] args) {
        //ReportPipeline reportPipeline = new ReportPipeline();
        //DateTime time = new DateTime(2021,1,1);
        //DroneReport Something = new DroneReport(1,time,32.5000,35.0000,"This is only for testing",350,58);
        //reportPipeline.ProcessReport(Something);
        //Console.WriteLine("=== STATISTICS ===");
        //reportPipeline.DisplayStatistics();
        //Console.WriteLine();
        //Console.WriteLine("=== VALIDATED REPORTS ===");
        //DisplayValidatedReports(reportPipeline.GetValidatedReports());
        //Console.WriteLine();
        //Console.WriteLine("=== REJECTED REPORTS ===");
        //DisplayRejectedReports(reportPipeline.GetRejectedReports());

        ReportPipeline reportPipeline = new ReportPipeline();
        DateTime validTime = new DateTime(2026, 7, 2);

        DroneReport droneReject1 = new DroneReport(1, new DateTime(2019, 12, 31), 32.5000, 35.0000, "Valid desc", 500, 80);
        reportPipeline.ProcessReport(droneReject1);

        DroneReport droneReject2 = new DroneReport(2, validTime, 32.5000, 35.0000, "Short", 500, 80);
        reportPipeline.ProcessReport(droneReject2);

        SoldierReport soldierReject = new SoldierReport(3, validTime, 32.5000, 35.0000, "Valid description text here", "David", "12345", "Unit 101", 3);
        reportPipeline.ProcessReport(soldierReject);

        RadarReport radarReject = new RadarReport(4, validTime, 32.5000, 35.0000, "Valid description text here", 2500, 45, 3500);
        reportPipeline.ProcessReport(radarReject);

        SignalReport signalReject=new SignalReport(5, validTime, 32.5000, 35.0000, "Valid description text here", 145.5, "Short", Language.Other, -55);
        reportPipeline.ProcessReport(signalReject);

        DroneReport droneCritical = new DroneReport(6, validTime, 32.5000, 35.0000, "An explosion was detected near the outpost", 4000, 90);
        reportPipeline.ProcessReport(droneCritical);

        RadarReport radarCritical = new RadarReport(7, validTime, 32.5000, 35.0000, "Valid description text here", 750, 180, 5000);
        reportPipeline.ProcessReport(radarCritical);

        SignalReport signalCritical = new SignalReport(8, validTime, 32.5000, 35.0000, "Valid description text here", 1500.0, "attack and target detected", Language.Arabic, -30);
        reportPipeline.ProcessReport(signalCritical);

        DroneReport droneHigh = new DroneReport(9, validTime, 32.5000, 35.0000, "A suspicious movement on the border", 2000, 60);
        reportPipeline.ProcessReport(droneHigh);

        SoldierReport soldierHigh = new SoldierReport(10, validTime, 32.5000, 35.0000, "Enemy movement reported", "Yossi", "1234567", "Golani", 2);
        reportPipeline.ProcessReport(soldierHigh);

        RadarReport radarMedium = new RadarReport(11, validTime, 32.5000, 35.0000, "Valid description text here", 100, 90, 15000);
        reportPipeline.ProcessReport(radarMedium);

        SoldierReport soldierRestricted = new SoldierReport(12, validTime, 32.5000, 35.0000, "Routine patrol report", "Avi", "7654321", "Paratroopers", 5);
        reportPipeline.ProcessReport(soldierRestricted);

        Console.WriteLine("=== SYSTEM STATISTICS ===");
        reportPipeline.DisplayStatistics();
        Console.WriteLine();

        Console.WriteLine("=== VALIDATED REPORTS ===");
        DisplayValidatedReports(reportPipeline.GetValidatedReports());
        Console.WriteLine();

        Console.WriteLine("=== REJECTED REPORTS ===");
        DisplayRejectedReports(reportPipeline.GetRejectedReports());
    } 
}
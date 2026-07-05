using Calculators;
using IntelligencePipeline.Calculators;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Storage;
using IntelligencePipeline.Validation;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Pipeline;

class ReportPipeline
{
    private ReportRepository _validatedReports;
    private RejectedReportRepository _rejectedReports;
    private int _nextReportId;

    public ReportPipeline() {
        _validatedReports = new ReportRepository();
        _rejectedReports = new RejectedReportRepository();
        _nextReportId = 1;
    }

    public void ProcessReport(Report report) {
        if (report == null) return;
        report.Status = ReportStatus.New;
        report.AssignId(_nextReportId);
        _nextReportId+=1;
        
        ValidateReport(report);
    }

    public ReportRepository GetValidatedReports() {
        return _validatedReports;
    }

    public RejectedReportRepository GetRejectedReports() {
        return _rejectedReports;
    }

    public void DisplayStatistics() {
        int validCount = _validatedReports.GetTotalCount();
        int rejectedCount = _rejectedReports.GetTotalCount();
        int total = validCount + rejectedCount;

        Console.WriteLine($"Total processed reports: {total}");
        Console.WriteLine($"Validated reports: {validCount}");
        Console.WriteLine($"Rejected reports: {rejectedCount}");
    }
    private IValidator? GetValidator(Report report) {
        if (report is SoldierReport)
            return new SoldierValidator();
        
        if (report is DroneReport)
            return new DroneValidator();
        
        if (report is RadarReport)
            return new RadarValidator();
        
        if (report is SignalReport)
            return new SignalValidator();
        
        return null;
    }

    private void ValidateReport(Report report) {
        report.Status = ReportStatus.Validating;
        IValidator? validator = GetValidator(report);
        
        if (validator == null) {
            report.Status = ReportStatus.Rejected;
            report.RejectionReason="Error: non valid report type enterd";
            StoreReport(report);
            return;
        }
        
        ValidationResult resultValid = validator.Validate(report);
        if (resultValid.IsValid == true)
        {
            report.Status = ReportStatus.Validated;
            CalculateMetrics(report);
            StoreReport(report);
        }
        else
        {
            report.Status = ReportStatus.Rejected;
            report.RejectionReason = resultValid.ErrorMessage;
            StoreReport(report);
        }
    }

    private void CalculateMetrics(Report report) {
        report.CalculateReliabilityScore();

        ReliabilityCalculator reliabilityObj = new ReliabilityCalculator();
        report.ReliabilityScore = reliabilityObj.Calculate(report);

        PriorityCalculator priorityObj = new PriorityCalculator();
        report.Priority=priorityObj.Calculate(report);

        ClassificationCalculator classificationObj = new ClassificationCalculator();
        report.Classification = classificationObj.Calculate(report);
    }

    private void StoreReport(Report report) {
        if (report.Status == ReportStatus.Validated)
            _validatedReports.Add(report);
        else
            _rejectedReports.Add(report);
    }
}
using System;
using System.Collections.Generic;
using System.Text;
using IntelligencePipeline.Models.Enums;
namespace IntelligencePipeline.Models.Reports;

abstract class Report
{
    private int _reportId;
    private DateTime _timestamp;
    private double _latitude;
    private double _longitude;
    private string _description = string.Empty;
    private ReportStatus _status;
    private Priority _priority;
    private Classification _classification;
    private int _reliabilityScore;
    private string _rejectionReason = string.Empty;

    private int _nextReportId;

    public int NextReportId
    {
        get { return _nextReportId; }
        protected set { _nextReportId += 1; }
    }
    public int ReportId { 
        get { return _reportId; }
        protected set { _reportId = value; }
    }
    public DateTime Timestamp {
        get { return _timestamp; }
        protected set { _timestamp = value; }
    }
    public double Latitude {
        get { return _latitude; }
        protected set { _latitude = value; }
    }
    public double Longitude {
        get { return _longitude; }
        protected set { _longitude = value; }
    }
    public string Description {
        get { return _description; }
        protected set{ _description = value; }
    }
    public ReportStatus Status {
        get { return _status; }
        set { _status = value; }
    }
    public Priority Priority { 
        get { return _priority; }
        set { _priority = value; }
    }
    public Classification Classification {
        get { return _classification; }
        set { _classification = value; }
    }
    public int ReliabilityScore {
        get { return _reliabilityScore; }
        set { _reliabilityScore = value; }
            }
    public string RejectionReason {
        get { return _rejectionReason; }
        set { _rejectionReason = value; }
    }

    protected Report(int reportId, DateTime timestamp, double latitude, double longitude, string description)
    {
        ReportId = reportId;
        Timestamp = timestamp;
        Description = description;
        Longitude = longitude;
        Latitude = latitude;
        Status = ReportStatus.New;
    }

    public abstract string GetSourceType();

    public abstract int CalculateReliabilityScore();

    public virtual string GetSummary()
        => "";

    public override string ToString()
        => $"reportId: {ReportId}, timestamp: {Timestamp}, description: {Description}, longitude: {Longitude}, latitude: {Latitude}, " +
        $"ReportStatus: {Status}, Priority: {Priority}, Classification:{Classification}, ReliabilityScore: {ReliabilityScore}, RejectionReason: {RejectionReason}";
}
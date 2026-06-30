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
    private string _description;
    private ReportStatus _status;
    private Priority _priority;
    private Classification _classification;
    private int _reliabilityScore;
    private string _rejectionReason;

    public int ReportId { get; }
    public DateTime Timestamp { get; protected set; }
    public double Latitude { get; protected set; }
    public double Longitude { get; protected set; }
    public string Description { get; protected set; }
    public ReportStatus Status { get; protected set; }
    public Priority Priority { get; set; }
    public Classification Classification { get; set; }
    public int ReliabilityScore { get; set; }
    public string RejectionReason { get; set; }
    
    protected Report(int reportId,DateTime timestamp,double latitude,double longitude,string description)
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

    public virtual string GetSummary();

    public override string ToString();
}
using System;
using System.Collections.Generic;
using System.Text;
using IntelligencePipeline.Configuration;

namespace IntelligencePipeline.Models.Reports;
class DroneReport:Report
{
    private int _altitude;
    private int _imageQuality;
    public int Altitude {
        get { return _altitude; }
        protected set { _altitude = value; }
    }
    public int ImageQuality {
        get { return _imageQuality; }
        protected set { _imageQuality = value; }
    }
    public override string GetSourceType() => "Drone";
    public override int CalculateReliabilityScore()
    {
        int Base = BusinessRules.Drone.BaseReliability;
        if (ImageQuality > BusinessRules.Drone.HighImageQualityThreshold)
            Base += BusinessRules.Drone.HighImageQualityBonus;
        else if (ImageQuality >= BusinessRules.Drone.MediumImageQualityThreshold)
            Base += BusinessRules.Drone.MediumImageQualityBonus;
        if (Altitude > BusinessRules.Drone.ExtremeAltitudeThreshold)
            Base -= BusinessRules.Drone.ExtremeAltitudePenalty;
        if (Altitude > BusinessRules.Drone.ReliabilityMinAltitude && Altitude < BusinessRules.Drone.ReliabilityMaxAltitude)
            Base += BusinessRules.Drone.ReliabilityAltitudeBonus;
        return Base;
    }
    public DroneReport(int reportId, DateTime timestamp, double latitude, double longitude, string description, int altitude, int imageQuality) : base(reportId, timestamp, latitude, longitude, description)
    {
        Altitude = altitude;
        ImageQuality = imageQuality;
    }
}

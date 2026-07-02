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
        if (ImageQuality > 80)
            Base += 3;
        else if (ImageQuality > 50)
            Base += 3;
        if (Altitude > BusinessRules.Drone.ReliabilityExtremeAltitude)
            Base -= 2;
        if (Altitude > 500 && Altitude < 3000)
            Base += 2;
        return Base;
    }
    public DroneReport(int reportId, DateTime timestamp, double latitude, double longitude, string description, int altitude, int imageQuality) : base(reportId, timestamp, latitude, longitude, description)
    {
        Altitude = altitude;
        ImageQuality = imageQuality;
    }
}

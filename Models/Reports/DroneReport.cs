using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Models.Reports;
class DroneReport:Report
{
    private int _altitude;
    private int _imageQuality;
    public int Altitude { get; protected set; }
    public int ImageQuality { get; protected set; }
    public DroneReport(int reportId, DateTime timestamp, double latitude,double longitude, string description, int altitude, int imageQuality):base(reportId,timestamp,latitude, longitude, description)
        Altitude=altitude;
        ImageQuality=imageQuality;
}

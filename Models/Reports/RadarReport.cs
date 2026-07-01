using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Models.Reports;
class RadarReport:Report
{
    private int _speed;
    private int _direction;
    private int _distance;
    public int Speed {
        get { return _speed; }
        protected set { _speed = value; } 
    }
    public int Direction {
        get { return _direction; }
        protected set { _direction = value; }
    }
    public int Distance {
        get { return _distance; }
        protected set { _distance = value; }
            }
    public override string GetSourceType() => "Radar";

    public override int CalculateReliabilityScore()
    {
        int Base = 6;
        if (Distance > 500 && Distance < 30000)
            Base += 2;
        if (Speed > 10 && Speed < 900)
            Base += 1;
        if (Distance > 70000)
            Base -= 2;
        if (Speed > 1500)
            Base -= 2;
        return Base;
    }
        
    public RadarReport(int reportId, DateTime timestamp, double latitude, double longitude, string description, int speed, int direction, int distance)
            : base(reportId, timestamp, latitude, longitude, description)
    {
        Speed = speed;
        Direction = direction;
        Distance = distance;
    }

}
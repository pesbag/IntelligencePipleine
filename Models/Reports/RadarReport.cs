using IntelligencePipeline.Configuration;
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
        int Base = BusinessRules.Radar.BaseReliability;
        if (Distance > BusinessRules.Radar.ReliabilityMinDistance && Distance < BusinessRules.Radar.ReliabilityMaxDistance)
            Base += BusinessRules.Radar.ReliabilityDistanceBonus;
        if (Speed > BusinessRules.Radar.ReliabilityMinSpeed && Speed < BusinessRules.Radar.ReliabilityMaxSpeed)
            Base += BusinessRules.Radar.ReliabilitySpeedBonus;
        if (Distance > BusinessRules.Radar.ExtremeDistanceThreshold)
            Base -= BusinessRules.Radar.ExtremeDistancePenalty;
        if (Speed > BusinessRules.Radar.ExtremeSpeedThreshold)
            Base -= BusinessRules.Radar.ExtremeSpeedPenalty;
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
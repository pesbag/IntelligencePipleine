using IntelligencePipeline.Models.Enums;
using System.Text.RegularExpressions;
using System;
using System.Collections.Generic;
using System.Text;

namespace IntelligencePipeline.Models.Reports;

class SoldierReport:Report
{
    private string _soldierName;
    private string _soldierID;
    private string _unit;
    private int _confidenceLevel;
    public string SoldierName {
        get { return _soldierName; }
        protected set { _soldierName = value; }
    }
    public string SoldierID {
        get { return _soldierID; }
        protected set { _soldierID = value; }
    }
    public string Unit { get; set; }
    public int ConfidenceLevel { get; set; }
    public override string GetSourceType() => "Soldier";

    //Description { weapon,vehicle,movement,explosion }
    public override int CalculateReliabilityScore()
    {
        string weaponExists = $@"\b{WordToIdentify.weapon.ToString()}\b";
        bool isWeaponExists = Regex.IsMatch(Description, weaponExists, RegexOptions.IgnoreCase);
        string vehicleExists = $@"\b{WordToIdentify.vehicle.ToString()}\b";
        bool isVehicleExists = Regex.IsMatch(Description, vehicleExists, RegexOptions.IgnoreCase);
        string movementExists = $@"\b{WordToIdentify.movement.ToString()}\b";
        bool isMovementExists = Regex.IsMatch(Description, movementExists, RegexOptions.IgnoreCase);
        string explosionExists = $@"\b{WordToIdentify.explosion.ToString()}\b";
        bool isExplosionExists = Regex.IsMatch(Description, explosionExists, RegexOptions.IgnoreCase);

        int Base = 4;
        Base += ConfidenceLevel;
        Base += (isWeaponExists ? 1 : 0) + (isVehicleExists ? 1 : 0) + (isMovementExists ? 1 : 0) + (isExplosionExists ? 1 : 0);
        return Base;
    }

    public SoldierReport(int reportId, DateTime timestamp, double latitude, double longitude, string description, string soldierName, string soldierID, string unit, int confidenceLevel) : base(reportId, timestamp, latitude, longitude, description)
    {
        SoldierName = soldierName;
        SoldierID = soldierID;
        Unit = unit;
        ConfidenceLevel = confidenceLevel;
    }
}

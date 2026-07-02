namespace IntelligencePipeline.Configuration
{
    public static class BusinessRules
    {
        public static class baseValidator
        {
            public const int MinDescriptionLength = 10;
            public const int MaxDescriptionLength = 500;
            public static readonly DateTime MinValidDate = new DateTime(2020, 1, 1);
            public const double MinLatitude = 29.5000;
            public const double MaxLatitude = 33.5000;
            public const double MinLongitude = 34.0000;
            public const double MaxLongitude = 36.0000;
        }

        public static class Drone
        {
            public const int MinAltitude = 100;
            public const int MaxAltitude = 10000;
            public const int MinImageQuality = 1;
            public const int MaxImageQuality = 100;

            public const int BaseReliability = 5;
            public const int ReliabilityMinAltitude = 500;
            public const int ReliabilityMaxAltitude = 3000;
            public const int ReliabilityExtremeAltitude = 7000;
            public const int HighPriorityAltitudeThreshold = 500;
        }

        public static class Radar
        {
            public const int MinSpeed = 0;
            public const int MaxSpeed = 2000;
            public const int CriticalSpeedThreshold = 800;
            public const int HighSpeedThreshold = 400;

            public const int BaseReliability = 6;
            public const int ReliabilityMinDistance = 500;
            public const int ReliabilityMaxDistance = 30000;
            public const int ReliabilityExtremeDistance = 70000;
            public const int ReliabilityMinSpeed = 10;
            public const int ReliabilityMaxSpeed = 900;
            public const int ReliabilityExtremeSpeed = 1500;
        }

        public static class Soldier
        {
            public const int IdRequiredLength = 7;
            public const int MinNameUnitLength = 2;
            public const int MaxNameUnitLength = 50;
            public const int HighConfidenceThreshold = 4;

            public const int BaseReliability = 4;
        }

        public static class Signal
        {

            public const double MinFrequency = 1.0;
            public const double MaxFrequency = 3000.0;
            public const int MinContentLength = 5;
            public const int MaxContentLength = 1000;
            public const int MinSignalStrength = -120;
            public const int MaxSignalStrength = 0;

            public const int BaseReliability = 5;
            public const int StrongSignalThreshold = -40;
            public const int MediumSignalThreshold = -70;
            public const int WeakSignalLimit = -100;
        }
    }
}
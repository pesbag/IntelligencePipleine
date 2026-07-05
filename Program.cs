using IntelligencePipeline.Calculators;
using IntelligencePipeline.Models.Enums;
using IntelligencePipeline.Models.Reports;
using IntelligencePipeline.Pipeline;
using IntelligencePipeline.Storage;
using IntelligencePipeline.Validation;
using System;
using static IntelligencePipeline.Configuration.BusinessRules;
namespace IntelligencePipeline;
class Program
{
    private static ReportPipeline reportPipeline = new ReportPipeline();
    private static void DisplayReport(Report report) {
        Console.WriteLine($"--- Report Details ---");
        Console.WriteLine($"ID: {report.ReportId}");
        Console.WriteLine($"Timestamp: {report.Timestamp}");
        Console.WriteLine($"Latitude: {report.Latitude}");
        Console.WriteLine($"Longitude: {report.Longitude}");
        Console.WriteLine($"Description: {report.Description}");
        Console.WriteLine($"Status: {report.Status}");
        Console.WriteLine($"Priority: {report.Priority}");
        Console.WriteLine($"Classification: {report.Classification}");
        Console.WriteLine($"ReliabilityScore: {report.ReliabilityScore}");
        Console.WriteLine($"RejectionReason: {report.RejectionReason}");

        if (report.Status == ReportStatus.Rejected && !string.IsNullOrEmpty(report.RejectionReason))
            Console.WriteLine($"Rejection Reason: {report.RejectionReason}");
    }

    private static void DisplayValidatedReports(ReportRepository repository) {
        List<Report> listOfReport = repository.GetAll();
        for (int i=0; i < listOfReport.Count; i++)
        {
            DisplayReport(listOfReport[i]);
        }
    }
    
    private static void DisplayRejectedReports(RejectedReportRepository repository) {
        List<Report> listOfRejectedReport = repository.GetAll();
        for (int i = 0; i < listOfRejectedReport.Count; i++)
        {
            DisplayReport(listOfRejectedReport[i]);
        }
    }

    public static void validateChoice(out int choice) {
        choice = 0;
        bool isValidChoice = false;
        while (!isValidChoice)
        {
            string? sChoice = Console.ReadLine();
            if (int.TryParse(sChoice, out choice))
            {
                if (choice <= 10 && choice >= 1)
                    isValidChoice = true;
                else
                    Console.WriteLine("Error choice of command should be between 1 to 9");
            }
            else Console.WriteLine("Error choice of command should be an integer. please try again");
        }
    }

    public static void validateReportChoice(out int reportChoice)
    {
        reportChoice = 0;
        bool isValidReportChoice = false;
        while (!isValidReportChoice)
        {
            string? sChoice = Console.ReadLine();
            if (int.TryParse(sChoice, out reportChoice))
            {
                if (reportChoice <= 4 && reportChoice >= 1)
                    isValidReportChoice = true;
                else
                    Console.WriteLine("Error report chice should be between 1 to 4");
            }
            else
                Console.WriteLine("Error rport choice should be an integer. please try again");
        }
    }
    public static void showMenu()
    {
        Console.WriteLine("Please enter your choice you whant to perform: \n" +
            "enter 1 to add new report\n" +
            "enter 2 to see the validated report\n" +
            "enter 3 to search a free text in Description\n" +
            "enter 4 to filter report by  SourceType, Priority, Classification, Status or range of dates\n" +
            "enter 5 to sort by Timestamp, Priority, ReliabilityScore\n" +
            "enter 6 to update status  to Completed or to InProgress\n" +
            "enter 7 to see a single reoport\n" +
            "enter 8 to see all the rejected reports\n" +
            "enter 9 to see the statistics reports by  SourceType, Priority, Status , Valid reports precent\n" +
            "enter 10 to exit from the program");
    }

    public static string getStringInput(string massege) {
        Console.WriteLine(massege);
        return Console.ReadLine() ?? "";
    }

    public static double getDoubleNumberInput(string massege)
    {
        Console.WriteLine(massege);
        bool isDouble = false;
        double ValidValue = 0;
        while (!isDouble)
        {
            string? number = Console.ReadLine();
            if (double.TryParse(number, out ValidValue))
            {
                isDouble = true;
            }
            Console.WriteLine("Error: you should enter a double number");
        }
        return ValidValue;
    }
    public static int getIntNumberInput(string massege)
    {
        Console.WriteLine(massege);
        bool isInteger = false;
        int ValidValue = 0;
        while (!isInteger)
        {
            string? number = Console.ReadLine();
            if (int.TryParse(number, out ValidValue))
            {
                isInteger = true;
            }
            Console.WriteLine("Error: you should enter an integer number");
        }
        return ValidValue;
    }

    public static double getLatitude()
    {
        double validLatitude = 0;
        bool isValidLatitude = false;
        Console.WriteLine("enter report atitude");
        while (!isValidLatitude)
        {
            string? latitude = Console.ReadLine();
            if (double.TryParse(latitude, out validLatitude))
            {
                isValidLatitude = true;
            }
            else Console.WriteLine("Error latitude should be an integer. please try again");
        }
        return validLatitude;
    }

    public static double getLongitude()
    {
        double validLongitude = 0;
        bool isValidLongitude = false;
        Console.WriteLine("enter repor logititude");
        while (!isValidLongitude)
        {
            string? latitude = Console.ReadLine();
            if (double.TryParse(latitude, out validLongitude))
            {
                isValidLongitude = true;
            }
            else Console.WriteLine("Error longitude should be an integer. please try again");
        }
        return validLongitude;
    }

    public static DateTime getTimestamp()
    {
        DateTime validDateTime = DateTime.MinValue;
        bool isValidDate = false;

        while (!isValidDate)
        {
            Console.WriteLine("enter report timestamp (YYYY-MM-DD)");
            string? input = Console.ReadLine();

            if (DateTime.TryParse(input, out validDateTime))
                isValidDate = true;
            else
                Console.WriteLine("Error: Invalid date format. Please try again");
        }
        return validDateTime;
    }

    public static void addNewReport(){
        addReportMenu();
        validateReportChoice(out int reportChoice);
        DateTime timestamp = getTimestamp();
        double latitude = getLatitude();
        double longitude = getLongitude();
        string? description = getStringInput("enter description of report");

        switch (reportChoice)
        {
            case 1:
                Console.WriteLine("You chose option 1: Adding a Drone report...");
                int altitude = getIntNumberInput("enter drone altitude");
                int imageQuality = getIntNumberInput("enter drone image Quality");
                
                DroneReport drone = new DroneReport(0, timestamp, latitude, longitude, description, altitude, imageQuality);
                reportPipeline.ProcessReport(drone);
                Console.WriteLine("Drone report processed");
                break;
            case 2:
                Console.WriteLine("You chose option 2: Adding a Radar report...");
                int speed = getIntNumberInput("enter radar speed");
                int direction = getIntNumberInput("enter radar direction");
                int distance = getIntNumberInput("enter radar distance");
                
                RadarReport radar = new RadarReport(0, timestamp, latitude, longitude, description, speed, direction, distance);
                reportPipeline.ProcessReport(radar);
                Console.WriteLine("radar report processed");
                break;
            case 3:
                Console.WriteLine("You chose option 3: Adding a Siganl report...");
                double frequency = getDoubleNumberInput("enter signal frequency");
                string? content = getStringInput("Enter Signal Content");
                Console.WriteLine("Select Language: 0=Hebrew, 1=Arabic, 2=English, 3=Russian, 4=Other");
                int languageChoice = getIntNumberInput("Enter Language number");
                int signalStrength = getIntNumberInput("Enter Signal Strength");
                Language language = (Language)Math.Clamp(languageChoice, 0, 4);
                
                SignalReport signal = new SignalReport(0, timestamp, latitude, longitude, description, frequency, content, language, signalStrength);
                reportPipeline.ProcessReport(signal);
                Console.WriteLine("signal report processed");
                break;
            case 4:
                Console.WriteLine("You chose option 4: Adding a Soldier report...");
                string? nameOfSoldier = getStringInput("enter name of soldier");
                string? soldierID = getStringInput("enter id of soldier");
                string? unitOfSoldier = getStringInput("enter unit of soldier");
                int ConfidenceLevel = getIntNumberInput("enter ConfidenceLevel of soldier");

                SoldierReport soldier = new SoldierReport(0, timestamp, latitude, longitude, description, nameOfSoldier, soldierID, unitOfSoldier, ConfidenceLevel);
                reportPipeline.ProcessReport(soldier);
                Console.WriteLine("Soldier report processed");
                break;
        }
    }

    public static ReportStatus choceStatusToUpsade()
    {
        Console.WriteLine("Select Status: 0=InProgress, 1=Completed");
        int statusChoice = getIntNumberInput("Enter status number");
        ReportStatus[] allowedUpdates = { ReportStatus.InProgress, ReportStatus.Completed };
        int safeIndex = Math.Clamp(statusChoice, 0, allowedUpdates.Length - 1);
        ReportStatus finalStatus = allowedUpdates[safeIndex];
        return finalStatus;
    }

    public static void updateStatus(ReportRepository repository)
    {
        int reportId = getIntNumberInput("enter report id to update status");
        var reportToUpdate = repository.GetById(reportId);
        
        if(reportToUpdate is not null)
        {
            ReportStatus newStatus= choceStatusToUpsade();

            repository.UpdateStatus(reportId, newStatus);
            Console.WriteLine($"Report {reportId} status successfully updated to {newStatus}");
        }
        else
            Console.WriteLine("Error can not found the id report to update");
    }
    public static void addReportMenu()
    {
        Console.WriteLine("\n\nPlease enter your choice you whant to perform" +
            "enter 1 to create a Drone report" +
            "enter 2 to create a Radar report" +
            "enter 3 to create a Signal report" +
            "enter 4 to create a Soldier report");
    }

    private static void DisplayReportsList(List<Report> reports)
    {
        if (reports.Count == 0)
            Console.WriteLine("No reports found m");

        for (int i = 0; i < reports.Count; i++)
            DisplayReport(reports[i]);
    }
    public static void searchByFreeText(ReportRepository repository) 
    {
        string? stringToFind = getStringInput("enter text to find in reports description");
        List<Report> listFound = repository.Search(stringToFind);
        DisplayReportsList(listFound);
    }

    public static string cleanWord(string word)
    {
        if(!string.IsNullOrWhiteSpace(word))
            return word.Trim().ToLower();
        return "";
    }
    public static void filterByValue(ReportRepository repository)
    {
        Console.WriteLine("What parameter do you want to filter by?\n" +
                          "enter 1 for Status\n" +
                          "enter 2 for Priority\n" +
                          "enter 3 for Classification\n" +
                          "enter 4 for Source Type (Drone, Radar, Signal, Soldier)\n" +
                          "enter 5 for Date Range");

        int filterType = getIntNumberInput("Enter your choice:");
        Console.Clear();

        switch (filterType)
        {
            case 1:
                string statusInput = getStringInput("Enter status (New, Validating, Validated, Rejected, InProgress, Completed)");
                if (Enum.TryParse(statusInput, true, out ReportStatus status))
                    DisplayReportsList(repository.GetByStatus(status));
                else
                    Console.WriteLine("Error: Invalid status entered.");
                break;

            case 2:
                string? priorityInput = getStringInput("Enter priority (Low, Medium, High, Critical)");
                if (Enum.TryParse(priorityInput, true, out Priority priority))
                    DisplayReportsList(repository.GetByPriority(priority));
                else
                    Console.WriteLine("Error: Invalid priority entered.");
                break;

            case 3:
                string? classInput = getStringInput("Enter classification (Unclassified, Restricted, Secret, TopSecret)");
                if (Enum.TryParse(classInput, true, out Classification classification))
                    DisplayReportsList(repository.GetAll().FindAll(r => r.Classification == classification));
                else
                    Console.WriteLine("Error: Invalid classification entered");
                break;

            case 4:
                string? sourceInput = getStringInput("Enter source type (Drone, Radar, Signal, Soldier)");
                List<Report> filteredBySource = repository.GetBySourceType(sourceInput);
                DisplayReportsList(filteredBySource);
                break;

            case 5:
                Console.WriteLine("start Date:");
                DateTime startDate = getTimestamp();

                Console.WriteLine("end Date:");
                DateTime endDate = getTimestamp();

                if (startDate <= endDate)
                    DisplayReportsList(repository.GetByDateRange(startDate, endDate));
                else
                    Console.WriteLine("Error: start date must be before or equal to end date");
                break;

            default:
                Console.WriteLine("Invalid filter option");
                break;
        }
    }
    public static void DisplayDroneReport(DroneReport report)
    {
        DisplayReport(report);
        Console.WriteLine($"Altitude: {report.Altitude}");
        Console.WriteLine($"ImageQuality: {report.ImageQuality}");
    }

    public static void DisplaySoldierReport(SoldierReport report)
    {
        DisplayReport(report);
        Console.WriteLine($"SoldierName: {report.SoldierName}");
        Console.WriteLine($"SoldierID: {report.SoldierID}");
        Console.WriteLine($"Soldier unit: {report.Unit}");
        Console.WriteLine($"ConfidenceLevel: {report.ConfidenceLevel}");

    }

    public static void DisplayRadarReport(RadarReport report)
    {
        DisplayReport(report);
        Console.WriteLine($"Speed: {report.Speed}");
        Console.WriteLine($"Dirction: {report.Direction}");
        Console.WriteLine($"Distance: {report.Distance}");

    }

    public static void DisplaySignalReport(SignalReport report)
    {
        DisplayReport(report);
        Console.WriteLine($"Frequency: {report.Frequency}");
        Console.WriteLine($"Content: {report.Content}");
        Console.WriteLine($"Language: {report.Language}");
        Console.WriteLine($"SignalStrength: {report.SignalStrength}");

    }

    public static void sortByValue(ReportRepository repository)
    {
        Console.WriteLine("How do you want to sort the reports?\n" +
                          "enter 1 for Timestamp (by Newest)\n" +
                          "enter 2 for Priority (by Highest)\n" +
                          "enter 3 for Reliability Score (by Highest)");

        int sortType = getIntNumberInput("Enter your choice:");
        switch (sortType)
        {
            case 1:
                Console.WriteLine("\n=== Reports Sorted by Timestamp ===");
                DisplayReportsList(repository.SortByTimestamp());
                break;

            case 2:
                Console.WriteLine("\n=== Reports Sorted by Priority ===");
                DisplayReportsList(repository.SortByPriority());
                break;

            case 3:
                Console.WriteLine("\n=== Reports Sorted by Reliability Score ===");
                DisplayReportsList(repository.SortByReliabilityScore());
                break;

            default:
                Console.WriteLine("Invalid sort option");
                break;
        }
    }

    private static void injectionData()
    {
        Console.WriteLine("Initializing pipeline with comprehensive test suite (8 reports)...\n");

        // ==================== דוחות תקינים (יעברו ל-Validated) ====================

        // 1. Drone - תקין, קריטי
        DroneReport droneValid = new DroneReport(
            reportId: 0, // מועבר ראשון בדיוק לפי חתימת הבנאי במפרט
            timestamp: new DateTime(2026, 07, 01, 12, 00, 00),
            latitude: 32.5000,
            longitude: 34.8000,
            description: "Drone visual confirmed immediate attack planned by enemy forces",
            altitude: 450,
            imageQuality: 90
        );

        // 2. Radar - תקין, מהירות גבוהה מאוד (קריטי)
        RadarReport radarValid = new RadarReport(
            reportId: 0,
            timestamp: new DateTime(2026, 07, 02, 08, 30, 00),
            latitude: 33.1000,
            longitude: 35.2000,
            description: "Radar tracking fast object heading south rapidly",
            speed: 850,
            direction: 180,
            distance: 12000
        );

        // 3. Signal - תקין, ערבית, עוצמה חזקה
        SignalReport signalValid = new SignalReport(
            reportId: 0,
            timestamp: new DateTime(2026, 07, 03, 14, 15, 00),
            latitude: 31.5000,
            longitude: 34.5000,
            description: "Routine transmission intercepted from active sector containing vehicle movements",
            frequency: 433.92,
            content: "Move the vehicle at midnight to the southern checkpoint",
            language: Language.Arabic,
            signalStrength: -30
        );

        // 4. Soldier - תקין, רמת ביטחון נמוכה
        SoldierReport soldierValid = new SoldierReport(
            reportId: 0,
            timestamp: new DateTime(2026, 07, 04, 22, 00, 00),
            latitude: 29.9000,
            longitude: 34.9000,
            description: "Soldier reports routine patrol completed with no unusual activity",
            soldierName: "Aviv Levi",
            soldierID: "9876543",
            unit: "Paratroopers",
            confidenceLevel: 2
        );

        // ==================== דוחות לא תקינים (יידחו ל-Rejected) ====================

        // 5. Drone Invalid - נכשל: תיאור קצר מדי
        DroneReport droneInvalid = new DroneReport(
            reportId: 0,
            timestamp: DateTime.Now,
            latitude: 32.0000,
            longitude: 34.5000,
            description: "Too short",
            altitude: 1500,
            imageQuality: 75
        );

        // 6. Radar Invalid - נכשל: כיוון לא חוקי
        RadarReport radarInvalid = new RadarReport(
            reportId: 0,
            timestamp: DateTime.Now,
            latitude: 31.0000,
            longitude: 35.0000,
            description: "Radar sweep showing ghost anomalies across sector",
            speed: 250,
            direction: 420,
            distance: 25000
        );

        // 7. Signal Invalid - נכשל: תאריך בעתיד
        SignalReport signalInvalid = new SignalReport(
            reportId: 0,
            timestamp: DateTime.Now.AddDays(5),
            latitude: 32.2000,
            longitude: 35.5000,
            description: "Encrypted burst transmission recorded from unidentified source",
            frequency: 900.0,
            content: "Secure message data burst",
            language: Language.English,
            signalStrength: -50
        );

        // 8. Soldier Invalid - נכשל: מזהה חייל לא תקין
        SoldierReport soldierInvalid = new SoldierReport(
            reportId: 0,
            timestamp: DateTime.Now,
            latitude: 30.5000,
            longitude: 34.2000,
            description: "Observation post reporting suspicious dust cloud in valley",
            soldierName: "Dan Cohen",
            soldierID: "12345",
            unit: "Givati",
            confidenceLevel: 3
        );

        // הזרקה לצינור לעיבוד אוטומטי
        reportPipeline.ProcessReport(droneValid);
        reportPipeline.ProcessReport(radarValid);
        reportPipeline.ProcessReport(signalValid);
        reportPipeline.ProcessReport(soldierValid);
        reportPipeline.ProcessReport(droneInvalid);
        reportPipeline.ProcessReport(radarInvalid);
        reportPipeline.ProcessReport(signalInvalid);
        reportPipeline.ProcessReport(soldierInvalid);
    }
    public static void showSingleReport(ReportRepository repository) 
    {
        int idOfReportToShow = getIntNumberInput("enter id of report that you whant to see");
        Report? report = repository.GetById(idOfReportToShow);
        if (report is null)
        {
            Console.WriteLine("Error: Cannot find the report");
            return;
        }

        if (report is DroneReport droneReport)
            DisplayDroneReport(droneReport);

        else if (report is SoldierReport soldierReport)
            DisplaySoldierReport(soldierReport);

        else if (report is RadarReport radarReport)
            DisplayRadarReport(radarReport);

        else if (report is SignalReport signalReport)
            DisplaySignalReport(signalReport);
    }

    public static void Main(string[] args) {
        Console.WriteLine("=== Welcom to IntelligencePipeline porject! lets get started ===\n\n");
        injectionData();
        bool running = true;
        while (running)
        {
            showMenu();
            validateChoice(out int choice);

            switch (choice)
            {
                case 1:
                    addNewReport();
                    break;
                
                case 2:
                    Console.WriteLine("\n=== Validated Reports ===");
                    DisplayValidatedReports(reportPipeline.GetValidatedReports());
                    break;
                
                case 3:
                    Console.WriteLine("\n=== Search By Free Text ===");
                    searchByFreeText(reportPipeline.GetValidatedReports());
                    break;
                
                case 4:
                    Console.WriteLine("\n=== Filter By Values ===");
                    filterByValue(reportPipeline.GetValidatedReports());
                    break;

                case 5:
                    Console.WriteLine("\n=== Sort Reports ===");
                    sortByValue(reportPipeline.GetValidatedReports());
                    break;

                case 6:
                    Console.WriteLine("\n=== Update Report Status ===");
                    updateStatus(reportPipeline.GetValidatedReports());
                    break;
                
                case 7:
                    Console.WriteLine("\n=== View Single Report ===");
                    showSingleReport(reportPipeline.GetValidatedReports());
                    break;
                
                case 8:
                    Console.WriteLine("\n=== Rejected Reports ===");
                    DisplayRejectedReports(reportPipeline.GetRejectedReports());
                    break;
                
                case 9:
                    Console.WriteLine("\n=== Statistics ===");
                    reportPipeline.DisplayStatistics();
                    break;
                
                default:
                    running = false;
                    Console.WriteLine($"exit from the program...");
                    break;
            }
        }
    }
}
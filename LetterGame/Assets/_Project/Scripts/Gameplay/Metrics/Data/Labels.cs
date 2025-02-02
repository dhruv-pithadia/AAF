
namespace LetterQuest.Gameplay.Metrics.Ui
{
    public static class Labels
    {
        public const string WordTimeLabel = "Average time between each word :\n";
        public const string GrabTimeLabel = "Average time between each grab attempt :\n";
        public const string LetterTimeLabel = "Average time between each letter placement :\n";

        public const string AvgFailGrabLabel = "Average Failed Grab Count : ";
        public const string MinFailGrabLabel = "Lowest Failed Grab Count : ";
        public const string MaxFailGrabLabel = "Highest Failed Grab Count : ";
        public const string FailGrabPositiveLabel = "Good Job grabbing the correct letters more often!";
        public const string FailGrabNegativeLabel = "Grabbing incorrect letters more than usual";
        public const string FailGrabStableLabel = "The amount of incorrect letters grabbed is around average";

        public const string AvgPerfectGrabLabel = "Average Perfect Grab Count : ";
        public const string MinPerfectGrabLabel = "Lowest Perfect Grab Count : ";
        public const string MaxPerfectGrabLabel = "Highest Perfect Grab Count : ";
        public const string PerfectGrabPositiveLabel = "Good Job placing the correct letters on your first attempt more often!";
        public const string PerfectGrabNegativeLabel = "Picking incorrect letters more than usual";
        public const string PerfectGrabStableLabel = "The amount of letters placed correctly on the first try is around average";

        public const string AvgInvalidLetterLabel = "Average Invalid Letter Count : ";
        public const string MinInvalidLetterLabel = "Lowest Invalid Letter Count : ";
        public const string MaxInvalidLetterLabel = "Highest Invalid Letter Count : ";
        public const string InvalidLetterPositiveLabel = "Good Job placing the letters in the correct order more often!";
        public const string InvalidLetterNegativeLabel = "Placing letters in the incorrect order more than usual";
        public const string InvalidLetterStableLabel = "The amount of letters placed incorrectly is around average";

        public const string AvgWordTimeLabel = "Average Time per Word : ";
        public const string WordTimePositiveLabel = "Good Job solving words faster!";
        public const string WordTimeNegativeLabel = "Solving words slower than usual";
        public const string WordTimeStableLabel = "The amount of time spent solving a word is around average";
        public const string WordGraphLabel = "Time in seconds to solve each Word";

        public const string AvgGrabTimeLabel = "Average Time per Grab : ";
        public const string GrabTimePositiveLabel = "Good Job grabbing letters faster!";
        public const string GrabTimeNegativeLabel = "Grabbing letters slower than usual";
        public const string GrabTimeStableLabel = "The amount of time spent grabbing a letter is around average";
        public const string GrabGraphLabel = "Time in seconds between each grab attempt";

        public const string AvgLetterTimeLabel = "Average Time per Letter : ";
        public const string LetterTimePositiveLabel = "Good Job placing letters faster!";
        public const string LetterTimeNegativeLabel = "Placing letters slower than usual";
        public const string LetterTimeStableLabel = "The amount of time spent placing a letter is around average";
        public const string LetterGraphLabel = "Time in seconds between each letter placement";

        public const string SecondsLabel = " seconds";
        public const string PositiveColorStart = "<b><color=#00aa00ff>";
        public const string NegativeColorStart = "<b><color=#cc0000ff>";
        public const string ColorEnd = "</color></b>";
    }
}

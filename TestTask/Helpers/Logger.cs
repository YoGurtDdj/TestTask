namespace TestTask.Helpers
{
    public static class Logger
    {
        public static readonly string logFilePath = "log.txt";
        public static void WriteToLog(string logMessage)
        {
            using (TextWriter w = File.AppendText(logFilePath))
            {
                w.Write("\r\n[ Log Entry : ");
                w.Write($"{DateTime.Now.ToLongTimeString()} {DateTime.Now.ToLongDateString()} ]");
                w.WriteLine($" : {logMessage}");
            }
        }
    }
}

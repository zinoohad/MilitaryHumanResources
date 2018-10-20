using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MilitaryHumanResources.Common
{
    public static class Logger
    {
        private static string CurrentTime { get { return DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss"); } }

        private static readonly object locker = new object();

        public static bool WriteErrorLog(string text, LogTarget target = LogTarget.StandardLog, bool addCallerParams = true, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string filePath = null)
        {
            return WriteLog("ERROR: " + text, target, addCallerParams, lineNumber, caller, filePath);
        }

        public static bool WriteWarningLog(string text, LogTarget target = LogTarget.StandardLog, bool addCallerParams = true, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string filePath = null)
        {
            return WriteLog("WARNING: " + text, target, addCallerParams, lineNumber, caller, filePath);
        }

        public static bool WriteLog(string text, LogTarget target, bool addCallerParams = true, [CallerLineNumber] int lineNumber = 0, [CallerMemberName] string caller = null, [CallerFilePath] string filePath = null)
        {
            if (string.IsNullOrEmpty(text))
                return false;

            if (addCallerParams)
                text = $"Log Time: {CurrentTime} | {text}. (Function: {caller}, Line: {lineNumber})";
            else
                text = $"Log Time: {CurrentTime} | {text}.";

#if(DEBUG)
            Console.WriteLine(text);
#endif

            switch (target)
            {
                case LogTarget.EventViewer:
                    return WriteToEventLog(text);

                default:
                    return WriteToFile(text, LogLocation(target));
            }
        }

        private static bool WriteToFile(string text, string filePath)
        {
            try
            {
                //check if file exist,if not,create one
                lock (locker)
                {
                    using (StreamWriter w = File.AppendText(filePath))
                    {
                        w.WriteLine(text);
                    }
                }
            }
            catch (System.Exception)
            {
                return false;
            }
            return true;
        }

        private static bool WriteToEventLog(string text)
        {
            try
            {
                if (!EventLog.SourceExists(MilitaryHumanResources.Properties.Resources.EventViewerSource))
                    EventLog.CreateEventSource(MilitaryHumanResources.Properties.Resources.EventViewerSource, 
                        MilitaryHumanResources.Properties.Resources.EventViewerLogApp);

                EventLog.WriteEntry(MilitaryHumanResources.Properties.Resources.EventViewerSource, text);
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
            
        }

        private static string LogLocation(LogTarget target)
        {
            string filePath = string.Format("{0}/logs/{1}.txt", Environment.CurrentDirectory, target.GetString());
            Directory.CreateDirectory(Path.GetDirectoryName(filePath));
            return filePath;
        }

        public static string GetString(this LogTarget enumObj)
        {
            return Enum.GetName(typeof(LogTarget), enumObj);
        }

    }

    public enum LogTarget
    {
        EventViewer,
        StandardLog,
        EMAIL
    }
}

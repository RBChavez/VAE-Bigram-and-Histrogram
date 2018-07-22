using System;
using System.IO;

namespace Bigram_VAE
{
    sealed class LogHistory
    {
        /// <summary>
        /// Write log history and Error log (**Need a write permission)
        /// </summary>
        /// <param name="LogPath"></param>
        /// <param name="logMessage"></param>
        /// <param name="IsError"></param>
        public LogHistory(string logMessage)
        {
            LogWrite(logMessage);
        }
        static public void LogWrite(string logMessage)
        {
            if (logMessage.Trim() != "")
            {
                string LogPath = @"C:\Logs\Bigram\";
                
                // Check exists path 
                if (!Directory.Exists(LogPath))
                {
                    Directory.CreateDirectory(LogPath);
                }
                LogPath += "\\" + DateTime.Now.ToString("yyyy_MM_dd-HHmm") + ".txt";
                try
                {
                    using (StreamWriter w = File.AppendText(LogPath))
                    {
                        Log(logMessage, w);
                    }
                }
                catch //(Exception ex)
                {
                }
            }
        }
        
        static public void Log(string logMessage, TextWriter txtWriter)
        {
            try
            {
                txtWriter.Write("\r\n");
                txtWriter.WriteLine("{0} {1}", DateTime.Now.ToLongTimeString(), DateTime.Now.ToLongDateString());
                txtWriter.WriteLine("{0}", logMessage);
            }
            catch //(Exception ex)
            {
            }
        }
    }
}

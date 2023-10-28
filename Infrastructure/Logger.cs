using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Diagnostics;

namespace CollectionViewSourceFilterWpfApp1
{
    public class Loger
    {
        string path = "CollectionViewSourceFilterWpfApp1.txt";
        DateTime timeStartApp; // Время старта приложения. TODO: сделать вычисление длительности работы приложения
        DateTime timeLastLog;

        //public void Log(string message)
        //{
        //    string messageFull = DateTime.Now.ToString("yyyy.MM.dd || hh:mm:ss:ffff") + " || " + message; 
        //    WriteLine(messageFull);
        //}

        public void Log(string owner, string message)
        {
            string messageFull =  owner + " || " + message; // 
            LogWriteLine(messageFull);
        }

        public void Log(string owner, string row, string message)
        {
            // string messageFull = DateTime.Now.ToString("yyyy.MM.dd || hh:mm:ss:ffff") + " || " + owner + " || " + row + " || " + message; //++ || 08:46:22:7034            
            string messageFull = owner + " || " + row + " || " + message; //++ || 08:46:22:7034            
            LogWriteLine(messageFull);
        }

        public void Log(string owner, string row, string member, string message)
        {
            // string messageFull = DateTime.Now.ToString("yyyy.MM.dd || hh:mm:ss:ffff") + " || " + owner + " || " + row + " || " + message; //++ || 08:46:22:7034            
            string messageFull = owner + " || " + row + " || " + member + " || " + message; //++ || 08:46:22:7034            
            LogWriteLine(messageFull);
        }



        /// <summary>
        /// -----
        /// </summary>
        public string ReadFile(string path )
        {
            string content = "";
            try
            {
                content = File.ReadAllText(path);                
            }
            catch (Exception ex)
            {
                string message = ex.Message;
                Log($"Loger ReadFile() {path}", "Проблема с файлом !!!");
                Log($"Loger ReadFile() ", message);
            }

            return content;
        }

        public void LogWriteLine(string messageFull)
        {
            using (StreamWriter sw = new StreamWriter(path, true))
            {
                CultureInfo ci = new CultureInfo("ru-RU"); // Форматирует время под текущую культуру. 
                                
                DateTime now = DateTime.Now;

                TimeSpan duration = now - timeLastLog;
                string durationOutput = $"{duration.Minutes:D4}:{duration.Seconds:D4}:{duration.Milliseconds:D4}";

                // string messageAll = DateTime.Now.ToString("yyyy.MM.dd || hh:mm:ss:ffff") + " || " + durationOutput + " || " + messageFull; //++ || 08:46:22:7034                 
                string messageLog = now.ToString("yyyy.MM.dd || hh:mm:ss:ffff") + " || " + durationOutput + " || " + messageFull; //++ || 08:46:22:7034                 
                sw.WriteLine(messageLog); //++ // 2022-12-15T07:02:43.5857720+03:00
                timeLastLog = now;

                Debug.WriteLine("=== Логер ===");
                Debug.WriteLine(messageLog);
                Debug.WriteLine("=== === === ===");
            }
        }
    }

}

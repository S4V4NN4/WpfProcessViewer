using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

using System.Management;


namespace lab1_sp_
{
    public class ProcessInfo
    {
        public int ID { get; set; }
        public string User { get; set; }
        public string Memory { get; set; }
        public string Prio { get; set; }
        public string Name { get; set; }
        public int ThreadCount { get; set; }

        public List<ThreadInfo> ThreadsInfo;


        public string ThreadsString
        {
            get
            {
                if (ThreadsInfo != null && ThreadsInfo.Count != 0)
                {
                    List<string> lines = new List<string>();

                    foreach (ThreadInfo threadInfo in ThreadsInfo)
                    {
                        lines.Add($"ID: {threadInfo.ID} Прио: {threadInfo.Prio}");
                    }

                    return string.Join(Environment.NewLine, lines);
                }

                return "NaN";
            }
        }


        public string GetProcessUser()
        {
            try
            {
                using (var searcher = new ManagementObjectSearcher($"Select * From Win32_Process Where ProcessID = {ID}"))
                {
                    foreach (ManagementObject obj in searcher.Get())
                    {
                        object[] args = new object[] { string.Empty, string.Empty };
                        int returnVal = Convert.ToInt32(obj.InvokeMethod("GetOwner", args));
                        if (returnVal == 0)
                        {
                            return $"{args[1]}\\{args[0]}";
                        }
                    }
                }
            }
            catch
            {

            }

            return "NaN";
        }
    }
}



// Отображение работающих в данный момент процессов;
// Отображение общего количества процессов в системе;
// Отображение ID процесса;
// Принадлежность процесса пользователям;
// Объём занимаемой памяти;
// Отображение текущего приоритета процесса;
// Количество потоков данного процесса;
// Отображение информации о потоках данного процесса (ID, приоритет);

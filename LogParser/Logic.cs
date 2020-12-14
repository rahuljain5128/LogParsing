using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using LogParser.Entity;

namespace LogParser
{
    public class Logic
    {
        private const string delimeter = ",";
        public void Log(string filePath)
        {
            Validation validation = new Validation();
            validation.ValidateFile(filePath);
            var apiLogs = ProcessFile(filePath);
            var apiLogDict = GetDictionary(apiLogs);
            TopNHighestThroughPutApis(5,apiLogDict);
            GetMinMaxAvgTime(apiLogDict);
            return;
        }
        public List<ApiLogOutput> GetMinMaxAvgTime(Dictionary<string,ApiLogOutput> apiLogDict)
        {
            Console.WriteLine("----GetMinMaxAvgTime------");
            var apiLogOutputList = new List<ApiLogOutput>();
            foreach (KeyValuePair<string, ApiLogOutput> kvp in apiLogDict)
            {
                var apiLogOutput = kvp.Value;
                apiLogOutputList.Add(apiLogOutput);
                var avgTime = Math.Round(apiLogOutput.TotalTime / apiLogOutput.Count,2);
                Console.WriteLine("Url = {0}, Method = {1}, MinTime = {2}, MaxTime = {3} ,AvgTime = {4}", 
                apiLogOutput.Method,apiLogOutput.Url, apiLogOutput.MinTime ,apiLogOutput.MaxTime, avgTime);
            }
            return apiLogOutputList;
        }

        private Dictionary<string,ApiLogOutput> GetDictionary(List<ApiLog> apiLogs)
        {
            var apiLogDict = new Dictionary<string,ApiLogOutput>();
            if(apiLogs == null || apiLogs.Count == 0)
            {
                throw new ArgumentException();
            }
            foreach(var apiLog in apiLogs)
            {
                string key = string.Format("{0}_{1}",apiLog.Method,apiLog.Url);
                var apiLogOutput = GetApiLogOutput(apiLog);
                if(apiLogDict.ContainsKey(key))
                {
                    apiLogDict[key].Count +=  1;
                    apiLogDict[key].TotalTime += apiLog.ResponseTime;
                    if(apiLogDict[key].MinTime > apiLog.ResponseTime)
                    {
                        apiLogDict[key].MinTime = apiLog.ResponseTime;
                    }
                    if(apiLogDict[key].MaxTime < apiLog.ResponseTime)
                    {
                        apiLogDict[key].MaxTime = apiLog.ResponseTime;
                    }
                }
                else
                {
                    apiLogDict.Add(key,apiLogOutput);
                }
            }
            return apiLogDict;
        }

        private ApiLogOutput GetApiLogOutput(ApiLog apiLog)
        {
            var apiLogOutput = new ApiLogOutput
            {
                Url = apiLog.Url,
                Method = apiLog.Method,
                MinTime = apiLog.ResponseTime,
                MaxTime = apiLog.ResponseTime,
                TotalTime = apiLog.ResponseTime,
                Count = 1
            };
            return apiLogOutput;    
        }

        public List<ApiLogOutput> TopNHighestThroughPutApis(int n,Dictionary<string,ApiLogOutput> apiLogDict)
        {
            Console.WriteLine("----TopNHighestThroughPutApis------");
            var apiLogOutputResult = new List<ApiLogOutput>();
            var apiLogDictSorted = apiLogDict.OrderByDescending(x => x.Value.Count).ToDictionary(x => x.Key, x => x.Value)?.Take(n);
            foreach (KeyValuePair<string, ApiLogOutput> kvp in apiLogDictSorted)  
            {  
                var apiLogOutput = kvp.Value;
                apiLogOutputResult.Add(apiLogOutput);
                Console.WriteLine("Url = {0}, Method = {1}, Count = {2}", 
                apiLogOutput.Method,apiLogOutput.Url, apiLogOutput.Count);
            }
            return apiLogOutputResult;
        }

        private List<ApiLog> ProcessFile(string filePath)
        {
            List<ApiLog> apiLogs = new List<ApiLog>();
            string[] lines = File.ReadAllLines(filePath);
            lines = lines.Skip(1).ToArray();
            foreach(var line in lines)
            {
                var values = line.Split(delimeter);
                string url = values[1];
                string maskedUrl = Regex.Replace(url, "[0-9]{1,}", "id" );
                int responseTime = 0;
                Int32.TryParse(values[3], out responseTime);
                int responseCode = 0;
                Int32.TryParse(values[4], out responseCode);
                var apiLog = new ApiLog
                {
                    Url = maskedUrl,
                    Method = values[2],
                    ResponseTime = responseTime,
                    ResponseCode = responseCode
                };
                apiLogs.Add(apiLog);
            }
            return apiLogs;
        }
    }
}

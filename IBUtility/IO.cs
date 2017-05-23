using System;
using System.Collections.Generic;
using System.IO;
using IBDatabase.Models;
using Newtonsoft.Json;
using Contract = IBApi.Contract;

namespace IBUtility
{
    public static class IO
    {
        public static string Basepath = @"H:\IBData\";

        public static void SetBasepath(string path)
        {
            Basepath = path;
        }
        public static void WriteMessageList(List<string> messageList)
        {
            //string json = Newtonsoft.Json.JsonConvert.SerializeObject(messageList);
            using (StreamWriter sw = new StreamWriter(Basepath + "MessageList.csv"))
            {
                //sw.WriteLine("reqId,Time,Open,High,Low,Close,Volume,WAP,Count");
                foreach (var s in messageList)
                    if (s.EndsWith("\n"))
                    {
                        sw.Write(s);
                    }
                    else
                    {
                        sw.WriteLine(s);
                    }
                sw.Flush();
                sw.Close();
            }
        }

        public static List<string> ReadMessageList()
        {
            List<string> messageList = new List<string>();
            using (StreamReader sr = new StreamReader(Basepath + "MessageList.csv"))
            {
                while (!sr.EndOfStream)
                {
                    messageList.Add(sr.ReadLine());
                }
                sr.Close();
            }
            return messageList;
        }

        public static List<string> ReadDataFile(string whereSerialized)
        {
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader(Basepath + whereSerialized))
            {
                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }
                sr.Close();
            }
            return lines;
        }
        public static List<string> ReadLinesFromDataFile(string whereSerialize, int numberOfLines)
        {
            List<string> lines = new List<string>();
            using (StreamReader sr = new StreamReader(Basepath + whereSerialize))
            {
                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                    if (lines.Count >= numberOfLines)
                        break;

                }
                sr.Close();
            }
            return lines;
        }
        public static void SerializeJson<T>(T toSerialize, string whereSerialize, bool append = false)
        {
            string json = Newtonsoft.Json.JsonConvert.SerializeObject(toSerialize, Formatting.Indented);
            using (var sw = new StreamWriter(Basepath + whereSerialize, append))
            {
                sw.Write(json);
                sw.Flush();
                sw.Close();
            }
        }

        public static Dictionary<int, DBContract> GetDBContractDictionaryJson(string whereSerialized)
        {
            string json;

            using (var sr = new StreamReader(Basepath + whereSerialized))
            {
                json = sr.ReadToEnd();
                sr.Close();
            }

            var dic = (Dictionary<int, DBContract>)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(Dictionary<int, DBContract>));
            return dic;
        }

        public static Dictionary<int, object> GetObjectsDictionary(string whereSerialized)
        {
            string json;

            using (var sr = new StreamReader(Basepath + whereSerialized))
            {
                json = sr.ReadToEnd();
                sr.Close();
            }

            var dic = (Dictionary<int, object>)Newtonsoft.Json.JsonConvert.DeserializeObject(json, typeof(Dictionary<int, object>));
            return dic;
        }

        public static void WriteString(string toWrite, string whereSerialize, bool append = false)
        {
            using (var sw = new StreamWriter(Basepath + whereSerialize, append))
            {
                sw.WriteLine(toWrite);
                sw.Flush();
                sw.Close();
            }
        }

        public static List<DateTime> GetDBContractDates(string whereSerialized)
        {

            List<DateTime> list = new List<DateTime>();
            using (var sr = new StreamReader(whereSerialized))
            {
                while (!sr.EndOfStream)
                {
                    string line = sr.ReadLine();
                    string[] arr = line.Split(',');
                    try
                    {
                        list.Add(DateTime.Parse(arr[1]));
                    }
                    catch (System.FormatException)
                    {
                        continue;
                    }
                }
                sr.Close();
            }
            return list;
        }
        public static Dictionary<int, DBContract> GetContractDictionaryJson(string whereSerialized)
        {
            string json;

            using (var sr = new StreamReader(Basepath + whereSerialized))
            {
                json = sr.ReadToEnd();
                sr.Close();
            }

            var dic = (Dictionary<int, DBContract>)JsonConvert.DeserializeObject(json, typeof(Dictionary<int, DBContract>));
            return dic;
        }
        public static List<Contract> GetContractListFromDictionaryJson(string whereSerialized)
        {
            string json;

            string filepath = Basepath + whereSerialized;
            if (!filepath.EndsWith(".json"))
                filepath += ".json";
            using (var sr = new StreamReader(filepath))
            {
                json = sr.ReadToEnd();
                sr.Close();
            }
            var dictionary = (Dictionary<string, DBContract>)JsonConvert.DeserializeObject(json, typeof(Dictionary<string, DBContract>));
            List<Contract> contracts = new List<Contract>();
            foreach (var kvp in dictionary)
            {
                contracts.Add(ContractConverter.DBContractToContract(kvp.Value));
            }
            return contracts;
        }

        public static void SaveContractQuotesJson(Dictionary<string, ContractQuote> dictionary, string whereSerialize)
        {
            SerializeJson(dictionary, whereSerialize);
        }
        public static List<IBApi.Contract> GetContractList(string whereSerialized)
        {
            string json;
            
            using (var sr = new StreamReader(Basepath + whereSerialized))
            {
                json = sr.ReadToEnd();
                sr.Close();
            }

            var list = (List<IBApi.Contract>)JsonConvert.DeserializeObject(json, typeof(List<IBApi.Contract>));
            return list;
        }

        public static ContractQuote GetContractQuotesList(string whereSerialized)
        {
            string json;

            using (var sr = new StreamReader(Basepath + whereSerialized))
            {
                json = sr.ReadToEnd();
                sr.Close();
            }
            
            var qc = (ContractQuote)JsonConvert.DeserializeObject(json, typeof(ContractQuote));
            return qc;
        }
        public static void WriteStringList(IEnumerable<string> strings, string whereSerialized)
        {
            using (StreamWriter sw = new StreamWriter(Basepath + whereSerialized))
            {
                foreach (var s in strings)
                    if (s.EndsWith("\n"))
                    {
                        sw.Write(s);
                    }
                    else
                    {
                        sw.WriteLine(s);
                    }
                sw.Flush();
                sw.Close();
            }
        }
    }
}

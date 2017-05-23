using System.Collections.Generic;
using System.Linq;
using CodeFromDatabase;


namespace IBUtility
{
    public class ContractReader : ICsvReader<ContractSummary>
    {
        /// <summary>
        /// Deserializes a csv file into records
        /// </summary>
        /// <param name="whereSerialized">string - the file path</param>
        /// <returns>A list of ContractSummary objects</returns>
        public List<ContractSummary> DeserializeRecords(string whereSerialized)
        {
            List<ContractSummary> records = new List<ContractSummary>();
            var list = IO.ReadDataFile(whereSerialized);
            ListToRecords(ref list, ref records);
            return records;
        }
        /// <summary>
        /// Gets the first data record from a csv file.  It skips the header line.
        /// If there is no header line, it skips the first record.
        /// </summary>
        /// <param name="whereSerialized">string - the file path</param>
        /// <returns>A list of ContractSummary objects</returns>
        public ContractSummary DeserializeFirstRecord(string whereSerialized)
        {
            List<ContractSummary> records = new List<ContractSummary>();
            var list = IO.ReadLinesFromDataFile(whereSerialized, 2);        // includes the header
            ListToRecords(ref list, ref records);
            return records.FirstOrDefault();
        }

        /// <summary>
        /// Gets the first n data records from a csv file.  It skips the header line.
        /// If there is no header line, it skips the first record.
        /// </summary>
        /// <param name="whereSerialized">string - the file path</param>
        /// <param name="numberOfLines">int - the number of records to return</param>
        /// <returns>A list of ContractSummary objects</returns>
        public List<ContractSummary> DeserializeNumberOfRecords(string whereSerialized, int numberOfLines)
        {
            List<ContractSummary> records = new List<ContractSummary>();
            var list = IO.ReadLinesFromDataFile(whereSerialized, numberOfLines);        // includes the header
            ListToRecords(ref list, ref records);
            return records;
        }
        /// <summary>
        /// Gets the first data record from a csv file which is assumed to have the field names
        /// </summary>
        /// <param name="whereSerialized">string - the file path</param>
        /// <returns>A list of ContractSummary objects</returns>
        public string GetHeaderLine(string whereSerialized)
        {
            return IO.ReadLinesFromDataFile(whereSerialized, 1).FirstOrDefault();
        }

        #region "Private Methods"
        /// <summary>
        /// Converts a list of strings to a list of ContractSummary objects
        /// </summary>
        /// <param name="list">the List of lines from the file></param>
        /// <param name="records">The list of ContractSummary objects</param>
        private static void ListToRecords(ref List<string> list, ref List<ContractSummary> records)
        {
            bool skip = true;
            foreach (var s in list)
            {
                if (skip)
                {
                    skip = false;
                    continue;
                }
                records.Add(RecordFromString(s));
            }
        }

        private static ContractSummary RecordFromString(string s)
        {
            ContractSummary ar = new ContractSummary();
            CsvSerializer.Deserialize(",", s, ref ar);
            return ar;
        }

        #endregion
    }
}

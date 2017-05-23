using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using IBDatabase.Models;
using IBDatabase.Redis;
using IBUtility;
using Newtonsoft.Json;

namespace IBDatabase.UI
{
    public partial class Form1 : Form
    {
        private QuoteReceiverAsync quoteReciever;
        private ContractReceiver contractReceiver;
        public Form1()
        {
            InitializeComponent();
        }

        private void ButtonStart_Click(object sender, EventArgs e)
        {
            //ContractDBRedis x = new ContractDBRedis();
            //var t = x.RefreshRedisFromDb();
            buttonStart.Enabled = false;
            label1.Text = "Starting...";
            // Create a quote receiver which blocks
            quoteReciever = new QuoteReceiverAsync();
            quoteReciever.SubscribeAsync();
            contractReceiver = new ContractReceiver();
            contractReceiver.Subscribe();
            label1.Text = "Started";


        }

        private void ButtonStop_Click(object sender, EventArgs e)
        {
            //quoteReciever.Getter.SaveCsv(quoteReciever.History, "History.csv");
            label1.Text = "Saving and Unsubscribing";
            Refresh();
            
            buttonStart.Enabled = true;
            SaveAndUnsubscribe();
            Thread.Sleep(1000);

            contractReceiver?.Unsubscribe();

            label1.Text = "Unsubscribed";
            Refresh();
        }

        private void SaveAndUnsubscribe()
        {
            string quotehistoryfile = @"H:\IBData\QuoteHistory.csv";
            string quotesummaryfile = @"H:\IBData\QuoteSummary.csv";

            try
            {
                quoteReciever?.Unsubscribe();
                
                if (quoteReciever?.Consumer != null)
                {

                    List<ContractQuote> quoteHistory = quoteReciever.Consumer.CloneQuoteHistory();
                    IEnumerable<string> serialized = CsvSerializer.Serialize(",", quoteHistory);
                    using (StreamWriter sw = new StreamWriter(quotehistoryfile))
                    {
                        foreach (string s in serialized)
                        {
                            sw.WriteLine(s);
                        }
                        sw.Flush();
                    }
                    var quoteSummary = quoteReciever.Consumer.CloneQuotesSummary();
                    IEnumerable<string> serializedquotes = CsvSerializer.Serialize(",", quoteSummary);
                    using (StreamWriter sw = new StreamWriter(quotesummaryfile))
                    {
                        foreach (string s in serializedquotes)
                        {
                            sw.WriteLine(s);
                        }
                        sw.Flush();
                    }
                }

            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Sync contracts in the db to those in redis eliminating any that have expired.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ContractsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var csr = new ContractSqlRedis())
            {
                int s;
                try
                {
                    // First send any new contracts in Redis to the db.
                    s = csr.RefreshDbFromRedis();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                try
                {
                    // Then refresh redis to those contracts that are still alive in the db
                    var t = csr.RefreshRedisFromDb();
                    MessageBox.Show($"{s} contracts updated in the db.\n{t} contracts added to redis.");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private void QuotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Initialize Redis from the database with contracts that expire later than today
            //  By adding the contracts to Redis, the response time speeds up.
            using (var csr = new QuoteSqlRedis())
            {
                int s;
                try
                {
                    // First send any new contracts in Redis to the db.
                    s = csr.RefreshDbFromRedis();
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
                try
                {
                    // Then refresh redis to those contracts that are still alive in the db
                    var t = csr.RefreshRedisFromDb();
                    MessageBox.Show($"{s} contracts updated in the db.\n{t} contracts added to redis.");
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }

        private void SendBarToolStripMenuItem_Click(object sender, EventArgs e)
        {

            foreach (string line in GetLines())
            {
                var bar = GetIncomingBar(line);
                HttpClient client = new HttpClient();
                string json = JsonConvert.SerializeObject(bar);
                HttpContent content = new StringContent(json);
                content.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                //api/IncomingBarsApi/5

                //var response = client.GetAsync(@"http://localhost:46393/api/IncomingBarsApi/1");
                var response = client.PutAsync(@"http://localhost:46393/api/IncomingBarsApi/1", content);

                string statusCode = response.Result.StatusCode.ToString();
                Thread.Sleep(1000);

            }
        }

        private List<string> GetLines()
        {
            var lines = new List<string>();
            using (StreamReader sr = new StreamReader(@"H:\IBData\MessageList.csv"))
            {
                while (!sr.EndOfStream)
                {
                    lines.Add(sr.ReadLine());
                }
                sr.Close();
            }
            return lines;
        }
        private static IncomingBar GetIncomingBar(string line)
        {


            IncomingBar bar = new IncomingBar();

            if (line != null)
            {
                string[] arr = line.Split(',');
                int i = 0;

                bar.Id = 1;
                bar.ReqId = Convert.ToInt32(arr[i++]);
                bar.Time = Convert.ToInt64(arr[i++]);
                bar.BarStartTime = TimeStampConverter.ToDateTime(bar.Time);
                bar.ConId = 215465490;
                bar.Open = Convert.ToDouble(arr[i++]);
                bar.High = Convert.ToDouble(arr[i++]);
                bar.Low = Convert.ToDouble(arr[i++]);
                bar.Close = Convert.ToDouble(arr[i++]);
                bar.Volume = Convert.ToInt64(arr[i++]);
                bar.Wap = Convert.ToDouble(arr[i++]);
                bar.Count = Convert.ToInt32(arr[i++]);
            }
            return bar;
        }

        private void RedisToCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            string redislistfile = "redislist.csv";
            label1.Text = "Getting ContractQuote list from Redis";
            Refresh();
            var getter = new QuoteGetter(new ContractQuoteConnectedRepository(), new DBContractsConnectedRepository());
            List<ContractQuote> list = getter.GetList();

            label1.Text = $"Saving ContractQuote list from Redis to {redislistfile}";
            Refresh();
            getter.SaveCsv(list, redislistfile);
            label1.Text = $"Saved to {redislistfile}";
            Refresh();
            

        }

        private void QuotesToCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var info = GetFeedFilename();
            QuoteReceiverAsync receiver = new QuoteReceiverAsync();
            // kick it off in a separate thread
            CancellationTokenSource source = new CancellationTokenSource();
            CancellationToken token = source.Token;
            Task.Run(() => receiver.FeedFromRedisAsync(info.Name), token);

        }


        private FileInfo GetFeedFilename()
        {
            openFileDialogFeed.CheckFileExists = true;
            openFileDialogFeed.InitialDirectory = @"H:\IBData\";
            DialogResult result = openFileDialogFeed.ShowDialog();
            if (result == DialogResult.OK)
            {
                return new FileInfo(openFileDialogFeed.FileName);
            }
            return null;
        }

        private void FutureContractsCsvToolStripMenuItem_Click(object sender, EventArgs e)
        {
            using (var redis = new ContractSqlRedis())
            {
                var serialized = CsvSerializer.Serialize(",", redis.GetListFromSqlLaterThanToday());
                IO.WriteStringList(serialized, "contract.csv");
            }
        }

        private void ReceiveQuotesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var quoteReciever = new QuoteReceiver();
            quoteReciever.Subscribe();

        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveAndUnsubscribe();
        }
    }
}

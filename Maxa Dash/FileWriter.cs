using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;

namespace Maxa_Dash
{
    public class FileWriter
    {
        string filePath;
        private string fileName = "\\Maxa recorded data" + DateTime.Now.ToLongDateString() + ".csv";
        bool isHeadersInitililzed = false;
        public Dictionary<string, string> dataDictionary = new Dictionary<string, string>();

        public FileWriter(string filePath)
        {
            this.filePath = filePath + fileName;
            dataDictionary["Time"] = DateTime.Now.ToString();
        }

        public void InitilizeFileHeaders()
        {
            string headlines = string.Join(",", dataDictionary.Keys);
            try
            {
                using (StreamWriter file = new StreamWriter(@filePath, true))
                {
                    file.WriteLine(headlines);
                    isHeadersInitililzed = true;
                }
            }
            catch
            {

            }
        }

        public void WriteToFile()
        {
            dataDictionary["Time"] = DateTime.Now.ToString();
            //WriteToCSV();
            Thread thread = new Thread(WriteToCSV);
            thread.Start();
        }


        private void WriteToCSV()
        {
            if (!isHeadersInitililzed) 
                InitilizeFileHeaders();

            string data = string.Join(",", dataDictionary.Values);
            try
            {
                using (StreamWriter file = new StreamWriter(@filePath, true))
                {
                    file.WriteLine(data);
                }
            }
            catch (Exception e)
            {
                MessageBox.Show("cant write " + e.ToString());
            }
        }
    }
}

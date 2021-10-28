using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;


/// <summary>
/// This class is responsible for writing recorded data to a csv file.
/// Initilizing an object of this class will create the ourput file path.
/// <param name="dataDictionary">This public dictionary will get data from other classes using a FileWriter object</param>
/// </summary>
namespace Maxa_Dash
{
    public class FileWriter
    {
        private string filePath;
        //private string fileName = "\\Maxa recorded data" + DateTime.Now.ToLongDateString() + ".csv";
        private bool isHeadersInitililzed = false;
        public Dictionary<string, string> dataDictionary = new Dictionary<string, string>();

        /// <summary>
        /// This function initilizes an object of the FileWriter class
        /// and adds time parameter to the dictionary (so it appears in the first column of the csv file) 
        /// </summary>
        /// <param name="filePath">This string specifies the location to save the csv file</param>
        public FileWriter(string filePath)
        {
            this.filePath = filePath + ".csv";
            dataDictionary["Time"] = DateTime.Now.ToString();
        }

        /// <summary>
        /// This function is called once to write the headers to the csv file before writing the first data points
        /// </summary>
        private void InitilizeFileHeaders()
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

        /// <summary>
        /// This function is the public function used to write data from the dataDictionary to the csv file.
        /// It initilizes a new thread to perform the actual writing.
        /// </summary>
        public void WriteToFile()
        {
            dataDictionary["Time"] = DateTime.Now.ToString();
            //WriteToCSV();
            Thread thread = new Thread(WriteToCSV);
            thread.Start();
        }

        /// <summary>
        /// This function is called by the public function WriteToFile as a new thread.
        /// It writes all the data from dataDictionary as a new line to the csv file.
        /// In the first iteration it calls InitilizeFileHeaders() to initiliza the headers for the data points.
        /// displays an error message box when an exception occurs.
        /// </summary>
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

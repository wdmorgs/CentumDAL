using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;

using System.Data;
using Microsoft.VisualBasic.FileIO; 

namespace CentumDisDAL
{
    public class CFHDisLayer
    {
        public bool ImportCFHCSVFile(string csvFilePath)
        {
            DataTable csvData = new DataTable();
            long lnNumAlarms = 0;

            /*Debug.Listeners.Add(new TextWriterTraceListener(Console.Out));
            Debug.AutoFlush = true;
            Debug.Indent(); */

            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(csvFilePath))
                {
                    csvReader.SetDelimiters(new string[] { "," });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields = csvReader.ReadFields();
                    foreach (string column in colFields)
                    {
                        DataColumn datecolumn = new DataColumn(column);
                        datecolumn.AllowDBNull = true;
                        csvData.Columns.Add(datecolumn);
                    }
                    while (!csvReader.EndOfData)
                    {
                        string[] fieldData = csvReader.ReadFields();
                        //Making empty value as null
                        for (int i = 0; i < fieldData.Length; i++)
                        {
                            if (fieldData[i] == "")
                            {
                                fieldData[i] = null;
                            }
                        }
                        csvData.Rows.Add(fieldData);
                        Debug.WriteLine("{0}, {1}, {2}", fieldData[0].ToString(), fieldData[1].ToString(), fieldData[2].ToString());
                        lnNumAlarms += 1;
                    }
                    Debug.WriteLine("{0} Alarms Imported", lnNumAlarms);
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}


using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;

namespace CSResults.LoadData
{
    public class LoadExceltoDB
    {

        //Method to connect and get data from excel
        public static DataSet getDataFromExcel(string connection, string queryStatement)
        {
            OleDbConnection con = new OleDbConnection(connection);
            OleDbCommand cmd = new OleDbCommand();
            DataSet dataSet = new DataSet();

            //Try opening the excel file
            try
            {
                con.Open();

            }
            catch (OleDbException e)
            {
                Console.WriteLine("Unable to connect to the excel data file");
            }

            //Try retriving the data from the excel file once opened
            try
            {
                cmd.Connection = con;
                OleDbDataAdapter adapter = new OleDbDataAdapter(queryStatement, con);
                adapter.Fill(dataSet, "Results");
                con.Close();

            }
            catch (OleDbException e)
            {
                Console.WriteLine("Able to connect to excel file but unable to" +
                                    "retrieve data from the file using the SQL command");
            }

            return dataSet;
        }
    }
}
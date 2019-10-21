using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.OleDb;
using System.Data;
using System.IO;
using CSResults.Models;
using System.Data.Entity.Migrations;
using System.Reflection;

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

        //Gets the root path of the project and combine it with the specified path ending to get the file path of the dataset
        public static string getDataPath(String pathEnding)
        {
            //Gets the path of the root project folder
            DirectoryInfo info = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string rootpath = info.Parent.FullName;

            //Combines the root folder with the path ending to return the full filepath
            return Path.Combine(rootpath, pathEnding);
        }

        //Saves the module data to the database using the module object
        public static void saveModule(DataRow row, CSResults.DAL.ModuleContext context)
        {
            Models.Module md = new Models.Module()
            {
                moduleID = row["Module Code"].ToString(),
                moduleName = row["Module Name"].ToString()
            };
            //Adds the module name and code if it does not exist in the databse
            context.Module.AddOrUpdate(x => x.moduleID, md);

            context.SaveChanges();
        }

        //Saves the results data to the database using the result object
        public static void saveResult (DataRow row, DataTable tbl, CSResults.DAL.ModuleContext context, IDictionary<string, string> dict)
        {
            
            Result res = new Result();

            foreach (DataColumn column in tbl.Columns)
            {
                //Get the column name
                string colName = column.ColumnName.ToString();

                if (dict.ContainsKey(colName))
                {
                    //Get the equivalent result object property name
                    string resProp = dict[colName];

                    PropertyInfo prop = res.GetType().GetProperty(resProp);

                    //Checks if the property of the data we are storing is a nullable double
                    if (prop.PropertyType == typeof(Nullable<Double>))
                    {
                        string data = row[colName].ToString();

                        //Checks if the data is a percentage
                        if (data.Contains("%"))
                        {
                            //Removes the character % from the percentage and converts percentage number to decimal
                            double? dataPercent = Convert.ToDouble(data.Substring(0, data.Length - 1)) / 100;
                            prop.SetValue(res, dataPercent);
                        }
                        else
                        {
                            double? dataNo = Convert.ToDouble(data);
                            prop.SetValue(res, dataNo);
                        }

                    }
                    else
                    {
                        prop.SetValue(res, row[colName].ToString());
                    }

                }

            }
            //Adds the module results if it does not exist in the databse
            context.Result.AddOrUpdate(x => new { x.modID, x.year }, res);
            context.SaveChanges();

        }


    }
}
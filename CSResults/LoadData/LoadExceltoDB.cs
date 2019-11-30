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
        public static void saveResult (DataRow row, CSResults.DAL.ModuleContext context, IDictionary<string, string> dict)
        {
            
            Result res = new Result();

            foreach (var item in dict){

                //Gets the data and the resulting property name from the dictionary
                string data = row[item.Key].ToString();
                string resProp = item.Value;
                setProp(res, resProp, data);
            }

            //Adds the module results if it does not exist in the databse
            context.Result.AddOrUpdate(x => new { x.moduleID, x.year }, res);
            context.SaveChanges();

        }

        //Assigns data to specified property in the result object
        public static void setProp (Result result, string propName, string data)
        {
            PropertyInfo prop = result.GetType().GetProperty(propName);

            //Checks if the property of the data we are storing is a nullable double
            if (prop.PropertyType == typeof(Nullable<Double>))
            {

                //Checks if the data is a percentage
                if (data.Contains("%"))
                {
                    //Removes the character % from the percentage and converts percentage number to decimal
                    double? dataPercent = Convert.ToDouble(data.Substring(0, data.Length - 1)) / 100;
                    prop.SetValue(result, dataPercent);
                }
                else
                {
                    double? dataNo = Math.Round(Convert.ToDouble(data),2);
                    prop.SetValue(result, dataNo);
                }

            }
            else
            {
                prop.SetValue(result, data);
            }

        }


    }
}
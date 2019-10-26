namespace CSResults.Migrations
{
    using CSResults.LoadData;
    using CSResults.Models;
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.Data.Entity.Migrations;
    using System.IO;



    internal sealed class Configuration : DbMigrationsConfiguration<CSResults.DAL.ModuleContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(CSResults.DAL.ModuleContext context)
        {
            //Debugger added to enable me to debug the seed method
            //if (System.Diagnostics.Debugger.IsAttached == false) { System.Diagnostics.Debugger.Launch(); }


            //Gets the path of where the excel file is
            string path = LoadExceltoDB.getDataPath("LoadData\\Results.xlsx");


            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"";

            // Select all data from the results worksheet where there is enough data
            string selectString = "SELECT * FROM [Results$] WHERE [% 0_30] <> 'Fewer than 10 enrolled'" +
                                    "AND [% 0_30] <> 'Not enough info'";

            //Gets excel data and store in a dataset
            DataSet excelDS = LoadExceltoDB.getDataFromExcel(connectionString, selectString);


            IDictionary<string, string> resultHeaders = new Dictionary<string, string>() {
                                                {"Module Code","modID"},
                                                {"Module Name", "modName"},
                                                {"Year","year"},
                                                {"Average","mean" },
                                                { "Median" ,"median"},
                                                {"% 0_30","below30" },
                                                {"% 30_39","below40" },
                                                {"% 40_49","below50" },
                                                {"% 50_59","below60" },
                                                {"% 60_69","below70" },
                                                {"%70_79","below80" },
                                                {"% 80 +","above80" }
            };

            foreach (System.Data.DataTable table in excelDS.Tables)
            {
                //Loop through each row to store each row into the database
                foreach (DataRow row in table.Rows)
                {
                    //Saves the data for the modules table
                    LoadExceltoDB.saveModule(row, context);

                    //Saves the data for the results table
                    LoadExceltoDB.saveResult(row,context, resultHeaders);
                }
            }
            
        }
    }
}

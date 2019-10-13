namespace CSResults.Migrations
{
    using CSResults.LoadData;
    using CSResults.Models;
    using System;
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


            //Gets the path of the root project folder
            DirectoryInfo info = new DirectoryInfo(AppDomain.CurrentDomain.BaseDirectory);
            string rootpath = info.Parent.FullName;
            string path = Path.Combine(rootpath, @"LoadData\Results.xlsx");

            string connectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path + ";Extended Properties=\"Excel 12.0 Xml;HDR=YES;IMEX=1;\"";

            // Select all data from the results worksheet where there is enough data
            string selectString = "SELECT * FROM [Results$] WHERE [% 0_30] <> 'Fewer than 10 enrolled'" +
                                    "AND [% 0_30] <> 'Not enough info'";

            //Gets excel data and store in a dataset
            DataSet excelDS = LoadExceltoDB.getDataFromExcel(connectionString, selectString);

            foreach (System.Data.DataTable table in excelDS.Tables)
            {
                //Loop through each row to store each row into the database
                foreach (DataRow row in table.Rows)
                {
                    Module md = new Module()
                    {
                        moduleID = row["Module Code"].ToString(),
                        moduleName = row["Module Name"].ToString()
                    };
                    //Adds the module name and code if it does not exist in the databse
                    context.Module.AddOrUpdate(x => x.moduleID,md);
                    
                    context.SaveChanges();

                    Result res = new Result()
                    {
                        modID = row["Module Code"].ToString(),
                        modName = row["Module Name"].ToString(),
                        year = row["Year"].ToString(),
                        mean = Convert.ToDouble(row["Average"].ToString()),
                        median = Convert.ToDouble(row["Median"].ToString()),
                        //Removes the character % from the percentage and converts percentage number to decimal
                        below30 = Convert.ToDouble(row["% 0_30"].ToString().Substring(0, row["% 0_30"].ToString().Length - 1)) / 100,
                        above80 = Convert.ToDouble(row["% 80 +"].ToString().Substring(0, row["% 80 +"].ToString().Length - 1)) / 100
                    };

                    //Adds the module results if it does not exist in the databse
                    context.Result.AddOrUpdate(x => new { x.modID,x.year },res);
                    context.SaveChanges();
                }
            }
            
        }
    }
}

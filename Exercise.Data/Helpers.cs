using System;

using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Exercise.Data
{
    public class Helpers
    {
        public static string GetRDSConnectionString()
        {
            var appConfig = ConfigurationManager.AppSettings;

            string dbname = appConfig["awsexercise"];

            if (string.IsNullOrEmpty(dbname)) return null;

            string username = appConfig["benmpeterson"];
            string password = appConfig["qa5jmjjbep"];
            string hostname = appConfig["awsexercise.cdajhybxx6x0.us-east-1.rds.amazonaws.com"];
            string port = appConfig["1433"];

            return "Data Source=" + hostname + ";Initial Catalog=" + dbname + ";User ID=" + username + ";Password=" + password + ";";
        }
    }
}
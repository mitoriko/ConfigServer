using Com.ACBC.Framework.Database;
using ConfigServer.Buss;
using ConfigServer.Dao;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigServer.Common
{
    public class Global
    {
        public const string ROUTE_PX = "/api/config";
        public static BaseBuss BUSS = new BaseBuss();
        public static Dictionary<string, Dictionary<string, ConfigGroup>> ConfigList;

        public static void StartUp()
        {
            if (DatabaseOperationWeb.TYPE == null)
            {
                DatabaseOperationWeb.TYPE = new DBManager();
            }

            new ConfigDao().GetConfigAll();

        }

        public static string DBUrl
        {
            get
            {
                return Environment.GetEnvironmentVariable("DBUrl");
            }
        }

        public static string DBUser
        {
            get
            {
                return Environment.GetEnvironmentVariable("DBUser");
            }
        }

        public static string DBPort
        {
            get
            {
                return Environment.GetEnvironmentVariable("DBPort");
            }
        }

        public static string DBPassword
        {
            get
            {
                return Environment.GetEnvironmentVariable("DBPassword");
            }
        }
    }
}

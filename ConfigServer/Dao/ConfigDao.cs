﻿using Com.ACBC.Framework.Database;
using ConfigServer.Buss;
using ConfigServer.Common;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConfigServer.Dao
{
    public class ConfigDao
    {
        public List<ConfigItem> GetConfigList(string env, string group)
        {
            List<ConfigItem> list = new List<ConfigItem>();
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(ConfigSqls.SELECT_CONFIG, env, group);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null)
            {
                foreach(DataRow dr in dt.Rows)
                {
                    ConfigItem ConfigItem = new ConfigItem
                    {
                        key = dr["CONFIG_KEY"].ToString(),
                        value = dr["CONFIG_VALUE"].ToString(),
                    };
                    list.Add(ConfigItem);
                }
                
            }

            return list;
        }

        public void GetConfigAll()
        {
            StringBuilder builder = new StringBuilder();
            builder.AppendFormat(ConfigSqls.SELECT_CONFIG_ALL);
            string sql = builder.ToString();
            DataTable dt = DatabaseOperationWeb.ExecuteSelectDS(sql, "T").Tables[0];
            if (dt != null)
            {
                List<ConfigItem> list = new List<ConfigItem>();
                Global.ConfigList = new Dictionary<string, Dictionary<string, ConfigGroup>>();
                string env = "";
                string group = "";

                foreach (DataRow dr in dt.Rows)
                {
                    if(env != dr["CONFIG_ENV"].ToString())
                    {
                        env = dr["CONFIG_ENV"].ToString();
                        Global.ConfigList.Add(env, new Dictionary<string, ConfigGroup>());
                    }

                    if (group != dr["CONFIG_GROUP"].ToString())
                    {
                        group = dr["CONFIG_GROUP"].ToString();
                        Global.ConfigList[env].Add(group, new ConfigGroup(group));
                    }

                    var curList = Global.ConfigList[env][group].list;

                    ConfigItem ConfigItem = new ConfigItem
                    {
                        key = dr["CONFIG_KEY"].ToString(),
                        value = dr["CONFIG_VALUE"].ToString(),
                    };
                    curList.Add(ConfigItem);
                }
            }
        }
    }

    public class ConfigSqls
    {
        public const string SELECT_CONFIG = ""
                + "SELECT * "
                + "FROM T_BUSS_CONFIG "
                + "WHERE IF_USE = 1 "
                + "AND CONFIG_ENV = '{0}' "
                + "AND GROUP = '{1}' "
                + "ORDER BY CONFIG_KEY";
        public const string SELECT_CONFIG_ALL = ""
                + "SELECT * "
                + "FROM T_BUSS_CONFIG "
                + "WHERE IF_USE = 1 "
                + "ORDER BY CONFIG_ENV,CONFIG_GROUP,CONFIG_KEY";

    }
}

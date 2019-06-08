using ConfigServer.Common;
using ConfigServer.Dao;
using Newtonsoft.Json;
using StackExchange.Redis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ConfigServer.Buss
{
    public class DevBuss : IBuss
    {
        public ApiType GetApiType()
        {
            return ApiType.DevApi;
        }

        public object Do_GetConfig(BaseApi baseApi)
        {
            GetConfigParam getConfigParam = JsonConvert.DeserializeObject<GetConfigParam>(baseApi.param.ToString());
            if (getConfigParam == null)
            {
                throw new ApiException(CodeMessage.InvalidParam, "InvalidParam");
            }

            if(getConfigParam.env == "DEV")
            {
                if (Global.ConfigList.ContainsKey(getConfigParam.env))
                {
                    if (Global.ConfigList[getConfigParam.env].ContainsKey(getConfigParam.group))
                    {
                        return (Global.ConfigList[getConfigParam.env])[getConfigParam.group].list;
                    }
                }

                throw new ApiException(CodeMessage.InvalidEnvAndGroup, "InvalidEnvAndGroup");
            }

            throw new ApiException(CodeMessage.NoAccessForGroup, "NoAccessForGroup");

        }
    }
}

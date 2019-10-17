using System;
using FreeSql;
using JXCMS.Core.Exception;
using JXCMS.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;

namespace JXCMS.Core.Db
{
    public static class DbExtension
    {
        public static IHostBuilder ConfigFreeDb(this IHostBuilder builder, IConfiguration configuration)
        {
            var dbSettings = configuration.GetValue<DbConfig>("Db");
            return builder;
        }

        public static void SetDb(DbConfig dbConfig)
        {
            if (!dbConfig.DbType.IsNullOrEmpty() && Enum.TryParse(dbConfig.DbType, true, out DataType dataType))
            {
                switch (dataType)
                {
                    case DataType.MySql:
                        var connstr = $"data source={dbConfig.DbUrl};PORT={dbConfig.DbPort};database={dbConfig.DbName}; uid={dbConfig.Username};pwd={dbConfig.Password};";
                        BaseEntity.Initialization(new FreeSqlBuilder()
                            .UseAutoSyncStructure(false)
                            .UseNoneCommandParameter(true)
                            .UseConnectionString(dataType, connstr)
                            .Build());
                        break;
                    case DataType.SqlServer:
                        break;
                    case DataType.PostgreSQL:
                        break;
                    case DataType.Oracle:
                        break;
                    case DataType.Sqlite:
                        break;
                    default:
                        throw new CMSException("数据库类型不在指定范围内");
                }
            }
            throw new CMSException("数据库类型不在指定范围内");
        }
    }
}
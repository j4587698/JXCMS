using System;
using System.IO;
using System.Linq;
using FreeSql;
using JXCMS.Core.Exception;
using JXCMS.Core.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace JXCMS.Core.Db
{
    public static class DbExtension
    {
        public static IHostBuilder ConfigFreeDb(this IHostBuilder builder)
        {
            builder.ConfigureServices((context, collection) =>
            {
                var dbSettings = context.Configuration.GetSection("Db").Get<DbConfig>();
                SetDb(dbSettings, context.HostingEnvironment.IsDevelopment());
            });
            return builder;
        }

        public static IServiceCollection AddDb(this IServiceCollection service, IConfiguration configuration, bool isDevVersion)
        {
            var dbSettings = configuration.GetSection("Db").Get<DbConfig>();
            if (dbSettings != null)
            {
                var ret = SetDb(dbSettings, isDevVersion);
                if (!ret.isSuccess)
                {
                    throw new CMSException(ret.msg);
                }
            } 
            else if (File.Exists("install.lock"))
            {
                throw new CMSException("数据库配置错误，无数据库配置信息！");
            }
            return service;
        }

        public static (bool isSuccess, string msg) SetDb(DbConfig dbConfig, bool isDevVersion)
        {
            if (!dbConfig.DbType.IsNullOrEmpty() && Enum.TryParse(dbConfig.DbType, true, out DataType dataType))
            {
                switch (dataType)
                {
                    case DataType.MySql:
                        var connStr = $"data source={dbConfig.DbName};PORT={dbConfig.DbPort};database={dbConfig.DbName}; uid={dbConfig.Username};pwd={dbConfig.Password};";
                        BaseEntity.Initialization(new FreeSqlBuilder()
                            .UseAutoSyncStructure(isDevVersion)
                            .UseNoneCommandParameter(true)
                            .UseConnectionString(dataType, connStr)
                            .Build());
                        break;
                    case DataType.SqlServer:
                        break;
                    case DataType.PostgreSQL:
                        break;
                    case DataType.Oracle:
                        break;
                    case DataType.Sqlite:
                        BaseEntity.Initialization(new FreeSqlBuilder()
                            .UseAutoSyncStructure(isDevVersion)
                            .UseNoneCommandParameter(true)
                            .UseConnectionString(dataType, $"data source={dbConfig.DbName}")
                            .Build());
                        break;
                    default:
                        return (false, "数据库类型不在指定范围内");
                }

                if (!BaseEntity.Orm.Ado.MasterPool.IsAvailable)
                {
                    BaseEntity.Orm.Dispose();
                    return (false, "数据库连接失败");
                }

                BaseEntity.Orm.Aop.ConfigEntity = (s, e) =>
                {
                    e.ModifyResult.Name = dbConfig.Prefix + e.EntityType.Name.Replace("Entity", "");
                };
                return (true, "");
            }
            return (false, "数据库类型不在指定范围内");;
        }

        public static void InstallDb(DbConfig dbConfig, Action<bool, string> syncTable = null)
        {
            var types = AppDomain.CurrentDomain.GetAssemblies().SelectMany(x =>
                x.GetTypes().Where(y => y.BaseType != null && y.BaseType.IsGenericType && y.BaseType.GetGenericTypeDefinition() == typeof(BaseEntity<,>)
                                        && y.FullName != null && !y.FullName.Contains("FreeSql")));
            foreach (var type in types)
            {
                var isSuccess = BaseEntity.Orm.CodeFirst.SyncStructure(type);
                syncTable?.Invoke(isSuccess, type.Name);
                //Console.WriteLine(type.FullName);
            }
            string contentPath = AppContext.BaseDirectory + @"\"; ;   //項目根目錄
            var filePath = contentPath + "appsettings.json";
            JObject jsonObject;
            using (StreamReader file = new StreamReader(filePath))
            using (JsonTextReader reader = new JsonTextReader(file))
            {
                jsonObject = (JObject)JToken.ReadFrom(reader);
                jsonObject.Add("Db", JObject.FromObject(dbConfig));
            }

            using (var writer = new StreamWriter(filePath))
            using (JsonTextWriter jsonwriter = new JsonTextWriter(writer))
            {
                jsonwriter.Formatting = Formatting.Indented;
                jsonObject.WriteTo(jsonwriter);
            }
        }
    }
}
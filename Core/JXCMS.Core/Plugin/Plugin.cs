using System;
using System.Collections.Generic;
using System.IO;
using McMaster.NETCore.Plugins;

namespace JXCMS.Core.Plugin
{
    public class Plugin
    {
        private static readonly Dictionary<string, PluginLoader> Loaders = new Dictionary<string, PluginLoader>();
        
        public static PluginLoader GetOrLoadPlugin(string path, params Type[] types)
        {
            if (Loaders.ContainsKey(path))
            {
                return Loaders[path];
            }
            if (File.Exists(path) && Path.GetExtension(path).ToLower() == ".dll")
            {
                var loader = PluginLoader.CreateFromAssemblyFile(path, true, types);
                Loaders.Add(path, loader);
                return loader;
            }

            return null;
        }
    }
}
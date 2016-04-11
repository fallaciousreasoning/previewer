using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using PreviewerPlugins;

namespace Previewer.Core
{
    public class PluginRegistrar
    {
        private List<IFilePreviewer> plugins = new List<IFilePreviewer>();

        public PluginRegistrar()
        {
            
        }

        public void Initialize()
        {
            var pluginsAssembly = Assembly.ReflectionOnlyLoad("PreviewerPlugins");
            var types = pluginsAssembly.GetTypes();

            foreach (var type in types)
            {
                if (!type.IsClass || !typeof (IFilePreviewer).IsAssignableFrom(type)) continue;

                var created = (IFilePreviewer)Activator.CreateInstance(type);
                plugins.Add(created);
            }
        }

        public IFilePreviewer GetPreviewerForFile(string file)
        {
            return plugins.FirstOrDefault(p => p.CanPreview(file));
        }
    }
}

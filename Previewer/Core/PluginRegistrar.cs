using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using PreviewerPlugins;

namespace Previewer.Core
{
    public class PluginRegistrar
    {
        private List<IFilePreviewer> plugins = new List<IFilePreviewer>();

        public PluginRegistrar()
        {
            Initialize();
        }

        public void Initialize()
        {
            var pluginType = typeof (IFilePreviewer);

            var pluginsAssembly = pluginType.Assembly;
            var types = pluginsAssembly.GetTypes();

            foreach (var type in types)
            {
                if (!type.IsClass || !pluginType.IsAssignableFrom(type)) continue;

                var created = (IFilePreviewer)Activator.CreateInstance(type);
                plugins.Add(created);
            }
        }

        public Control GetPreviewerForFile(string file)
        {
            return plugins.FirstOrDefault(p => p.CanPreview(file))?.GetPreviewer(file);
        }
    }
}

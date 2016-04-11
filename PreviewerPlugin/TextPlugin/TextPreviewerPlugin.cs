using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PreviewerPlugins.TextPlugin
{
    public class TextPreviewerPlugin : IFilePreviewer
    {
        private static readonly string[] SupportedTypes =
        {
            ".txt",
            ".cs",
            ".cpp",
            ".ahk",
            ".bat",
            ".config",
            ".xml",
            ".xaml",
            ".csproj",
            ".ini"
        };

        private static TextPreviewer previewer; 

        public string Name { get { return "Text Previewer"; } }
        public string Author { get { return "Jay Harris"; } }
        public string Description { get { return "A plugin for viewing text files"; } }
        public string Version { get { return "0"; } }

        public bool CanPreview(string file)
        {
            return SupportedTypes.Any(file.EndsWith);
        }

        public Control GetPreviewer(string file)
        {
            return GetPreviewSynchronusly(file);
        }

        private Control GetPreviewSynchronusly(string file)
        {
            if (previewer == null)
                previewer = new TextPreviewer();

            var text = File.ReadAllText(file);
            previewer.ViewModel.Text = text;

            return previewer;
        }
    }
}

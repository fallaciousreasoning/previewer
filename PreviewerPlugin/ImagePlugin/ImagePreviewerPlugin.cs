using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PreviewerPlugins.ImagePlugin
{
    public class ImagePreviewerPlugin : IFilePreviewer
    {
        private static readonly string[] SupportedTypes =
        {
            ".jpg",
            ".jpeg",
            ".png",
            ".bmp",
            ".gif",
            ".tiff",
            ".ico",
        };

        private ImagePreviewer previewer;

        public string Name { get { return "Image Previewer"; } }
        public string Author { get { return "Jay Harris"; } }
        public string Description { get { return "A basic image previewer"; } }
        public string Version { get { return "0"; } }

        public bool CanPreview(string file)
        {
            var lower = file.ToLower();
            return SupportedTypes.Any(lower.EndsWith);
        }

        public Control GetPreviewer(string file)
        {
            if (previewer == null)
                previewer = new ImagePreviewer();

            previewer.ViewModel.ImageUri = file;
            return previewer;
        }
    }
}

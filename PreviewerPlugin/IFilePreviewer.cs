using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PreviewerPlugin
{
    public interface IFilePreviewer
    {
        string Author { get; set; }
        string Description { get; set; }
        string Version { get; set; }

        bool CanPreview(string file);
        Control GetPreviewer(string file);
    }
}

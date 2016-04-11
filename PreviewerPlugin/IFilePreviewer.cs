using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PreviewerPlugins
{
    public interface IFilePreviewer
    {
        string Name { get; }
        string Author { get; }
        string Description { get; }
        string Version { get; }

        bool CanPreview(string file);
        Control GetPreviewer(string file);
    }
}

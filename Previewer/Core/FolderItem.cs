using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Previewer.Core
{
    public class FolderItem
    {
        public DateTime ModifiedDate { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public bool IsFolder { get; set; }
        public long Size { get; set; }

        public override string ToString()
        {
            return $"[FolderItem: {Path} (IsFolder={IsFolder})]";
        }
    }
}

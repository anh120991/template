using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HDBH.Models.ViewModel
{
    public class AttachmentViewModel
    {
        public long fileId { get; set; }
        public long fileAutoId { get; set; }
        public string fileName { get; set; }
        public string filePath { get; set; }
        public string extention { get; set; }
        public bool isRemove { get; set; }
        public string downloadUrl { get; set; }
    }
}

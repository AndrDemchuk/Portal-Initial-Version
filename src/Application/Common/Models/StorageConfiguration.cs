using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Common.Models
{
    public class StorageConfiguration
    {
        private int _maxSize;
        public int MaxSize 
        { 
            get => _maxSize; 
            set => _maxSize = value * 1_048_576; 
        }
        public List<string> AllowedFileTypes { get; set; }

        public string Path { get; set; }
    }
}

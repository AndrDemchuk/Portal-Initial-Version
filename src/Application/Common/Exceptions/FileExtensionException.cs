using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Common.Exceptions
{
    public class FileExtensionException: Exception
    {
        public FileExtensionException()
            :base()
        {
        }

        public FileExtensionException(string message)
            :base(message)
        {
        }
    }
}

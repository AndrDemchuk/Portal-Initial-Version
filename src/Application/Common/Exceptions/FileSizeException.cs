using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Application.Common.Exceptions
{
    public class FileSizeException: Exception
    {
        public FileSizeException()
            :base()
        {
        }

        public FileSizeException(string message)
            :base(message)
        {
        }
    }
}

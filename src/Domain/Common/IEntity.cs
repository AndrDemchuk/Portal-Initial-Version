using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BvAcademyPortal.Domain.Common
{
    public interface IUseSoftDelete
    {
        bool IsDeleted { get; set; }
    }

    public interface IAuditable
    {
        DateTime Created { get; set; }
        string CreatedBy { get; set; }
        DateTime? LastModified { get; set; }
        string LastModifiedBy { get; set; }
    }

    public interface IIdentifiable<T>
    {
        [Key]
        T Id { get; set; }
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Common;

public abstract class BaseEntity
{
    public Guid Id { get; private init; }
    public DateTime Created { get; set; } = DateTime.Now;
    public DateTime? LastModified { get; set; }

    protected BaseEntity(Guid id)
    {
        Id = id;
    }

    protected BaseEntity() { }
}

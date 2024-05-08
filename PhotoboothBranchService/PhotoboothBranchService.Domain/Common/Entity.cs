﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhotoboothBranchService.Domain.Common;

public abstract class Entity
{
    public Guid Id { get; private init; }


    protected Entity(Guid id)
    {
        Id = id;
    }

    protected Entity() { }
}

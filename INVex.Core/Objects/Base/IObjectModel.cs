﻿using System;
using System.Collections.Generic;
using System.Text;

namespace INVex.Core.Objects.Base
{
    public interface IObjectModel
    {
        int Id { get; }
        string Name { get; }
        string Description { get; }
    }
}

using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public enum ServiceResultType
    {
        Success,
        NoContent,
        NotFound,
        AlreadyExists,
        Error
    }
}

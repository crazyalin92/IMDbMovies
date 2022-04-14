using System;
using System.Collections.Generic;
using System.Text;

namespace Services.Models
{
    public class ServiceResult<T>
    {
        public T Result { get; set; }
        public ServiceResultType Status { get; set; }
        public string Message { get; set; }
        public bool IsSuccess { get { return Status == ServiceResultType.Success; } }
    }
}

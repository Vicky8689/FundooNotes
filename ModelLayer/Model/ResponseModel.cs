using System;
using System.Collections.Generic;
using System.Text;

namespace ModelLayer.Model
{
    public class ResponseModel<T>
    {
        public bool Success { get; set; } = true;
        public string Message { get; set; } = "";

        public T Data { get; set; } = default(T);
    }
}

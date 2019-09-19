using Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Results
{
    public class ServiceResult
    {
        public string Message { get; set; }
        public ProcessStateEnum State { get; set; }

        public ServiceResult(ProcessStateEnum state, string message)
        {
            Message = message;
            State = state;
        }
        
    }
    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult(ProcessStateEnum state, string message, T result) : base(state, message)
        {
            Result = result;
        }
        public T Result { get; set; }

    }
}

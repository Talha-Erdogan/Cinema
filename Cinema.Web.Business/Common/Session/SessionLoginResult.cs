using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cinema.Web.Business.Common.Session
{
    public class SessionLoginResult
    {
        public bool IsSuccess { get; private set; }
        public string Message { get; private set; }
        public SessionLoginResult(bool isSuccess, string message)
        {
            IsSuccess = isSuccess;
            Message = message;
        }

    }
}

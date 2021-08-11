using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Utilities.Results
{
    public class Result:IResult
    {
        //Kod tekrarı olmaması için this ile kendi içerisindeki tek parametreli constructora this ile gönderme yapılır.
        public Result(bool success, string message):this(success)
        {
            //getter readonly ama ilk başta constructorda set edilebilir.
            Message = message;
        }
        public Result(bool success)
        {
            //getter readonly ama ilk başta constructorda set edilebilir.
            Success = success;
        }
        public bool Success { get; }
        public string Message { get; }
    }
}

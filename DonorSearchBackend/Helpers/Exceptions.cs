using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DonorSearchBackend.Helpers
{
    public enum ExceptionEnum
    {
        DBException,
        DonorsearchApiException,
        NotRightJSONFormat,
        EmptyNonRequiredParameter

    }
    public class Result
    {
        public bool isSuccess;
        public string error;
        public string errorDescription;
        public Result(bool _isSuccess, string _error = null, string _descr = null)
        {
            error = _error;
            isSuccess = _isSuccess;
            errorDescription = _descr;
        }
    }
    public static class ResultHelper
    {
      public static string Error(ExceptionEnum type, string description = null)
        { return JsonConvert.SerializeObject(new Result(false, type.ToString(),description)); }

        public static string Success()
        { return JsonConvert.SerializeObject(new Result(true)); }
    }
}

using Newtonsoft.Json;

namespace DonorSearchBackend.Helpers
{
    public enum ExceptionEnum
    {
        DBException,
        DonorsearchApiException,
        NotRightJSONFormat,
        EmptyNonRequiredParameter,
        WrongRequest

    }
    public class ErrorsResult
    {
        public string error;
        public string errorDescription;
        public ErrorsResult(bool _isSuccess, string _error = null, string _descr = null)
        {
            error = _error;
            errorDescription = _descr;
        }
    }
    public class OkResult
    {
        public bool isSuccess;
        public OkResult(bool _isSuccess)
        {
            isSuccess = _isSuccess;
        }
    }
    public static class ResultHelper
    {
      public static string Error(ExceptionEnum type, string description = null)
        { return JsonConvert.SerializeObject(new ErrorsResult(false, type.ToString(),description)); }

        public static string Success()
        { return JsonConvert.SerializeObject(new OkResult(true)); }
    }
}

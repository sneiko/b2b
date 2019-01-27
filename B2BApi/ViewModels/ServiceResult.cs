using B2BApi.Enums;

namespace B2BApi.ViewModels
{
    public class ServiceResult
    {
        public ServiceResult(ResultStatus status, string message = null)
        {
            Status = status;
            Message = message;
        }

        public ResultStatus Status { get; }
    
        public string Message { get; }
    }

    public class ServiceResult<T> : ServiceResult
    {
        public ServiceResult(T resultObject, ResultStatus status, string message = null)
            : base(status, message)
            => ResultObject = resultObject;

        public T ResultObject { get; }
    }
}
using AppCore.Bussiness.Models.Results.Bases;
using AppCore.Bussiness.Models.Results.Enums;

namespace AppCore.Bussiness.Models.Results
{
    public abstract class Result
    {
        public ResultStatus Status { get; set; }
        public string Message { get; set; }
        public Result(ResultStatus satatus, string message)
        {
            Status = satatus;
            Message = message;
        }
    }
    public abstract class Result<TResultType>: Result, IResultData<TResultType>
    {
        public TResultType Data { get; }
        public Result(ResultStatus status, string message,  TResultType data): base(status, message)
        {
            Data = data;
        }
    }
}

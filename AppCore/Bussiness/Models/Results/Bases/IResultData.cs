namespace AppCore.Bussiness.Models.Results.Bases
{
    public interface IResultData<out TResultType>
    {
        TResultType Data { get; }
    }
}

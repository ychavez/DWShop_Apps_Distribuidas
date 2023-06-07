namespace DWShop.Shared.Wrapper
{
    public interface IResult
    {
        List<string> Messages { get; set; }
        bool Succeded { get; set; }
    }

    public interface IResult<out T> : IResult 
    {
        T Data { get; }
    }
}

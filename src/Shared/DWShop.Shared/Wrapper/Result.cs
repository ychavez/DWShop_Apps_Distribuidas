namespace DWShop.Shared.Wrapper
{
    public class Result : IResult
    {
        public List<string> Messages { get; set; } = new();
        public bool Succeded { get; set; }

        public static IResult Fail()
        {
            return new Result { Succeded = false };
        }

        public static IResult Fail(string message)
        {
            return new Result { Succeded = false, Messages = new() { message } };
        }

        public static IResult Fail(List<string> messages)
        {
            return new Result { Succeded = false, Messages = messages };
        }

        public static Task<IResult> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public static Task<IResult> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public static Task<IResult> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }

        public static IResult Success()
        {
            return new Result { Succeded = true };
        }

        public static IResult Success(string message)
        {
            return new Result { Succeded = true, Messages = new() { message } };
        }

        public static Task<IResult> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public static Task<IResult> SuccessAsync(string message)
        {
            return Task.FromResult(Success(message));
        }
    }

    public class Result<T> : Result, IResult<T>
    {
        public T Data { get; set; }

        public new static Result<T> Fail()
        {
            return new Result<T> { Succeded = false };
        }

        public new static Result<T> Fail(string message)
        {
            return new Result<T> { Succeded = false, Messages = new() { message } };
        }

        public new static Result<T> Fail(List<string> messages)
        {
            return new Result<T> { Succeded = false, Messages = messages };
        }

        public new static Task<Result<T>> FailAsync()
        {
            return Task.FromResult(Fail());
        }

        public new static Task<Result<T>> FailAsync(string message)
        {
            return Task.FromResult(Fail(message));
        }

        public new static Task<Result<T>> FailAsync(List<string> messages)
        {
            return Task.FromResult(Fail(messages));
        }

        public new static Result<T> Success()
        {
            return new Result<T> { Succeded = true };
        }

        public new static Result<T> Success(T Data, string message)
        {
            return new Result<T>
            {
                Succeded = true,
                Messages = new() { message },
                Data = Data
            };
        }

        public new static Task<Result<T>> SuccessAsync()
        {
            return Task.FromResult(Success());
        }

        public new static Task<Result<T>> SuccessAsync(T Data, string message="")
        {
            return Task.FromResult(Success(Data,message));
        }
    }
}

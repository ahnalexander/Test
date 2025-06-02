namespace MyApp.Application.Common.Results;

public class Result
{
    protected Result(bool success, string error)
    {
        Success = success;
        Error = error;
    }

    public bool Success { get; }
    public string Error { get; }
    public bool Failure => !Success;

    public static Result Ok() => new(true, string.Empty);
    public static Result Fail(string error) => new(false, error);
    public static Result<T> Ok<T>(T value) => Result<T>.Ok(value);
    public static Result<T> Fail<T>(string error) => Result<T>.Fail(error);
}

public class Result<T> : Result
{
    private readonly T? _value;

    protected internal Result(T? value, bool success, string error) 
        : base(success, error)
    {
        _value = value;
    }

    public T Value => Success 
        ? _value! 
        : throw new InvalidOperationException("Cannot access Value of failed result");

    public static Result<T> Ok(T value) => new(value, true, string.Empty);
    public static new Result<T> Fail(string error) => new(default, false, error);
}
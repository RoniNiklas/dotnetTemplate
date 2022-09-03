using System.Text.Json.Serialization;

namespace Shared.DTOs;
public record RequestResult<T>
{
    public T? Data { get; init; }
    public ValidationError? ValidationError { get; init; }
    public RequestResult(T data) => Data = data;
    public RequestResult(ValidationError error) => ValidationError = error;

    [JsonConstructor]
    public RequestResult() { }

    public void Switch(Action<T> f0, Action<ValidationError> f1)
    {
        if (Data is not null)
        {
            f0(Data);
        }
        if (ValidationError is not null)
        {
            f1(ValidationError);
        }
    }

    public TResult Match<TResult>(Func<T, TResult> f0, Func<ValidationError, TResult> f1)
    {
        if (Data is not null)
        {
            return f0(Data);
        }

        if (ValidationError is not null)
        {
            return f1(ValidationError);
        }

        throw new ArgumentException($"Neither a {typeof(T).Name} or {nameof(ValidationError)} was provided for the Match-method");
    }
}

public abstract record RequestResult
{
    public static RequestResult<T> Success<T>(T data) => new(data);
    public static RequestResult<T> Invalid<T>(ValidationError error) => new(error);
    public static RequestResult<T> Invalid<T>(Dictionary<string, string> errors) => new(new ValidationError(errors));
}

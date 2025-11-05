namespace Common.Domain.Shared;

/// <summary>
/// Represents the result of an operation, which can be either successful or a failure.
/// </summary>
public class ResponseModel
{

    #region Props
    /// <summary>
    /// Indicates whether the operation was successful.
    /// </summary>
    public bool IsSuccess { get; }
    /// <summary>
    /// Gets a message describing the result of the operation.
    /// </summary>
    public string Message { get; } 
    #endregion

    #region CTOR
    /// <summary>
    /// Initializes a new instance of the <see cref="ResponseModel"/> class for a successful operation.
    /// </summary>
    protected internal ResponseModel(bool isSuccess, string? message = null)
    {
        IsSuccess = isSuccess;
        Message = message ?? (isSuccess ? "Success" : "Failure");
    } 
    #endregion


    /// <summary>
    /// Creates a successful result without any additional data.
    /// </summary>
    public static ResponseModel Success()
        => new(true);

    /// <summary>
    /// Creates a successful result with the specified value.
    /// </summary>
    public static ResponseModel<TValue> Success<TValue>(TValue value)
        => new(value);

    public static ResponseModel<TValue> Success<TValue>(TValue value, string message)
         => new(value, message);

    /// <summary>
    /// Creates a successful result with the specified value and total count.
    /// </summary>
    public static ResponseModel<TValue> Success<TValue>(TValue value, int totalCount)
        => new(value, totalCount);


    /// <summary>
    /// Creates a failure result with the specified message.
    /// </summary>
    public static ResponseModel Failure(string message)
        => new(false, message);

    /// <summary>
    /// Creates a failure result with the specified message.
    /// </summary>
    public static ResponseModel<TValue> Failure<TValue>(string message) =>
       new(default!, message);



    /// <summary>
    /// Creates a result based on the specified value.
    /// If the value is not null, creates a successful result; otherwise, creates a failure result.
    /// </summary>
    public static ResponseModel<TValue> Create<TValue>(TValue? value) =>
        value is not null ? Success(value) : Failure<TValue>("null value");
}

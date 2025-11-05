namespace Common.Domain.Shared;

/// <summary>
/// Represents the result of an operation that includes a value.
/// </summary>
public class ResponseModel<TData> : ResponseModel
{
    #region Props
    /// <summary>
    /// Gets the total count associated with the result.
    /// </summary>
    public int? TotalCount { get; }

    /// <summary>
    /// Gets the value associated with the result.
    /// </summary>
    public TData Data { get; }
    #endregion

    #region CTOR
    /// <summary>
    /// Initializes a new instance of the <see cref="ResponseModel{TData}"/> class for a successful operation.
    /// </summary>
    protected internal ResponseModel(TData data) : base(true)
    {
        Data = data;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResponseModel{TData}"/> class for a successful operation with total count.
    /// </summary>
    protected internal ResponseModel(TData data, int totalCount) : base(true)
    {
        Data = data;
        TotalCount = totalCount;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="ResponseModel{TValue}"/> class for a failure operation.
    /// </summary>
    protected internal ResponseModel(TData data, string message) : base(false, message)
    {
        Data = data;
    }
    #endregion



    /// <summary>
    /// Implicitly converts a value to a result.
    /// If the value is not null, creates a successful result; otherwise, creates a failure result.
    /// </summary>
    public static implicit operator ResponseModel<TData>(TData? value) => Create(value);
}

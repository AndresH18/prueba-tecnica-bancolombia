namespace BancolombiaExtractos.Helpers;

/// <summary>
/// Struct to encapsulate a result value
/// </summary>
/// <typeparam name="TValue">The type of result to hold</typeparam>
public readonly struct Result<TValue>
{
    /// <summary>
    /// Creates a new <b>success</b> instance that holds the <paramref name="value"/>
    /// </summary>
    /// <param name="value">The value to hold</param>
    public Result(TValue value)
    {
        Value = value;

        IsError = false;
    }

    /// <summary>
    /// Creates a new <b>error</b> instance.
    /// </summary>
    public Result()
    {
        IsError = false;
    }

    /// <summary>
    /// The value that represents the result
    /// </summary>
    public TValue? Value { get; }

    /// <summary>
    /// Returns Whether the Result is an <b>Error</b>
    /// </summary>
    public bool IsError { get; }

    /// <summary>
    /// Returns Whether the Result is a <b>Success</b>
    /// </summary>
    public bool IsSuccess => !IsError;

    /// <summary>
    /// Implicit operator to convert a <typeparamref name="TValue"/> into a new <i>success</i> instance of <see cref="Result{TValue}"/>
    /// </summary>
    /// <param name="value">The success value to hold</param>
    /// <returns>A new <i>success</i> instance of <see cref="Result{TValue}"/></returns>
    public static implicit operator Result<TValue>(TValue value) => new(value);

    // Boolean operators
    public static bool operator true(Result<TValue> r) => r.IsSuccess;
    public static bool operator false(Result<TValue> r) => r.IsError;
    public static bool operator !(Result<TValue> r) => r.IsError;
}
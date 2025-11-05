namespace Common.Domain.Shared;

public interface IValidationResult
{
    string[] ErrorMessages { get; }
}

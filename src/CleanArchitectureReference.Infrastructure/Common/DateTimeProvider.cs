using CleanArchitectureReference.Application.Common.Abstractions;

namespace CleanArchitectureReference.Infrastructure.Common;

public sealed class DateTimeProvider : IDateTimeProvider
{
    public DateTimeOffset UtcNow => DateTimeOffset.UtcNow;
}

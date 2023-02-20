using Moq.Language.Flow;

namespace ZenCode.Common.Testing.Extensions;

public static class SetupExtensions
{
    public static IReturnsResult<TMock> ReturnsSequence<TMock, TResult>(this ISetup<TMock, TResult> setup,
        IReadOnlyList<TResult> sequence) where TMock : class
    {
        var callCount = 0;

        return setup.Returns(() => sequence[callCount++]);
    }
}
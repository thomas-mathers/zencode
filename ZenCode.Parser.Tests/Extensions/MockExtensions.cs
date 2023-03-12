using Moq.Language.Flow;

namespace ZenCode.Parser.Tests.Extensions;

public static class MockOfExpressionExtensions
{
    public static IReturnsResult<TMock> ReturnsSequence<TMock, TResult>(this ISetup<TMock, TResult> setup,
        params TResult[] sequence) where TMock : class
    {
        var index = 0;

        return setup.Returns(() => sequence[index++]);
    }
}
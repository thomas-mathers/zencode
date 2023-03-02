using Moq.Language.Flow;
using ZenCode.Lexer.Abstractions;

namespace ZenCode.Parser.Tests.Extensions;

public static class MockOfExpressionExtensions
{
    public static ICallbackResult ConsumesToken<TMock>(this IReturnsResult<TMock> returns, ITokenStream tokenStream)
        where TMock : class
    {
        return returns.Callback(() => tokenStream.Consume());
    }

    public static IReturnsResult<TMock> ReturnsSequence<TMock, TResult>(this ISetup<TMock, TResult> setup,
        params TResult[] sequence) where TMock : class
    {
        var index = 0;

        return setup.Returns(() => sequence[index++]);
    }
}
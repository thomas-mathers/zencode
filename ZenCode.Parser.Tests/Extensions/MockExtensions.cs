using Moq;
using Moq.Language.Flow;
using ZenCode.Grammar.Expressions;
using ZenCode.Grammar.Statements;
using ZenCode.Lexer;
using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;

namespace ZenCode.Parser.Tests.Extensions;

public static class MockOfExpressionExtensions
{
    public static ICallbackResult ReturnsExpression(this Mock<IExpressionParser> mock, Expression expression)
    {
        return mock
            .Setup(x => x.Parse(It.IsAny<TokenStream>(), It.IsAny<int>()))
            .Returns(() => expression)
            .Callback<ITokenStream, int>((tokenStream, _) => tokenStream.Consume());
    }
    
    public static ICallbackResult ReturnsExpressionSequence(this Mock<IExpressionListParser> mock, IReadOnlyList<Expression> expressions)
    {
        return mock
            .Setup(x => x.Parse(It.IsAny<TokenStream>()))
            .Returns(expressions)
            .Callback<ITokenStream>(tokenStream => tokenStream.Consume());
    }
    
    public static ICallbackResult ReturnsStatementSequence(this Mock<IStatementParser> mock, IReadOnlyList<Statement> statements)
    {
        var index = 0;
        return mock
            .Setup(x => x.Parse(It.IsAny<TokenStream>()))
            .Returns(() => statements[index++])
            .Callback<ITokenStream>(tokenStream => tokenStream.Consume());
    }
}
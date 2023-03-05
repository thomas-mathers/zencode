using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class ReturnStatementParsingStrategyTests
{
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly ReturnStatementParsingStrategy _sut;
    private readonly Fixture _fixture = new();

    public ReturnStatementParsingStrategyTests()
    {
        _sut = new ReturnStatementParsingStrategy(_expressionParserMock.Object);
    }

    [Fact]
    public void Parse_AnyExpression_ReturnsReturnStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Return),
            new Token(TokenType.Unknown)
        });

        var expression = _fixture.Create<Expression>();

        var expected = new ReturnStatement { Expression = expression };

        _expressionParserMock
            .Setup(x => x.ParseExpression(tokenStream, 0))
            .Returns(expression)
            .ConsumesToken(tokenStream);
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}
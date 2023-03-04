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

public class PrintStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IExpressionParser> _parserMock = new();
    private readonly PrintStatementParsingStrategy _sut;

    public PrintStatementParsingStrategyTests()
    {
        _sut = new PrintStatementParsingStrategy(_parserMock.Object);
    }

    [Fact]
    public void Parse_ValidInput_ReturnsPrintStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Print),
            new Token(TokenType.None)
        });

        var expression = _fixture.Create<Expression>();

        var expected = new PrintStatement(expression);

        _parserMock
            .Setup(x => x.ParseExpression(tokenStream, 0))
            .Returns(expression)
            .ConsumesToken(tokenStream);

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}
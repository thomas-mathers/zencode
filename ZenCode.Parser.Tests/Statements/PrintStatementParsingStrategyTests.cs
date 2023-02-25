using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Grammar.Statements;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Statements;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Statements;

public class PrintStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly PrintStatementParsingStrategy _sut;

    public PrintStatementParsingStrategyTests()
    {
        _sut = new PrintStatementParsingStrategy(_expressionParserMock.Object);
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

        _expressionParserMock
            .Setup(x => x.Parse(tokenStream, 0))
            .Returns(expression)
            .ConsumesToken(tokenStream);
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}
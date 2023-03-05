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

public class VariableDeclarationStatementParsingStrategyTests
{
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly VariableDeclarationStatementParsingStrategy _sut;

    public VariableDeclarationStatementParsingStrategyTests()
    {
        _sut = new VariableDeclarationStatementParsingStrategy(_expressionParserMock.Object);
    }

    [Fact]
    public void Parse_VariableDeclaration_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Var),
            new Token(TokenType.Identifier),
            new Token(TokenType.Assignment),
            new Token(TokenType.Unknown)
        });

        var expression = new Expression();
        var expected = new VariableDeclarationStatement(new Token(TokenType.Identifier), expression);

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
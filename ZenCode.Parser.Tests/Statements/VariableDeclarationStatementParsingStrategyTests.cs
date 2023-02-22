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
            new Token(TokenType.None)
        });

        var expression = new Expression();
        var expected = new VariableDeclarationStatement(new Token(TokenType.Identifier), expression);

        _expressionParserMock.ReturnsExpression(expression);
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);        
    }
}
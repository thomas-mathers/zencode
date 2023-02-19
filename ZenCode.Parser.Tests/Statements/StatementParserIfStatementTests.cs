using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Grammar.Statements;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Statements;

namespace ZenCode.Parser.Tests.Statements;

public class StatementParserIfStatementTests
{
    private readonly StatementParser _sut;

    public StatementParserIfStatementTests()
    {
        _sut = new StatementParser(new ExpressionParser());
    }

    [Fact]
    public void Parse_EmptyBody_ReturnsIfStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.If
            },
            new Token
            {
                Type = TokenType.Identifier
            },
            new Token
            {
                Type = TokenType.GreaterThan
            },
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = TokenType.LeftBrace
            },
            new Token
            {
                Type = TokenType.RightBrace
            }
        });

        var expected = new IfStatement(
            new BinaryExpression(
                new VariableReferenceExpression(new Token { Type = TokenType.Identifier }, Array.Empty<Expression>()),
                new Token { Type = TokenType.GreaterThan },
                new ConstantExpression(new Token { Type = TokenType.Integer })), 
            Array.Empty<Statement>());
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}
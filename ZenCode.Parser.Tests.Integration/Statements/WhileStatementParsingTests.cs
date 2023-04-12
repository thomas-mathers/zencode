using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Tests.Integration.Statements;

public class WhileStatementParsingTests
{
    private readonly IParser _sut;

    public WhileStatementParsingTests()
    {
        _sut = new ParserFactory().Create();
    }

    [Fact]
    public void Parse_WhileBinaryExpressionAssignmentStatement_ReturnsWhileStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.While),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.GreaterThan),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace)
            }
        );

        var condition = new BinaryExpression
        (
            new VariableReferenceExpression(new Token(TokenType.Identifier)),
            new Token(TokenType.GreaterThan),
            new LiteralExpression(new Token(TokenType.IntegerLiteral))
        );

        var scope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement
                (
                    new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral))
                )
            }
        };

        var conditionScope = new ConditionScope(condition, scope);

        var expectedStatement = new WhileStatement(conditionScope);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
}

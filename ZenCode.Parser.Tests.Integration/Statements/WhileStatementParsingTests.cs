using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
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
        {
            LeftOperand = new VariableReferenceExpression(new Token(TokenType.Identifier)),
            Operator = new Token(TokenType.GreaterThan),
            RightOperand = new LiteralExpression(new Token(TokenType.IntegerLiteral))
        };

        var scope = new Scope
        (
            new AssignmentStatement
            {
                Variable = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        );

        var conditionScope = new ConditionScope(condition, scope);

        var expectedStatement = new WhileStatement(conditionScope);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_MissingLeftParenthesis_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.While),
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

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Expected '(', got 'Identifier'", exception.Message);
    }

    [Fact]
    public void Parse_MissingRightParenthesis_ThrowsException()
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
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Expected ')', got '{'", exception.Message);
    }

    [Fact]
    public void Parse_MissingLeftBrace_ThrowsException()
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
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Expected '{', got 'Identifier'", exception.Message);
    }

    [Fact]
    public void Parse_MissingRightBrace_ThrowsException()
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
                new Token(TokenType.IntegerLiteral)
            }
        );

        // Act
        var exception = Assert.Throws<EndOfTokenStreamException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.NotNull(exception);
    }
}

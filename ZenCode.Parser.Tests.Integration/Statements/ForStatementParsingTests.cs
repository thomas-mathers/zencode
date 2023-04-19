using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Tests.Integration.Statements;

public class ForStatementParsingTests
{
    private readonly IParser _sut;

    public ForStatementParsingTests()
    {
        _sut = new ParserFactory().Create();
    }

    [Fact]
    public void Parse_ForStatement_ReturnsForStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.For),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon),
                new Token(TokenType.Identifier),
                new Token(TokenType.LessThan),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.Identifier),
                new Token(TokenType.Plus),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace)
            }
        );

        var initialization = new VariableDeclarationStatement
        {
            Name = new Token(TokenType.Identifier),
            Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
        };

        var condition = new BinaryExpression
        {
            LeftOperand = new VariableReferenceExpression(new Token(TokenType.Identifier)),
            Operator = new Token(TokenType.LessThan),
            RightOperand = new LiteralExpression(new Token(TokenType.IntegerLiteral))
        };

        var iterator = new AssignmentStatement
        {
            Variable = new VariableReferenceExpression(new Token(TokenType.Identifier)),
            Value = new BinaryExpression
            {
                LeftOperand = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Operator = new Token(TokenType.Plus),
                RightOperand = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        };

        var variableReferenceExpression = new VariableReferenceExpression(new Token(TokenType.Identifier))
        {
            Indices = new ArrayIndexExpressionList(new LiteralExpression(new Token(TokenType.IntegerLiteral)))
        };

        var expression = new LiteralExpression(new Token(TokenType.IntegerLiteral));

        var scopeStatement = new AssignmentStatement
        {
            Variable = variableReferenceExpression, 
            Value = expression
        };

        var scope = new Scope { Statements = new[] { scopeStatement } };

        var expectedStatement = new ForStatement
        {
            Initializer = initialization,
            Condition = condition,
            Iterator = iterator,
            Body = scope
        };

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
                new Token(TokenType.For),
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon),
                new Token(TokenType.Identifier),
                new Token(TokenType.LessThan),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.Identifier),
                new Token(TokenType.Plus),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Expected '(', got 'var'", exception.Message);
    }

    [Fact]
    public void Parse_MissingRightParenthesis_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.For),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon),
                new Token(TokenType.Identifier),
                new Token(TokenType.LessThan),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.Identifier),
                new Token(TokenType.Plus),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
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
                new Token(TokenType.For),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon),
                new Token(TokenType.Identifier),
                new Token(TokenType.LessThan),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.Identifier),
                new Token(TokenType.Plus),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
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
                new Token(TokenType.For),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon),
                new Token(TokenType.Identifier),
                new Token(TokenType.LessThan),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.Identifier),
                new Token(TokenType.Plus),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral)
            }
        );

        // Act
        var exception = Assert.Throws<EndOfTokenStreamException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.NotNull(exception);
    }
    
    [Fact]
    public void Parse_MissingInitializer_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.For),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Semicolon),
                new Token(TokenType.Identifier),
                new Token(TokenType.LessThan),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.Identifier),
                new Token(TokenType.Plus),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Expected 'var', got ';'", exception.Message);
    }
    
    [Fact]
    public void Parse_MissingCondition_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.For),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon),
                new Token(TokenType.Semicolon),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.Identifier),
                new Token(TokenType.Plus),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Unexpected token ';'", exception.Message);
    }
    
    [Fact]
    public void Parse_MissingIncrementor_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.For),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Var),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon),
                new Token(TokenType.Identifier),
                new Token(TokenType.LessThan),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.Semicolon),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBracket),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace)
            }
        );

        // Act
        var exception = Assert.Throws<UnexpectedTokenException>(() => _sut.ParseStatement(tokenStream));

        // Assert
        Assert.Equal("Expected 'Identifier', got ')'", exception.Message);
    }
}

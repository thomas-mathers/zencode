using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Tests.Integration.Statements;

public class IfStatementParsingTests
{
    private readonly IParser _sut;

    public IfStatementParsingTests()
    {
        _sut = new ParserFactory().Create();
    }

    [Fact]
    public void Parse_IfBinaryExpressionThenAssignmentStatement_ReturnsIfStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.If),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Equals),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace)
            }
        );

        var thenCondition = new BinaryExpression
        {
            LeftOperand = new VariableReferenceExpression(new Token(TokenType.Identifier)),
            Operator = new Token(TokenType.Equals),
            RightOperand = new LiteralExpression(new Token(TokenType.IntegerLiteral))
        };

        var thenScope = new Scope
        (
            new AssignmentStatement
            {
                Variable = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        );

        var expectedStatement = new IfStatement
        {
            ThenScope = new ConditionScope(thenCondition, thenScope)
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_IfBinaryExpressionThenAssignmentStatementElseAssignmentStatement_ReturnsIfStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.If),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Equals),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace),
                new Token(TokenType.Else),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace)
            }
        );

        var thenCondition = new BinaryExpression
        {
            LeftOperand = new VariableReferenceExpression(new Token(TokenType.Identifier)),
            Operator = new Token(TokenType.Equals),
            RightOperand = new LiteralExpression(new Token(TokenType.IntegerLiteral))
        };

        var thenScope = new Scope
        (
            new AssignmentStatement
            {
                Variable = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        );

        var thenConditionScope = new ConditionScope(thenCondition, thenScope);

        var elseScope = new Scope
        (
            new AssignmentStatement
            {
                Variable = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        );

        var expectedStatement = new IfStatement
        {
            ThenScope = thenConditionScope,
            ElseScope = elseScope
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void
        Parse_IfBinaryExpressionThenAssignmentStatementElseIfBinaryExpressionThenAssignmentStatementElseAssignmentStatement_ReturnsIfStatement()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.If),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Equals),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace),
                new Token(TokenType.ElseIf),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Equals),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace),
                new Token(TokenType.Else),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace)
            }
        );

        var thenCondition = new BinaryExpression
        {
            LeftOperand = new VariableReferenceExpression(new Token(TokenType.Identifier)),
            Operator = new Token(TokenType.Equals),
            RightOperand = new LiteralExpression(new Token(TokenType.IntegerLiteral))
        };

        var thenScope = new Scope
        (
            new AssignmentStatement
            {
                Variable = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        );

        var thenConditionScope = new ConditionScope(thenCondition, thenScope);

        var elseIfCondition = new BinaryExpression
        {
            LeftOperand = new VariableReferenceExpression(new Token(TokenType.Identifier)),
            Operator = new Token(TokenType.Equals),
            RightOperand = new LiteralExpression(new Token(TokenType.IntegerLiteral))
        };

        var elseIfScope = new Scope
        (
            new AssignmentStatement
            {
                Variable = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        );

        var elseIfConditionScope = new ConditionScope(elseIfCondition, elseIfScope);

        var elseScope = new Scope
        (
            new AssignmentStatement
            {
                Variable = new VariableReferenceExpression(new Token(TokenType.Identifier)),
                Value = new LiteralExpression(new Token(TokenType.IntegerLiteral))
            }
        );

        var expectedStatement = new IfStatement
        {
            ThenScope = thenConditionScope,
            ElseIfScopes = new[] { elseIfConditionScope },
            ElseScope = elseScope
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
                new Token(TokenType.If),
                new Token(TokenType.Identifier),
                new Token(TokenType.Equals),
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
                new Token(TokenType.If),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Equals),
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
    public void Parse_MissingIfCondition_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.If),
                new Token(TokenType.LeftParenthesis),
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
        Assert.Equal("Unexpected token ')'", exception.Message);
    }

    [Fact]
    public void Parse_MissingElseIfCondition_ThrowsException()
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.If),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.Identifier),
                new Token(TokenType.Equals),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.LeftBrace),
                new Token(TokenType.Identifier),
                new Token(TokenType.Assignment),
                new Token(TokenType.IntegerLiteral),
                new Token(TokenType.RightBrace),
                new Token(TokenType.ElseIf),
                new Token(TokenType.LeftParenthesis),
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
        Assert.Equal("Unexpected token ')'", exception.Message);
    }
}

using Xunit;
using ZenCode.Lexer;
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
        var tokenStream = new TokenStream(new[]
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
        });

        var thenCondition = new BinaryExpression(new VariableReferenceExpression(new Token(TokenType.Identifier)),
            new Token(TokenType.Equals), new LiteralExpression(new Token(TokenType.IntegerLiteral)));
        var thenScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)))
            }
        };
        var thenConditionScope = new ConditionScope(thenCondition, thenScope);

        var expectedStatement = new IfStatement(thenConditionScope);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_IfBinaryExpressionThenAssignmentStatementElseAssignmentStatement_ReturnsIfStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
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
        });

        var thenCondition = new BinaryExpression(new VariableReferenceExpression(new Token(TokenType.Identifier)),
            new Token(TokenType.Equals), new LiteralExpression(new Token(TokenType.IntegerLiteral)));
        var thenScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)))
            }
        };
        var thenConditionScope = new ConditionScope(thenCondition, thenScope);

        var elseScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)))
            }
        };

        var expectedStatement = new IfStatement(thenConditionScope)
        {
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
        var tokenStream = new TokenStream(new[]
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
        });

        var thenCondition = new BinaryExpression(new VariableReferenceExpression(new Token(TokenType.Identifier)),
            new Token(TokenType.Equals), new LiteralExpression(new Token(TokenType.IntegerLiteral)));
        var thenScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)))
            }
        };
        var thenConditionScope = new ConditionScope(thenCondition, thenScope);

        var elseIfCondition = new BinaryExpression(new VariableReferenceExpression(new Token(TokenType.Identifier)),
            new Token(TokenType.Equals), new LiteralExpression(new Token(TokenType.IntegerLiteral)));
        var elseIfScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)))
            }
        };
        var elseIfConditionScope = new ConditionScope(elseIfCondition, elseIfScope);

        var elseScope = new Scope
        {
            Statements = new[]
            {
                new AssignmentStatement(new VariableReferenceExpression(new Token(TokenType.Identifier)),
                    new LiteralExpression(new Token(TokenType.IntegerLiteral)))
            }
        };

        var expectedStatement = new IfStatement(thenConditionScope)
        {
            ElseIfScopes = new[]
            {
                elseIfConditionScope
            },
            ElseScope = elseScope
        };

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
}
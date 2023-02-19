using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Grammar.Statements;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Statements;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Statements;

public class StatementParserAssignmentStatementTests
{
    private readonly StatementParser _sut;

    public StatementParserAssignmentStatementTests()
    {
        _sut = new StatementParser(new ExpressionParser());
    }

    [Theory]
    [ClassData(typeof(ConstantTestData))]
    public void Parse_AssignmentToConstant_ReturnsAssignmentStatement(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Identifier
            },
            new Token
            {
                Type = TokenType.Assignment
            },
            new Token
            {
                Type = tokenType
            }
        });

        var expected = new AssignmentStatement(
            new Token { Type = TokenType.Identifier },
            new ConstantExpression(new Token { Type = tokenType }));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_AssignmentToIdentifier_ReturnsAssignmentStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Identifier
            },
            new Token
            {
                Type = TokenType.Assignment
            },
            new Token
            {
                Type = TokenType.Identifier
            }
        });

        var expected = new AssignmentStatement(
            new Token { Type = TokenType.Identifier },
            new VariableReferenceExpression(new Token { Type = TokenType.Identifier }, Array.Empty<Expression>()));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_AssignmentToArrayReference_ReturnsAssignmentStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Identifier
            },
            new Token
            {
                Type = TokenType.Assignment
            },
            new Token
            {
                Type = TokenType.Identifier
            },
            new Token
            {
                Type = TokenType.LeftBracket
            },
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = TokenType.RightBracket
            }
        });

        var expected = new AssignmentStatement(
            new Token { Type = TokenType.Identifier },
            new VariableReferenceExpression(
                new Token { Type = TokenType.Identifier },
                new[]
                {
                    new ConstantExpression(new Token { Type = TokenType.Integer })
                }));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [ClassData(typeof(ConstantOpConstantTestData))]
    public void Parse_AssignmentToBinaryExpression_ReturnsAssignmentStatement(TokenType lOperand, TokenType op, TokenType rOperand)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Identifier
            },
            new Token
            {
                Type = TokenType.Assignment
            },
            new Token
            {
                Type = lOperand
            },
            new Token
            {
                Type = op
            },
            new Token
            {
                Type = rOperand
            }
        });

        var expected = new AssignmentStatement(
            new Token { Type = TokenType.Identifier },
            new BinaryExpression(
                new ConstantExpression(new Token { Type = lOperand }), 
                new Token { Type = op }, 
                new ConstantExpression(new Token { Type = rOperand })));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}
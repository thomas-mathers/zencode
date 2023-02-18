using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Expressions;

public class ParenthesizedExpressionIntegrationTests
{
    private readonly ExpressionParser _sut;

    public ParenthesizedExpressionIntegrationTests()
    {
        _sut = new ExpressionParser();
    }

    [Theory]
    [ClassData(typeof(ConstantTestData))]
    public void Parse_ParenthesizedConstant_ReturnsConstantExpression(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.LeftParenthesis
            },
            new Token
            {
                Type = tokenType
            },
            new Token
            {
                Type = TokenType.RightParenthesis
            }
        });

        var expected = new ConstantExpression(new Token
        {
            Type = tokenType
        });

        // Act
        var actual = _sut.Parse(tokenStream);

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_ParenthesizedIdentifier_ReturnsVariableReferenceExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.LeftParenthesis
            },
            new Token
            {
                Type = TokenType.Identifier
            },
            new Token
            {
                Type = TokenType.RightParenthesis
            }
        });

        var expected = new VariableReferenceExpression(
            new Token
            {
                Type = TokenType.Identifier
            },
            Array.Empty<Expression>());

        // Act
        var actual = _sut.Parse(tokenStream);

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_ParenthesizedFunctionCall_ReturnsFunctionCallExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.LeftParenthesis
            },
            new Token
            {
                Type = TokenType.Identifier
            },
            new Token
            {
                Type = TokenType.LeftParenthesis
            },
            new Token
            {
                Type = TokenType.RightParenthesis
            },
            new Token
            {
                Type = TokenType.RightParenthesis
            }
        });

        var expected = new FunctionCall(
            new VariableReferenceExpression
            (
                new Token
                {
                    Type = TokenType.Identifier
                },
                Array.Empty<Expression>()
            ),
            Array.Empty<Expression>());

        // Act
        var actual = _sut.Parse(tokenStream);

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(LoPrecedenceOpHiPrecedenceOpTestData))]
    public void
        Parse_ParenthesizedLoPrecedenceOpThenHighPrecedenceOp_ReturnsBinaryExpressionWithFirstTwoTermsGroupedFirst(
            TokenType loOp, TokenType hiOp)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.LeftParenthesis
            },
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = loOp
            },
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = TokenType.RightParenthesis
            },
            new Token
            {
                Type = hiOp
            },
            new Token
            {
                Type = TokenType.Integer
            }
        });

        var expected = new BinaryExpression(
            new BinaryExpression(
                new ConstantExpression(new Token
                {
                    Type = TokenType.Integer
                }),
                new Token
                {
                    Type = loOp
                },
                new ConstantExpression(new Token
                {
                    Type = TokenType.Integer
                })),
            new Token
            {
                Type = hiOp
            },
            new ConstantExpression(new Token
            {
                Type = TokenType.Integer
            }));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}
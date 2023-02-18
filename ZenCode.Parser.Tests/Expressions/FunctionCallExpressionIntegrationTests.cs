using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Expressions;

public class FunctionCallExpressionIntegrationTests
{
    private readonly ExpressionParser _sut;

    public FunctionCallExpressionIntegrationTests()
    {
        _sut = new ExpressionParser();
    }

    [Fact]
    public void Parse_FunctionCallNoParameters_ReturnsFunctionCallExpression()
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
                Type = TokenType.LeftParenthesis
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
    [ClassData(typeof(ConstantTestData))]
    public void Parse_FunctionCallOneConstantParameter_ReturnsFunctionCallExpression(TokenType parameterType)
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
                Type = TokenType.LeftParenthesis
            },
            new Token
            {
                Type = parameterType
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
            new[]
            {
                new ConstantExpression(new Token
                {
                    Type = parameterType
                })
            });

        // Act
        var actual = _sut.Parse(tokenStream);

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ConstantPairTestData))]
    public void Parse_FunctionCallTwoConstantParameters_ReturnsFunctionCallExpression(TokenType parameterType1,
        TokenType parameterType2)
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
                Type = TokenType.LeftParenthesis
            },
            new Token
            {
                Type = parameterType1
            },
            new Token
            {
                Type = TokenType.Comma
            },
            new Token
            {
                Type = parameterType2
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
            new[]
            {
                new ConstantExpression(new Token
                {
                    Type = parameterType1
                }),
                new ConstantExpression(new Token
                {
                    Type = parameterType2
                })
            });

        // Act
        var actual = _sut.Parse(tokenStream);

        // Arrange
        Assert.Equal(expected, actual);
    }
}
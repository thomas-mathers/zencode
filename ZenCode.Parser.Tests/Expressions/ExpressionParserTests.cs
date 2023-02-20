using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Expressions;

public class ExpressionParserTests
{
    private readonly ExpressionParser _sut;

    public ExpressionParserTests()
    {
        _sut = new ExpressionParser();
    }
    
    [Theory]
    [ClassData(typeof(LoPrecedenceOpHiPrecedenceOpTestData))]
    public void Parse_LoPrecedenceOpThenHiPrecedenceOp_ReturnsBinaryExpressionWithLastTwoTermsGroupedFirst(
        TokenType loOp,
        TokenType hiOp)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
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
                Type = hiOp
            },
            new Token
            {
                Type = TokenType.Integer
            }
        });

        var expected = new BinaryExpression(
            new ConstantExpression(new Token
            {
                Type = TokenType.Integer
            }),
            new Token
            {
                Type = loOp
            },
            new BinaryExpression(
                new ConstantExpression(new Token
                {
                    Type = TokenType.Integer
                }),
                new Token
                {
                    Type = hiOp
                },
                new ConstantExpression(new Token
                {
                    Type = TokenType.Integer
                })));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(LoPrecedenceOpHiPrecedenceOpTestData))]
    public void Parse_HiPrecedenceOpThenLoPrecedenceOp_ReturnsBinaryExpressionWithFirstTwoTermsGroupedFirst(
        TokenType loOp,
        TokenType hiOp)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = hiOp
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
                    Type = hiOp
                },
                new ConstantExpression(new Token
                {
                    Type = TokenType.Integer
                })),
            new Token
            {
                Type = loOp
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

    [Theory]
    [ClassData(typeof(LeftAssociativeOpTestData))]
    public void Parse_LeftAssociativeOperator_ReturnsBinaryExpressionWithFirstTwoTermsGroupedFirst(TokenType op)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = op
            },
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = op
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
                    Type = op
                },
                new ConstantExpression(new Token
                {
                    Type = TokenType.Integer
                })),
            new Token
            {
                Type = op
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

    [Theory]
    [InlineData(TokenType.Exponentiation)]
    public void Parse_RightAssociativeOperator_ReturnsBinaryExpressionWithLastTwoTermsGroupedFirst(TokenType op)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = op
            },
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = op
            },
            new Token
            {
                Type = TokenType.Integer
            }
        });

        var expected = new BinaryExpression(
            new ConstantExpression(new Token
            {
                Type = TokenType.Integer
            }),
            new Token
            {
                Type = op
            },
            new BinaryExpression(
                new ConstantExpression(new Token
                {
                    Type = TokenType.Integer
                }),
                new Token
                {
                    Type = op
                },
                new ConstantExpression(new Token
                {
                    Type = TokenType.Integer
                })));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(LoPrecedenceOpHiPrecedenceOpTestData))]
    public void Parse_HiPrecedenceOpThenParenthesizedLoPrecedenceOp_ReturnsBinaryExpressionWithLastTwoTermsGroupedFirst(
        TokenType hiOp, TokenType loOp)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = hiOp
            },
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
            }
        });

        var expected = new BinaryExpression(
            new ConstantExpression(new Token
            {
                Type = TokenType.Integer
            }),
            new Token
            {
                Type = hiOp
            },
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
                })));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}
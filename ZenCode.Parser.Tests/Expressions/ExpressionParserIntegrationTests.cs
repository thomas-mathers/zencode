using System.Linq.Expressions;
using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Tests.TestData;
using BinaryExpression = ZenCode.Grammar.Expressions.BinaryExpression;
using ConstantExpression = ZenCode.Grammar.Expressions.ConstantExpression;
using Expression = ZenCode.Grammar.Expressions.Expression;
using UnaryExpression = ZenCode.Grammar.Expressions.UnaryExpression;

namespace ZenCode.Parser.Tests.Expressions;

public class ExpressionParserIntegrationTests
{
    private readonly ExpressionParser _sut;

    public ExpressionParserIntegrationTests()
    {
        _sut = new ExpressionParser(
            new PrefixExpressionParsingContext(new Dictionary<TokenType, IPrefixExpressionParsingStrategy>
            {
                [TokenType.Boolean] = new ConstantParsingStrategy(),
                [TokenType.Integer] = new ConstantParsingStrategy(),
                [TokenType.Float] = new ConstantParsingStrategy(),
                [TokenType.Identifier] = new VariableReferenceParsingStrategy(),
                [TokenType.Not] = new UnaryExpressionParsingStrategy(),
                [TokenType.LeftParenthesis] = new ParenthesizedExpressionParsingStrategy()
            }), 
            new InfixExpressionParsingContext(new Dictionary<TokenType, IInfixExpressionParsingStrategy>
            {
                [TokenType.Addition] = new BinaryExpressionParsingStrategy(4),
                [TokenType.Subtraction] = new BinaryExpressionParsingStrategy(4),
                [TokenType.Multiplication] = new BinaryExpressionParsingStrategy(5),
                [TokenType.Division] = new BinaryExpressionParsingStrategy(5),
                [TokenType.Modulus] = new BinaryExpressionParsingStrategy(5),
                [TokenType.Exponentiation] = new BinaryExpressionParsingStrategy(6, true),
                [TokenType.LessThan] = new BinaryExpressionParsingStrategy(3),
                [TokenType.LessThanOrEqual] = new BinaryExpressionParsingStrategy(3),
                [TokenType.Equals] = new BinaryExpressionParsingStrategy(3),
                [TokenType.NotEquals] = new BinaryExpressionParsingStrategy(3),
                [TokenType.GreaterThan] = new BinaryExpressionParsingStrategy(3),
                [TokenType.GreaterThanOrEqual] = new BinaryExpressionParsingStrategy(3),
                [TokenType.And] = new BinaryExpressionParsingStrategy(2),
                [TokenType.Or] = new BinaryExpressionParsingStrategy(1),
                [TokenType.LeftParenthesis] = new FunctionCallParsingStrategy(7)
            }));
    }

    [Fact]
    public void Parse_Boolean_ReturnsConstantExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Boolean
            }
        });

        var expected = new ConstantExpression(new Token
        {
            Type = TokenType.Boolean
        });

        // Act
        var actual = _sut.Parse(tokenStream);

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_Integer_ReturnsConstantExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Integer
            }
        });

        var expected = new ConstantExpression(new Token
        {
            Type = TokenType.Integer
        });

        // Act
        var actual = _sut.Parse(tokenStream);

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_Float_ReturnsConstantExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Float
            }
        });

        var expected = new ConstantExpression(new Token
        {
            Type = TokenType.Float
        });

        // Act
        var actual = _sut.Parse(tokenStream);

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_Identifier_ReturnsVariableReferenceExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = TokenType.Identifier
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
    public void Parse_SingleDimensionalArrayReference_ReturnsVariableReferenceExpression()
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

        var expected = new VariableReferenceExpression(
            new Token
            {
                Type = TokenType.Identifier
            },
            new[]
            {
                new ConstantExpression(new Token { Type = TokenType.Integer })
            });

        // Act
        var actual = _sut.Parse(tokenStream);

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_MultiDimensionalArrayReference_ReturnsVariableReferenceExpression()
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
                Type = TokenType.LeftBracket
            },
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = TokenType.Comma
            },
            new Token
            {
                Type = TokenType.Integer
            },
            new Token
            {
                Type = TokenType.Comma
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

        var expected = new VariableReferenceExpression(
            new Token
            {
                Type = TokenType.Identifier
            },
            new[]
            {
                new ConstantExpression(new Token { Type = TokenType.Integer }),
                new ConstantExpression(new Token { Type = TokenType.Integer }),
                new ConstantExpression(new Token { Type = TokenType.Integer })
            });

        // Act
        var actual = _sut.Parse(tokenStream);

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(ConstantOpConstantTestData))]
    public void Parse_ConstantOpConstant_ReturnsBinaryExpression(TokenType lOperand, TokenType op, TokenType rOperand)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
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

        var expected = new BinaryExpression(
            new ConstantExpression(new Token { Type = lOperand }),
            new Token
            {
                Type = op
            },
            new ConstantExpression(new Token { Type = rOperand }));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Theory]
    [ClassData(typeof(LoPrecedenceOpHiPrecedenceOpTestData))]
    public void Parse_LoPrecedenceOpThenHiPrecedenceOp_ReturnsParseTreeWithLastTwoTermsGroupedFirst(TokenType loOp,
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
    public void Parse_HiPrecedenceOpThenLoPrecedenceOp_ReturnsParseTreeWithFirstTwoTermsGroupedFirst(TokenType loOp,
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
    public void Parse_LeftAssociativeOperator_ReturnsParseTreeWithFirstTwoTermsGroupedFirst(TokenType op)
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
    public void Parse_RightAssociativeOperator_ReturnsParseTreeWithLastTwoTermsGroupedFirst(TokenType op)
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
    [InlineData(TokenType.Not, TokenType.Boolean)]
    [InlineData(TokenType.Not, TokenType.Integer)]
    [InlineData(TokenType.Not, TokenType.Float)]
    public void Parse_UnaryExpression_ReturnsUnaryExpression(TokenType op, TokenType operand)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token
            {
                Type = op
            },
            new Token
            {
                Type = operand
            }
        });

        var expected = new UnaryExpression(
            new Token
            {
                Type = op
            },
            new ConstantExpression(new Token { Type = operand }));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Arrange
        Assert.Equal(expected, actual);
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
    public void Parse_ParenthesizedLoPrecedenceOpThenHighPrecedenceOp_ReturnsParseTreeWithFirstTwoTermsGroupedFirst(
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

    [Theory]
    [ClassData(typeof(LoPrecedenceOpHiPrecedenceOpTestData))]
    public void Parse_HiPrecedenceOpThenParenthesizedLoPrecedenceOp_ReturnsParseTreeWithLastTwoTermsGroupedFirst(
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
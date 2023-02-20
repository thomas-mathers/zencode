using Moq;
using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Expressions;

public class ParenthesizedExpressionParsingStrategyTests
{
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly ParenthesizedExpressionParsingStrategy _sut;

    public ParenthesizedExpressionParsingStrategyTests()
    {
        _sut = new ParenthesizedExpressionParsingStrategy(_expressionParserMock.Object);
    }
    
    [Fact]
    public void Parse_ExtraLeftParenthesis_ThrowsUnexpectedTokenException()
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
                Type = TokenType.LeftParenthesis
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
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new ConstantExpression(new Token { Type = TokenType.Integer }))
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });        
        
        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream));
    }
    
    [Fact]
    public void Parse_NoExpression_ThrowsUnexpectedTokenException()
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
                Type = TokenType.RightParenthesis
            },
        });

        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0)).Throws<UnexpectedTokenException>();

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream));
    }

    [Fact]
    public void Parse_MissingRightParenthesis_ThrowsUnexpectedTokenException()
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
            }
        });
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new ConstantExpression(new Token { Type = TokenType.Integer }))
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream));
    }

    [Theory]
    [ClassData(typeof(ConstantTestData))]
    public void Parse_ParenthesizedConstant_ReturnsConstantExpression(TokenType constantType)
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
                Type = constantType
            },
            new Token
            {
                Type = TokenType.RightParenthesis
            }
        });
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new ConstantExpression(new Token { Type = constantType }))
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });

        var expected = new ConstantExpression(new Token
        {
            Type = constantType
        });

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
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
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new VariableReferenceExpression
            {
                Identifier = new Token { Type = TokenType.Identifier }
            })
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });

        var expected = new VariableReferenceExpression
        {
            Identifier = new Token
            {
                Type = TokenType.Identifier
            }
        };

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
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
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new FunctionCall
            {
                VariableReferenceExpression = new VariableReferenceExpression
                {
                    Identifier = new Token
                    {
                        Type = TokenType.Identifier
                    }
                }
            })
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
                tokenStream.Consume();
                tokenStream.Consume();
            });

        var expected = new FunctionCall
        {
            VariableReferenceExpression = new VariableReferenceExpression
            {
                Identifier = new Token
                {
                    Type = TokenType.Identifier
                }
            }
        };

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}
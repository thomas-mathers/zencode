using System.Runtime.InteropServices;
using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Parser.Grammar;
using ZenCode.Parser.Grammar.Expressions;
using ZenCode.Parser.Grammar.Statements;

namespace ZenCode.Parser.Tests;

public class ParserTests
{
    private readonly Mock<ITokenizer> _tokenizerMock = new();
    private readonly Parser _sut;

    public ParserTests()
    {
        _sut = new Parser(_tokenizerMock.Object);
    }

    [Fact]
    public void Parse_Boolean_ReturnsConstantExpression()
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
            {
                new Token
                {
                    Type = TokenType.Boolean
                }
            }));

        var expected = new Program(new List<Statement>
        {
            new ConstantExpression(new Token
            {
                Type = TokenType.Boolean
            })
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_Integer_ReturnsConstantExpression()
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
            {
                new Token
                {
                    Type = TokenType.Integer
                }
            }));

        var expected = new Program(new List<Statement>
        {
            new ConstantExpression(new Token
            {
                Type = TokenType.Integer
            })
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_Float_ReturnsConstantExpression()
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
            {
                new Token
                {
                    Type = TokenType.Float
                }
            }));

        var expected = new Program(new List<Statement>
        {
            new ConstantExpression(new Token
            {
                Type = TokenType.Float
            })
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_Identifier_ReturnsVariableReferenceExpression()
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
            {
                new Token
                {
                    Type = TokenType.Identifier
                }
            }));

        var expected = new Program(new List<Statement>
        {
            new VariableReferenceExpression(
                new Token
                {
                    Type = TokenType.Identifier
                }, 
                Array.Empty<Expression>())
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());

        // Arrange
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_SingleDimensionalArrayReference_ReturnsVariableReferenceExpression()
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
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
            }));

        var expected = new Program(new List<Statement>
        {
            new VariableReferenceExpression(
                new Token
                {
                    Type = TokenType.Identifier
                }, 
                new[]
                {
                    new ConstantExpression(new Token { Type = TokenType.Integer })
                })
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());

        // Arrange
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_MultiDimensionalArrayReference_ReturnsVariableReferenceExpression()
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
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
            }));

        var expected = new Program(new List<Statement>
        {
            new VariableReferenceExpression(
                new Token
                {
                    Type = TokenType.Identifier
                }, 
                new[]
                {
                    new ConstantExpression(new Token { Type = TokenType.Integer }),
                    new ConstantExpression(new Token { Type = TokenType.Integer }),
                    new ConstantExpression(new Token { Type = TokenType.Integer }),
                })
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());

        // Arrange
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData(TokenType.Boolean, TokenType.Addition, TokenType.Boolean)]
    [InlineData(TokenType.Boolean, TokenType.Subtraction, TokenType.Boolean)]
    [InlineData(TokenType.Boolean, TokenType.Multiplication, TokenType.Boolean)]
    [InlineData(TokenType.Boolean, TokenType.Division, TokenType.Boolean)]
    [InlineData(TokenType.Boolean, TokenType.Modulus, TokenType.Boolean)]
    [InlineData(TokenType.Boolean, TokenType.Exponentiation, TokenType.Boolean)]
    [InlineData(TokenType.Boolean, TokenType.LessThan, TokenType.Boolean)]
    [InlineData(TokenType.Boolean, TokenType.LessThanOrEqual, TokenType.Boolean)]
    [InlineData(TokenType.Boolean, TokenType.Equals, TokenType.Boolean)]
    [InlineData(TokenType.Boolean, TokenType.NotEquals, TokenType.Boolean)]
    [InlineData(TokenType.Boolean, TokenType.GreaterThan, TokenType.Boolean)]
    [InlineData(TokenType.Boolean, TokenType.GreaterThanOrEqual, TokenType.Boolean)]
    [InlineData(TokenType.Boolean, TokenType.And, TokenType.Boolean)]
    [InlineData(TokenType.Boolean, TokenType.Or, TokenType.Boolean)]
    [InlineData(TokenType.Boolean, TokenType.Addition, TokenType.Integer)]
    [InlineData(TokenType.Boolean, TokenType.Subtraction, TokenType.Integer)]
    [InlineData(TokenType.Boolean, TokenType.Multiplication, TokenType.Integer)]
    [InlineData(TokenType.Boolean, TokenType.Division, TokenType.Integer)]
    [InlineData(TokenType.Boolean, TokenType.Modulus, TokenType.Integer)]
    [InlineData(TokenType.Boolean, TokenType.Exponentiation, TokenType.Integer)]
    [InlineData(TokenType.Boolean, TokenType.LessThan, TokenType.Integer)]
    [InlineData(TokenType.Boolean, TokenType.LessThanOrEqual, TokenType.Integer)]
    [InlineData(TokenType.Boolean, TokenType.Equals, TokenType.Integer)]
    [InlineData(TokenType.Boolean, TokenType.NotEquals, TokenType.Integer)]
    [InlineData(TokenType.Boolean, TokenType.GreaterThan, TokenType.Integer)]
    [InlineData(TokenType.Boolean, TokenType.GreaterThanOrEqual, TokenType.Integer)]
    [InlineData(TokenType.Boolean, TokenType.And, TokenType.Integer)]
    [InlineData(TokenType.Boolean, TokenType.Or, TokenType.Integer)]
    [InlineData(TokenType.Boolean, TokenType.Addition, TokenType.Float)]
    [InlineData(TokenType.Boolean, TokenType.Subtraction, TokenType.Float)]
    [InlineData(TokenType.Boolean, TokenType.Multiplication, TokenType.Float)]
    [InlineData(TokenType.Boolean, TokenType.Division, TokenType.Float)]
    [InlineData(TokenType.Boolean, TokenType.Modulus, TokenType.Float)]
    [InlineData(TokenType.Boolean, TokenType.Exponentiation, TokenType.Float)]
    [InlineData(TokenType.Boolean, TokenType.LessThan, TokenType.Float)]
    [InlineData(TokenType.Boolean, TokenType.LessThanOrEqual, TokenType.Float)]
    [InlineData(TokenType.Boolean, TokenType.Equals, TokenType.Float)]
    [InlineData(TokenType.Boolean, TokenType.NotEquals, TokenType.Float)]
    [InlineData(TokenType.Boolean, TokenType.GreaterThan, TokenType.Float)]
    [InlineData(TokenType.Boolean, TokenType.GreaterThanOrEqual, TokenType.Float)]
    [InlineData(TokenType.Boolean, TokenType.And, TokenType.Float)]
    [InlineData(TokenType.Boolean, TokenType.Or, TokenType.Float)]
    [InlineData(TokenType.Integer, TokenType.Addition, TokenType.Boolean)]
    [InlineData(TokenType.Integer, TokenType.Subtraction, TokenType.Boolean)]
    [InlineData(TokenType.Integer, TokenType.Multiplication, TokenType.Boolean)]
    [InlineData(TokenType.Integer, TokenType.Division, TokenType.Boolean)]
    [InlineData(TokenType.Integer, TokenType.Modulus, TokenType.Boolean)]
    [InlineData(TokenType.Integer, TokenType.Exponentiation, TokenType.Boolean)]
    [InlineData(TokenType.Integer, TokenType.LessThan, TokenType.Boolean)]
    [InlineData(TokenType.Integer, TokenType.LessThanOrEqual, TokenType.Boolean)]
    [InlineData(TokenType.Integer, TokenType.Equals, TokenType.Boolean)]
    [InlineData(TokenType.Integer, TokenType.NotEquals, TokenType.Boolean)]
    [InlineData(TokenType.Integer, TokenType.GreaterThan, TokenType.Boolean)]
    [InlineData(TokenType.Integer, TokenType.GreaterThanOrEqual, TokenType.Boolean)]
    [InlineData(TokenType.Integer, TokenType.And, TokenType.Boolean)]
    [InlineData(TokenType.Integer, TokenType.Or, TokenType.Boolean)]
    [InlineData(TokenType.Integer, TokenType.Addition, TokenType.Integer)]
    [InlineData(TokenType.Integer, TokenType.Subtraction, TokenType.Integer)]
    [InlineData(TokenType.Integer, TokenType.Multiplication, TokenType.Integer)]
    [InlineData(TokenType.Integer, TokenType.Division, TokenType.Integer)]
    [InlineData(TokenType.Integer, TokenType.Modulus, TokenType.Integer)]
    [InlineData(TokenType.Integer, TokenType.Exponentiation, TokenType.Integer)]
    [InlineData(TokenType.Integer, TokenType.LessThan, TokenType.Integer)]
    [InlineData(TokenType.Integer, TokenType.LessThanOrEqual, TokenType.Integer)]
    [InlineData(TokenType.Integer, TokenType.Equals, TokenType.Integer)]
    [InlineData(TokenType.Integer, TokenType.NotEquals, TokenType.Integer)]
    [InlineData(TokenType.Integer, TokenType.GreaterThan, TokenType.Integer)]
    [InlineData(TokenType.Integer, TokenType.GreaterThanOrEqual, TokenType.Integer)]
    [InlineData(TokenType.Integer, TokenType.And, TokenType.Integer)]
    [InlineData(TokenType.Integer, TokenType.Or, TokenType.Integer)]
    [InlineData(TokenType.Integer, TokenType.Addition, TokenType.Float)]
    [InlineData(TokenType.Integer, TokenType.Subtraction, TokenType.Float)]
    [InlineData(TokenType.Integer, TokenType.Multiplication, TokenType.Float)]
    [InlineData(TokenType.Integer, TokenType.Division, TokenType.Float)]
    [InlineData(TokenType.Integer, TokenType.Modulus, TokenType.Float)]
    [InlineData(TokenType.Integer, TokenType.Exponentiation, TokenType.Float)]
    [InlineData(TokenType.Integer, TokenType.LessThan, TokenType.Float)]
    [InlineData(TokenType.Integer, TokenType.LessThanOrEqual, TokenType.Float)]
    [InlineData(TokenType.Integer, TokenType.Equals, TokenType.Float)]
    [InlineData(TokenType.Integer, TokenType.NotEquals, TokenType.Float)]
    [InlineData(TokenType.Integer, TokenType.GreaterThan, TokenType.Float)]
    [InlineData(TokenType.Integer, TokenType.GreaterThanOrEqual, TokenType.Float)]
    [InlineData(TokenType.Integer, TokenType.And, TokenType.Float)]
    [InlineData(TokenType.Integer, TokenType.Or, TokenType.Float)]
    [InlineData(TokenType.Float, TokenType.Addition, TokenType.Boolean)]
    [InlineData(TokenType.Float, TokenType.Subtraction, TokenType.Boolean)]
    [InlineData(TokenType.Float, TokenType.Multiplication, TokenType.Boolean)]
    [InlineData(TokenType.Float, TokenType.Division, TokenType.Boolean)]
    [InlineData(TokenType.Float, TokenType.Modulus, TokenType.Boolean)]
    [InlineData(TokenType.Float, TokenType.Exponentiation, TokenType.Boolean)]
    [InlineData(TokenType.Float, TokenType.LessThan, TokenType.Boolean)]
    [InlineData(TokenType.Float, TokenType.LessThanOrEqual, TokenType.Boolean)]
    [InlineData(TokenType.Float, TokenType.Equals, TokenType.Boolean)]
    [InlineData(TokenType.Float, TokenType.NotEquals, TokenType.Boolean)]
    [InlineData(TokenType.Float, TokenType.GreaterThan, TokenType.Boolean)]
    [InlineData(TokenType.Float, TokenType.GreaterThanOrEqual, TokenType.Boolean)]
    [InlineData(TokenType.Float, TokenType.And, TokenType.Boolean)]
    [InlineData(TokenType.Float, TokenType.Or, TokenType.Boolean)]
    [InlineData(TokenType.Float, TokenType.Addition, TokenType.Integer)]
    [InlineData(TokenType.Float, TokenType.Subtraction, TokenType.Integer)]
    [InlineData(TokenType.Float, TokenType.Multiplication, TokenType.Integer)]
    [InlineData(TokenType.Float, TokenType.Division, TokenType.Integer)]
    [InlineData(TokenType.Float, TokenType.Modulus, TokenType.Integer)]
    [InlineData(TokenType.Float, TokenType.Exponentiation, TokenType.Integer)]
    [InlineData(TokenType.Float, TokenType.LessThan, TokenType.Integer)]
    [InlineData(TokenType.Float, TokenType.LessThanOrEqual, TokenType.Integer)]
    [InlineData(TokenType.Float, TokenType.Equals, TokenType.Integer)]
    [InlineData(TokenType.Float, TokenType.NotEquals, TokenType.Integer)]
    [InlineData(TokenType.Float, TokenType.GreaterThan, TokenType.Integer)]
    [InlineData(TokenType.Float, TokenType.GreaterThanOrEqual, TokenType.Integer)]
    [InlineData(TokenType.Float, TokenType.And, TokenType.Integer)]
    [InlineData(TokenType.Float, TokenType.Or, TokenType.Integer)]
    [InlineData(TokenType.Float, TokenType.Addition, TokenType.Float)]
    [InlineData(TokenType.Float, TokenType.Subtraction, TokenType.Float)]
    [InlineData(TokenType.Float, TokenType.Multiplication, TokenType.Float)]
    [InlineData(TokenType.Float, TokenType.Division, TokenType.Float)]
    [InlineData(TokenType.Float, TokenType.Modulus, TokenType.Float)]
    [InlineData(TokenType.Float, TokenType.Exponentiation, TokenType.Float)]
    [InlineData(TokenType.Float, TokenType.LessThan, TokenType.Float)]
    [InlineData(TokenType.Float, TokenType.LessThanOrEqual, TokenType.Float)]
    [InlineData(TokenType.Float, TokenType.Equals, TokenType.Float)]
    [InlineData(TokenType.Float, TokenType.NotEquals, TokenType.Float)]
    [InlineData(TokenType.Float, TokenType.GreaterThan, TokenType.Float)]
    [InlineData(TokenType.Float, TokenType.GreaterThanOrEqual, TokenType.Float)]
    [InlineData(TokenType.Float, TokenType.And, TokenType.Float)]
    [InlineData(TokenType.Float, TokenType.Or, TokenType.Float)]
    public void Parse_ConstantOpConstant_ReturnsBinaryExpression(TokenType lOperand, TokenType op, TokenType rOperand)
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
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
            }));
        
        var expected = new Program(new List<Statement>
        {
            new BinaryExpression(
                new ConstantExpression(new Token { Type = lOperand }),
                new Token
                {
                    Type = op
                },
                new ConstantExpression(new Token { Type = rOperand }))
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(TokenType.Or, TokenType.And)]
    [InlineData(TokenType.Or, TokenType.LessThan)]
    [InlineData(TokenType.Or, TokenType.LessThanOrEqual)]
    [InlineData(TokenType.Or, TokenType.Equals)]
    [InlineData(TokenType.Or, TokenType.NotEquals)]
    [InlineData(TokenType.Or, TokenType.GreaterThan)]
    [InlineData(TokenType.Or, TokenType.GreaterThanOrEqual)]
    [InlineData(TokenType.Or, TokenType.Addition)]
    [InlineData(TokenType.Or, TokenType.Subtraction)]
    [InlineData(TokenType.Or, TokenType.Multiplication)]
    [InlineData(TokenType.Or, TokenType.Division)]
    [InlineData(TokenType.Or, TokenType.Modulus)]
    [InlineData(TokenType.Or, TokenType.Exponentiation)]
    [InlineData(TokenType.And, TokenType.LessThan)]
    [InlineData(TokenType.And, TokenType.LessThanOrEqual)]
    [InlineData(TokenType.And, TokenType.Equals)]
    [InlineData(TokenType.And, TokenType.NotEquals)]
    [InlineData(TokenType.And, TokenType.GreaterThan)]
    [InlineData(TokenType.And, TokenType.GreaterThanOrEqual)]
    [InlineData(TokenType.And, TokenType.Addition)]
    [InlineData(TokenType.And, TokenType.Subtraction)]
    [InlineData(TokenType.And, TokenType.Multiplication)]
    [InlineData(TokenType.And, TokenType.Division)]
    [InlineData(TokenType.And, TokenType.Modulus)]
    [InlineData(TokenType.And, TokenType.Exponentiation)]
    [InlineData(TokenType.LessThan, TokenType.Addition)]
    [InlineData(TokenType.LessThan, TokenType.Subtraction)]
    [InlineData(TokenType.LessThan, TokenType.Multiplication)]
    [InlineData(TokenType.LessThan, TokenType.Division)]
    [InlineData(TokenType.LessThan, TokenType.Modulus)]
    [InlineData(TokenType.LessThan, TokenType.Exponentiation)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Addition)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Subtraction)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Multiplication)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Division)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Modulus)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Exponentiation)]
    [InlineData(TokenType.Equals, TokenType.Addition)]
    [InlineData(TokenType.Equals, TokenType.Subtraction)]
    [InlineData(TokenType.Equals, TokenType.Multiplication)]
    [InlineData(TokenType.Equals, TokenType.Division)]
    [InlineData(TokenType.Equals, TokenType.Modulus)]
    [InlineData(TokenType.Equals, TokenType.Exponentiation)]
    [InlineData(TokenType.NotEquals, TokenType.Addition)]
    [InlineData(TokenType.NotEquals, TokenType.Subtraction)]
    [InlineData(TokenType.NotEquals, TokenType.Multiplication)]
    [InlineData(TokenType.NotEquals, TokenType.Division)]
    [InlineData(TokenType.NotEquals, TokenType.Modulus)]
    [InlineData(TokenType.NotEquals, TokenType.Exponentiation)]
    [InlineData(TokenType.GreaterThan, TokenType.Addition)]
    [InlineData(TokenType.GreaterThan, TokenType.Subtraction)]
    [InlineData(TokenType.GreaterThan, TokenType.Multiplication)]
    [InlineData(TokenType.GreaterThan, TokenType.Division)]
    [InlineData(TokenType.GreaterThan, TokenType.Modulus)]
    [InlineData(TokenType.GreaterThan, TokenType.Exponentiation)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Addition)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Subtraction)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Multiplication)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Division)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Modulus)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Exponentiation)]
    [InlineData(TokenType.Addition, TokenType.Multiplication)]
    [InlineData(TokenType.Addition, TokenType.Division)]
    [InlineData(TokenType.Addition, TokenType.Modulus)]
    [InlineData(TokenType.Addition, TokenType.Exponentiation)]
    [InlineData(TokenType.Subtraction, TokenType.Multiplication)]
    [InlineData(TokenType.Subtraction, TokenType.Division)]
    [InlineData(TokenType.Subtraction, TokenType.Modulus)]
    [InlineData(TokenType.Subtraction, TokenType.Exponentiation)]
    [InlineData(TokenType.Multiplication, TokenType.Exponentiation)]
    [InlineData(TokenType.Division, TokenType.Exponentiation)]
    [InlineData(TokenType.Modulus, TokenType.Exponentiation)]
    public void Parse_LoPrecedenceOpThenHiPrecedenceOp_ReturnsParseTreeWithLastTwoTermsGroupedFirst(TokenType loOp, TokenType hiOp)
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
            {
                new Token
                {
                    Type = TokenType.Integer
                },
                new Token
                {
                    Type = loOp,
                },
                new Token
                {
                    Type = TokenType.Integer
                },
                new Token
                {
                    Type = hiOp,
                },
                new Token
                {
                    Type = TokenType.Integer
                }
            }));

        var expected = new Program(new List<Statement>
        {
            new BinaryExpression(
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
                    })))
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData(TokenType.Or, TokenType.And)]
    [InlineData(TokenType.Or, TokenType.LessThan)]
    [InlineData(TokenType.Or, TokenType.LessThanOrEqual)]
    [InlineData(TokenType.Or, TokenType.Equals)]
    [InlineData(TokenType.Or, TokenType.NotEquals)]
    [InlineData(TokenType.Or, TokenType.GreaterThan)]
    [InlineData(TokenType.Or, TokenType.GreaterThanOrEqual)]
    [InlineData(TokenType.Or, TokenType.Addition)]
    [InlineData(TokenType.Or, TokenType.Subtraction)]
    [InlineData(TokenType.Or, TokenType.Multiplication)]
    [InlineData(TokenType.Or, TokenType.Division)]
    [InlineData(TokenType.Or, TokenType.Modulus)]
    [InlineData(TokenType.Or, TokenType.Exponentiation)]
    [InlineData(TokenType.And, TokenType.LessThan)]
    [InlineData(TokenType.And, TokenType.LessThanOrEqual)]
    [InlineData(TokenType.And, TokenType.Equals)]
    [InlineData(TokenType.And, TokenType.NotEquals)]
    [InlineData(TokenType.And, TokenType.GreaterThan)]
    [InlineData(TokenType.And, TokenType.GreaterThanOrEqual)]
    [InlineData(TokenType.And, TokenType.Addition)]
    [InlineData(TokenType.And, TokenType.Subtraction)]
    [InlineData(TokenType.And, TokenType.Multiplication)]
    [InlineData(TokenType.And, TokenType.Division)]
    [InlineData(TokenType.And, TokenType.Modulus)]
    [InlineData(TokenType.And, TokenType.Exponentiation)]
    [InlineData(TokenType.LessThan, TokenType.Addition)]
    [InlineData(TokenType.LessThan, TokenType.Subtraction)]
    [InlineData(TokenType.LessThan, TokenType.Multiplication)]
    [InlineData(TokenType.LessThan, TokenType.Division)]
    [InlineData(TokenType.LessThan, TokenType.Modulus)]
    [InlineData(TokenType.LessThan, TokenType.Exponentiation)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Addition)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Subtraction)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Multiplication)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Division)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Modulus)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Exponentiation)]
    [InlineData(TokenType.Equals, TokenType.Addition)]
    [InlineData(TokenType.Equals, TokenType.Subtraction)]
    [InlineData(TokenType.Equals, TokenType.Multiplication)]
    [InlineData(TokenType.Equals, TokenType.Division)]
    [InlineData(TokenType.Equals, TokenType.Modulus)]
    [InlineData(TokenType.Equals, TokenType.Exponentiation)]
    [InlineData(TokenType.NotEquals, TokenType.Addition)]
    [InlineData(TokenType.NotEquals, TokenType.Subtraction)]
    [InlineData(TokenType.NotEquals, TokenType.Multiplication)]
    [InlineData(TokenType.NotEquals, TokenType.Division)]
    [InlineData(TokenType.NotEquals, TokenType.Modulus)]
    [InlineData(TokenType.NotEquals, TokenType.Exponentiation)]
    [InlineData(TokenType.GreaterThan, TokenType.Addition)]
    [InlineData(TokenType.GreaterThan, TokenType.Subtraction)]
    [InlineData(TokenType.GreaterThan, TokenType.Multiplication)]
    [InlineData(TokenType.GreaterThan, TokenType.Division)]
    [InlineData(TokenType.GreaterThan, TokenType.Modulus)]
    [InlineData(TokenType.GreaterThan, TokenType.Exponentiation)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Addition)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Subtraction)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Multiplication)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Division)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Modulus)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Exponentiation)]
    [InlineData(TokenType.Addition, TokenType.Multiplication)]
    [InlineData(TokenType.Addition, TokenType.Division)]
    [InlineData(TokenType.Addition, TokenType.Modulus)]
    [InlineData(TokenType.Addition, TokenType.Exponentiation)]
    [InlineData(TokenType.Subtraction, TokenType.Multiplication)]
    [InlineData(TokenType.Subtraction, TokenType.Division)]
    [InlineData(TokenType.Subtraction, TokenType.Modulus)]
    [InlineData(TokenType.Subtraction, TokenType.Exponentiation)]
    [InlineData(TokenType.Multiplication, TokenType.Exponentiation)]
    [InlineData(TokenType.Division, TokenType.Exponentiation)]
    [InlineData(TokenType.Modulus, TokenType.Exponentiation)]    
    public void Parse_HiPrecedenceOpThenLoPrecedenceOp_ReturnsParseTreeWithFirstTwoTermsGroupedFirst(TokenType loOp, TokenType hiOp)
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
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
            }));

        var expected = new Program(new List<Statement>
        {
            new BinaryExpression(
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
                }))
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData(TokenType.Addition)]
    [InlineData(TokenType.Subtraction)]
    [InlineData(TokenType.Multiplication)]
    [InlineData(TokenType.Division)]
    [InlineData(TokenType.Modulus)]
    [InlineData(TokenType.LessThan)]
    [InlineData(TokenType.LessThanOrEqual)]
    [InlineData(TokenType.Equals)]
    [InlineData(TokenType.NotEquals)]
    [InlineData(TokenType.GreaterThan)]
    [InlineData(TokenType.GreaterThanOrEqual)]
    [InlineData(TokenType.And)]
    [InlineData(TokenType.Or)]
    public void Parse_LeftAssociativeOperator_ReturnsParseTreeWithFirstTwoTermsGroupedFirst(TokenType op)
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
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
            }));

        var expected = new Program(new List<Statement>
        {
            new BinaryExpression(
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
                }))
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());
        
        // Assert
        Assert.Equal(expected, actual);
    }    

    [Theory]
    [InlineData(TokenType.Exponentiation)]
    public void Parse_RightAssociativeOperator_ReturnsParseTreeWithLastTwoTermsGroupedFirst(TokenType op)
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
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
            }));

        var expected = new Program(new List<Statement>
        {
            new BinaryExpression(
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
                    })))
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());
        
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
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
            {
                new Token
                {
                    Type = op
                },
                new Token
                {
                    Type = operand
                }
            }));
        
        var expected = new Program(new List<Statement>
        {
            new UnaryExpression(
                new Token
                {
                    Type = op
                },
                new ConstantExpression(new Token { Type = operand }))
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_FunctionCallNoParameters_ReturnsFunctionCallExpression()
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
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
            }));
        
        var expected = new Program(new List<Statement>
        {
            new FunctionCall(
                new Token
                {
                    Type = TokenType.Identifier
                },
                Array.Empty<Expression>())
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());

        // Arrange
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [InlineData(TokenType.Boolean)]
    [InlineData(TokenType.Integer)]
    [InlineData(TokenType.Float)]
    public void Parse_FunctionCallOneConstantParameter_ReturnsFunctionCallExpression(TokenType parameterType)
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
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
            }));
        
        var expected = new Program(new List<Statement>
        {
            new FunctionCall(
                new Token
                {
                    Type = TokenType.Identifier
                },
                new[]
                {
                    new ConstantExpression(new Token
                    {
                        Type = parameterType
                    })
                })
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(TokenType.Boolean, TokenType.Boolean)]
    [InlineData(TokenType.Boolean, TokenType.Integer)]
    [InlineData(TokenType.Boolean, TokenType.Float)]
    [InlineData(TokenType.Integer, TokenType.Boolean)]
    [InlineData(TokenType.Integer, TokenType.Integer)]
    [InlineData(TokenType.Integer, TokenType.Float)]
    [InlineData(TokenType.Float, TokenType.Boolean)]
    [InlineData(TokenType.Float, TokenType.Integer)]
    [InlineData(TokenType.Float, TokenType.Float)]
    public void Parse_FunctionCallTwoConstantParameters_ReturnsFunctionCallExpression(TokenType parameterType1, TokenType parameterType2)
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
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
            }));
        
        var expected = new Program(new List<Statement>
        {
            new FunctionCall(
                new Token
                {
                    Type = TokenType.Identifier
                },
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
                })
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(TokenType.Boolean)]
    [InlineData(TokenType.Integer)]
    [InlineData(TokenType.Float)]
    public void Parse_ParenthesizedConstant_ReturnsConstantExpression(TokenType tokenType)
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
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
            }));
        
        var expected = new Program(new List<Statement>
        {
            new ConstantExpression(new Token
            {
                Type = tokenType
            })
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());

        // Arrange
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_ParenthesizedIdentifier_ReturnsVariableReferenceExpression()
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
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
            }));
        
        var expected = new Program(new List<Statement>
        {
            new VariableReferenceExpression(
                new Token
                {
                    Type = TokenType.Identifier
                }, 
                Array.Empty<Expression>())
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());

        // Arrange
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_ParenthesizedFunctionCall_ReturnsFunctionCallExpression()
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
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
            }));
        
        var expected = new Program(new List<Statement>
        {
            new FunctionCall(new Token
            {
                Type = TokenType.Identifier
            },
            Array.Empty<Expression>())
        });
        
        // Act
        var actual = _sut.Parse(It.IsAny<string>());

        // Arrange
        Assert.Equal(expected, actual);
    }

    [Theory]
    [InlineData(TokenType.Or, TokenType.And)]
    [InlineData(TokenType.Or, TokenType.LessThan)]
    [InlineData(TokenType.Or, TokenType.LessThanOrEqual)]
    [InlineData(TokenType.Or, TokenType.Equals)]
    [InlineData(TokenType.Or, TokenType.NotEquals)]
    [InlineData(TokenType.Or, TokenType.GreaterThan)]
    [InlineData(TokenType.Or, TokenType.GreaterThanOrEqual)]
    [InlineData(TokenType.Or, TokenType.Addition)]
    [InlineData(TokenType.Or, TokenType.Subtraction)]
    [InlineData(TokenType.Or, TokenType.Multiplication)]
    [InlineData(TokenType.Or, TokenType.Division)]
    [InlineData(TokenType.Or, TokenType.Modulus)]
    [InlineData(TokenType.Or, TokenType.Exponentiation)]
    [InlineData(TokenType.And, TokenType.LessThan)]
    [InlineData(TokenType.And, TokenType.LessThanOrEqual)]
    [InlineData(TokenType.And, TokenType.Equals)]
    [InlineData(TokenType.And, TokenType.NotEquals)]
    [InlineData(TokenType.And, TokenType.GreaterThan)]
    [InlineData(TokenType.And, TokenType.GreaterThanOrEqual)]
    [InlineData(TokenType.And, TokenType.Addition)]
    [InlineData(TokenType.And, TokenType.Subtraction)]
    [InlineData(TokenType.And, TokenType.Multiplication)]
    [InlineData(TokenType.And, TokenType.Division)]
    [InlineData(TokenType.And, TokenType.Modulus)]
    [InlineData(TokenType.And, TokenType.Exponentiation)]
    [InlineData(TokenType.LessThan, TokenType.Addition)]
    [InlineData(TokenType.LessThan, TokenType.Subtraction)]
    [InlineData(TokenType.LessThan, TokenType.Multiplication)]
    [InlineData(TokenType.LessThan, TokenType.Division)]
    [InlineData(TokenType.LessThan, TokenType.Modulus)]
    [InlineData(TokenType.LessThan, TokenType.Exponentiation)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Addition)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Subtraction)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Multiplication)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Division)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Modulus)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Exponentiation)]
    [InlineData(TokenType.Equals, TokenType.Addition)]
    [InlineData(TokenType.Equals, TokenType.Subtraction)]
    [InlineData(TokenType.Equals, TokenType.Multiplication)]
    [InlineData(TokenType.Equals, TokenType.Division)]
    [InlineData(TokenType.Equals, TokenType.Modulus)]
    [InlineData(TokenType.Equals, TokenType.Exponentiation)]
    [InlineData(TokenType.NotEquals, TokenType.Addition)]
    [InlineData(TokenType.NotEquals, TokenType.Subtraction)]
    [InlineData(TokenType.NotEquals, TokenType.Multiplication)]
    [InlineData(TokenType.NotEquals, TokenType.Division)]
    [InlineData(TokenType.NotEquals, TokenType.Modulus)]
    [InlineData(TokenType.NotEquals, TokenType.Exponentiation)]
    [InlineData(TokenType.GreaterThan, TokenType.Addition)]
    [InlineData(TokenType.GreaterThan, TokenType.Subtraction)]
    [InlineData(TokenType.GreaterThan, TokenType.Multiplication)]
    [InlineData(TokenType.GreaterThan, TokenType.Division)]
    [InlineData(TokenType.GreaterThan, TokenType.Modulus)]
    [InlineData(TokenType.GreaterThan, TokenType.Exponentiation)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Addition)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Subtraction)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Multiplication)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Division)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Modulus)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Exponentiation)]
    [InlineData(TokenType.Addition, TokenType.Multiplication)]
    [InlineData(TokenType.Addition, TokenType.Division)]
    [InlineData(TokenType.Addition, TokenType.Modulus)]
    [InlineData(TokenType.Addition, TokenType.Exponentiation)]
    [InlineData(TokenType.Subtraction, TokenType.Multiplication)]
    [InlineData(TokenType.Subtraction, TokenType.Division)]
    [InlineData(TokenType.Subtraction, TokenType.Modulus)]
    [InlineData(TokenType.Subtraction, TokenType.Exponentiation)]
    [InlineData(TokenType.Multiplication, TokenType.Exponentiation)]
    [InlineData(TokenType.Division, TokenType.Exponentiation)]
    [InlineData(TokenType.Modulus, TokenType.Exponentiation)] 
    public void Parse_ParenthesizedLoPrecedenceOpThenHighPrecedenceOp_ReturnsParseTreeWithFirstTwoTermsGroupedFirst(TokenType loOp, TokenType hiOp)
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
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
            }));

        var expected = new Program(new List<Statement>
        {
            new BinaryExpression(
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
                }))
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
        [Theory]
    [InlineData(TokenType.Or, TokenType.And)]
    [InlineData(TokenType.Or, TokenType.LessThan)]
    [InlineData(TokenType.Or, TokenType.LessThanOrEqual)]
    [InlineData(TokenType.Or, TokenType.Equals)]
    [InlineData(TokenType.Or, TokenType.NotEquals)]
    [InlineData(TokenType.Or, TokenType.GreaterThan)]
    [InlineData(TokenType.Or, TokenType.GreaterThanOrEqual)]
    [InlineData(TokenType.Or, TokenType.Addition)]
    [InlineData(TokenType.Or, TokenType.Subtraction)]
    [InlineData(TokenType.Or, TokenType.Multiplication)]
    [InlineData(TokenType.Or, TokenType.Division)]
    [InlineData(TokenType.Or, TokenType.Modulus)]
    [InlineData(TokenType.Or, TokenType.Exponentiation)]
    [InlineData(TokenType.And, TokenType.LessThan)]
    [InlineData(TokenType.And, TokenType.LessThanOrEqual)]
    [InlineData(TokenType.And, TokenType.Equals)]
    [InlineData(TokenType.And, TokenType.NotEquals)]
    [InlineData(TokenType.And, TokenType.GreaterThan)]
    [InlineData(TokenType.And, TokenType.GreaterThanOrEqual)]
    [InlineData(TokenType.And, TokenType.Addition)]
    [InlineData(TokenType.And, TokenType.Subtraction)]
    [InlineData(TokenType.And, TokenType.Multiplication)]
    [InlineData(TokenType.And, TokenType.Division)]
    [InlineData(TokenType.And, TokenType.Modulus)]
    [InlineData(TokenType.And, TokenType.Exponentiation)]
    [InlineData(TokenType.LessThan, TokenType.Addition)]
    [InlineData(TokenType.LessThan, TokenType.Subtraction)]
    [InlineData(TokenType.LessThan, TokenType.Multiplication)]
    [InlineData(TokenType.LessThan, TokenType.Division)]
    [InlineData(TokenType.LessThan, TokenType.Modulus)]
    [InlineData(TokenType.LessThan, TokenType.Exponentiation)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Addition)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Subtraction)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Multiplication)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Division)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Modulus)]
    [InlineData(TokenType.LessThanOrEqual, TokenType.Exponentiation)]
    [InlineData(TokenType.Equals, TokenType.Addition)]
    [InlineData(TokenType.Equals, TokenType.Subtraction)]
    [InlineData(TokenType.Equals, TokenType.Multiplication)]
    [InlineData(TokenType.Equals, TokenType.Division)]
    [InlineData(TokenType.Equals, TokenType.Modulus)]
    [InlineData(TokenType.Equals, TokenType.Exponentiation)]
    [InlineData(TokenType.NotEquals, TokenType.Addition)]
    [InlineData(TokenType.NotEquals, TokenType.Subtraction)]
    [InlineData(TokenType.NotEquals, TokenType.Multiplication)]
    [InlineData(TokenType.NotEquals, TokenType.Division)]
    [InlineData(TokenType.NotEquals, TokenType.Modulus)]
    [InlineData(TokenType.NotEquals, TokenType.Exponentiation)]
    [InlineData(TokenType.GreaterThan, TokenType.Addition)]
    [InlineData(TokenType.GreaterThan, TokenType.Subtraction)]
    [InlineData(TokenType.GreaterThan, TokenType.Multiplication)]
    [InlineData(TokenType.GreaterThan, TokenType.Division)]
    [InlineData(TokenType.GreaterThan, TokenType.Modulus)]
    [InlineData(TokenType.GreaterThan, TokenType.Exponentiation)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Addition)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Subtraction)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Multiplication)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Division)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Modulus)]
    [InlineData(TokenType.GreaterThanOrEqual, TokenType.Exponentiation)]
    [InlineData(TokenType.Addition, TokenType.Multiplication)]
    [InlineData(TokenType.Addition, TokenType.Division)]
    [InlineData(TokenType.Addition, TokenType.Modulus)]
    [InlineData(TokenType.Addition, TokenType.Exponentiation)]
    [InlineData(TokenType.Subtraction, TokenType.Multiplication)]
    [InlineData(TokenType.Subtraction, TokenType.Division)]
    [InlineData(TokenType.Subtraction, TokenType.Modulus)]
    [InlineData(TokenType.Subtraction, TokenType.Exponentiation)]
    [InlineData(TokenType.Multiplication, TokenType.Exponentiation)]
    [InlineData(TokenType.Division, TokenType.Exponentiation)]
    [InlineData(TokenType.Modulus, TokenType.Exponentiation)] 
    public void Parse_HiPrecedenceOpThenParenthesizedLoPrecedenceOp_ReturnsParseTreeWithLastTwoTermsGroupedFirst(TokenType hiOp, TokenType loOp)
    {
        // Arrange
        _tokenizerMock
            .Setup(x => x.Tokenize(It.IsAny<string>()))
            .Returns(new TokenStream(new[]
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
            }));

        var expected = new Program(new List<Statement>
        {
            new BinaryExpression(
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
                    })))
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());
        
        // Assert
        Assert.Equal(expected, actual);
    }
}
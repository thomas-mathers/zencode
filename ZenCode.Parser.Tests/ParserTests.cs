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
        _tokenizerMock.Setup(x => x.Tokenize(It.IsAny<string>())).Returns(new[]
        {
            new Token
            {
                Type = TokenType.Boolean
            }
        });

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
        _tokenizerMock.Setup(x => x.Tokenize(It.IsAny<string>())).Returns(new[]
        {
            new Token
            {
                Type = TokenType.Integer
            }
        });

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
        _tokenizerMock.Setup(x => x.Tokenize(It.IsAny<string>())).Returns(new[]
        {
            new Token
            {
                Type = TokenType.Float
            }
        });

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
    public void Parse_Identifier_ReturnsConstantExpression()
    {
        // Arrange
        _tokenizerMock.Setup(x => x.Tokenize(It.IsAny<string>())).Returns(new[]
        {
            new Token
            {
                Type = TokenType.Identifier
            }
        });

        var expected = new Program(new List<Statement>
        {
            new IdentifierExpression(new Token
            {
                Type = TokenType.Identifier
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
        _tokenizerMock.Setup(x => x.Tokenize(It.IsAny<string>())).Returns(new[]
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
    [InlineData(TokenType.Not, TokenType.Boolean)]
    [InlineData(TokenType.Not, TokenType.Integer)]
    [InlineData(TokenType.Not, TokenType.Float)]
    public void Parse_OpConstant_ReturnsUnaryExpression(TokenType op, TokenType operand)
    {
        // Arrange
        _tokenizerMock.Setup(x => x.Tokenize(It.IsAny<string>())).Returns(new[]
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
    public void Parse_FunctionCall_ReturnsFunctionCallExpression()
    {
        // Arrange
        _tokenizerMock.Setup(x => x.Tokenize(It.IsAny<string>())).Returns(new[]
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
                Type = TokenType.Identifier
            },
            new Token
            {
                Type = TokenType.RightParenthesis
            }
        });
        
        var expected = new Program(new List<Statement>
        {
            new FunctionCall(
                new Token
                {
                    Type = TokenType.Identifier
                },
                new[]
                {
                    new IdentifierExpression(new Token
                    {
                        Type = TokenType.Identifier
                    })
                })
        });

        // Act
        var actual = _sut.Parse(It.IsAny<string>());

        // Arrange
        Assert.Equal(expected, actual);
    }
}
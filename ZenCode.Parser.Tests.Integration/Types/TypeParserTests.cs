using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Types;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Tests.Integration.Types;

public class TypeParserTests
{
    public static readonly IEnumerable<object[]> PrimitiveReturnTypes = new List<object[]>
    {
        new object[] { TokenType.Void, new VoidType() },
        new object[] { TokenType.Boolean, new BooleanType() },
        new object[] { TokenType.Integer, new IntegerType() },
        new object[] { TokenType.Float, new FloatType() },
        new object[] { TokenType.String, new StringType() }
    };

    private readonly IParser _sut;

    public TypeParserTests()
    {
        _sut = new ParserFactory().Create();
    }

    [Theory]
    [MemberData(nameof(PrimitiveReturnTypes))]
    public void Parse_PrimitiveType_ReturnsPrimitiveType(TokenType tokenType, Type expectedType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[] { new Token(tokenType) });

        // Act
        var actualType = _sut.ParseType(tokenStream);

        // Assert
        Assert.Equal(expectedType, actualType);
    }

    [Theory]
    [MemberData(nameof(PrimitiveReturnTypes))]
    public void Parse_Array_ReturnsArrayType(TokenType tokenType, Type type)
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(tokenType),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket)
            }
        );

        var expectedType = new ArrayType { BaseType = type };

        // Act
        var actualType = _sut.ParseType(tokenStream);

        // Assert
        Assert.Equal(expectedType, actualType);
    }

    [Theory]
    [MemberData(nameof(PrimitiveReturnTypes))]
    public void Parse_JaggedArray_ReturnsArrayType(TokenType tokenType, Type type)
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(tokenType),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket),
                new Token(TokenType.LeftBracket),
                new Token(TokenType.RightBracket)
            }
        );

        var expectedType = new ArrayType
        {
            BaseType = new ArrayType
            {
                BaseType = new ArrayType
                {
                    BaseType = type
                }
            }
        };

        // Act
        var actualType = _sut.ParseType(tokenStream);

        // Assert
        Assert.Equal(expectedType, actualType);
    }

    [Theory]
    [MemberData(nameof(PrimitiveReturnTypes))]
    public void Parse_FunctionWithNoParameters_ReturnsFunctionType(TokenType tokenType, Type type)
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(tokenType)
            }
        );

        var expectedType = new FunctionType
        {
            ReturnType = type
        };

        // Act
        var actualType = _sut.ParseType(tokenStream);

        // Assert
        Assert.Equal(expectedType, actualType);
    }

    [Theory]
    [MemberData(nameof(PrimitiveReturnTypes))]
    public void Parse_FunctionWithOneParameter_ReturnsFunctionType(TokenType tokenType, Type type)
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.LeftParenthesis),
                new Token(tokenType),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(tokenType)
            }
        );

        var expectedType = new FunctionType
        {
            ReturnType = type, 
            ParameterTypes = new TypeList(type)
        };

        // Act
        var actualType = _sut.ParseType(tokenStream);

        // Assert
        Assert.Equal(expectedType, actualType);
    }

    [Theory]
    [MemberData(nameof(PrimitiveReturnTypes))]
    public void Parse_FunctionWithMultipleParameters_ReturnsFunctionType(TokenType tokenType, Type type)
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.LeftParenthesis),
                new Token(tokenType),
                new Token(TokenType.Comma),
                new Token(tokenType),
                new Token(TokenType.Comma),
                new Token(tokenType),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(tokenType)
            }
        );

        var expectedType = new FunctionType
        {
            ReturnType = type, 
            ParameterTypes = new TypeList(type, type, type)
        };

        // Act
        var actualType = _sut.ParseType(tokenStream);

        // Assert
        Assert.Equal(expectedType, actualType);
    }

    [Theory]
    [MemberData(nameof(PrimitiveReturnTypes))]
    public void Parse_FunctionWithFunctionParameter_ReturnsFunctionType(TokenType tokenType, Type type)
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(tokenType),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(tokenType),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(tokenType)
            }
        );

        var innerInnerFunctionType = new FunctionType
        {
            ReturnType = type
        };

        var innerFunctionType = new FunctionType
        {
            ReturnType = type, 
            ParameterTypes = new TypeList(innerInnerFunctionType)
        };

        var expectedType = new FunctionType
        {
            ReturnType = type,
            ParameterTypes = new TypeList(innerFunctionType)
        };

        // Act
        var actualType = _sut.ParseType(tokenStream);

        // Assert
        Assert.Equal(expectedType, actualType);
    }

    [Theory]
    [MemberData(nameof(PrimitiveReturnTypes))]
    public void Parse_FunctionWithFunctionReturnType_ReturnsFunctionType(TokenType tokenType, Type type)
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(tokenType)
            }
        );

        var expectedType = new FunctionType
        {
            ReturnType = new FunctionType
            {
                ReturnType = type
            }
        };

        // Act
        var actualType = _sut.ParseType(tokenStream);

        // Assert
        Assert.Equal(expectedType, actualType);
    }

    [Theory]
    [MemberData(nameof(PrimitiveReturnTypes))]
    public void Parse_FunctionWithFunctionParameterAndFunctionReturnType_ReturnsFunctionType
    (
        TokenType tokenType,
        Type type
    )
    {
        // Arrange
        var tokenStream = new TokenStream
        (
            new[]
            {
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(tokenType),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(TokenType.LeftParenthesis),
                new Token(TokenType.RightParenthesis),
                new Token(TokenType.RightArrow),
                new Token(tokenType)
            }
        );

        var expectedType = new FunctionType
        {
            ReturnType = new FunctionType
            {
                ReturnType = type
            },
            ParameterTypes = new TypeList
            (
                new FunctionType
                {
                    ReturnType = type
                }
            )
        };

        // Act
        var actualType = _sut.ParseType(tokenStream);

        // Assert
        Assert.Equal(expectedType, actualType);
    }
}

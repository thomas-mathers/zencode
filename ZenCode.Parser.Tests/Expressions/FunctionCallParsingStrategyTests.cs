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

public class FunctionCallParsingStrategyTests
{
    public static readonly IEnumerable<object[]> ConstantTokenTypes =
        from c in TokenTypeGroups.GetConstants()
        select new object[] { c };
    
    public static readonly IEnumerable<object[]> ConstantTokenTypePairs =
        from c1 in TokenTypeGroups.GetConstants()
        from c2 in TokenTypeGroups.GetConstants()
        select new object[] { c1, c2 };
    
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly FunctionCallParsingStrategy _sut;
    private readonly VariableReferenceExpression _variableReferenceExpression;

    public FunctionCallParsingStrategyTests()
    {
        _sut = new FunctionCallParsingStrategy(_expressionParserMock.Object, 7);
        _variableReferenceExpression =
            new VariableReferenceExpression(new Token(TokenType.Identifier));
    }

    [Fact]
    public void Parse_FunctionCallMissingRightParenthesis_ThrowsUnexpectedTokenException()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.Integer),
        });

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream, _variableReferenceExpression));
    }

    [Fact]
    public void Parse_FunctionCallMissingCommas_ThrowsUnexpectedTokenException()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.Integer),    
            new Token(TokenType.Integer),    
            new Token(TokenType.RightParenthesis)
        });

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream, _variableReferenceExpression));
    }
    
    [Theory]
    [MemberData(nameof(ConstantTokenTypes))]
    public void Parse_FunctionCallNoVariableReferenceExpression_ThrowsUnexpectedTokenException(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(tokenType),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.Integer),       
            new Token(TokenType.RightParenthesis)
        });

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream, _variableReferenceExpression));
    }
    
    [Fact]
    public void Parse_FunctionCallMissingIndexExpression_ThrowsUnexpectedTokenException()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.Integer),
            new Token(TokenType.Comma),
            new Token(TokenType.Comma),
            new Token(TokenType.Integer),            
            new Token(TokenType.RightParenthesis)
        });

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream, _variableReferenceExpression));
    }

    [Fact]
    public void Parse_FunctionCallDanglingComma_ThrowsUnexpectedTokenException()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.Integer),
            new Token(TokenType.Comma),
            new Token(TokenType.RightParenthesis)
        });

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream, _variableReferenceExpression));
    }

    [Fact]
    public void Parse_FunctionCallNoParameters_ReturnsFunctionCallExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.RightParenthesis)
        });

        var expected = new FunctionCall(_variableReferenceExpression);

        // Act
        var actual = _sut.Parse(tokenStream, _variableReferenceExpression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(ConstantTokenTypes))]
    public void Parse_FunctionCallOneConstantParameter_ReturnsFunctionCallExpression(TokenType parameterType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(parameterType),
            new Token(TokenType.RightParenthesis)
        });
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new ConstantExpression(new Token(parameterType)))
            .Callback<ITokenStream, int>((_, _) => { tokenStream.Consume(); });

        var expected = new FunctionCall(_variableReferenceExpression)
        {
            Parameters = new[]
            {
                new ConstantExpression(new Token(parameterType))
            }
        };

        // Act
        var actual = _sut.Parse(tokenStream, _variableReferenceExpression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(ConstantTokenTypePairs))]
    public void Parse_FunctionCallTwoConstantParameters_ReturnsFunctionCallExpression(TokenType parameterType1,
        TokenType parameterType2)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(parameterType1),
            new Token(TokenType.Comma),
            new Token(parameterType2),
            new Token(TokenType.RightParenthesis)
        });

        var invocationCount = 0;

        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(() =>
            {
                return ++invocationCount switch
                {
                    1 => new ConstantExpression(new Token(parameterType1)),
                    _ => new ConstantExpression(new Token(parameterType2))
                };
            })
            .Callback<ITokenStream, int>((_, _) => { tokenStream.Consume(); });

        var expected = new FunctionCall(_variableReferenceExpression)
        {
            Parameters = new[]
            {
                new ConstantExpression(new Token(parameterType1)),
                new ConstantExpression(new Token(parameterType2))
            }
        };

        // Act
        var actual = _sut.Parse(tokenStream, _variableReferenceExpression);

        // Assert
        Assert.Equal(expected, actual);
    }
}
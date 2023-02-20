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
    public static readonly IEnumerable<object[]> ConstantTokenTypes =
        from c in TokenTypeGroups.GetConstants()
        select new object[] { c };
    
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
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.Integer),
            new Token(TokenType.RightParenthesis)
        });
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new ConstantExpression(new Token(TokenType.Integer)))
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
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.RightParenthesis),
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
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.Integer)
        });
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new ConstantExpression(new Token(TokenType.Integer)))
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream));
    }

    [Theory]
    [MemberData(nameof(ConstantTokenTypes))]
    public void Parse_ParenthesizedConstant_ReturnsConstantExpression(TokenType constantType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(constantType),
            new Token(TokenType.RightParenthesis)
        });
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new ConstantExpression(new Token(constantType)))
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });

        var expected = new ConstantExpression(new Token(constantType));

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
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.Identifier),
            new Token(TokenType.RightParenthesis)
        });
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new VariableReferenceExpression(new Token(TokenType.Identifier)))
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
            });

        var expected = new VariableReferenceExpression(new Token(TokenType.Identifier));

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
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.RightParenthesis),
            new Token(TokenType.RightParenthesis)
        });
        
        _expressionParserMock.Setup(x => x.Parse(tokenStream, 0))
            .Returns(new FunctionCall(new VariableReferenceExpression(new Token(TokenType.Identifier))))
            .Callback<ITokenStream, int>((_, _) =>
            {
                tokenStream.Consume();
                tokenStream.Consume();
                tokenStream.Consume();
            });

        var expected = new FunctionCall(new VariableReferenceExpression(new Token(TokenType.Identifier)));

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }
}
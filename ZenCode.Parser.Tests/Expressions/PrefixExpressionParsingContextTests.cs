using Moq;
using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Expressions;

namespace ZenCode.Parser.Tests.Expressions;

public class PrefixExpressionParsingContextTests
{
    private readonly IReadOnlyDictionary<TokenType, Mock<IPrefixExpressionParsingStrategy>> _strategyMocks;
    private readonly PrefixExpressionParsingContext _sut;

    public PrefixExpressionParsingContextTests()
    {
        _strategyMocks = new Dictionary<TokenType, Mock<IPrefixExpressionParsingStrategy>>()
        {
            [TokenType.Boolean] = new(),
            [TokenType.Integer] = new(),
            [TokenType.Float] = new(),
            [TokenType.String] = new(),
            [TokenType.Identifier] = new(),
            [TokenType.Subtraction] = new(),
            [TokenType.Not] = new(),
            [TokenType.LeftParenthesis] = new()
        };

        _sut = new PrefixExpressionParsingContext
        {
            Strategies = _strategyMocks.ToDictionary(k => k.Key, v => v.Value.Object)
        };
    }

    [Fact]
    public void Parse_UnknownTokenType_ThrowsUnexpectedTokenException()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Addition)
        });
        
        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream));
    }
    
    [Theory]
    [InlineData(TokenType.Boolean)] 
    [InlineData(TokenType.Integer)] 
    [InlineData(TokenType.Float)]
    [InlineData(TokenType.Identifier)] 
    [InlineData(TokenType.Subtraction)] 
    [InlineData(TokenType.Not)] 
    [InlineData(TokenType.LeftParenthesis)]
    public void Parse_KnownTokenType_ReturnsSameValueAsStrategy(TokenType operatorToken)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(operatorToken)
        });
        
        var expected = new Expression();

        _strategyMocks[operatorToken].Setup(x => x.Parse(tokenStream)).Returns(expected);
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}
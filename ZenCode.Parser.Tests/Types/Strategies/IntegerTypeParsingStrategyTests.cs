using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Types;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser.Tests.Types.Strategies;

public class IntegerTypeParsingStrategyTests
{
    public static readonly IEnumerable<object[]> OtherTokenTypes =
        Enum.GetValues<TokenType>().Where(t => t != TokenType.Integer).Select(t => new object[] { t });
    
    private readonly IntegerTypeParsingStrategy _sut = new();
    
    [Fact]
    public void Parse_Integer_ReturnsIntegerType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Integer),
        });

        var expected = new IntegerType();
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Theory]
    [MemberData(nameof(OtherTokenTypes))]
    public void Parse_NotFloat_ThrowsUnexpectedTokenException(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(tokenType),
        });
        
        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream));
    }
}
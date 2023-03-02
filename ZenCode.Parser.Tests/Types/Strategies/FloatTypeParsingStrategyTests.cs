using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Types;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser.Tests.Types.Strategies;

public class FloatTypeParsingStrategyTests
{
    public static readonly IEnumerable<object[]> OtherTokenTypes =
        Enum.GetValues<TokenType>().Where(t => t != TokenType.Float).Select(t => new object[] { t });

    private readonly FloatTypeParsingStrategy _sut = new();

    [Fact]
    public void Parse_Float_ReturnsFloatType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Float)
        });

        var expected = new FloatType();

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
            new Token(tokenType)
        });

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream));
    }
}
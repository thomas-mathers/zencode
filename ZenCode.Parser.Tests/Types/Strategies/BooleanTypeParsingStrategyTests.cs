using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Types;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser.Tests.Types.Strategies;

public class BooleanTypeParsingStrategyTests
{
    public static readonly IEnumerable<object[]> OtherTokenTypes =
        Enum.GetValues<TokenType>().Where(t => t != TokenType.Boolean).Select(t => new object[] { t });

    private readonly BooleanTypeParsingStrategy _sut = new();

    [Fact]
    public void Parse_Boolean_ReturnsBooleanType()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Boolean)
        });

        var expected = new BooleanType();

        // Act
        var actual = _sut.Parse(tokenStream);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Theory]
    [MemberData(nameof(OtherTokenTypes))]
    public void Parse_NotBoolean_ThrowsUnexpectedTokenException(TokenType tokenType)
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
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Tests.Expressions.Strategies
{
    public class LiteralParsingStrategyTests
    {
        private readonly Mock<ITokenStream> _tokenStreamMock = new();
        private readonly LiteralParsingStrategy _sut;

        public LiteralParsingStrategyTests()
        {
            _sut = new LiteralParsingStrategy();
        }

        [Theory]
        [InlineData(TokenType.BooleanLiteral)]
        [InlineData(TokenType.IntegerLiteral)]
        [InlineData(TokenType.FloatLiteral)]
        [InlineData(TokenType.StringLiteral)]
        public void Parse_StringLiteral_ReturnsConstantExpression(TokenType tokenType)
        {
            // Arrange
            var expected = new LiteralExpression(new Token(tokenType));

            _tokenStreamMock
                .Setup(x => x.Consume(tokenType))
                .Returns(new Token(tokenType));

            // Act
            var actual = _sut.Parse(_tokenStreamMock.Object, tokenType);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
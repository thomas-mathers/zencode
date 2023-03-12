using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Exceptions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Tests.Expressions.Strategies
{
    public class VariableReferenceParsingStrategyTests
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<ITokenStream> _tokenStreamMock = new();
        private readonly Mock<IParser> _parserMock = new();
        private readonly VariableReferenceParsingStrategy _sut;

        public VariableReferenceParsingStrategyTests()
        {
            _sut = new VariableReferenceParsingStrategy();
        }

        [Fact]
        public void Parse_Identifier_ReturnsVariableReferenceExpression()
        {
            // Arrange
            var expected = new VariableReferenceExpression(new Token(TokenType.Identifier));

            _tokenStreamMock
                .Setup(x => x.Consume())
                .Returns(new Token(TokenType.Identifier));

            // Act
            var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

            // Assert
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void Parse_ZeroDimensionalArrayReference_ThrowsException()
        {
            // Arrange
            _tokenStreamMock
                .Setup(x => x.Consume())
                .Returns(new Token(TokenType.Identifier));
        
            _tokenStreamMock
                .Setup(x => x.Match(TokenType.LeftBracket))
                .Returns(true);
        
            _tokenStreamMock
                .Setup(x => x.Match(TokenType.RightBracket))
                .Returns(true);

            // Act + Assert
            Assert.Throws<MissingIndexExpressionException>(() => _sut.Parse(_parserMock.Object, _tokenStreamMock.Object));
        }

        [Fact]
        public void Parse_ArrayReference_ReturnsVariableReferenceExpression()
        {
            // Arrange
            var indices = _fixture.Create<ExpressionList>();

            var expected = new VariableReferenceExpression(new Token(TokenType.Identifier))
            {
                Indices = indices
            };
        
            _tokenStreamMock
                .Setup(x => x.Consume())
                .Returns(new Token(TokenType.Identifier));
        
            _tokenStreamMock
                .Setup(x => x.Match(TokenType.LeftBracket))
                .Returns(true);
        
            _tokenStreamMock
                .Setup(x => x.Match(TokenType.RightBracket))
                .Returns(false);

            _parserMock
                .Setup(x => x.ParseExpressionList(_tokenStreamMock.Object))
                .Returns(indices);

            // Act
            var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

            // Assert
            Assert.Equal(expected, actual);
        }
    }
}
using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;

namespace ZenCode.Parser.Tests.Statements.Strategies
{
    public class PrintStatementParsingStrategyTests
    {
        private readonly Fixture _fixture = new();
        private readonly Mock<ITokenStream> _tokenStreamMock = new();
        private readonly Mock<IParser> _parserMock = new();
        private readonly PrintStatementParsingStrategy _sut;

        public PrintStatementParsingStrategyTests()
        {
            _sut = new PrintStatementParsingStrategy();
        }

        [Fact]
        public void Parse_ValidInput_ReturnsPrintStatement()
        {
            // Arrange
            var expected = _fixture.Create<PrintStatement>();

            _parserMock
                .Setup(x => x.ParseExpression(_tokenStreamMock.Object, 0))
                .Returns(expected.Expression);

            // Act
            var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

            // Assert
            Assert.Equal(expected, actual);
        
            _tokenStreamMock.Verify(x => x.Consume(TokenType.Print));
        }
    }
}
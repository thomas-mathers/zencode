using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class ContinueStatementParsingStrategyTests
{
    private readonly ContinueStatementParsingStrategy _sut = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();

    [Fact]
    public void Parse_Break_ReturnsBreakStatement()
    {
        // Act
        var actual = _sut.Parse(_tokenStreamMock.Object);

        // Assert
        Assert.IsType<ContinueStatement>(actual);

        _tokenStreamMock.Verify(x => x.Consume(TokenType.Continue));
    }
}
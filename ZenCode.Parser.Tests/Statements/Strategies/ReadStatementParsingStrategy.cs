using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class ReadStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly ReadStatementParsingStrategy _sut = new();

    [Fact]
    public void Parse_ReadStatement_ReturnsBreakStatement()
    {
        // Arrange
        var variableReferenceExpression = _fixture.Create<VariableReferenceExpression>();

        _parserMock
            .Setup(x => x.ParseVariableReferenceExpression(_tokenStreamMock.Object))
            .Returns(variableReferenceExpression);
        
        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);
        
        // Assert
        Assert.IsType<ReadStatement>(actual);
        
        _tokenStreamMock.Verify(x => x.Consume(TokenType.Read));
    }
}
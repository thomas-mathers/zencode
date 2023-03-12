using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class VariableDeclarationStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly VariableDeclarationStatementParsingStrategy _sut;

    public VariableDeclarationStatementParsingStrategyTests()
    {
        _sut = new VariableDeclarationStatementParsingStrategy();
    }

    [Fact]
    public void Parse_VariableDeclaration_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var expected = _fixture.Create<VariableDeclarationStatement>();

        _tokenStreamMock
            .Setup(x => x.Consume(TokenType.Identifier))
            .Returns(expected.Identifier);

        _parserMock
            .Setup(x => x.ParseExpression(_tokenStreamMock.Object, 0))
            .Returns(expected.Expression);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);
        
        _tokenStreamMock.Verify(x => x.Consume(TokenType.Var));
        _tokenStreamMock.Verify(x => x.Consume(TokenType.Assignment));
    }
}
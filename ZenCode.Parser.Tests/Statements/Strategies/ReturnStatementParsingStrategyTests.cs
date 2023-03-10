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

public class ReturnStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly ReturnStatementParsingStrategy _sut;
    
    public ReturnStatementParsingStrategyTests()
    {
        _sut = new ReturnStatementParsingStrategy();
    }

    [Fact]
    public void Parse_ReturnNothing_ReturnsReturnStatement()
    {
        // Arrange
        var expected = new ReturnStatement();

        _tokenStreamMock.Setup(x => x.Match(TokenType.Semicolon)).Returns(true);
        
        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);
        
        // Assert
        Assert.Equal(expected, actual);
        
        _tokenStreamMock.Verify(x => x.Consume(TokenType.Return));
    }
    
    [Fact]
    public void Parse_ReturnAnyExpression_ReturnsReturnStatement()
    {
        // Arrange
        var expression = _fixture.Create<Expression>();

        var expected = new ReturnStatement { Expression = expression };

        _tokenStreamMock.Setup(x => x.Match(TokenType.Semicolon)).Returns(false);
        
        _parserMock
            .Setup(x => x.ParseExpression(_tokenStreamMock.Object, 0))
            .Returns(expression);
        
        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);
        
        // Assert
        Assert.Equal(expected, actual);
        
        _tokenStreamMock.Verify(x => x.Consume(TokenType.Return));
    }
}
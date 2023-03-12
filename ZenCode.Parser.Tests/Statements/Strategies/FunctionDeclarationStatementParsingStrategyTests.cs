using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class FunctionDeclarationStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly FunctionDeclarationStatementParsingStrategy _sut;
    

    public FunctionDeclarationStatementParsingStrategyTests()
    {
        _sut = new FunctionDeclarationStatementParsingStrategy();
    }
    
    [Fact]
    public void Parse_NoParameters_ReturnsFunctionDeclarationStatement()
    {
        // Arrange
        var type = _fixture.Create<Type>();
        var parameters = new ParameterList();
        var scope = _fixture.Create<Scope>();
        var expected = new FunctionDeclarationStatement(type, parameters, scope);

        _tokenStreamMock
            .Setup(x => x.Match(TokenType.RightParenthesis))
            .Returns(true);

        _parserMock
            .Setup(x => x.ParseType(_tokenStreamMock.Object, 0))
            .Returns(type);

        _parserMock
            .Setup(x => x.ParseScope(_tokenStreamMock.Object))
            .Returns(scope);
        
        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);
        
        // Assert
        Assert.Equal(expected, actual);
        
        _tokenStreamMock.Verify(x => x.Consume(TokenType.Function));
        _tokenStreamMock.Verify(x => x.Consume(TokenType.Identifier));
        _tokenStreamMock.Verify(x => x.Consume(TokenType.LeftParenthesis));
        _tokenStreamMock.Verify(x => x.Consume(TokenType.RightArrow));
    }
    
    [Fact]
    public void Parse_HasParameters_ReturnsFunctionDeclarationStatement()
    {
        // Arrange
        var expected = _fixture.Create<FunctionDeclarationStatement>();
        
        _tokenStreamMock
            .Setup(x => x.Match(TokenType.RightParenthesis))
            .Returns(false);

        _parserMock
            .Setup(x => x.ParseType(_tokenStreamMock.Object, 0))
            .Returns(expected.ReturnType);

        _parserMock
            .Setup(x => x.ParseParameterList(_tokenStreamMock.Object))
            .Returns(expected.Parameters);

        _parserMock
            .Setup(x => x.ParseScope(_tokenStreamMock.Object))
            .Returns(expected.Scope);
        
        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);
        
        // Assert
        Assert.Equal(expected, actual);
        
        _tokenStreamMock.Verify(x => x.Consume(TokenType.Function));
        _tokenStreamMock.Verify(x => x.Consume(TokenType.Identifier));
        _tokenStreamMock.Verify(x => x.Consume(TokenType.LeftParenthesis));
        _tokenStreamMock.Verify(x => x.Consume(TokenType.RightArrow));
    }
}
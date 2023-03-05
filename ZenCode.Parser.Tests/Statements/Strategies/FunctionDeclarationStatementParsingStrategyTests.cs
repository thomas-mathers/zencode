using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class FunctionDeclarationStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly Mock<ITypeParser> _typeParserMock = new();
    private readonly Mock<IStatementParser> _statementParserMock = new();
    private readonly FunctionDeclarationStatementParsingStrategy _sut;
    

    public FunctionDeclarationStatementParsingStrategyTests()
    {
        _sut = new FunctionDeclarationStatementParsingStrategy(_typeParserMock.Object, _statementParserMock.Object);
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

        _typeParserMock
            .Setup(x => x.ParseType(_tokenStreamMock.Object, 0))
            .Returns(type);

        _statementParserMock
            .Setup(x => x.ParseScope(_tokenStreamMock.Object))
            .Returns(scope);
        
        // Act
        var actual = _sut.Parse(_tokenStreamMock.Object);
        
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

        _typeParserMock
            .Setup(x => x.ParseType(_tokenStreamMock.Object, 0))
            .Returns(expected.ReturnType);

        _typeParserMock
            .Setup(x => x.ParseParameterList(_tokenStreamMock.Object))
            .Returns(expected.Parameters);

        _statementParserMock
            .Setup(x => x.ParseScope(_tokenStreamMock.Object))
            .Returns(expected.Scope);
        
        // Act
        var actual = _sut.Parse(_tokenStreamMock.Object);
        
        // Assert
        Assert.Equal(expected, actual);
        
        _tokenStreamMock.Verify(x => x.Consume(TokenType.Function));
        _tokenStreamMock.Verify(x => x.Consume(TokenType.Identifier));
        _tokenStreamMock.Verify(x => x.Consume(TokenType.LeftParenthesis));
        _tokenStreamMock.Verify(x => x.Consume(TokenType.RightArrow));
    }
}
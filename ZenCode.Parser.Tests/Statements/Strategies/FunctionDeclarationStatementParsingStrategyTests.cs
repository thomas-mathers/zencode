using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;
using ZenCode.Parser.Tests.Extensions;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class FunctionDeclarationStatementParsingStrategyTests
{
    private readonly Mock<ITypeParser> _typeParserMock = new();
    private readonly Mock<IStatementParser> _statementParserMock = new();
    private readonly FunctionDeclarationStatementParsingStrategy _sut;
    private readonly Fixture _fixture = new();

    public FunctionDeclarationStatementParsingStrategyTests()
    {
        _sut = new FunctionDeclarationStatementParsingStrategy(_typeParserMock.Object, _statementParserMock.Object);
    }
    
    [Fact]
    public void Parse_NoParameters_ReturnsFunctionDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Function),
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.RightParenthesis),
            new Token(TokenType.RightArrow),
            new Token(TokenType.Unknown),
            new Token(TokenType.Unknown)
        });

        var type = _fixture.Create<Type>();
        var parameters = new ParameterList();
        var scope = _fixture.Create<Scope>();
        var expected = new FunctionDeclarationStatement(type, parameters, scope);

        _typeParserMock
            .Setup(x => x.ParseType(tokenStream, 0))
            .Returns(type)
            .ConsumesToken(tokenStream);

        _statementParserMock
            .Setup(x => x.ParseScope(tokenStream))
            .Returns(scope)
            .ConsumesToken(tokenStream);
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_HasParameters_ReturnsFunctionDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Function),
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.Unknown),
            new Token(TokenType.RightParenthesis),
            new Token(TokenType.RightArrow),
            new Token(TokenType.Unknown),
            new Token(TokenType.Unknown)
        });

        var expected = _fixture.Create<FunctionDeclarationStatement>();

        _typeParserMock
            .Setup(x => x.ParseType(tokenStream, 0))
            .Returns(expected.ReturnType)
            .ConsumesToken(tokenStream);
        
        _typeParserMock
            .Setup(x => x.ParseParameterList(tokenStream))
            .Returns(expected.Parameters)
            .ConsumesToken(tokenStream);

        _statementParserMock
            .Setup(x => x.ParseScope(tokenStream))
            .Returns(expected.Scope)
            .ConsumesToken(tokenStream);
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}
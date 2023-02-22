using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Grammar.Statements;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Statements;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Statements;

public class ScopeParserTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IStatementParser> _statementParserMock = new();
    private readonly ScopeParser _sut;

    public ScopeParserTests()
    {
        _sut = new ScopeParser(_statementParserMock.Object);
    }

    [Fact]
    public void Parse_EmptyScope_ReturnsScope()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftBrace),
            new Token(TokenType.RightBrace)
        });

        var expected = new Scope();
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_OneStatement_ReturnsScope()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftBrace),
            new Token(TokenType.None),
            new Token(TokenType.RightBrace)
        });

        var expected = new Scope
        {
            Statements = _fixture.CreateMany<Statement>(1).ToArray()
        };

        _statementParserMock.ReturnsStatementSequence(expected.Statements);
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_MultipleStatements_ReturnsScope()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftBrace),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.None),
            new Token(TokenType.RightBrace)
        });

        var expected = new Scope
        {
            Statements = _fixture.CreateMany<Statement>(3).ToArray()
        };

        _statementParserMock.ReturnsStatementSequence(expected.Statements);
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}
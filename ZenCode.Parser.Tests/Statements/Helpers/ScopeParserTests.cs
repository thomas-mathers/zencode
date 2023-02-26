using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Helpers;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Statements.Helpers;

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

        var statement = _fixture.Create<Statement>();

        var expected = new Scope
        {
            Statements = new[] { statement }
        };

        _statementParserMock
            .Setup(x => x.Parse(tokenStream))
            .Returns(statement)
            .ConsumesToken(tokenStream);
        
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

        var statements = _fixture.CreateMany<Statement>(3).ToArray();

        var expected = new Scope
        {
            Statements = statements
        };

        _statementParserMock
            .Setup(x => x.Parse(tokenStream))
            .ReturnsSequence(statements)
            .ConsumesToken(tokenStream);
        
        // Act
        var actual = _sut.Parse(tokenStream);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}
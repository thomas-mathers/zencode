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

public class IfStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IConditionScopeParser> _conditionScopeParserMock = new();
    private readonly Mock<IScopeParser> _scopeParserMock = new();
    private readonly IfStatementParsingStrategy _sut;

    public IfStatementParsingStrategyTests()
    {
        _sut = new IfStatementParsingStrategy(_conditionScopeParserMock.Object, _scopeParserMock.Object);
    }

    [Fact]
    public void Parse_HasThen_ReturnsIfStatementWithOnlyThen()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.None)
        });

        var thenConditionScope = _fixture.Create<ConditionScope>();

        var expected = new IfStatement(thenConditionScope);

        _conditionScopeParserMock
            .Setup(x => x.Parse(tokenStream))
            .Returns(thenConditionScope)
            .ConsumesToken(tokenStream);

        // Arrange
        var actual = _sut.Parse(tokenStream);

        // Act
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_HasThenAndOneElseIf_ReturnsIfStatementWithThenAndOneElseIf()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None)
        });

        var thenConditionScope = _fixture.Create<ConditionScope>();
        var elseIfConditionScopes = _fixture.CreateMany<ConditionScope>(1).ToArray();

        var conditionScopeSequence = new[] { thenConditionScope }.Concat(elseIfConditionScopes).ToArray();

        var expected = new IfStatement(thenConditionScope) { ElseIfScopes = elseIfConditionScopes };

        _conditionScopeParserMock
            .Setup(x => x.Parse(tokenStream))
            .ReturnsSequence(conditionScopeSequence)
            .ConsumesToken(tokenStream);

        // Arrange
        var actual = _sut.Parse(tokenStream);

        // Act
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_HasThenAndMultipleElseIfs_ReturnsIfStatementWithThenAndMultipleElseIfs()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None)
        });

        var thenConditionScope = _fixture.Create<ConditionScope>();
        var elseIfConditionScopes = _fixture.CreateMany<ConditionScope>(3).ToArray();

        var conditionScopeSequence = new[] { thenConditionScope }.Concat(elseIfConditionScopes).ToArray();

        var expected = new IfStatement(thenConditionScope) { ElseIfScopes = elseIfConditionScopes };

        _conditionScopeParserMock
            .Setup(x => x.Parse(tokenStream))
            .ReturnsSequence(conditionScopeSequence)
            .ConsumesToken(tokenStream);

        // Arrange
        var actual = _sut.Parse(tokenStream);

        // Act
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_HasThenAndElse_ReturnsIfStatementWithThenAndElse()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.None),
            new Token(TokenType.Else),
            new Token(TokenType.None)
        });

        var thenConditionScope = _fixture.Create<ConditionScope>();
        var elseIfConditionScopes = _fixture.CreateMany<ConditionScope>(0).ToArray();
        var elseScope = _fixture.Create<Scope>();

        var conditionScopeSequence = new[] { thenConditionScope }.Concat(elseIfConditionScopes).ToArray();

        var expected = new IfStatement(thenConditionScope)
            { ElseIfScopes = elseIfConditionScopes, ElseScope = elseScope };
        
        _conditionScopeParserMock
            .Setup(x => x.Parse(tokenStream))
            .ReturnsSequence(conditionScopeSequence)
            .ConsumesToken(tokenStream);
        
        _scopeParserMock
            .Setup(x => x.Parse(tokenStream))
            .Returns(elseScope)
            .ConsumesToken(tokenStream);        

        // Arrange
        var actual = _sut.Parse(tokenStream);

        // Act
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_HasThenAndOneElseIfAndElse_ReturnsIfStatementWithThenAndOneElseIfAndElse()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None),
            new Token(TokenType.Else),
            new Token(TokenType.None),
        });

        var thenConditionScope = _fixture.Create<ConditionScope>();
        var elseIfConditionScopes = _fixture.CreateMany<ConditionScope>(1).ToArray();
        var elseScope = _fixture.Create<Scope>();

        var conditionScopeSequence = new[] { thenConditionScope }.Concat(elseIfConditionScopes).ToArray();

        var expected = new IfStatement(thenConditionScope)
            { ElseIfScopes = elseIfConditionScopes, ElseScope = elseScope };
        
        _conditionScopeParserMock
            .Setup(x => x.Parse(tokenStream))
            .ReturnsSequence(conditionScopeSequence)
            .ConsumesToken(tokenStream);
        
        _scopeParserMock
            .Setup(x => x.Parse(tokenStream))
            .Returns(elseScope)
            .ConsumesToken(tokenStream);        

        // Arrange
        var actual = _sut.Parse(tokenStream);

        // Act
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_HasThenAndMultipleElseIfAndElse_ReturnsIfStatementWithThenAndMultipleElseIfAndElse()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None),
            new Token(TokenType.ElseIf),
            new Token(TokenType.None),
            new Token(TokenType.Else),
            new Token(TokenType.None),
        });

        var thenConditionScope = _fixture.Create<ConditionScope>();
        var elseIfConditionScopes = _fixture.CreateMany<ConditionScope>(3).ToArray();
        var elseScope = _fixture.Create<Scope>();

        var conditionScopeSequence = new[] { thenConditionScope }.Concat(elseIfConditionScopes).ToArray();

        var expected = new IfStatement(thenConditionScope)
            { ElseIfScopes = elseIfConditionScopes, ElseScope = elseScope };
        
        _conditionScopeParserMock
            .Setup(x => x.Parse(tokenStream))
            .ReturnsSequence(conditionScopeSequence)
            .ConsumesToken(tokenStream);
        
        _scopeParserMock
            .Setup(x => x.Parse(tokenStream))
            .Returns(elseScope)
            .ConsumesToken(tokenStream);        

        // Arrange
        var actual = _sut.Parse(tokenStream);

        // Act
        Assert.Equal(expected, actual);
    }
}
using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements;

namespace ZenCode.Parser.Tests.Statements;

public class StatementParserTests
{
    private readonly Mock<IStatementParsingStrategy> _ifStatementParsingStrategyMock = new();
    private readonly Mock<IStatementParsingStrategy> _whileStatementParsingStrategyMock = new();
    private readonly Mock<IStatementParsingStrategy> _functionDeclarationStatementParsingStrategyMock = new();
    private readonly Mock<IStatementParsingStrategy> _assignmentStatementParsingStrategyMock = new();
    private readonly Mock<IStatementParsingStrategy> _printStatementParsingStrategyMock = new();
    private readonly Mock<IStatementParsingStrategy> _variableDeclarationStatementParsingStrategyMock = new();
    private readonly Mock<IStatementParsingStrategy> _returnStatementParsingStrategyMock = new();
    
    private readonly StatementParser _sut = new();

    private readonly Fixture _fixture = new();

    public StatementParserTests()
    {
        _sut.SetStatementParsingStrategy(TokenType.If, _ifStatementParsingStrategyMock.Object);
        _sut.SetStatementParsingStrategy(TokenType.While, _whileStatementParsingStrategyMock.Object);
        _sut.SetStatementParsingStrategy(TokenType.Function, _functionDeclarationStatementParsingStrategyMock.Object);
        _sut.SetStatementParsingStrategy(TokenType.Identifier, _assignmentStatementParsingStrategyMock.Object);
        _sut.SetStatementParsingStrategy(TokenType.Print, _printStatementParsingStrategyMock.Object);
        _sut.SetStatementParsingStrategy(TokenType.Var, _variableDeclarationStatementParsingStrategyMock.Object);
        _sut.SetStatementParsingStrategy(TokenType.Return, _returnStatementParsingStrategyMock.Object);
    }
    
    [Fact]
    public void Parse_IfStatement_CallsIfStatementParsingStrategy()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.If)
        });

        var expectedStatement = _fixture.Create<IfStatement>();

        _ifStatementParsingStrategyMock.Setup(x => x.Parse(tokenStream)).Returns(expectedStatement);
        
        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);
        
        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
    
    [Fact]
    public void Parse_WhileStatement_CallsWhileStatementParsingStrategy()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.While)
        });

        var expectedStatement = _fixture.Create<WhileStatement>();

        _whileStatementParsingStrategyMock.Setup(x => x.Parse(tokenStream)).Returns(expectedStatement);
        
        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);
        
        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
    
    [Fact]
    public void Parse_FunctionStatement_CallsFunctionDeclarationStatementParsingStrategy()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Function)
        });

        var expectedStatement = _fixture.Create<FunctionDeclarationStatement>();

        _functionDeclarationStatementParsingStrategyMock.Setup(x => x.Parse(tokenStream)).Returns(expectedStatement);
        
        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);
        
        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
    
    [Fact]
    public void Parse_AssignmentStatement_CallsAssignmentStatementParsingStrategy()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Identifier)
        });

        var expectedStatement = _fixture.Create<AssignmentStatement>();

        _assignmentStatementParsingStrategyMock.Setup(x => x.Parse(tokenStream)).Returns(expectedStatement);
        
        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);
        
        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
    
    [Fact]
    public void Parse_PrintStatement_CallsPrintStatementParsingStrategy()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Print)
        });

        var expectedStatement = _fixture.Create<PrintStatement>();

        _printStatementParsingStrategyMock.Setup(x => x.Parse(tokenStream)).Returns(expectedStatement);
        
        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);
        
        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
    
    [Fact]
    public void Parse_VariableDeclarationStatement_CallsVariableDeclarationStatementParsingStrategy()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Var)
        });

        var expectedStatement = _fixture.Create<VariableDeclarationStatement>();

        _variableDeclarationStatementParsingStrategyMock.Setup(x => x.Parse(tokenStream)).Returns(expectedStatement);
        
        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);
        
        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
    
    [Fact]
    public void Parse_ReturnStatement_CallsReturnStatementParsingStrategy()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Return)
        });

        var expectedStatement = _fixture.Create<ReturnStatement>();

        _returnStatementParsingStrategyMock.Setup(x => x.Parse(tokenStream)).Returns(expectedStatement);
        
        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);
        
        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
}
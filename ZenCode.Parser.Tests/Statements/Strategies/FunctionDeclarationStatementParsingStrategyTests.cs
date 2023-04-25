using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;
using ZenCode.Tests.Common.Mocks;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class FunctionDeclarationStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly FunctionDeclarationStatementParsingStrategy _sut;
    private readonly Mock<ITokenStream> _tokenStreamMock = new();

    public FunctionDeclarationStatementParsingStrategyTests()
    {
        _sut = new FunctionDeclarationStatementParsingStrategy();

        _fixture.Customizations.Add(new TypeRelay(typeof(Type), typeof(TypeMock)));
        _fixture.Customizations.Add(new TypeRelay(typeof(Statement), typeof(StatementMock)));
    }

    [Fact]
    public void Parse_NoParameters_ReturnsFunctionDeclarationStatement()
    {
        // Arrange
        var returnType = _fixture.Create<Type>();
        var name = new Token(TokenType.Identifier);
        var parameters = new ParameterList();
        var scope = _fixture.Create<Scope>();
        var expected = new FunctionDeclarationStatement
        {
            ReturnType = returnType,
            Name = name,
            Parameters = parameters,
            Body = scope
        };

        _tokenStreamMock
            .Setup(x => x.Consume(TokenType.Identifier))
            .Returns(name);

        _tokenStreamMock
            .Setup(x => x.Match(TokenType.RightParenthesis))
            .Returns(true);

        _parserMock
            .Setup(x => x.ParseType(_tokenStreamMock.Object))
            .Returns(returnType);

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
            .Setup(x => x.Consume(TokenType.Identifier))
            .Returns(expected.Name);

        _tokenStreamMock
            .Setup(x => x.Match(TokenType.RightParenthesis))
            .Returns(false);

        _parserMock
            .Setup(x => x.ParseType(_tokenStreamMock.Object))
            .Returns(expected.ReturnType);

        _parserMock
            .Setup(x => x.ParseParameterList(_tokenStreamMock.Object))
            .Returns(expected.Parameters);

        _parserMock
            .Setup(x => x.ParseScope(_tokenStreamMock.Object))
            .Returns(expected.Body);

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
    public void Parse_UnexpectedToken_ThrowsUnexpectedTokenException()
    {
        // Arrange
        _tokenStreamMock
            .Setup(x => x.Consume(It.IsAny<TokenType>()))
            .Throws<UnexpectedTokenException>();

        // Act
        var actual = Assert.Throws<UnexpectedTokenException>
        (
            () => _sut.Parse(_parserMock.Object, _tokenStreamMock.Object)
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_NullParser_ThrowsArgumentNullException()
    {
        // Arrange + Act
        var actual = Assert.Throws<ArgumentNullException>
        (
            () => _sut.Parse(null!, _tokenStreamMock.Object)
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_NullTokenStream_ThrowsArgumentNullException()
    {
        // Arrange + Act
        var actual = Assert.Throws<ArgumentNullException>
        (
            () => _sut.Parse(_parserMock.Object, null!)
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_ParseParameterListThrowsException_ThrowsException()
    {
        // Arrange
        _parserMock
            .Setup(x => x.ParseParameterList(_tokenStreamMock.Object))
            .Throws<Exception>();

        // Act
        var actual = Assert.Throws<Exception>
        (
            () => _sut.Parse(_parserMock.Object, _tokenStreamMock.Object)
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_ParseTypeThrowsException_ThrowsException()
    {
        // Arrange
        _parserMock
            .Setup(x => x.ParseType(_tokenStreamMock.Object))
            .Throws<Exception>();

        // Act
        var actual = Assert.Throws<Exception>
        (
            () => _sut.Parse(_parserMock.Object, _tokenStreamMock.Object)
        );

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_ParseScopeThrowsException_ThrowsException()
    {
        // Arrange
        _parserMock
            .Setup(x => x.ParseScope(_tokenStreamMock.Object))
            .Throws<Exception>();

        // Act
        var actual = Assert.Throws<Exception>
        (
            () => _sut.Parse(_parserMock.Object, _tokenStreamMock.Object)
        );

        // Assert
        Assert.NotNull(actual);
    }
}

using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Tests.Mocks;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class AnonymousFunctionDeclarationParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly AnonymousFunctionDeclarationParsingStrategy _sut;
    private readonly Mock<ITokenStream> _tokenStreamMock = new();

    public AnonymousFunctionDeclarationParsingStrategyTests()
    {
        _sut = new AnonymousFunctionDeclarationParsingStrategy();

        _fixture.Customizations.Add(new TypeRelay(typeof(Type), typeof(TypeMock)));
        _fixture.Customizations.Add(new TypeRelay(typeof(Statement), typeof(StatementMock)));
    }

    [Fact]
    public void Parse_ValidAnonymousFunctionDeclaration_ReturnsAnonymousFunctionDeclarationExpression()
    {
        // Arrange
        var expected = _fixture.Create<AnonymousFunctionDeclarationExpression>();

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
    }
    
    [Fact]
    public void Parse_UnexpectedToken_ThrowsUnexpectedTokenException()
    {
        // Arrange
        _tokenStreamMock
            .Setup(x => x.Consume(It.IsAny<TokenType>()))
            .Throws<UnexpectedTokenException>();

        // Act
        var actual = Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(_parserMock.Object, _tokenStreamMock.Object));

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
    public void Parse_NullParserAndTokenStream_ThrowsArgumentNullException()
    {
        // Arrange + Act
        var actual = Assert.Throws<ArgumentNullException>
        (
            () => _sut.Parse(null!, null!)
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
        var actual = Assert.Throws<Exception>(() => _sut.Parse(_parserMock.Object, _tokenStreamMock.Object));

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_ParseParameterListThrowsException_ThrowsException()
    {
        // Arrange
        _parserMock
            .Setup(x => x.ParseType(_tokenStreamMock.Object))
            .Returns(_fixture.Create<Type>());

        _parserMock
            .Setup(x => x.ParseParameterList(_tokenStreamMock.Object))
            .Throws<Exception>();

        // Act
        var actual = Assert.Throws<Exception>(() => _sut.Parse(_parserMock.Object, _tokenStreamMock.Object));

        // Assert
        Assert.NotNull(actual);
    }
    
    [Fact]
    public void Parse_ParseScopeThrowsException_ThrowsException()
    {
        // Arrange
        _parserMock
            .Setup(x => x.ParseType(_tokenStreamMock.Object))
            .Returns(_fixture.Create<Type>());

        _parserMock
            .Setup(x => x.ParseParameterList(_tokenStreamMock.Object))
            .Returns(_fixture.Create<ParameterList>());

        _parserMock
            .Setup(x => x.ParseScope(_tokenStreamMock.Object))
            .Throws<Exception>();

        // Act
        var actual = Assert.Throws<Exception>(() => _sut.Parse(_parserMock.Object, _tokenStreamMock.Object));

        // Assert
        Assert.NotNull(actual);
    }
}

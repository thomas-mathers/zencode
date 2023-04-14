using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Statements.Strategies;

public class AssignmentStatementParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly AssignmentStatementParsingStrategy _sut;
    private readonly Mock<ITokenStream> _tokenStreamMock = new();

    public AssignmentStatementParsingStrategyTests()
    {
        _sut = new AssignmentStatementParsingStrategy();

        _fixture.Customizations.Add(new TypeRelay(typeof(Expression), typeof(ExpressionMock)));
    }

    [Fact]
    public void Parse_AssignmentToExpression_ReturnsAssignmentStatement()
    {
        // Arrange
        var expected = _fixture.Create<AssignmentStatement>();

        _parserMock
            .Setup(x => x.ParseVariableReferenceExpression(_tokenStreamMock.Object))
            .Returns(expected.VariableReferenceExpression);

        _parserMock
            .Setup(x => x.ParseExpression(_tokenStreamMock.Object, 0))
            .Returns(expected.Expression);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);

        _tokenStreamMock.Verify(x => x.Consume(TokenType.Assignment));
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
    public void Parse_ParseVariableReferenceExpressionThrowsException_ThrowsException()
    {
        // Arrange
        _parserMock
            .Setup(x => x.ParseVariableReferenceExpression(_tokenStreamMock.Object))
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
    public void Parse_ParseExpressionThrowsException_ThrowsException()
    {
        // Arrange
        _parserMock
            .Setup(x => x.ParseVariableReferenceExpression(_tokenStreamMock.Object))
            .Returns(_fixture.Create<VariableReferenceExpression>());

        _parserMock
            .Setup(x => x.ParseExpression(_tokenStreamMock.Object, 0))
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

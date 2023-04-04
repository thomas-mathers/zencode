using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Expressions.Strategies;
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

        _fixture.Customizations.Add(
            new TypeRelay(
                typeof(Type),
                typeof(TypeMock)));

        _fixture.Customizations.Add(
            new TypeRelay(
                typeof(Statement),
                typeof(StatementMock)));
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
            .Returns(expected.Scope);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);

        // Assert
        Assert.Equal(expected, actual);
    }
}
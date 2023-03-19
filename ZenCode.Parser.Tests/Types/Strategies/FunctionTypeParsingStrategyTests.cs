using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Types;
using ZenCode.Parser.Tests.Mocks;
using ZenCode.Parser.Types.Strategies;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Tests.Types.Strategies;

public class FunctionTypeParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly FunctionTypeParsingStrategy _sut = new();

    public FunctionTypeParsingStrategyTests()
    {
        _fixture.Customizations.Add(
            new TypeRelay(
                typeof(Expression),
                typeof(ExpressionMock)));
        
        _fixture.Customizations.Add(
            new TypeRelay(
                typeof(Type),
                typeof(TypeMock)));
    }

    [Fact]
    public void Parse_NoParameters_ReturnsFunctionType()
    {
        // Arrange
        var returnType = _fixture.Create<Type>();
        var expected = new FunctionType(returnType, new TypeList());
        
        _tokenStreamMock
            .Setup(x => x.Match(TokenType.RightParenthesis))
            .Returns(true);

        _parserMock
            .Setup(x => x.ParseType(_tokenStreamMock.Object))
            .Returns(returnType);
        
        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);
        
        // Assert
        Assert.Equal(expected, actual);
    }
    
    [Fact]
    public void Parse_HasParameters_ReturnsFunctionType()
    {
        // Arrange
        var expected = _fixture.Create<FunctionType>();
        
        _tokenStreamMock
            .Setup(x => x.Match(TokenType.RightParenthesis))
            .Returns(false);
        
        _parserMock
            .Setup(x => x.ParseTypeList(_tokenStreamMock.Object))
            .Returns(expected.ParameterTypes);

        _parserMock
            .Setup(x => x.ParseType(_tokenStreamMock.Object))
            .Returns(expected.ReturnType);
        
        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object);
        
        // Assert
        Assert.Equal(expected, actual);
    }
}
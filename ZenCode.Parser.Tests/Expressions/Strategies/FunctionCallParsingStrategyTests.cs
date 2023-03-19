using AutoFixture;
using AutoFixture.Kernel;
using Moq;
using Xunit;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class FunctionCallParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<ITokenStream> _tokenStreamMock = new();
    private readonly Mock<IParser> _parserMock = new();
    private readonly FunctionCallParsingStrategy _sut;
    private readonly VariableReferenceExpression _variableReferenceExpression;

    public FunctionCallParsingStrategyTests()
    {
        _sut = new FunctionCallParsingStrategy();
        _variableReferenceExpression =
            new VariableReferenceExpression(new Token(TokenType.Identifier));
        
        _fixture.Customizations.Add(
            new TypeRelay(
                typeof(Expression),
                typeof(ExpressionMock)));
    }

    [Fact]
    public void Parse_FunctionCallNoParameters_ReturnsFunctionCallExpression()
    {
        // Arrange
        var expected = new FunctionCallExpression(_variableReferenceExpression);
        
        _tokenStreamMock
            .Setup(x => x.Match(TokenType.RightParenthesis))
            .Returns(true);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object, _variableReferenceExpression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_FunctionCallOneOrMoreParameters_ReturnsFunctionCallExpression()
    {
        // Arrange
        var arguments = _fixture.Create<ExpressionList>();

        var expected = new FunctionCallExpression(_variableReferenceExpression)
        {
            Arguments = arguments
        };
        
        _tokenStreamMock
            .Setup(x => x.Match(TokenType.RightParenthesis))
            .Returns(false);

        _parserMock
            .Setup(x => x.ParseExpressionList(_tokenStreamMock.Object))
            .Returns(arguments);

        // Act
        var actual = _sut.Parse(_parserMock.Object, _tokenStreamMock.Object, _variableReferenceExpression);

        // Assert
        Assert.Equal(expected, actual);
    }
}
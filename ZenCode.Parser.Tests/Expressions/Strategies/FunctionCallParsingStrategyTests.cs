using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Expressions.Strategies;

public class FunctionCallParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IExpressionParser> _parserMock = new();
    private readonly FunctionCallParsingStrategy _sut;
    private readonly VariableReferenceExpression _variableReferenceExpression;

    public FunctionCallParsingStrategyTests()
    {
        _sut = new FunctionCallParsingStrategy(_parserMock.Object, 7);
        _variableReferenceExpression =
            new VariableReferenceExpression(new Token(TokenType.Identifier));
    }

    [Fact]
    public void Parse_FunctionCallMissingRightParenthesis_ThrowsUnexpectedTokenException()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.Any)
        });

        var arguments = _fixture.Create<ExpressionList>();

        _parserMock
            .Setup(x => x.ParseExpressionList(tokenStream))
            .Returns(arguments)
            .ConsumesToken(tokenStream);

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream, _variableReferenceExpression));
    }

    [Fact]
    public void Parse_FunctionCallNoParameters_ReturnsFunctionCallExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.RightParenthesis)
        });

        var expected = new FunctionCallExpression(_variableReferenceExpression);

        // Act
        var actual = _sut.Parse(tokenStream, _variableReferenceExpression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_FunctionCallOneOrMoreParameters_ReturnsFunctionCallExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.Any),
            new Token(TokenType.RightParenthesis)
        });

        var arguments = _fixture.Create<ExpressionList>();

        var expected = new FunctionCallExpression(_variableReferenceExpression)
        {
            Arguments = arguments
        };

        _parserMock
            .Setup(x => x.ParseExpressionList(tokenStream))
            .Returns(arguments)
            .ConsumesToken(tokenStream);

        // Act
        var actual = _sut.Parse(tokenStream, _variableReferenceExpression);

        // Assert
        Assert.Equal(expected, actual);
    }
}
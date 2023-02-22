using AutoFixture;
using Moq;
using Xunit;
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Expressions;
using ZenCode.Parser.Tests.Extensions;

namespace ZenCode.Parser.Tests.Expressions;

public class FunctionCallParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IExpressionListParser> _expressionListParserMock = new();
    private readonly FunctionCallParsingStrategy _sut;
    private readonly VariableReferenceExpression _variableReferenceExpression;

    public FunctionCallParsingStrategyTests()
    {
        _sut = new FunctionCallParsingStrategy(_expressionListParserMock.Object, 7);
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
            new Token(TokenType.None),
        });

        _expressionListParserMock.ReturnsExpressionSequence(_fixture.CreateMany<Expression>().ToArray());

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

        var expected = new FunctionCall(_variableReferenceExpression);

        // Act
        var actual = _sut.Parse(tokenStream, _variableReferenceExpression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_FunctionCallOneParameter_ReturnsFunctionCallExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.None),
            new Token(TokenType.RightParenthesis)
        });

        var parameters = _fixture.CreateMany<Expression>(1).ToArray();
        
        var expected = new FunctionCall(_variableReferenceExpression)
        {
            Parameters = parameters
        };
        
        _expressionListParserMock.ReturnsExpressionSequence(parameters);

        // Act
        var actual = _sut.Parse(tokenStream, _variableReferenceExpression);

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void Parse_FunctionCallManyParameters_ReturnsFunctionCallExpression()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.None),
            new Token(TokenType.RightParenthesis)
        });

        var parameters = _fixture.CreateMany<Expression>(3).ToList();
        
        var expected = new FunctionCall(_variableReferenceExpression)
        {
            Parameters = parameters
        };

        _expressionListParserMock.ReturnsExpressionSequence(parameters);

        // Act
        var actual = _sut.Parse(tokenStream, _variableReferenceExpression);

        // Assert
        Assert.Equal(expected, actual);
    }
}
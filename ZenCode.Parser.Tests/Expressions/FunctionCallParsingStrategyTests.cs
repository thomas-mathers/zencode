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
using ZenCode.Parser.Tests.TestData;

namespace ZenCode.Parser.Tests.Expressions;

public class FunctionCallParsingStrategyTests
{
    private readonly Fixture _fixture = new();
    private readonly Mock<IExpressionParser> _expressionParserMock = new();
    private readonly FunctionCallParsingStrategy _sut;
    private readonly VariableReferenceExpression _variableReferenceExpression;

    public FunctionCallParsingStrategyTests()
    {
        _sut = new FunctionCallParsingStrategy(_expressionParserMock.Object, 7);
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

        _expressionParserMock.ReturnsExpression(_fixture.Create<Expression>());

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream, _variableReferenceExpression));
    }

    [Fact]
    public void Parse_FunctionCallMissingCommas_ThrowsUnexpectedTokenException()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.None),    
            new Token(TokenType.None),    
            new Token(TokenType.RightParenthesis)
        });
        
        _expressionParserMock.ReturnsExpression(_fixture.Create<Expression>());

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream, _variableReferenceExpression));
    }
    
    [Theory]
    [ClassData(typeof(Constants))]
    public void Parse_FunctionCallNoVariableReferenceExpression_ThrowsUnexpectedTokenException(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(tokenType),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.None),       
            new Token(TokenType.RightParenthesis)
        });
        
        _expressionParserMock.ReturnsExpression(_fixture.Create<Expression>());

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream, _variableReferenceExpression));
    }
    
    [Fact]
    public void Parse_FunctionCallMissingIndexExpression_ThrowsUnexpectedTokenException()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.None),
            new Token(TokenType.Comma),
            new Token(TokenType.Comma),
            new Token(TokenType.None),            
            new Token(TokenType.RightParenthesis)
        });
        
        _expressionParserMock.ReturnsExpression(_fixture.Create<Expression>());

        // Act + Assert
        Assert.Throws<UnexpectedTokenException>(() => _sut.Parse(tokenStream, _variableReferenceExpression));
    }

    [Fact]
    public void Parse_FunctionCallDanglingComma_ThrowsUnexpectedTokenException()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.None),
            new Token(TokenType.Comma),
            new Token(TokenType.RightParenthesis)
        });
        
        _expressionParserMock.ReturnsExpression(_fixture.Create<Expression>());

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
    public void Parse_FunctionCallOneConstantParameter_ReturnsFunctionCallExpression()
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
        
        _expressionParserMock.ReturnsExpression(parameters[0]);

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
            new Token(TokenType.Comma),
            new Token(TokenType.None),
            new Token(TokenType.Comma),
            new Token(TokenType.None),
            new Token(TokenType.RightParenthesis)
        });

        var parameters = _fixture.CreateMany<Expression>(3).ToList();
        
        var expected = new FunctionCall(_variableReferenceExpression)
        {
            Parameters = parameters
        };

        _expressionParserMock.ReturnsExpressionSequence(parameters);

        // Act
        var actual = _sut.Parse(tokenStream, _variableReferenceExpression);

        // Assert
        Assert.Equal(expected, actual);
    }
}
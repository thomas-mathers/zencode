using Xunit;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Tests.Integration.Statements;

public class VariableDeclarationStatementParsingTests
{
    private readonly IParser _sut;

    public VariableDeclarationStatementParsingTests()
    {
        _sut = new ParserFactory().Create();
    }

    [Fact]
    public void Parse_DeclareVariableAndAssignBinaryExpression_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Var), 
            new Token(TokenType.Identifier), 
            new Token(TokenType.Assignment),
            new Token(TokenType.IntegerLiteral), 
            new Token(TokenType.Plus), 
            new Token(TokenType.IntegerLiteral)
        });

        var identifier = new Token(TokenType.Identifier);

        var expression = new BinaryExpression(new LiteralExpression(new Token(TokenType.IntegerLiteral)),
            new Token(TokenType.Plus), new LiteralExpression(new Token(TokenType.IntegerLiteral)));

        var expectedStatement = new VariableDeclarationStatement(identifier, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Theory]
    [InlineData(TokenType.BooleanLiteral)]
    [InlineData(TokenType.IntegerLiteral)]
    [InlineData(TokenType.FloatLiteral)]
    [InlineData(TokenType.StringLiteral)]
    public void Parse_DeclareVariableAndAssignLiteral_ReturnsVariableDeclarationStatement(TokenType tokenType)
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Var), 
            new Token(TokenType.Identifier), 
            new Token(TokenType.Assignment),
            new Token(tokenType)
        });

        var identifier = new Token(TokenType.Identifier);

        var expression = new LiteralExpression(new Token(tokenType));

        var expectedStatement = new VariableDeclarationStatement(identifier, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_DeclareVariableAndAssignFunctionCallExpression_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Var),
            new Token(TokenType.Identifier), 
            new Token(TokenType.Assignment),
            new Token(TokenType.Identifier),
            new Token(TokenType.LeftParenthesis),
            new Token(TokenType.RightParenthesis)
        });

        var identifier = new Token(TokenType.Identifier);

        var expression = new FunctionCallExpression(new VariableReferenceExpression(new Token(TokenType.Identifier)));

        var expectedStatement = new VariableDeclarationStatement(identifier, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_DeclareVariableAndAssignParenthesisExpression_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Var), 
            new Token(TokenType.Identifier), 
            new Token(TokenType.Assignment),
            new Token(TokenType.LeftParenthesis), 
            new Token(TokenType.StringLiteral),
            new Token(TokenType.RightParenthesis)
        });

        var identifier = new Token(TokenType.Identifier);

        var expression = new LiteralExpression(new Token(TokenType.StringLiteral));

        var expectedStatement = new VariableDeclarationStatement(identifier, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_DeclareVariableAndAssignUnaryExpression_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Var), 
            new Token(TokenType.Identifier), 
            new Token(TokenType.Assignment),
            new Token(TokenType.Minus), 
            new Token(TokenType.FloatLiteral)
        });

        var identifier = new Token(TokenType.Identifier);

        var expression = new UnaryExpression(new Token(TokenType.Minus),
            new LiteralExpression(new Token(TokenType.FloatLiteral)));

        var expectedStatement = new VariableDeclarationStatement(identifier, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }

    [Fact]
    public void Parse_DeclareVariableAndAssignVariableReferenceExpression_ReturnsVariableDeclarationStatement()
    {
        // Arrange
        var tokenStream = new TokenStream(new[]
        {
            new Token(TokenType.Var),
            new Token(TokenType.Identifier), 
            new Token(TokenType.Assignment),
            new Token(TokenType.Identifier)
        });

        var identifier = new Token(TokenType.Identifier);

        var expression = new VariableReferenceExpression(new Token(TokenType.Identifier));

        var expectedStatement = new VariableDeclarationStatement(identifier, expression);

        // Act
        var actualStatement = _sut.ParseStatement(tokenStream);

        // Assert
        Assert.Equal(expectedStatement, actualStatement);
    }
}
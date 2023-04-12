using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Expressions;

public class AnonymousFunctionDeclarationExpressionTests
{
    [Fact]
    public void ToString_NoParametersNoStatements_ReturnsCorrectString()
    {
        // Arrange
        var returnType = new TypeMock();
        var parameterList = new ParameterList();
        var scope = new Scope();

        var anonymousFunctionDeclarationExpression =
            new AnonymousFunctionDeclarationExpression(returnType, parameterList, scope);

        const string expected = """
        function () => {Type}
        {
        }
        """;

        // Act
        var actual = anonymousFunctionDeclarationExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_NoParametersOneStatement_ReturnsCorrectString()
    {
        // Arrange
        var returnType = new TypeMock();
        var parameterList = new ParameterList();

        var scope = new Scope
        {
            Statements = new[]
            {
                new StatementMock()
            }
        };

        var anonymousFunctionDeclarationExpression =
            new AnonymousFunctionDeclarationExpression(returnType, parameterList, scope);

        const string expected = """
        function () => {Type}
        {
            {Statement}
        }
        """;

        // Act
        var actual = anonymousFunctionDeclarationExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_NoParametersThreeStatement_ReturnsCorrectString()
    {
        // Arrange
        var returnType = new TypeMock();
        var parameterList = new ParameterList();

        var scope = new Scope
        {
            Statements = new[]
            {
                new StatementMock(),
                new StatementMock(),
                new StatementMock()
            }
        };

        var anonymousFunctionDeclarationExpression =
            new AnonymousFunctionDeclarationExpression(returnType, parameterList, scope);

        const string expected = """
        function () => {Type}
        {
            {Statement}
            {Statement}
            {Statement}
        }
        """;

        // Act
        var actual = anonymousFunctionDeclarationExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_OneParameterNoStatements_ReturnsCorrectString()
    {
        // Arrange
        var returnType = new TypeMock();

        var parameterList = new ParameterList
        {
            Parameters = new[]
            {
                new Parameter(new Token(TokenType.Identifier) { Text = "x" }, new TypeMock())
            }
        };

        var scope = new Scope();

        var anonymousFunctionDeclarationExpression =
            new AnonymousFunctionDeclarationExpression(returnType, parameterList, scope);

        const string expected = """
        function (x : {Type}) => {Type}
        {
        }
        """;

        // Act
        var actual = anonymousFunctionDeclarationExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_OneParameterOneStatement_ReturnsCorrectString()
    {
        // Arrange
        var returnType = new TypeMock();

        var parameterList = new ParameterList
        {
            Parameters = new[]
            {
                new Parameter(new Token(TokenType.Identifier) { Text = "x" }, new TypeMock())
            }
        };

        var scope = new Scope
        {
            Statements = new[]
            {
                new StatementMock()
            }
        };

        var anonymousFunctionDeclarationExpression =
            new AnonymousFunctionDeclarationExpression(returnType, parameterList, scope);

        const string expected = """
        function (x : {Type}) => {Type}
        {
            {Statement}
        }
        """;

        // Act
        var actual = anonymousFunctionDeclarationExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_OneParameterThreeStatement_ReturnsCorrectString()
    {
        // Arrange
        var returnType = new TypeMock();

        var parameterList = new ParameterList
        {
            Parameters = new[]
            {
                new Parameter(new Token(TokenType.Identifier) { Text = "x" }, new TypeMock())
            }
        };

        var scope = new Scope
        {
            Statements = new[]
            {
                new StatementMock(),
                new StatementMock(),
                new StatementMock()
            }
        };

        var anonymousFunctionDeclarationExpression =
            new AnonymousFunctionDeclarationExpression(returnType, parameterList, scope);

        const string expected = """
        function (x : {Type}) => {Type}
        {
            {Statement}
            {Statement}
            {Statement}
        }
        """;

        // Act
        var actual = anonymousFunctionDeclarationExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_MultipleParameterNoStatements_ReturnsCorrectString()
    {
        // Arrange
        var returnType = new TypeMock();

        var parameterList = new ParameterList
        {
            Parameters = new[]
            {
                new Parameter(new Token(TokenType.Identifier) { Text = "x" }, new TypeMock()),
                new Parameter(new Token(TokenType.Identifier) { Text = "y" }, new TypeMock()),
                new Parameter(new Token(TokenType.Identifier) { Text = "z" }, new TypeMock())
            }
        };

        var scope = new Scope();

        var anonymousFunctionDeclarationExpression =
            new AnonymousFunctionDeclarationExpression(returnType, parameterList, scope);

        const string expected = """
        function (x : {Type}, y : {Type}, z : {Type}) => {Type}
        {
        }
        """;

        // Act
        var actual = anonymousFunctionDeclarationExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_MultipleParameterOneStatement_ReturnsCorrectString()
    {
        // Arrange
        var returnType = new TypeMock();

        var parameterList = new ParameterList
        {
            Parameters = new[]
            {
                new Parameter(new Token(TokenType.Identifier) { Text = "x" }, new TypeMock()),
                new Parameter(new Token(TokenType.Identifier) { Text = "y" }, new TypeMock()),
                new Parameter(new Token(TokenType.Identifier) { Text = "z" }, new TypeMock())
            }
        };

        var scope = new Scope
        {
            Statements = new[]
            {
                new StatementMock()
            }
        };

        var anonymousFunctionDeclarationExpression =
            new AnonymousFunctionDeclarationExpression(returnType, parameterList, scope);

        const string expected = """
        function (x : {Type}, y : {Type}, z : {Type}) => {Type}
        {
            {Statement}
        }
        """;

        // Act
        var actual = anonymousFunctionDeclarationExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_MultipleParameterThreeStatement_ReturnsCorrectString()
    {
        // Arrange
        var returnType = new TypeMock();

        var parameterList = new ParameterList
        {
            Parameters = new[]
            {
                new Parameter(new Token(TokenType.Identifier) { Text = "x" }, new TypeMock()),
                new Parameter(new Token(TokenType.Identifier) { Text = "y" }, new TypeMock()),
                new Parameter(new Token(TokenType.Identifier) { Text = "z" }, new TypeMock())
            }
        };

        var scope = new Scope
        {
            Statements = new[]
            {
                new StatementMock(),
                new StatementMock(),
                new StatementMock()
            }
        };

        var anonymousFunctionDeclarationExpression =
            new AnonymousFunctionDeclarationExpression(returnType, parameterList, scope);

        const string expected = """
        function (x : {Type}, y : {Type}, z : {Type}) => {Type}
        {
            {Statement}
            {Statement}
            {Statement}
        }
        """;

        // Act
        var actual = anonymousFunctionDeclarationExpression.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}

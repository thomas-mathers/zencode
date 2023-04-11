using AutoFixture;
using AutoFixture.Kernel;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Tests.Mocks;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Tests.Grammar.Statements;

public class FunctionDeclarationStatementTests
{
    private readonly Fixture _fixture = new();

    public FunctionDeclarationStatementTests()
    {
        _fixture.Customizations.Add(new TypeRelay(typeof(Statement), typeof(StatementMock)));
        _fixture.Customizations.Add(new TypeRelay(typeof(Type), typeof(TypeMock)));
    }

    [Fact]
    public void ToString_NoParametersEmptyBody_ReturnsCorrectString()
    {
        // Arrange
        var returnType = _fixture.Create<Type>();
        var functionName = new Token(TokenType.Identifier, "f");
        var parameterList = new ParameterList();
        var scope = new Scope();
        var functionDeclarationStatement = new FunctionDeclarationStatement(
            returnType,
            functionName, 
            parameterList, 
            scope);

        const string expected = """
        function f() => {Type}
        {
        }
        """;

        // Act
        var actual = functionDeclarationStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_ThreeParametersEmptyBody_ReturnsCorrectString()
    {
        // Arrange
        var returnType = _fixture.Create<Type>();
        var functionName = new Token(TokenType.Identifier, "f");
        var parameterList = new ParameterList
        {
            Parameters = new[]
            {
                new Parameter(new Token(TokenType.Identifier, "a"), _fixture.Create<Type>()),
                new Parameter(new Token(TokenType.Identifier, "b"), _fixture.Create<Type>()),
                new Parameter(new Token(TokenType.Identifier, "c"), _fixture.Create<Type>())
            }
        };
        var scope = new Scope();
        var functionDeclarationStatement = new FunctionDeclarationStatement(
            returnType,
            functionName,
            parameterList, 
            scope);

        const string expected = """
        function f(a : {Type}, b : {Type}, c : {Type}) => {Type}
        {
        }
        """;

        // Act
        var actual = functionDeclarationStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_NoParameters_ReturnsCorrectString()
    {
        // Arrange
        var returnType = _fixture.Create<Type>();
        var functionName = new Token(TokenType.Identifier, "f");
        var parameterList = new ParameterList();
        var scope = new Scope
        {
            Statements = _fixture.CreateMany<Statement>(3).ToArray()
        };
        var functionDeclarationStatement = new FunctionDeclarationStatement(
            returnType,
            functionName,
            parameterList, 
            scope);

        const string expected = """
        function f() => {Type}
        {
            {Statement}
            {Statement}
            {Statement}
        }
        """;

        // Act
        var actual = functionDeclarationStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_ThreeParameters_ReturnsCorrectString()
    {
        // Arrange
        var returnType = _fixture.Create<Type>();
        var functionName = new Token(TokenType.Identifier, "f");
        var parameterList = new ParameterList
        {
            Parameters = new[]
            {
                new Parameter(new Token(TokenType.Identifier, "a"), _fixture.Create<Type>()),
                new Parameter(new Token(TokenType.Identifier, "b"), _fixture.Create<Type>()),
                new Parameter(new Token(TokenType.Identifier, "c"), _fixture.Create<Type>())
            }
        };
        var scope = new Scope
        {
            Statements = _fixture.CreateMany<Statement>(3).ToArray()
        };
        var functionDeclarationStatement = new FunctionDeclarationStatement(
            returnType,
            functionName,
            parameterList, 
            scope);

        const string expected = """
        function f(a : {Type}, b : {Type}, c : {Type}) => {Type}
        {
            {Statement}
            {Statement}
            {Statement}
        }
        """;

        // Act
        var actual = functionDeclarationStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}
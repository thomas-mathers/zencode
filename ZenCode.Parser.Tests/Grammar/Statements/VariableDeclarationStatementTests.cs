using AutoFixture;
using AutoFixture.Kernel;
using Xunit;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Statements;

public class VariableDeclarationStatementTests
{
    private readonly Fixture _fixture = new();

    public VariableDeclarationStatementTests()
    {
        _fixture.Customizations.Add(new TypeRelay(typeof(Expression), typeof(ExpressionMock)));
    }

    [Fact]
    private void ToString_VariableDeclaration_ReturnsCorrectString()
    {
        // Arrange
        var variableDeclarationStatement = new VariableDeclarationStatement
        (
            new Token(TokenType.Identifier, "x"),
            _fixture.Create<Expression>()
        );

        const string expected = "var x := {Expression}";

        // Act
        var actual = variableDeclarationStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}

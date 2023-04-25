using AutoFixture;
using AutoFixture.Kernel;
using Xunit;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Tests.Common.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Statements;

public class ReturnStatementTests
{
    private readonly Fixture _fixture = new();

    public ReturnStatementTests()
    {
        _fixture.Customizations.Add(new TypeRelay(typeof(Expression), typeof(ExpressionMock)));
    }

    [Fact]
    public void ToString_ReturnStatementNoExpression_ReturnsCorrectString()
    {
        // Arrange
        var returnStatement = new ReturnStatement();

        const string expected = "return";

        // Act
        var actual = returnStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }

    [Fact]
    public void ToString_ReturnStatement_ReturnsCorrectString()
    {
        // Arrange
        var returnStatement = _fixture.Create<ReturnStatement>();

        const string expected = "return {Expression}";

        // Act
        var actual = returnStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}

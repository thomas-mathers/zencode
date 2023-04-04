using Xunit;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Tests.Grammar.Statements;

public class ContinueStatementTests
{
    [Fact]
    public void ToString_ContinueStatement_ReturnsCorrectString()
    {
        // Arrange
        var breakStatement = new ContinueStatement();
        const string expected = "continue";

        // Act
        var actual = breakStatement.ToString();

        // Assert
        Assert.Equal(expected, actual);
    }
}
using Xunit;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Tests.Grammar.Statements;

public class BreakStatementTests
{
    [Fact]
    public void ToString_BreakStatement_ReturnsCorrectString()
    {
        // Arrange
        var breakStatement = new BreakStatement();
        const string expected = "break";
        
        // Act
        var actual = breakStatement.ToString();
        
        // Assert
        Assert.Equal(expected, actual);
    }
}
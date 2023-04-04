using Xunit;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Tests.Mocks;

namespace ZenCode.Parser.Tests.Grammar.Statements;

public class PrintStatementTests
{
    [Fact]
    public void ToString_PrintStatement_ReturnsCorrectString()
    {
        // Arrange
        var printStatement = new PrintStatement(new ExpressionMock());
        const string expected = "print {Expression}";
        
        // Act
        var actual = printStatement.ToString();
        
        // Assert
        Assert.Equal(expected, actual);
    }
}
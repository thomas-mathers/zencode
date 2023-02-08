using System.Collections;
using ZenCode.Lexer;

namespace ZenCode.Parser.Tests.TestData;

public class ConstantPairTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { TokenType.Boolean, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.Float };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
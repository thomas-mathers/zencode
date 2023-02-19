using System.Collections;
using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Tests.TestData;

public class ConstantPairTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { TokenType.Boolean, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.Float };
        yield return new object[] { TokenType.Boolean, TokenType.String };
        yield return new object[] { TokenType.Integer, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.String };
        yield return new object[] { TokenType.Float, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.String };
        yield return new object[] { TokenType.String, TokenType.Boolean };
        yield return new object[] { TokenType.String, TokenType.Integer };
        yield return new object[] { TokenType.String, TokenType.Float };
        yield return new object[] { TokenType.String, TokenType.String };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
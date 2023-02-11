using System.Collections;
using ZenCode.Lexer;
using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Tests.TestData;

public class ConstantTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { TokenType.Boolean };
        yield return new object[] { TokenType.Integer };
        yield return new object[] { TokenType.Float };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
using System.Collections;
using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Tests.TestData;

public class UnaryOperators : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { TokenType.Minus };
        yield return new object[] { TokenType.Not };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
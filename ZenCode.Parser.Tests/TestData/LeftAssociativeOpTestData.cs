using System.Collections;
using ZenCode.Lexer;

namespace ZenCode.Parser.Tests.TestData;

public class LeftAssociativeOpTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { TokenType.Addition };
        yield return new object[] { TokenType.Subtraction };
        yield return new object[] { TokenType.Multiplication };
        yield return new object[] { TokenType.Division };
        yield return new object[] { TokenType.Modulus };
        yield return new object[] { TokenType.LessThan };
        yield return new object[] { TokenType.LessThanOrEqual };
        yield return new object[] { TokenType.Equals };
        yield return new object[] { TokenType.NotEquals };
        yield return new object[] { TokenType.GreaterThan };
        yield return new object[] { TokenType.GreaterThanOrEqual };
        yield return new object[] { TokenType.And };
        yield return new object[] { TokenType.Or };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
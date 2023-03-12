using System.Collections;
using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Tests.TestData
{
    public class BinaryOperators : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { TokenType.Plus };
            yield return new object[] { TokenType.Minus };
            yield return new object[] { TokenType.Multiplication };
            yield return new object[] { TokenType.Division };
            yield return new object[] { TokenType.Modulus };
            yield return new object[] { TokenType.Exponentiation };
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
}
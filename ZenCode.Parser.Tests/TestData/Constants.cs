using System.Collections;
using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Tests.TestData
{
    public class Constants : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[] { TokenType.BooleanLiteral };
            yield return new object[] { TokenType.IntegerLiteral };
            yield return new object[] { TokenType.FloatLiteral };
            yield return new object[] { TokenType.StringLiteral };
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
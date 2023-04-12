using System.Collections;
using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Tests.Integration.TestData;

public class LowPrecedenceOperatorHighPrecedenceOperatorPairs : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { TokenType.Or, TokenType.And };
        yield return new object[] { TokenType.Or, TokenType.LessThan };
        yield return new object[] { TokenType.Or, TokenType.LessThanOrEqual };
        yield return new object[] { TokenType.Or, TokenType.Equals };
        yield return new object[] { TokenType.Or, TokenType.NotEquals };
        yield return new object[] { TokenType.Or, TokenType.GreaterThan };
        yield return new object[] { TokenType.Or, TokenType.GreaterThanOrEqual };
        yield return new object[] { TokenType.Or, TokenType.Plus };
        yield return new object[] { TokenType.Or, TokenType.Minus };
        yield return new object[] { TokenType.Or, TokenType.Multiplication };
        yield return new object[] { TokenType.Or, TokenType.Division };
        yield return new object[] { TokenType.Or, TokenType.Modulus };
        yield return new object[] { TokenType.Or, TokenType.Exponentiation };
        yield return new object[] { TokenType.And, TokenType.LessThan };
        yield return new object[] { TokenType.And, TokenType.LessThanOrEqual };
        yield return new object[] { TokenType.And, TokenType.Equals };
        yield return new object[] { TokenType.And, TokenType.NotEquals };
        yield return new object[] { TokenType.And, TokenType.GreaterThan };
        yield return new object[] { TokenType.And, TokenType.GreaterThanOrEqual };
        yield return new object[] { TokenType.And, TokenType.Plus };
        yield return new object[] { TokenType.And, TokenType.Minus };
        yield return new object[] { TokenType.And, TokenType.Multiplication };
        yield return new object[] { TokenType.And, TokenType.Division };
        yield return new object[] { TokenType.And, TokenType.Modulus };
        yield return new object[] { TokenType.And, TokenType.Exponentiation };
        yield return new object[] { TokenType.LessThan, TokenType.Plus };
        yield return new object[] { TokenType.LessThan, TokenType.Minus };
        yield return new object[] { TokenType.LessThan, TokenType.Multiplication };
        yield return new object[] { TokenType.LessThan, TokenType.Division };
        yield return new object[] { TokenType.LessThan, TokenType.Modulus };
        yield return new object[] { TokenType.LessThan, TokenType.Exponentiation };
        yield return new object[] { TokenType.LessThanOrEqual, TokenType.Plus };
        yield return new object[] { TokenType.LessThanOrEqual, TokenType.Minus };
        yield return new object[] { TokenType.LessThanOrEqual, TokenType.Multiplication };
        yield return new object[] { TokenType.LessThanOrEqual, TokenType.Division };
        yield return new object[] { TokenType.LessThanOrEqual, TokenType.Modulus };
        yield return new object[] { TokenType.LessThanOrEqual, TokenType.Exponentiation };
        yield return new object[] { TokenType.Equals, TokenType.Plus };
        yield return new object[] { TokenType.Equals, TokenType.Minus };
        yield return new object[] { TokenType.Equals, TokenType.Multiplication };
        yield return new object[] { TokenType.Equals, TokenType.Division };
        yield return new object[] { TokenType.Equals, TokenType.Modulus };
        yield return new object[] { TokenType.Equals, TokenType.Exponentiation };
        yield return new object[] { TokenType.NotEquals, TokenType.Plus };
        yield return new object[] { TokenType.NotEquals, TokenType.Minus };
        yield return new object[] { TokenType.NotEquals, TokenType.Multiplication };
        yield return new object[] { TokenType.NotEquals, TokenType.Division };
        yield return new object[] { TokenType.NotEquals, TokenType.Modulus };
        yield return new object[] { TokenType.NotEquals, TokenType.Exponentiation };
        yield return new object[] { TokenType.GreaterThan, TokenType.Plus };
        yield return new object[] { TokenType.GreaterThan, TokenType.Minus };
        yield return new object[] { TokenType.GreaterThan, TokenType.Multiplication };
        yield return new object[] { TokenType.GreaterThan, TokenType.Division };
        yield return new object[] { TokenType.GreaterThan, TokenType.Modulus };
        yield return new object[] { TokenType.GreaterThan, TokenType.Exponentiation };
        yield return new object[] { TokenType.GreaterThanOrEqual, TokenType.Plus };
        yield return new object[] { TokenType.GreaterThanOrEqual, TokenType.Minus };
        yield return new object[] { TokenType.GreaterThanOrEqual, TokenType.Multiplication };
        yield return new object[] { TokenType.GreaterThanOrEqual, TokenType.Division };
        yield return new object[] { TokenType.GreaterThanOrEqual, TokenType.Modulus };
        yield return new object[] { TokenType.GreaterThanOrEqual, TokenType.Exponentiation };
        yield return new object[] { TokenType.Plus, TokenType.Multiplication };
        yield return new object[] { TokenType.Plus, TokenType.Division };
        yield return new object[] { TokenType.Plus, TokenType.Modulus };
        yield return new object[] { TokenType.Plus, TokenType.Exponentiation };
        yield return new object[] { TokenType.Minus, TokenType.Multiplication };
        yield return new object[] { TokenType.Minus, TokenType.Division };
        yield return new object[] { TokenType.Minus, TokenType.Modulus };
        yield return new object[] { TokenType.Minus, TokenType.Exponentiation };
        yield return new object[] { TokenType.Multiplication, TokenType.Exponentiation };
        yield return new object[] { TokenType.Division, TokenType.Exponentiation };
        yield return new object[] { TokenType.Modulus, TokenType.Exponentiation };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

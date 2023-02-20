using System.Collections;
using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Tests.TestData;

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
        yield return new object[] { TokenType.Or, TokenType.Addition };
        yield return new object[] { TokenType.Or, TokenType.Subtraction };
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
        yield return new object[] { TokenType.And, TokenType.Addition };
        yield return new object[] { TokenType.And, TokenType.Subtraction };
        yield return new object[] { TokenType.And, TokenType.Multiplication };
        yield return new object[] { TokenType.And, TokenType.Division };
        yield return new object[] { TokenType.And, TokenType.Modulus };
        yield return new object[] { TokenType.And, TokenType.Exponentiation };
        yield return new object[] { TokenType.LessThan, TokenType.Addition };
        yield return new object[] { TokenType.LessThan, TokenType.Subtraction };
        yield return new object[] { TokenType.LessThan, TokenType.Multiplication };
        yield return new object[] { TokenType.LessThan, TokenType.Division };
        yield return new object[] { TokenType.LessThan, TokenType.Modulus };
        yield return new object[] { TokenType.LessThan, TokenType.Exponentiation };
        yield return new object[] { TokenType.LessThanOrEqual, TokenType.Addition };
        yield return new object[] { TokenType.LessThanOrEqual, TokenType.Subtraction };
        yield return new object[] { TokenType.LessThanOrEqual, TokenType.Multiplication };
        yield return new object[] { TokenType.LessThanOrEqual, TokenType.Division };
        yield return new object[] { TokenType.LessThanOrEqual, TokenType.Modulus };
        yield return new object[] { TokenType.LessThanOrEqual, TokenType.Exponentiation };
        yield return new object[] { TokenType.Equals, TokenType.Addition };
        yield return new object[] { TokenType.Equals, TokenType.Subtraction };
        yield return new object[] { TokenType.Equals, TokenType.Multiplication };
        yield return new object[] { TokenType.Equals, TokenType.Division };
        yield return new object[] { TokenType.Equals, TokenType.Modulus };
        yield return new object[] { TokenType.Equals, TokenType.Exponentiation };
        yield return new object[] { TokenType.NotEquals, TokenType.Addition };
        yield return new object[] { TokenType.NotEquals, TokenType.Subtraction };
        yield return new object[] { TokenType.NotEquals, TokenType.Multiplication };
        yield return new object[] { TokenType.NotEquals, TokenType.Division };
        yield return new object[] { TokenType.NotEquals, TokenType.Modulus };
        yield return new object[] { TokenType.NotEquals, TokenType.Exponentiation };
        yield return new object[] { TokenType.GreaterThan, TokenType.Addition };
        yield return new object[] { TokenType.GreaterThan, TokenType.Subtraction };
        yield return new object[] { TokenType.GreaterThan, TokenType.Multiplication };
        yield return new object[] { TokenType.GreaterThan, TokenType.Division };
        yield return new object[] { TokenType.GreaterThan, TokenType.Modulus };
        yield return new object[] { TokenType.GreaterThan, TokenType.Exponentiation };
        yield return new object[] { TokenType.GreaterThanOrEqual, TokenType.Addition };
        yield return new object[] { TokenType.GreaterThanOrEqual, TokenType.Subtraction };
        yield return new object[] { TokenType.GreaterThanOrEqual, TokenType.Multiplication };
        yield return new object[] { TokenType.GreaterThanOrEqual, TokenType.Division };
        yield return new object[] { TokenType.GreaterThanOrEqual, TokenType.Modulus };
        yield return new object[] { TokenType.GreaterThanOrEqual, TokenType.Exponentiation };
        yield return new object[] { TokenType.Addition, TokenType.Multiplication };
        yield return new object[] { TokenType.Addition, TokenType.Division };
        yield return new object[] { TokenType.Addition, TokenType.Modulus };
        yield return new object[] { TokenType.Addition, TokenType.Exponentiation };
        yield return new object[] { TokenType.Subtraction, TokenType.Multiplication };
        yield return new object[] { TokenType.Subtraction, TokenType.Division };
        yield return new object[] { TokenType.Subtraction, TokenType.Modulus };
        yield return new object[] { TokenType.Subtraction, TokenType.Exponentiation };
        yield return new object[] { TokenType.Multiplication, TokenType.Exponentiation };
        yield return new object[] { TokenType.Division, TokenType.Exponentiation };
        yield return new object[] { TokenType.Modulus, TokenType.Exponentiation };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
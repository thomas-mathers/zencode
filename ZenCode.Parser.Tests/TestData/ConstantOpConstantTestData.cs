using System.Collections;
using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Tests.TestData;

public class ConstantOpConstantTestData : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { TokenType.Boolean, TokenType.Addition, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.Subtraction, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.Multiplication, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.Division, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.Modulus, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.Exponentiation, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.LessThan, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.LessThanOrEqual, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.Equals, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.NotEquals, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.GreaterThan, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.GreaterThanOrEqual, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.And, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.Or, TokenType.Boolean };
        yield return new object[] { TokenType.Boolean, TokenType.Addition, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.Subtraction, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.Multiplication, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.Division, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.Modulus, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.Exponentiation, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.LessThan, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.LessThanOrEqual, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.Equals, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.NotEquals, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.GreaterThan, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.GreaterThanOrEqual, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.And, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.Or, TokenType.Integer };
        yield return new object[] { TokenType.Boolean, TokenType.Addition, TokenType.Float };
        yield return new object[] { TokenType.Boolean, TokenType.Subtraction, TokenType.Float };
        yield return new object[] { TokenType.Boolean, TokenType.Multiplication, TokenType.Float };
        yield return new object[] { TokenType.Boolean, TokenType.Division, TokenType.Float };
        yield return new object[] { TokenType.Boolean, TokenType.Modulus, TokenType.Float };
        yield return new object[] { TokenType.Boolean, TokenType.Exponentiation, TokenType.Float };
        yield return new object[] { TokenType.Boolean, TokenType.LessThan, TokenType.Float };
        yield return new object[] { TokenType.Boolean, TokenType.LessThanOrEqual, TokenType.Float };
        yield return new object[] { TokenType.Boolean, TokenType.Equals, TokenType.Float };
        yield return new object[] { TokenType.Boolean, TokenType.NotEquals, TokenType.Float };
        yield return new object[] { TokenType.Boolean, TokenType.GreaterThan, TokenType.Float };
        yield return new object[] { TokenType.Boolean, TokenType.GreaterThanOrEqual, TokenType.Float };
        yield return new object[] { TokenType.Boolean, TokenType.And, TokenType.Float };
        yield return new object[] { TokenType.Boolean, TokenType.Or, TokenType.Float };
        yield return new object[] { TokenType.Boolean, TokenType.Addition, TokenType.String };
        yield return new object[] { TokenType.Boolean, TokenType.Subtraction, TokenType.String };
        yield return new object[] { TokenType.Boolean, TokenType.Multiplication, TokenType.String };
        yield return new object[] { TokenType.Boolean, TokenType.Division, TokenType.String };
        yield return new object[] { TokenType.Boolean, TokenType.Modulus, TokenType.String };
        yield return new object[] { TokenType.Boolean, TokenType.Exponentiation, TokenType.String };
        yield return new object[] { TokenType.Boolean, TokenType.LessThan, TokenType.String };
        yield return new object[] { TokenType.Boolean, TokenType.LessThanOrEqual, TokenType.String };
        yield return new object[] { TokenType.Boolean, TokenType.Equals, TokenType.String };
        yield return new object[] { TokenType.Boolean, TokenType.NotEquals, TokenType.String };
        yield return new object[] { TokenType.Boolean, TokenType.GreaterThan, TokenType.String };
        yield return new object[] { TokenType.Boolean, TokenType.GreaterThanOrEqual, TokenType.String };
        yield return new object[] { TokenType.Boolean, TokenType.And, TokenType.String };
        yield return new object[] { TokenType.Boolean, TokenType.Or, TokenType.String };
        
        yield return new object[] { TokenType.Integer, TokenType.Addition, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.Subtraction, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.Multiplication, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.Division, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.Modulus, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.Exponentiation, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.LessThan, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.LessThanOrEqual, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.Equals, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.NotEquals, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.GreaterThan, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.GreaterThanOrEqual, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.And, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.Or, TokenType.Boolean };
        yield return new object[] { TokenType.Integer, TokenType.Addition, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.Subtraction, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.Multiplication, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.Division, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.Modulus, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.Exponentiation, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.LessThan, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.LessThanOrEqual, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.Equals, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.NotEquals, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.GreaterThan, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.GreaterThanOrEqual, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.And, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.Or, TokenType.Integer };
        yield return new object[] { TokenType.Integer, TokenType.Addition, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.Subtraction, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.Multiplication, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.Division, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.Modulus, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.Exponentiation, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.LessThan, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.LessThanOrEqual, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.Equals, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.NotEquals, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.GreaterThan, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.GreaterThanOrEqual, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.And, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.Or, TokenType.Float };
        yield return new object[] { TokenType.Integer, TokenType.Addition, TokenType.String };
        yield return new object[] { TokenType.Integer, TokenType.Subtraction, TokenType.String };
        yield return new object[] { TokenType.Integer, TokenType.Multiplication, TokenType.String };
        yield return new object[] { TokenType.Integer, TokenType.Division, TokenType.String };
        yield return new object[] { TokenType.Integer, TokenType.Modulus, TokenType.String };
        yield return new object[] { TokenType.Integer, TokenType.Exponentiation, TokenType.String };
        yield return new object[] { TokenType.Integer, TokenType.LessThan, TokenType.String };
        yield return new object[] { TokenType.Integer, TokenType.LessThanOrEqual, TokenType.String };
        yield return new object[] { TokenType.Integer, TokenType.Equals, TokenType.String };
        yield return new object[] { TokenType.Integer, TokenType.NotEquals, TokenType.String };
        yield return new object[] { TokenType.Integer, TokenType.GreaterThan, TokenType.String };
        yield return new object[] { TokenType.Integer, TokenType.GreaterThanOrEqual, TokenType.String };
        yield return new object[] { TokenType.Integer, TokenType.And, TokenType.String };
        yield return new object[] { TokenType.Integer, TokenType.Or, TokenType.String };
        
        yield return new object[] { TokenType.Float, TokenType.Addition, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.Subtraction, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.Multiplication, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.Division, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.Modulus, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.Exponentiation, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.LessThan, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.LessThanOrEqual, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.Equals, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.NotEquals, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.GreaterThan, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.GreaterThanOrEqual, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.And, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.Or, TokenType.Boolean };
        yield return new object[] { TokenType.Float, TokenType.Addition, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.Subtraction, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.Multiplication, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.Division, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.Modulus, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.Exponentiation, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.LessThan, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.LessThanOrEqual, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.Equals, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.NotEquals, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.GreaterThan, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.GreaterThanOrEqual, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.And, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.Or, TokenType.Integer };
        yield return new object[] { TokenType.Float, TokenType.Addition, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.Subtraction, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.Multiplication, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.Division, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.Modulus, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.Exponentiation, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.LessThan, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.LessThanOrEqual, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.Equals, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.NotEquals, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.GreaterThan, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.GreaterThanOrEqual, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.And, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.Or, TokenType.Float };
        yield return new object[] { TokenType.Float, TokenType.Addition, TokenType.String };
        yield return new object[] { TokenType.Float, TokenType.Subtraction, TokenType.String };
        yield return new object[] { TokenType.Float, TokenType.Multiplication, TokenType.String };
        yield return new object[] { TokenType.Float, TokenType.Division, TokenType.String };
        yield return new object[] { TokenType.Float, TokenType.Modulus, TokenType.String };
        yield return new object[] { TokenType.Float, TokenType.Exponentiation, TokenType.String };
        yield return new object[] { TokenType.Float, TokenType.LessThan, TokenType.String };
        yield return new object[] { TokenType.Float, TokenType.LessThanOrEqual, TokenType.String };
        yield return new object[] { TokenType.Float, TokenType.Equals, TokenType.String };
        yield return new object[] { TokenType.Float, TokenType.NotEquals, TokenType.String };
        yield return new object[] { TokenType.Float, TokenType.GreaterThan, TokenType.String };
        yield return new object[] { TokenType.Float, TokenType.GreaterThanOrEqual, TokenType.String };
        yield return new object[] { TokenType.Float, TokenType.And, TokenType.String };
        yield return new object[] { TokenType.Float, TokenType.Or, TokenType.String };
        
        yield return new object[] { TokenType.String, TokenType.Addition, TokenType.Boolean };
        yield return new object[] { TokenType.String, TokenType.Subtraction, TokenType.Boolean };
        yield return new object[] { TokenType.String, TokenType.Multiplication, TokenType.Boolean };
        yield return new object[] { TokenType.String, TokenType.Division, TokenType.Boolean };
        yield return new object[] { TokenType.String, TokenType.Modulus, TokenType.Boolean };
        yield return new object[] { TokenType.String, TokenType.Exponentiation, TokenType.Boolean };
        yield return new object[] { TokenType.String, TokenType.LessThan, TokenType.Boolean };
        yield return new object[] { TokenType.String, TokenType.LessThanOrEqual, TokenType.Boolean };
        yield return new object[] { TokenType.String, TokenType.Equals, TokenType.Boolean };
        yield return new object[] { TokenType.String, TokenType.NotEquals, TokenType.Boolean };
        yield return new object[] { TokenType.String, TokenType.GreaterThan, TokenType.Boolean };
        yield return new object[] { TokenType.String, TokenType.GreaterThanOrEqual, TokenType.Boolean };
        yield return new object[] { TokenType.String, TokenType.And, TokenType.Boolean };
        yield return new object[] { TokenType.String, TokenType.Or, TokenType.Boolean };
        yield return new object[] { TokenType.String, TokenType.Addition, TokenType.Integer };
        yield return new object[] { TokenType.String, TokenType.Subtraction, TokenType.Integer };
        yield return new object[] { TokenType.String, TokenType.Multiplication, TokenType.Integer };
        yield return new object[] { TokenType.String, TokenType.Division, TokenType.Integer };
        yield return new object[] { TokenType.String, TokenType.Modulus, TokenType.Integer };
        yield return new object[] { TokenType.String, TokenType.Exponentiation, TokenType.Integer };
        yield return new object[] { TokenType.String, TokenType.LessThan, TokenType.Integer };
        yield return new object[] { TokenType.String, TokenType.LessThanOrEqual, TokenType.Integer };
        yield return new object[] { TokenType.String, TokenType.Equals, TokenType.Integer };
        yield return new object[] { TokenType.String, TokenType.NotEquals, TokenType.Integer };
        yield return new object[] { TokenType.String, TokenType.GreaterThan, TokenType.Integer };
        yield return new object[] { TokenType.String, TokenType.GreaterThanOrEqual, TokenType.Integer };
        yield return new object[] { TokenType.String, TokenType.And, TokenType.Integer };
        yield return new object[] { TokenType.String, TokenType.Or, TokenType.Integer };
        yield return new object[] { TokenType.String, TokenType.Addition, TokenType.Float };
        yield return new object[] { TokenType.String, TokenType.Subtraction, TokenType.Float };
        yield return new object[] { TokenType.String, TokenType.Multiplication, TokenType.Float };
        yield return new object[] { TokenType.String, TokenType.Division, TokenType.Float };
        yield return new object[] { TokenType.String, TokenType.Modulus, TokenType.Float };
        yield return new object[] { TokenType.String, TokenType.Exponentiation, TokenType.Float };
        yield return new object[] { TokenType.String, TokenType.LessThan, TokenType.Float };
        yield return new object[] { TokenType.String, TokenType.LessThanOrEqual, TokenType.Float };
        yield return new object[] { TokenType.String, TokenType.Equals, TokenType.Float };
        yield return new object[] { TokenType.String, TokenType.NotEquals, TokenType.Float };
        yield return new object[] { TokenType.String, TokenType.GreaterThan, TokenType.Float };
        yield return new object[] { TokenType.String, TokenType.GreaterThanOrEqual, TokenType.Float };
        yield return new object[] { TokenType.String, TokenType.And, TokenType.Float };
        yield return new object[] { TokenType.String, TokenType.Or, TokenType.Float };
        yield return new object[] { TokenType.String, TokenType.Addition, TokenType.String };
        yield return new object[] { TokenType.String, TokenType.Subtraction, TokenType.String };
        yield return new object[] { TokenType.String, TokenType.Multiplication, TokenType.String };
        yield return new object[] { TokenType.String, TokenType.Division, TokenType.String };
        yield return new object[] { TokenType.String, TokenType.Modulus, TokenType.String };
        yield return new object[] { TokenType.String, TokenType.Exponentiation, TokenType.String };
        yield return new object[] { TokenType.String, TokenType.LessThan, TokenType.String };
        yield return new object[] { TokenType.String, TokenType.LessThanOrEqual, TokenType.String };
        yield return new object[] { TokenType.String, TokenType.Equals, TokenType.String };
        yield return new object[] { TokenType.String, TokenType.NotEquals, TokenType.String };
        yield return new object[] { TokenType.String, TokenType.GreaterThan, TokenType.String };
        yield return new object[] { TokenType.String, TokenType.GreaterThanOrEqual, TokenType.String };
        yield return new object[] { TokenType.String, TokenType.And, TokenType.String };
        yield return new object[] { TokenType.String, TokenType.Or, TokenType.String };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
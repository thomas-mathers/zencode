using System.Collections;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.SemanticAnalysis.Tests.TestData;

public class BinaryOperatorSupportedTypes : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
            { TokenType.And, new BooleanType(), new BooleanType(), new BooleanType() };

        yield return new object[]
            { TokenType.Or, new BooleanType(), new BooleanType(), new BooleanType() };

        yield return new object[]
            { TokenType.Plus, new IntegerType(), new IntegerType(), new IntegerType() };

        yield return new object[]
            { TokenType.Minus, new IntegerType(), new IntegerType(), new IntegerType() };

        yield return new object[]
            { TokenType.Multiplication, new IntegerType(), new IntegerType(), new IntegerType() };

        yield return new object[]
            { TokenType.Division, new IntegerType(), new IntegerType(), new IntegerType() };

        yield return new object[]
            { TokenType.Modulus, new IntegerType(), new IntegerType(), new IntegerType() };

        yield return new object[]
            { TokenType.Exponentiation, new IntegerType(), new IntegerType(), new IntegerType() };

        yield return new object[]
            { TokenType.LessThan, new IntegerType(), new IntegerType(), new BooleanType() };

        yield return new object[]
            { TokenType.LessThanOrEqual, new IntegerType(), new IntegerType(), new BooleanType() };

        yield return new object[]
            { TokenType.Equals, new IntegerType(), new IntegerType(), new BooleanType() };

        yield return new object[]
            { TokenType.NotEquals, new IntegerType(), new IntegerType(), new BooleanType() };

        yield return new object[]
            { TokenType.GreaterThan, new IntegerType(), new IntegerType(), new BooleanType() };

        yield return new object[]
            { TokenType.GreaterThanOrEqual, new IntegerType(), new IntegerType(), new BooleanType() };

        yield return new object[] { TokenType.Plus, new FloatType(), new FloatType(), new FloatType() };
        
        yield return new object[] { TokenType.Minus, new FloatType(), new FloatType(), new FloatType() };

        yield return new object[]
            { TokenType.Multiplication, new FloatType(), new FloatType(), new FloatType() };

        yield return new object[]
            { TokenType.Division, new FloatType(), new FloatType(), new FloatType() };

        yield return new object[]
            { TokenType.Modulus, new FloatType(), new FloatType(), new FloatType() };

        yield return new object[]
            { TokenType.Exponentiation, new FloatType(), new FloatType(), new FloatType() };

        yield return new object[]
            { TokenType.LessThan, new FloatType(), new FloatType(), new BooleanType() };

        yield return new object[]
            { TokenType.LessThanOrEqual, new FloatType(), new FloatType(), new BooleanType() };

        yield return new object[]
            { TokenType.Equals, new FloatType(), new FloatType(), new BooleanType() };

        yield return new object[]
            { TokenType.NotEquals, new FloatType(), new FloatType(), new BooleanType() };

        yield return new object[]
            { TokenType.GreaterThan, new FloatType(), new FloatType(), new BooleanType() };

        yield return new object[]
            { TokenType.GreaterThanOrEqual, new FloatType(), new FloatType(), new BooleanType() };

        yield return new object[]
            { TokenType.Plus, new StringType(), new StringType(), new StringType() };

        yield return new object[]
            { TokenType.LessThan, new StringType(), new StringType(), new BooleanType() };

        yield return new object[]
            { TokenType.LessThanOrEqual, new StringType(), new StringType(), new BooleanType() };

        yield return new object[]
            { TokenType.Equals, new StringType(), new StringType(), new BooleanType() };

        yield return new object[]
            { TokenType.NotEquals, new StringType(), new StringType(), new BooleanType() };

        yield return new object[]
            { TokenType.GreaterThan, new StringType(), new StringType(), new BooleanType() };

        yield return new object[]
            { TokenType.GreaterThanOrEqual, new StringType(), new StringType(), new BooleanType() };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

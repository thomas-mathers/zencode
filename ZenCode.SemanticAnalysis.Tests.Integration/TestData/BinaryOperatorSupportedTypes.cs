using System.Collections;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.SemanticAnalysis.Tests.Integration.TestData;

public class BinaryOperatorSupportedTypes : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[]
            { TokenType.And, TokenType.BooleanLiteral, TokenType.BooleanLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.Or, TokenType.BooleanLiteral, TokenType.BooleanLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.Plus, TokenType.IntegerLiteral, TokenType.IntegerLiteral, new IntegerType() };

        yield return new object[]
            { TokenType.Minus, TokenType.IntegerLiteral, TokenType.IntegerLiteral, new IntegerType() };

        yield return new object[]
            { TokenType.Multiplication, TokenType.IntegerLiteral, TokenType.IntegerLiteral, new IntegerType() };

        yield return new object[]
            { TokenType.Division, TokenType.IntegerLiteral, TokenType.IntegerLiteral, new IntegerType() };

        yield return new object[]
            { TokenType.Modulus, TokenType.IntegerLiteral, TokenType.IntegerLiteral, new IntegerType() };

        yield return new object[]
            { TokenType.Exponentiation, TokenType.IntegerLiteral, TokenType.IntegerLiteral, new IntegerType() };

        yield return new object[]
            { TokenType.LessThan, TokenType.IntegerLiteral, TokenType.IntegerLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.LessThanOrEqual, TokenType.IntegerLiteral, TokenType.IntegerLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.Equals, TokenType.IntegerLiteral, TokenType.IntegerLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.NotEquals, TokenType.IntegerLiteral, TokenType.IntegerLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.GreaterThan, TokenType.IntegerLiteral, TokenType.IntegerLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.GreaterThanOrEqual, TokenType.IntegerLiteral, TokenType.IntegerLiteral, new BooleanType() };

        yield return new object[] { TokenType.Plus, TokenType.FloatLiteral, TokenType.FloatLiteral, new FloatType() };
        yield return new object[] { TokenType.Minus, TokenType.FloatLiteral, TokenType.FloatLiteral, new FloatType() };

        yield return new object[]
            { TokenType.Multiplication, TokenType.FloatLiteral, TokenType.FloatLiteral, new FloatType() };

        yield return new object[]
            { TokenType.Division, TokenType.FloatLiteral, TokenType.FloatLiteral, new FloatType() };

        yield return new object[]
            { TokenType.Modulus, TokenType.FloatLiteral, TokenType.FloatLiteral, new FloatType() };

        yield return new object[]
            { TokenType.Exponentiation, TokenType.FloatLiteral, TokenType.FloatLiteral, new FloatType() };

        yield return new object[]
            { TokenType.LessThan, TokenType.FloatLiteral, TokenType.FloatLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.LessThanOrEqual, TokenType.FloatLiteral, TokenType.FloatLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.Equals, TokenType.FloatLiteral, TokenType.FloatLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.NotEquals, TokenType.FloatLiteral, TokenType.FloatLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.GreaterThan, TokenType.FloatLiteral, TokenType.FloatLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.GreaterThanOrEqual, TokenType.FloatLiteral, TokenType.FloatLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.Plus, TokenType.StringLiteral, TokenType.StringLiteral, new StringType() };

        yield return new object[]
            { TokenType.LessThan, TokenType.StringLiteral, TokenType.StringLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.LessThanOrEqual, TokenType.StringLiteral, TokenType.StringLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.Equals, TokenType.StringLiteral, TokenType.StringLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.NotEquals, TokenType.StringLiteral, TokenType.StringLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.GreaterThan, TokenType.StringLiteral, TokenType.StringLiteral, new BooleanType() };

        yield return new object[]
            { TokenType.GreaterThanOrEqual, TokenType.StringLiteral, TokenType.StringLiteral, new BooleanType() };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}

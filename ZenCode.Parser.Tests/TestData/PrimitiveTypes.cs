using System.Collections;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Tests.TestData;

public class PrimitiveTypes : IEnumerable<object[]>
{
    public IEnumerator<object[]> GetEnumerator()
    {
        yield return new object[] { new VoidType() };
        yield return new object[] { new BooleanType() };
        yield return new object[] { new IntegerType() };
        yield return new object[] { new FloatType() };
        yield return new object[] { new StringType() };
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }
}
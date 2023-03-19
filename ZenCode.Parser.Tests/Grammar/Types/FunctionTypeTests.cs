using Xunit;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Types;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Tests.Grammar.Types;

public class FunctionTypeTests
{
    public static readonly IEnumerable<object[]> TypeStringPairs = new List<object[]>
    {
        new object[] { new FunctionType(new VoidType(), new TypeList()), "() => void" },
        new object[] { new FunctionType(new VoidType(), new TypeList { Types = new[] { new BooleanType() }}), "(boolean) => void" },
        new object[] { new FunctionType(new VoidType(), new TypeList { Types = new[] { new IntegerType() }}), "(integer) => void" },
        new object[] { new FunctionType(new VoidType(), new TypeList { Types = new[] { new FloatType() }}), "(float) => void" },
        new object[] { new FunctionType(new VoidType(), new TypeList { Types = new[] { new StringType() }}), "(string) => void" },
    };

    [Theory]
    [MemberData(nameof(TypeStringPairs))]
    public void ToString_FunctionType_ReturnsCorrectString(Type type, string expected)
    {
        // Arrange + Act + Assert
        Assert.Equal(expected, type.ToString());
    }
}
using Xunit;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Types;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Tests.Grammar.Types;

public class ArrayTypeTests
{
    public static readonly IEnumerable<object[]> TypeStringPairs = new List<object[]>
    {
        new object[] { new ArrayType(new ArrayType(new BooleanType())), "boolean[][]" },
        new object[] { new ArrayType(new ArrayType(new IntegerType())), "integer[][]" },
        new object[] { new ArrayType(new ArrayType(new FloatType())), "float[][]" },
        new object[] { new ArrayType(new ArrayType(new StringType())), "string[][]" },
        new object[] { new ArrayType(new ArrayType(new FunctionType(new VoidType(), new TypeList()))), "() => void[][]" },
        new object[] { new ArrayType(new ArrayType(new FunctionType(new VoidType(), new TypeList { Types = new[] { new BooleanType() }}))), "(boolean) => void[][]" },
        new object[] { new ArrayType(new ArrayType(new FunctionType(new VoidType(), new TypeList { Types = new[] { new IntegerType() }}))), "(integer) => void[][]" },
        new object[] { new ArrayType(new ArrayType(new FunctionType(new VoidType(), new TypeList { Types = new[] { new FloatType() }}))), "(float) => void[][]" },
        new object[] { new ArrayType(new ArrayType(new FunctionType(new VoidType(), new TypeList { Types = new[] { new StringType() }}))), "(string) => void[][]" },
        new object[] { new ArrayType(new BooleanType()), "boolean[]" },
        new object[] { new ArrayType(new IntegerType()), "integer[]" },
        new object[] { new ArrayType(new FloatType()), "float[]" },
        new object[] { new ArrayType(new StringType()), "string[]" },
        new object[] { new ArrayType(new FunctionType(new VoidType(), new TypeList())), "() => void[]" },
        new object[] { new ArrayType(new FunctionType(new VoidType(), new TypeList { Types = new[] { new BooleanType() }})), "(boolean) => void[]" },
        new object[] { new ArrayType(new FunctionType(new VoidType(), new TypeList { Types = new[] { new IntegerType() }})), "(integer) => void[]" },
        new object[] { new ArrayType(new FunctionType(new VoidType(), new TypeList { Types = new[] { new FloatType() }})), "(float) => void[]" },
        new object[] { new ArrayType(new FunctionType(new VoidType(), new TypeList { Types = new[] { new StringType() }})), "(string) => void[]" },
    };

    [Theory]
    [MemberData(nameof(TypeStringPairs))]
    public void ToString_ArrayType_ReturnsCorrectString(Type type, string expected)
    {
        // Arrange + Act + Assert
        Assert.Equal(expected, type.ToString());
    }
}
using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Types;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Abstractions.Types;

public interface IArrayTypeParsingStrategy
{
    ArrayType Parse(ITokenStream tokenStream, Type baseType);
}

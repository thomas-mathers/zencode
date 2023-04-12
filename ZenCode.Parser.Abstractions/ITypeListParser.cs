using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar;

namespace ZenCode.Parser.Abstractions;

public interface ITypeListParser
{
    TypeList ParseTypeList(IParser parser, ITokenStream tokenStream);
}

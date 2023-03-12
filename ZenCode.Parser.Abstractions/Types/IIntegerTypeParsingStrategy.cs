using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Abstractions.Types;

public interface IIntegerTypeParsingStrategy
{
    IntegerType Parse(ITokenStream tokenStream);
}
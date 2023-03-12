using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Types;

namespace ZenCode.Parser.Abstractions.Types;

public interface IFunctionTypeParsingStrategy
{
    FunctionType Parse(IParser parser, ITokenStream tokenStream);
}
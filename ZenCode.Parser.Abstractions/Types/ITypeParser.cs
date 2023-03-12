using ZenCode.Lexer.Abstractions;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Abstractions.Types;

public interface ITypeParser
{
    Type ParseType(IParser parser, ITokenStream tokenStream);
}
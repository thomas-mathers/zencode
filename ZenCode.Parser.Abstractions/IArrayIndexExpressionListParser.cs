using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar;

namespace ZenCode.Parser.Abstractions;

public interface IArrayIndexExpressionListParser
{
    ArrayIndexExpressionList Parse(IParser parser, ITokenStream tokenStream);
}

using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar;

namespace ZenCode.Parser.Abstractions;

public interface IExpressionListParser
{
    ExpressionList ParseExpressionList(IParser parser, ITokenStream tokenStream);
}
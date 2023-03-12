using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface IParenthesisParsingStrategy
{
    Expression Parse(IParser parser, ITokenStream tokenStream);
}
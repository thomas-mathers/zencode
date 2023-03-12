using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies
{
    public interface IAnonymousFunctionDeclarationParsingStrategy
    {
        AnonymousFunctionDeclarationExpression Parse(IParser parser, ITokenStream tokenStream);
    }
}
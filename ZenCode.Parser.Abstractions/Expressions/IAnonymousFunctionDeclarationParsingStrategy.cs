using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface IAnonymousFunctionDeclarationParsingStrategy
{
    AnonymousFunctionDeclarationExpression Parse(IParser parser, ITokenStream tokenStream);
}

using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface ILiteralParsingStrategy
{
    LiteralExpression Parse(ITokenStream tokenStream, TokenType tokenType);
}

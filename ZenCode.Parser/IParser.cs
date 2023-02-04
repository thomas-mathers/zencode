using ZenCode.Lexer;
using ZenCode.Parser.Grammar;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser;

public interface IParser
{
    Token Expect(TokenType tokenType);
    Token? Consume();
    Program Parse(string input);
    Expression ParseExpression();
}
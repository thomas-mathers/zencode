using ZenCode.Lexer;
using ZenCode.Parser.Grammar;
using ZenCode.Parser.Grammar.Expressions;

namespace ZenCode.Parser;

public interface IParser
{
    ITokenStream TokenStream { get; }
    Program Parse(string input);
    Expression ParseExpression(int precedence = 0);
}
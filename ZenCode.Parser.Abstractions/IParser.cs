using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Abstractions;

public interface IParser
{
    Program Parse(string program);
    Expression ParseExpression(ITokenStream tokenStream, int precedence = 0);
    ExpressionList ParseExpressionList(ITokenStream tokenStream);
    Scope ParseScope(ITokenStream tokenStream);
    ConditionScope ParseConditionScope(ITokenStream tokenStream);
    Type ParseType(ITokenStream tokenStream, int precedence = 0);
    ParameterList ParseParameterList(ITokenStream tokenStream);
}
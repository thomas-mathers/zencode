using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Abstractions.Expressions;

public interface IPrefixExpressionParser
{
    Expression ParsePrefixExpression(IParser parser, ITokenStream tokenStream);
    VariableReferenceExpression ParseVariableReferenceExpression(IParser parser, ITokenStream tokenStream);
}

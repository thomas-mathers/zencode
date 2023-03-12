using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public interface IVariableReferenceParsingStrategy
{
    VariableReferenceExpression Parse(IParser parser, ITokenStream tokenStream);
}
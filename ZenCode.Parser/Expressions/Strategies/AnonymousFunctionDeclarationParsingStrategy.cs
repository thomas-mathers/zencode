using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class AnonymousFunctionDeclarationParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly IParser _parser;
    
    public AnonymousFunctionDeclarationParsingStrategy(IParser parser)
    {
        _parser = parser;
    }
    
    public Expression Parse(ITokenStream tokenStream)
    {
        throw new NotImplementedException();
    }
}
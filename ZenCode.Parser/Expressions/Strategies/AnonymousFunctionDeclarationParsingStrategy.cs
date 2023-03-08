using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model;
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
        tokenStream.Consume(TokenType.Function);
        tokenStream.Consume(TokenType.LeftParenthesis);

        var parameters = tokenStream.Match(TokenType.RightParenthesis) 
            ? new ParameterList() 
            : _parser.ParseParameterList(tokenStream);

        tokenStream.Consume(TokenType.RightParenthesis);
        tokenStream.Consume(TokenType.RightArrow);

        var returnType = _parser.ParseType(tokenStream);
        var scope = _parser.ParseScope(tokenStream);

        return new AnonymousFunctionDeclarationExpression(returnType, parameters, scope);
    }
}
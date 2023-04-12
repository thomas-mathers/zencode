using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class AnonymousFunctionDeclarationParsingStrategy : IAnonymousFunctionDeclarationParsingStrategy
{
    public AnonymousFunctionDeclarationExpression Parse(IParser parser, ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Function);
        tokenStream.Consume(TokenType.LeftParenthesis);

        var parameters = tokenStream.Match(TokenType.RightParenthesis)
            ? new ParameterList()
            : parser.ParseParameterList(tokenStream);

        tokenStream.Consume(TokenType.RightParenthesis);
        tokenStream.Consume(TokenType.RightArrow);

        var returnType = parser.ParseType(tokenStream);
        var scope = parser.ParseScope(tokenStream);

        return new AnonymousFunctionDeclarationExpression(returnType, parameters, scope);
    }
}

using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions.Helpers;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Abstractions.Statements.Helpers;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class AnonymousFunctionDeclarationParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly IScopeParser _scopeParser;
    private readonly IParameterListParser _parameterListParser;

    public AnonymousFunctionDeclarationParsingStrategy(IParameterListParser parameterListParser,
        IScopeParser scopeParser)
    {
        _parameterListParser = parameterListParser;
        _scopeParser = scopeParser;
    }

    public Expression Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Function);

        tokenStream.Consume(TokenType.LeftParenthesis);

        var parameters = Array.Empty<Parameter>();

        if (!tokenStream.Match(TokenType.RightParenthesis))
        {
            parameters = _parameterListParser.Parse(tokenStream).ToArray();

            tokenStream.Consume(TokenType.RightParenthesis);
        }

        var scope = _scopeParser.Parse(tokenStream);

        return new AnonymousFunctionDeclarationExpression(parameters, scope);
    }
}
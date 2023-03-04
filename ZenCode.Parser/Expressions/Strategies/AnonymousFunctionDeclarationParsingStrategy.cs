using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class AnonymousFunctionDeclarationParsingStrategy : IPrefixExpressionParsingStrategy
{
    private readonly ITypeParser _typeParser;
    private readonly IStatementParser _statementParser;

    public AnonymousFunctionDeclarationParsingStrategy(ITypeParser typeParser, IStatementParser statementParser)
    {
        _typeParser = typeParser;
        _statementParser = statementParser;
    }

    public Expression Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Function);

        tokenStream.Consume(TokenType.LeftParenthesis);

        var parameters = new ParameterList();

        if (!tokenStream.Match(TokenType.RightParenthesis))
        {
            parameters = _typeParser.ParseParameterList(tokenStream);

            tokenStream.Consume(TokenType.RightParenthesis);
        }

        tokenStream.Consume(TokenType.RightArrow);

        var returnType = _typeParser.ParseType(tokenStream);

        var scope = _statementParser.ParseScope(tokenStream);

        return new AnonymousFunctionDeclarationExpression(returnType, parameters, scope);
    }
}
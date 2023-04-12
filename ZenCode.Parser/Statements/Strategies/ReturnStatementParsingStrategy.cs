using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class ReturnStatementParsingStrategy : IReturnStatementParsingStrategy
{
    public ReturnStatement Parse(IParser parser, ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Return);

        if (tokenStream.Match(TokenType.Semicolon))
        {
            return new ReturnStatement();
        }

        var expression = parser.ParseExpression(tokenStream);

        tokenStream.Consume(TokenType.Semicolon);

        return new ReturnStatement { Expression = expression };
    }
}

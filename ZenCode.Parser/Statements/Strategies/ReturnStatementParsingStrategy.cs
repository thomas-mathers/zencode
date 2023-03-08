using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class ReturnStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IParser _parser;

    public ReturnStatementParsingStrategy(IParser parser)
    {
        _parser = parser;
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Return);
        
        if (tokenStream.Match(TokenType.Semicolon))
        {
            return new ReturnStatement();
        }
        
        var expression = _parser.ParseExpression(tokenStream);
        
        tokenStream.Consume(TokenType.Semicolon);
        
        return new ReturnStatement { Expression = expression };
    }
}
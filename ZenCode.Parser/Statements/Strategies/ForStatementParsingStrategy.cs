using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class ForStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IParser _parser;

    public ForStatementParsingStrategy(IParser parser)
    {
        _parser = parser;
    }
    
    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.For);
        tokenStream.Consume(TokenType.LeftParenthesis);

        var initializer = _parser.ParseStatement(tokenStream);

        tokenStream.Consume(TokenType.Semicolon);

        var condition = _parser.ParseExpression(tokenStream);

        tokenStream.Consume(TokenType.Semicolon);

        var iterator = _parser.ParseStatement(tokenStream);
        
        tokenStream.Consume(TokenType.RightParenthesis);

        var scope = _parser.ParseScope(tokenStream);
        
        return new ForStatement(initializer, condition, iterator, scope);
    }
}
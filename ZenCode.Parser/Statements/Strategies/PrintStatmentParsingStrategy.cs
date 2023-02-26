using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class PrintStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IExpressionParser _expressionParser;

    public PrintStatementParsingStrategy(IExpressionParser expressionParser)
    {
        _expressionParser = expressionParser;
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Print);
        
        var expression = _expressionParser.Parse(tokenStream);
        
        return new PrintStatement(expression);
    }
}
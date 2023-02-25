using ZenCode.Grammar.Statements;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;

namespace ZenCode.Parser.Statements;

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
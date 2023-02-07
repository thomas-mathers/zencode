using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Statements;
using ZenCode.Parser.Parsers.Expressions;

namespace ZenCode.Parser.Parsers.Statements;

public class StatementParser : IStatementParser
{
    private readonly IExpressionParser _expressionParser;
    
    public StatementParser(IExpressionParser expressionParser)
    {
        _expressionParser = expressionParser;
    }
    
    public Statement Parse(ITokenStream tokenStream)
    {
        return _expressionParser.Parse(tokenStream);
    }
}
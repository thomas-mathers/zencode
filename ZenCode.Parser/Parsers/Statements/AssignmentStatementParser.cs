using ZenCode.Lexer;
using ZenCode.Parser.Grammar.Statements;
using ZenCode.Parser.Parsers.Expressions;

namespace ZenCode.Parser.Parsers.Statements;

public class AssignmentStatementParser : IStatementParser
{
    private readonly IExpressionParser _expressionParser;
    
    public AssignmentStatementParser(IExpressionParser expressionParser)
    {
        _expressionParser = expressionParser;
    }
    
    public Statement Parse(ITokenStream tokenStream)
    {
        var identifier = tokenStream.Consume(TokenType.Identifier);
        tokenStream.Consume(TokenType.Assignment);
        var expression = _expressionParser.Parse(tokenStream);
        return new AssignmentStatement(identifier, expression);
    }
}
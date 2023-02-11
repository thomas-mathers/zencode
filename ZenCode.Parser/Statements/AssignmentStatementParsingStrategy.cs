using ZenCode.Grammar.Statements;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;

namespace ZenCode.Parser.Statements;

public class AssignmentStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IExpressionParser _expressionParser;

    public AssignmentStatementParsingStrategy(IExpressionParser expressionParser)
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
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class AssignmentStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IParser _parser;

    public AssignmentStatementParsingStrategy(IParser parser)
    {
        _parser = parser;
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        var variableReferenceExpression = _parser.ParseExpression(tokenStream);
        tokenStream.Consume(TokenType.Assignment);
        var expression = _parser.ParseExpression(tokenStream);
        return new AssignmentStatement(variableReferenceExpression, expression);
    }
}
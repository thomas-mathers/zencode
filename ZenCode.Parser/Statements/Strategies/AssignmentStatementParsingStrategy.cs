using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class AssignmentStatementParsingStrategy : IAssignmentStatementParsingStrategy
{
    public AssignmentStatement Parse(IParser parser, ITokenStream tokenStream)
    {
        ArgumentNullException.ThrowIfNull(parser);
        ArgumentNullException.ThrowIfNull(tokenStream);
        
        var variableReferenceExpression = parser.ParseVariableReferenceExpression(tokenStream);

        tokenStream.Consume(TokenType.Assignment);

        var expression = parser.ParseExpression(tokenStream);

        return new AssignmentStatement(variableReferenceExpression, expression);
    }
}

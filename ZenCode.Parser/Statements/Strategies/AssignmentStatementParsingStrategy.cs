using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies
{
    public class AssignmentStatementParsingStrategy : IAssignmentStatementParsingStrategy
    {
        public AssignmentStatement Parse(IParser parser, ITokenStream tokenStream)
        {
            var variableReferenceExpression = parser.ParseExpression(tokenStream);
            tokenStream.Consume(TokenType.Assignment);
            var expression = parser.ParseExpression(tokenStream);
            return new AssignmentStatement(variableReferenceExpression, expression);
        }
    }
}
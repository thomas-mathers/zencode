using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class ReadStatementParsingStrategy : IReadStatementParsingStrategy
{
    public ReadStatement Parse(IParser parser, ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Read);
        
        var variableReferenceExpression = parser.ParseVariableReferenceExpression(tokenStream);
        
        return new ReadStatement(variableReferenceExpression);
    }
}
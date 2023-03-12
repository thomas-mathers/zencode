using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class ForStatementParsingStrategy : IForStatementParsingStrategy
{
    public ForStatement Parse(IParser parser, ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.For);
        tokenStream.Consume(TokenType.LeftParenthesis);

        var initializer = parser.ParseVariableDeclarationStatement(tokenStream);

        tokenStream.Consume(TokenType.Semicolon);

        var condition = parser.ParseExpression(tokenStream);

        tokenStream.Consume(TokenType.Semicolon);

        var iterator = parser.ParseAssignmentStatement(tokenStream);
        
        tokenStream.Consume(TokenType.RightParenthesis);

        var scope = parser.ParseScope(tokenStream);
        
        return new ForStatement(initializer, condition, iterator, scope);
    }
}
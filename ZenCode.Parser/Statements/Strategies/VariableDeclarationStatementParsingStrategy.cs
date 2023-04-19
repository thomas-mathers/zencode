using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class VariableDeclarationStatementParsingStrategy : IVariableDeclarationStatementParsingStrategy
{
    public VariableDeclarationStatement Parse(IParser parser, ITokenStream tokenStream)
    {
        ArgumentNullException.ThrowIfNull(parser);
        ArgumentNullException.ThrowIfNull(tokenStream);
        
        tokenStream.Consume(TokenType.Var);

        var identifier = tokenStream.Consume(TokenType.Identifier);

        tokenStream.Consume(TokenType.Assignment);

        var initializer = parser.ParseExpression(tokenStream);

        return new VariableDeclarationStatement
        {
            VariableName = identifier,
            Value = initializer
        };
    }
}

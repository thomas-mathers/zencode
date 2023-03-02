using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class VariableDeclarationStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IParser _parser;

    public VariableDeclarationStatementParsingStrategy(IParser parser)
    {
        _parser = parser;
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Var);

        var identifier = tokenStream.Consume(TokenType.Identifier);

        tokenStream.Consume(TokenType.Assignment);

        var expression = _parser.ParseExpression(tokenStream);

        return new VariableDeclarationStatement(identifier, expression);
    }
}
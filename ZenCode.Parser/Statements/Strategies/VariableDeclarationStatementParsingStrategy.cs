using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class VariableDeclarationStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IExpressionParser _expressionParser;

    public VariableDeclarationStatementParsingStrategy(IExpressionParser expressionParser)
    {
        _expressionParser = expressionParser;
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Var);

        var identifier = tokenStream.Consume(TokenType.Identifier);

        tokenStream.Consume(TokenType.Assignment);

        var expression = _expressionParser.ParseExpression(tokenStream);

        return new VariableDeclarationStatement(identifier, expression);
    }
}
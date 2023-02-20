using ZenCode.Grammar.Statements;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;

namespace ZenCode.Parser.Statements;

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
        
        var expression = _expressionParser.Parse(tokenStream);
        
        return new VariableDeclarationStatement(identifier, expression);
    }
}
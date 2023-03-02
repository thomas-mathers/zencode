using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class FunctionDeclarationStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IParser _parser;

    public FunctionDeclarationStatementParsingStrategy(IParser parser)
    {
        _parser = parser;
    }
    
    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Function);
        tokenStream.Consume(TokenType.Identifier);
        tokenStream.Consume(TokenType.LeftParenthesis);

        var parameters = _parser.ParseParameterList(tokenStream);

        tokenStream.Consume(TokenType.RightParenthesis);
        tokenStream.Consume(TokenType.RightArrow);

        var returnType = _parser.ParseType(tokenStream);
        
        var scope = _parser.ParseScope(tokenStream);

        return new FunctionDeclarationStatement(returnType, parameters, scope);
    }
}
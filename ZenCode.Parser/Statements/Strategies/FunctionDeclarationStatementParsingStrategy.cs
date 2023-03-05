using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class FunctionDeclarationStatementParsingStrategy : IStatementParsingStrategy
{
    private readonly IStatementParser _statementParser;
    private readonly ITypeParser _typeParser;

    public FunctionDeclarationStatementParsingStrategy(ITypeParser typeParser, IStatementParser statementParser)
    {
        _typeParser = typeParser;
        _statementParser = statementParser;
    }

    public Statement Parse(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Function);
        tokenStream.Consume(TokenType.Identifier);
        tokenStream.Consume(TokenType.LeftParenthesis);
        
        var parameters = new ParameterList();

        if (!tokenStream.Match(TokenType.RightParenthesis))
        {
            parameters = _typeParser.ParseParameterList(tokenStream);   
            
            tokenStream.Consume(TokenType.RightParenthesis);
        }

        tokenStream.Consume(TokenType.RightArrow);

        var returnType = _typeParser.ParseType(tokenStream);

        var scope = _statementParser.ParseScope(tokenStream);

        return new FunctionDeclarationStatement(returnType, parameters, scope);
    }
}
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class FunctionDeclarationStatementParsingStrategy : IFunctionDeclarationStatementParsingStrategy
{
    public FunctionDeclarationStatement Parse(IParser parser, ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Function);
        tokenStream.Consume(TokenType.Identifier);
        tokenStream.Consume(TokenType.LeftParenthesis);
        
        var parameters = tokenStream.Match(TokenType.RightParenthesis) 
            ? new ParameterList()
            : parser.ParseParameterList(tokenStream);

        tokenStream.Consume(TokenType.RightParenthesis);

        tokenStream.Consume(TokenType.RightArrow);

        var returnType = parser.ParseType(tokenStream);

        var scope = parser.ParseScope(tokenStream);

        return new FunctionDeclarationStatement(returnType, parameters, scope);
    }
}
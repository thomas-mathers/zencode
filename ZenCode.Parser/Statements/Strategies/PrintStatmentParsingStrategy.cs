using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements.Strategies;

public class PrintStatementParsingStrategy
{
    public PrintStatement Parse(IParser parser, ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.Print);

        var expression = parser.ParseExpression(tokenStream);

        return new PrintStatement(expression);
    }
}
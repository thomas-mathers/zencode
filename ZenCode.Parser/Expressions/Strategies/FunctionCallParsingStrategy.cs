using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions.Strategies;

public class FunctionCallParsingStrategy : IInfixExpressionParsingStrategy
{
    private readonly IParser _parser;

    public FunctionCallParsingStrategy(IParser parser, int precedence)
    {
        _parser = parser;
        Precedence = precedence;
    }

    public int Precedence { get; }

    public Expression Parse(ITokenStream tokenStream, Expression lOperand)
    {
        tokenStream.Consume(TokenType.LeftParenthesis);

        var arguments = tokenStream.Match(TokenType.RightParenthesis)
            ? new ExpressionList()
            : _parser.ParseExpressionList(tokenStream);

        tokenStream.Consume(TokenType.RightParenthesis);

        return new FunctionCallExpression(lOperand) { Arguments = arguments };
    }
}
using ZenCode.Grammar.Expressions;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;

namespace ZenCode.Parser.Expressions;

public class ExpressionParser : IExpressionParser
{
    public IPrefixExpressionParsingContext PrefixExpressionParsingContext { get; init; }
    public IInfixExpressionParsingContext InfixExpressionParsingContext { get; init; }

    public ExpressionParser()
    {
        PrefixExpressionParsingContext = new PrefixExpressionParsingContext
        {
            Strategies = new Dictionary<TokenType, IPrefixExpressionParsingStrategy>
            {
                [TokenType.Boolean] = new ConstantParsingStrategy(),
                [TokenType.Integer] = new ConstantParsingStrategy(),
                [TokenType.Float] = new ConstantParsingStrategy(),
                [TokenType.String] = new ConstantParsingStrategy(),
                [TokenType.Identifier] = new VariableReferenceParsingStrategy(this),
                [TokenType.Subtraction] = new UnaryExpressionParsingStrategy(this),
                [TokenType.Not] = new UnaryExpressionParsingStrategy(this),
                [TokenType.LeftParenthesis] = new ParenthesizedExpressionParsingStrategy(this)
            }
        };

        InfixExpressionParsingContext = new InfixExpressionParsingContext
        {
            Strategies = new Dictionary<TokenType, IInfixExpressionParsingStrategy>
            {
                [TokenType.Addition] = new BinaryExpressionParsingStrategy(this, 4),
                [TokenType.Subtraction] = new BinaryExpressionParsingStrategy(this, 4),
                [TokenType.Multiplication] = new BinaryExpressionParsingStrategy(this, 5),
                [TokenType.Division] = new BinaryExpressionParsingStrategy(this, 5),
                [TokenType.Modulus] = new BinaryExpressionParsingStrategy(this, 5),
                [TokenType.Exponentiation] = new BinaryExpressionParsingStrategy(this, 6, true),
                [TokenType.LessThan] = new BinaryExpressionParsingStrategy(this, 3),
                [TokenType.LessThanOrEqual] = new BinaryExpressionParsingStrategy(this, 3),
                [TokenType.Equals] = new BinaryExpressionParsingStrategy(this, 3),
                [TokenType.NotEquals] = new BinaryExpressionParsingStrategy(this, 3),
                [TokenType.GreaterThan] = new BinaryExpressionParsingStrategy(this, 3),
                [TokenType.GreaterThanOrEqual] = new BinaryExpressionParsingStrategy(this, 3),
                [TokenType.And] = new BinaryExpressionParsingStrategy(this, 2),
                [TokenType.Or] = new BinaryExpressionParsingStrategy(this, 1),
                [TokenType.LeftParenthesis] = new FunctionCallParsingStrategy(this, 7)
            }
        };
    }

    public Expression Parse(ITokenStream tokenStream, int precedence = 0)
    {
        var lExpression = PrefixExpressionParsingContext.Parse(tokenStream);

        while (precedence < InfixExpressionParsingContext.GetPrecedence(tokenStream))
        {
            lExpression = InfixExpressionParsingContext.Parse(tokenStream, lExpression);
        }

        return lExpression;
    }
}
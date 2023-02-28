using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions.Helpers;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Expressions.Strategies;

namespace ZenCode.Parser.Expressions;

public class ExpressionParser : BaseExpressionParser
{
    public ExpressionParser(IArgumentListParser argumentListParser)
    {
        PrefixStrategies = new Dictionary<TokenType, IPrefixExpressionParsingStrategy>
        {
            [TokenType.BooleanLiteral] = new ConstantParsingStrategy(),
            [TokenType.IntegerLiteral] = new ConstantParsingStrategy(),
            [TokenType.FloatLiteral] = new ConstantParsingStrategy(),
            [TokenType.StringLiteral] = new ConstantParsingStrategy(),
            [TokenType.Identifier] = new VariableReferenceParsingStrategy(this, argumentListParser),
            [TokenType.Subtraction] = new UnaryExpressionParsingStrategy(this),
            [TokenType.Not] = new UnaryExpressionParsingStrategy(this),
            [TokenType.LeftParenthesis] = new ParenthesisParsingStrategy(this)
        };

        InfixStrategies = new Dictionary<TokenType, IInfixExpressionParsingStrategy>
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
            [TokenType.LeftParenthesis] = new FunctionCallParsingStrategy(this, argumentListParser, 7)
        };
    }
}
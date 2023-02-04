using ZenCode.Lexer;
using ZenCode.Parser.Parselets.Expressions.Infix;
using ZenCode.Parser.Parselets.Expressions.Prefix;

namespace ZenCode.Parser;

public class Parser : BaseParser
{
    private static readonly IReadOnlyDictionary<TokenType, IPrefixExpressionParser> PrefixExpressionParsers = new Dictionary<TokenType, IPrefixExpressionParser>()
    {
        [TokenType.Boolean] = new ConstantParser(),
        [TokenType.Integer] = new ConstantParser(),
        [TokenType.Float] = new ConstantParser(),
        [TokenType.Identifier] = new IdentifierParser(),
        [TokenType.Not] = new UnaryExpressionParser()
    };
    
    private static readonly IReadOnlyDictionary<TokenType, IInfixExpressionParser> InfixExpressionParsers = new Dictionary<TokenType, IInfixExpressionParser>()
    {
        [TokenType.Addition] = new BinaryExpressionParser(),
        [TokenType.Subtraction] = new BinaryExpressionParser(),
        [TokenType.Multiplication] = new BinaryExpressionParser(),
        [TokenType.Division] = new BinaryExpressionParser(),
        [TokenType.Modulus] = new BinaryExpressionParser(),
        [TokenType.Exponentiation] = new BinaryExpressionParser(),
        [TokenType.LessThan] = new BinaryExpressionParser(),
        [TokenType.LessThanOrEqual] = new BinaryExpressionParser(),
        [TokenType.Equals] = new BinaryExpressionParser(),
        [TokenType.NotEquals] = new BinaryExpressionParser(),
        [TokenType.GreaterThan] = new BinaryExpressionParser(),
        [TokenType.GreaterThanOrEqual] = new BinaryExpressionParser(),
        [TokenType.And] = new BinaryExpressionParser(),
        [TokenType.Or] = new BinaryExpressionParser(),
        [TokenType.LeftParenthesis] = new FunctionCallParser()
    };
    
    public Parser(ITokenizer tokenizer) : base(tokenizer, PrefixExpressionParsers, InfixExpressionParsers)
    {
    }
}
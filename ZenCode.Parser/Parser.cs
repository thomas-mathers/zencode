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
        [TokenType.Addition] = new BinaryExpressionParser(4),
        [TokenType.Subtraction] = new BinaryExpressionParser(4),
        [TokenType.Multiplication] = new BinaryExpressionParser(5),
        [TokenType.Division] = new BinaryExpressionParser(5),
        [TokenType.Modulus] = new BinaryExpressionParser(5),
        [TokenType.Exponentiation] = new BinaryExpressionParser(6),
        [TokenType.LessThan] = new BinaryExpressionParser(3),
        [TokenType.LessThanOrEqual] = new BinaryExpressionParser(3),
        [TokenType.Equals] = new BinaryExpressionParser(3),
        [TokenType.NotEquals] = new BinaryExpressionParser(3),
        [TokenType.GreaterThan] = new BinaryExpressionParser(3),
        [TokenType.GreaterThanOrEqual] = new BinaryExpressionParser(3),
        [TokenType.And] = new BinaryExpressionParser(2),
        [TokenType.Or] = new BinaryExpressionParser(1),
        [TokenType.LeftParenthesis] = new FunctionCallParser()
    };
    
    public Parser(ITokenizer tokenizer) : base(tokenizer, PrefixExpressionParsers, InfixExpressionParsers)
    {
    }
}
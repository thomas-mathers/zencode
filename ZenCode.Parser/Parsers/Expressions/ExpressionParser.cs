using ZenCode.Lexer;
using ZenCode.Parser.Exceptions;
using ZenCode.Parser.Grammar.Expressions;
using ZenCode.Parser.Parsers.Expressions.Infix;
using ZenCode.Parser.Parsers.Expressions.Prefix;

namespace ZenCode.Parser.Parsers.Expressions;

public class ExpressionParser : IExpressionParser
{
    private static readonly IReadOnlyDictionary<TokenType, IPrefixExpressionParser> PrefixExpressionParsers = new Dictionary<TokenType, IPrefixExpressionParser>()
    {
        [TokenType.Boolean] = new ConstantParser(),
        [TokenType.Integer] = new ConstantParser(),
        [TokenType.Float] = new ConstantParser(),
        [TokenType.Identifier] = new VariableReferenceParser(),
        [TokenType.Not] = new UnaryExpressionParser(),
        [TokenType.LeftParenthesis] = new ParenthesizedExpressionParser()
    };
    
    private static readonly IReadOnlyDictionary<TokenType, IInfixExpressionParser> InfixExpressionParsers = new Dictionary<TokenType, IInfixExpressionParser>()
    {
        [TokenType.Addition] = new BinaryExpressionParser(4),
        [TokenType.Subtraction] = new BinaryExpressionParser(4),
        [TokenType.Multiplication] = new BinaryExpressionParser(5),
        [TokenType.Division] = new BinaryExpressionParser(5),
        [TokenType.Modulus] = new BinaryExpressionParser(5),
        [TokenType.Exponentiation] = new BinaryExpressionParser(6, true),
        [TokenType.LessThan] = new BinaryExpressionParser(3),
        [TokenType.LessThanOrEqual] = new BinaryExpressionParser(3),
        [TokenType.Equals] = new BinaryExpressionParser(3),
        [TokenType.NotEquals] = new BinaryExpressionParser(3),
        [TokenType.GreaterThan] = new BinaryExpressionParser(3),
        [TokenType.GreaterThanOrEqual] = new BinaryExpressionParser(3),
        [TokenType.And] = new BinaryExpressionParser(2),
        [TokenType.Or] = new BinaryExpressionParser(1),
        [TokenType.LeftParenthesis] = new FunctionCallParser(7)
    };

    public Expression Parse(ITokenStream tokenStream, int precedence = 0)
    {
        var token = tokenStream.Consume();

        if (!PrefixExpressionParsers.TryGetValue(token.Type, out var prefixExpressionParser))
        {
            throw new ParseException();   
        }

        var lExpression = prefixExpressionParser.Parse(this, tokenStream, token);

        while (precedence < GetPrecedence(tokenStream))
        {
            var op = tokenStream.Consume();

            if (!InfixExpressionParsers.TryGetValue(op.Type, out var infixExpressionParser))
            {
                throw new ParseException();
            }

            lExpression = infixExpressionParser.Parse(this, tokenStream, lExpression, op);
        }
        
        return lExpression;
    }
    
    private static int GetPrecedence(ITokenStream tokenStream)
    {
        var currentToken = tokenStream?.Peek(0);

        if (currentToken == null)
        {
            return 0;
        }

        return !InfixExpressionParsers.TryGetValue(currentToken.Type, out var parser) 
            ? 0 
            : parser.GetPrecedence();
    }
}
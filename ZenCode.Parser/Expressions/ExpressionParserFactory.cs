using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Expressions.Strategies;

namespace ZenCode.Parser.Expressions;

public class ExpressionParserFactory : IExpressionParserFactory
{
    private readonly ITypeParser _typeParser;
    
    public ExpressionParserFactory(ITypeParserFactory typeParserFactory)
    {
        _typeParser = typeParserFactory.Create();
    }
    
    public IExpressionParser Create()
    {
        var expressionParser = new ExpressionParser();

        expressionParser.SetPrefixExpressionParsingStrategy(TokenType.BooleanLiteral,
            new ConstantParsingStrategy());
        expressionParser.SetPrefixExpressionParsingStrategy(TokenType.IntegerLiteral,
            new ConstantParsingStrategy());
        expressionParser.SetPrefixExpressionParsingStrategy(TokenType.FloatLiteral,
            new ConstantParsingStrategy());
        expressionParser.SetPrefixExpressionParsingStrategy(TokenType.StringLiteral,
            new ConstantParsingStrategy());
        expressionParser.SetPrefixExpressionParsingStrategy(TokenType.Identifier,
            new VariableReferenceParsingStrategy(expressionParser));
        expressionParser.SetPrefixExpressionParsingStrategy(TokenType.Minus,
            new UnaryExpressionParsingStrategy(expressionParser));
        expressionParser.SetPrefixExpressionParsingStrategy(TokenType.Not,
            new UnaryExpressionParsingStrategy(expressionParser));
        expressionParser.SetPrefixExpressionParsingStrategy(TokenType.LeftParenthesis,
            new ParenthesisParsingStrategy(expressionParser));
        expressionParser.SetPrefixExpressionParsingStrategy(TokenType.New,
            new NewExpressionParsingStrategy(_typeParser, expressionParser));
        
        expressionParser.SetInfixExpressionParsingStrategy(TokenType.Addition,
            new BinaryExpressionParsingStrategy(expressionParser, 4));
        expressionParser.SetInfixExpressionParsingStrategy(TokenType.Minus,
            new BinaryExpressionParsingStrategy(expressionParser, 4));
        expressionParser.SetInfixExpressionParsingStrategy(TokenType.Multiplication,
            new BinaryExpressionParsingStrategy(expressionParser, 5));
        expressionParser.SetInfixExpressionParsingStrategy(TokenType.Division,
            new BinaryExpressionParsingStrategy(expressionParser, 5));
        expressionParser.SetInfixExpressionParsingStrategy(TokenType.Modulus,
            new BinaryExpressionParsingStrategy(expressionParser, 5));
        expressionParser.SetInfixExpressionParsingStrategy(TokenType.Exponentiation,
            new BinaryExpressionParsingStrategy(expressionParser, 6, true));
        expressionParser.SetInfixExpressionParsingStrategy(TokenType.LessThan,
            new BinaryExpressionParsingStrategy(expressionParser, 3));
        expressionParser.SetInfixExpressionParsingStrategy(TokenType.LessThanOrEqual,
            new BinaryExpressionParsingStrategy(expressionParser, 3));
        expressionParser.SetInfixExpressionParsingStrategy(TokenType.Equals,
            new BinaryExpressionParsingStrategy(expressionParser, 3));
        expressionParser.SetInfixExpressionParsingStrategy(TokenType.NotEquals,
            new BinaryExpressionParsingStrategy(expressionParser, 3));
        expressionParser.SetInfixExpressionParsingStrategy(TokenType.GreaterThan,
            new BinaryExpressionParsingStrategy(expressionParser, 3));
        expressionParser.SetInfixExpressionParsingStrategy(TokenType.GreaterThanOrEqual,
            new BinaryExpressionParsingStrategy(expressionParser, 3));
        expressionParser.SetInfixExpressionParsingStrategy(TokenType.And,
            new BinaryExpressionParsingStrategy(expressionParser, 2));
        expressionParser.SetInfixExpressionParsingStrategy(TokenType.Or,
            new BinaryExpressionParsingStrategy(expressionParser, 1));
        expressionParser.SetInfixExpressionParsingStrategy(TokenType.LeftParenthesis,
            new FunctionCallParsingStrategy(expressionParser, 7));

        return expressionParser;
    }
}
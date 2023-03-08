using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Statements.Strategies;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser;

public class ParserFactory : IParserFactory
{
    public IParser Create()
    {
        var parser = new Parser();

        parser.SetPrefixExpressionParsingStrategy(TokenType.BooleanLiteral,
            new BooleanLiteralParsingStrategy());
        parser.SetPrefixExpressionParsingStrategy(TokenType.IntegerLiteral,
            new IntegerLiteralParsingStrategy());
        parser.SetPrefixExpressionParsingStrategy(TokenType.FloatLiteral,
            new FloatLiteralParsingStrategy());
        parser.SetPrefixExpressionParsingStrategy(TokenType.StringLiteral,
            new StringLiteralParsingStrategy());
        parser.SetPrefixExpressionParsingStrategy(TokenType.Identifier,
            new VariableReferenceParsingStrategy(parser));
        parser.SetPrefixExpressionParsingStrategy(TokenType.Minus,
            new UnaryExpressionParsingStrategy(parser));
        parser.SetPrefixExpressionParsingStrategy(TokenType.Not,
            new UnaryExpressionParsingStrategy(parser));
        parser.SetPrefixExpressionParsingStrategy(TokenType.LeftParenthesis,
            new ParenthesisParsingStrategy(parser));
        parser.SetPrefixExpressionParsingStrategy(TokenType.New,
            new NewExpressionParsingStrategy(parser, parser));
        
        parser.SetInfixExpressionParsingStrategy(TokenType.Plus,
            new BinaryExpressionParsingStrategy(parser, 4));
        parser.SetInfixExpressionParsingStrategy(TokenType.Minus,
            new BinaryExpressionParsingStrategy(parser, 4));
        parser.SetInfixExpressionParsingStrategy(TokenType.Multiplication,
            new BinaryExpressionParsingStrategy(parser, 5));
        parser.SetInfixExpressionParsingStrategy(TokenType.Division,
            new BinaryExpressionParsingStrategy(parser, 5));
        parser.SetInfixExpressionParsingStrategy(TokenType.Modulus,
            new BinaryExpressionParsingStrategy(parser, 5));
        parser.SetInfixExpressionParsingStrategy(TokenType.Exponentiation,
            new BinaryExpressionParsingStrategy(parser, 6, true));
        parser.SetInfixExpressionParsingStrategy(TokenType.LessThan,
            new BinaryExpressionParsingStrategy(parser, 3));
        parser.SetInfixExpressionParsingStrategy(TokenType.LessThanOrEqual,
            new BinaryExpressionParsingStrategy(parser, 3));
        parser.SetInfixExpressionParsingStrategy(TokenType.Equals,
            new BinaryExpressionParsingStrategy(parser, 3));
        parser.SetInfixExpressionParsingStrategy(TokenType.NotEquals,
            new BinaryExpressionParsingStrategy(parser, 3));
        parser.SetInfixExpressionParsingStrategy(TokenType.GreaterThan,
            new BinaryExpressionParsingStrategy(parser, 3));
        parser.SetInfixExpressionParsingStrategy(TokenType.GreaterThanOrEqual,
            new BinaryExpressionParsingStrategy(parser, 3));
        parser.SetInfixExpressionParsingStrategy(TokenType.And,
            new BinaryExpressionParsingStrategy(parser, 2));
        parser.SetInfixExpressionParsingStrategy(TokenType.Or,
            new BinaryExpressionParsingStrategy(parser, 1));
        parser.SetInfixExpressionParsingStrategy(TokenType.LeftParenthesis,
            new FunctionCallParsingStrategy(parser, 7));
        
        parser.SetStatementParsingStrategy(TokenType.If,
            new IfStatementParsingStrategy(parser, parser));
        parser.SetStatementParsingStrategy(TokenType.While,
            new WhileStatementParsingStrategy(parser, parser));
        parser.SetStatementParsingStrategy(TokenType.Function,
            new FunctionDeclarationStatementParsingStrategy(parser, parser));
        parser.SetStatementParsingStrategy(TokenType.Identifier,
            new AssignmentStatementParsingStrategy(parser));
        parser.SetStatementParsingStrategy(TokenType.Print,
            new PrintStatementParsingStrategy(parser));
        parser.SetStatementParsingStrategy(TokenType.Var,
            new VariableDeclarationStatementParsingStrategy(parser));
        parser.SetStatementParsingStrategy(TokenType.Return,
            new ReturnStatementParsingStrategy(parser));
        parser.SetStatementParsingStrategy(TokenType.For,
            new ForStatementParsingStrategy(parser, parser));
        
        parser.SetPrefixTypeParsingStrategy(TokenType.Void, new VoidTypeParsingStrategy());
        parser.SetPrefixTypeParsingStrategy(TokenType.Boolean, new BooleanTypeParsingStrategy());
        parser.SetPrefixTypeParsingStrategy(TokenType.Integer, new IntegerTypeParsingStrategy());
        parser.SetPrefixTypeParsingStrategy(TokenType.Float, new FloatTypeParsingStrategy());
        parser.SetPrefixTypeParsingStrategy(TokenType.String, new StringTypeParsingStrategy());

        return parser;
    }
}
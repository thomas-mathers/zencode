using ZenCode.Lexer;
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Statements.Strategies;
using ZenCode.Parser.Types.Strategies;

namespace ZenCode.Parser;

public class ParserFactory
{
    private readonly ITokenizer _tokenizer;

    public ParserFactory(TokenizerFactory tokenizerFactory)
    {
        _tokenizer = tokenizerFactory.Create();
    }
    
    public IParser Create()
    {
        var parser = new Parser(_tokenizer);

        parser.SetPrefixTypeParsingStrategy(TokenType.Void, new VoidTypeParsingStrategy());
        parser.SetPrefixTypeParsingStrategy(TokenType.Boolean, new BooleanTypeParsingStrategy());
        parser.SetPrefixTypeParsingStrategy(TokenType.Integer, new IntegerTypeParsingStrategy());
        parser.SetPrefixTypeParsingStrategy(TokenType.Float, new FloatTypeParsingStrategy());
        parser.SetPrefixTypeParsingStrategy(TokenType.String, new StringTypeParsingStrategy());

        parser.SetInfixTypeParsingStrategy(TokenType.LeftBracket, new ArrayTypeParsingStrategy(1));

        parser.SetStatementParsingStrategy(TokenType.Identifier,
            new AssignmentStatementParsingStrategy(parser));
        parser.SetStatementParsingStrategy(TokenType.If,
            new IfStatementParsingStrategy(parser));
        parser.SetStatementParsingStrategy(TokenType.Print,
            new PrintStatementParsingStrategy(parser));
        parser.SetStatementParsingStrategy(TokenType.Var,
            new VariableDeclarationStatementParsingStrategy(parser));
        parser.SetStatementParsingStrategy(TokenType.While,
            new WhileStatementParsingStrategy(parser));
        parser.SetStatementParsingStrategy(TokenType.Return,
            new ReturnStatementParsingStrategy(parser));
        parser.SetStatementParsingStrategy(TokenType.Function,
            new FunctionDeclarationStatementParsingStrategy(parser));

        parser.SetPrefixExpressionParsingStrategy(TokenType.BooleanLiteral,
            new ConstantParsingStrategy());
        parser.SetPrefixExpressionParsingStrategy(TokenType.IntegerLiteral,
            new ConstantParsingStrategy());
        parser.SetPrefixExpressionParsingStrategy(TokenType.FloatLiteral,
            new ConstantParsingStrategy());
        parser.SetPrefixExpressionParsingStrategy(TokenType.StringLiteral,
            new ConstantParsingStrategy());
        parser.SetPrefixExpressionParsingStrategy(TokenType.Identifier,
            new VariableReferenceParsingStrategy(parser));
        parser.SetPrefixExpressionParsingStrategy(TokenType.Subtraction,
            new UnaryExpressionParsingStrategy(parser));
        parser.SetPrefixExpressionParsingStrategy(TokenType.Not,
            new UnaryExpressionParsingStrategy(parser));
        parser.SetPrefixExpressionParsingStrategy(TokenType.LeftParenthesis,
            new ParenthesisParsingStrategy(parser));
        parser.SetPrefixExpressionParsingStrategy(TokenType.Function,
            new AnonymousFunctionDeclarationParsingStrategy(parser));

        parser.SetInfixExpressionParsingStrategy(TokenType.Addition,
            new BinaryExpressionParsingStrategy(parser, 4));
        parser.SetInfixExpressionParsingStrategy(TokenType.Subtraction,
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

        return parser;
    }
}
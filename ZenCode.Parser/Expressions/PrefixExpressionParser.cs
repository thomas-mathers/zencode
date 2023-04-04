using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions;

public class PrefixExpressionParser : IPrefixExpressionParser
{
    private readonly IAnonymousFunctionDeclarationParsingStrategy _anonymousFunctionDeclarationParsingStrategy;
    private readonly ILiteralParsingStrategy _literalParsingStrategy;
    private readonly INewExpressionParsingStrategy _newExpressionParsingStrategy;
    private readonly IParenthesisParsingStrategy _parenthesisParsingStrategy;
    private readonly IUnaryExpressionParsingStrategy _unaryExpressionParsingStrategy;
    private readonly IVariableReferenceParsingStrategy _variableReferenceParsingStrategy;

    public PrefixExpressionParser(
        IAnonymousFunctionDeclarationParsingStrategy anonymousFunctionDeclarationParsingStrategy,
        ILiteralParsingStrategy literalParsingStrategy,
        INewExpressionParsingStrategy newExpressionParsingStrategy,
        IParenthesisParsingStrategy parenthesisParsingStrategy,
        IUnaryExpressionParsingStrategy unaryExpressionParsingStrategy,
        IVariableReferenceParsingStrategy variableReferenceParsingStrategy)
    {
        _anonymousFunctionDeclarationParsingStrategy = anonymousFunctionDeclarationParsingStrategy;
        _newExpressionParsingStrategy = newExpressionParsingStrategy;
        _parenthesisParsingStrategy = parenthesisParsingStrategy;
        _literalParsingStrategy = literalParsingStrategy;
        _unaryExpressionParsingStrategy = unaryExpressionParsingStrategy;
        _variableReferenceParsingStrategy = variableReferenceParsingStrategy;
    }

    public Expression ParsePrefixExpression(IParser parser, ITokenStream tokenStream)
    {
        return tokenStream.Current.Type switch
        {
            TokenType.Function => ParseAnonymousFunctionDeclarationExpression(parser, tokenStream),
            TokenType.BooleanLiteral => ParseBooleanLiteralExpression(tokenStream),
            TokenType.IntegerLiteral => ParseIntegerLiteralExpression(tokenStream),
            TokenType.FloatLiteral => ParseFloatLiteralExpression(tokenStream),
            TokenType.StringLiteral => ParseStringLiteralExpression(tokenStream),
            TokenType.New => ParseNewExpression(parser, tokenStream),
            TokenType.LeftParenthesis => ParseParenthesisExpression(parser, tokenStream),
            TokenType.Minus => ParseUnaryExpression(parser, tokenStream, TokenType.Minus),
            TokenType.Not => ParseUnaryExpression(parser, tokenStream, TokenType.Not),
            TokenType.Identifier => ParseVariableReferenceExpression(parser, tokenStream),
            _ => throw new UnexpectedTokenException(tokenStream.Current.Type)
        };
    }

    public VariableReferenceExpression ParseVariableReferenceExpression(IParser parser, ITokenStream tokenStream)
    {
        return _variableReferenceParsingStrategy.Parse(parser, tokenStream);
    }

    private AnonymousFunctionDeclarationExpression ParseAnonymousFunctionDeclarationExpression(IParser parser,
        ITokenStream tokenStream)
    {
        return _anonymousFunctionDeclarationParsingStrategy.Parse(parser, tokenStream);
    }

    private LiteralExpression ParseBooleanLiteralExpression(ITokenStream tokenStream)
    {
        return _literalParsingStrategy.Parse(tokenStream, TokenType.BooleanLiteral);
    }

    private LiteralExpression ParseIntegerLiteralExpression(ITokenStream tokenStream)
    {
        return _literalParsingStrategy.Parse(tokenStream, TokenType.IntegerLiteral);
    }

    private LiteralExpression ParseFloatLiteralExpression(ITokenStream tokenStream)
    {
        return _literalParsingStrategy.Parse(tokenStream, TokenType.FloatLiteral);
    }

    private LiteralExpression ParseStringLiteralExpression(ITokenStream tokenStream)
    {
        return _literalParsingStrategy.Parse(tokenStream, TokenType.StringLiteral);
    }

    private NewArrayExpression ParseNewExpression(IParser parser, ITokenStream tokenStream)
    {
        return _newExpressionParsingStrategy.Parse(parser, tokenStream);
    }

    private Expression ParseParenthesisExpression(IParser parser, ITokenStream tokenStream)
    {
        return _parenthesisParsingStrategy.Parse(parser, tokenStream);
    }

    private UnaryExpression ParseUnaryExpression(IParser parser, ITokenStream tokenStream, TokenType tokenType)
    {
        return _unaryExpressionParsingStrategy.Parse(parser, tokenStream, tokenType);
    }
}
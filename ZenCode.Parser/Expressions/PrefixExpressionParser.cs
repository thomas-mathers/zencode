using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Expressions.Strategies;
using ZenCode.Parser.Model.Grammar.Expressions;

namespace ZenCode.Parser.Expressions;

public class PrefixExpressionParser : IPrefixExpressionParser
{
    private readonly AnonymousFunctionDeclarationParsingStrategy _anonymousFunctionDeclarationParsingStrategy;
    private readonly BooleanLiteralParsingStrategy _booleanLiteralParsingStrategy;
    private readonly FloatLiteralParsingStrategy _floatLiteralParsingStrategy;
    private readonly IntegerLiteralParsingStrategy _integerLiteralParsingStrategy;
    private readonly NewExpressionParsingStrategy _newExpressionParsingStrategy;
    private readonly ParenthesisParsingStrategy _parenthesisParsingStrategy;
    private readonly StringLiteralParsingStrategy _stringLiteralParsingStrategy;
    private readonly UnaryExpressionParsingStrategy _unaryExpressionParsingStrategy;
    private readonly VariableReferenceParsingStrategy _variableReferenceParsingStrategy;

    public PrefixExpressionParser(
        AnonymousFunctionDeclarationParsingStrategy anonymousFunctionDeclarationParsingStrategy,
        BooleanLiteralParsingStrategy booleanLiteralParsingStrategy,
        FloatLiteralParsingStrategy floatLiteralParsingStrategy,
        IntegerLiteralParsingStrategy integerLiteralParsingStrategy,
        NewExpressionParsingStrategy newExpressionParsingStrategy,
        ParenthesisParsingStrategy parenthesisParsingStrategy,
        StringLiteralParsingStrategy stringLiteralParsingStrategy,
        UnaryExpressionParsingStrategy unaryExpressionParsingStrategy,
        VariableReferenceParsingStrategy variableReferenceParsingStrategy)
    {
        _anonymousFunctionDeclarationParsingStrategy = anonymousFunctionDeclarationParsingStrategy;
        _booleanLiteralParsingStrategy = booleanLiteralParsingStrategy;
        _floatLiteralParsingStrategy = floatLiteralParsingStrategy;
        _integerLiteralParsingStrategy = integerLiteralParsingStrategy;
        _newExpressionParsingStrategy = newExpressionParsingStrategy;
        _parenthesisParsingStrategy = parenthesisParsingStrategy;
        _stringLiteralParsingStrategy = stringLiteralParsingStrategy;
        _unaryExpressionParsingStrategy = unaryExpressionParsingStrategy;
        _variableReferenceParsingStrategy = variableReferenceParsingStrategy;
    }
    
    public Expression ParsePrefixExpression(IParser parser, ITokenStream tokenStream)
    {
        return tokenStream.Current.Type switch
        {
            TokenType.Function => ParseAnonymousFunctionDeclarationExpression(parser, tokenStream),
            TokenType.BooleanLiteral => ParseBooleanLiteralExpression(tokenStream),
            TokenType.FloatLiteral => ParseFloatLiteralExpression(tokenStream),
            TokenType.IntegerLiteral => ParseIntegerLiteralExpression(tokenStream),
            TokenType.New => ParseNewExpression(parser, tokenStream),
            TokenType.LeftParenthesis => ParseParenthesisExpression(parser, tokenStream),
            TokenType.StringLiteral => ParseStringLiteralExpression(tokenStream),
            TokenType.Minus => ParseUnaryExpression(parser, tokenStream),
            TokenType.Not => ParseUnaryExpression(parser, tokenStream),
            TokenType.Identifier => ParseVariableReferenceExpression(parser, tokenStream),
            _ => throw new UnexpectedTokenException()
        };
    }

    private AnonymousFunctionDeclarationExpression ParseAnonymousFunctionDeclarationExpression(IParser parser,
        ITokenStream tokenStream)
    {
        return _anonymousFunctionDeclarationParsingStrategy.Parse(parser, tokenStream);
    }

    private LiteralExpression ParseBooleanLiteralExpression(ITokenStream tokenStream)
    {
        return _booleanLiteralParsingStrategy.Parse(tokenStream);
    }

    private LiteralExpression ParseIntegerLiteralExpression(ITokenStream tokenStream)
    {
        return _integerLiteralParsingStrategy.Parse(tokenStream);
    }

    private LiteralExpression ParseFloatLiteralExpression(ITokenStream tokenStream)
    {
        return _floatLiteralParsingStrategy.Parse(tokenStream);
    }

    private LiteralExpression ParseStringLiteralExpression(ITokenStream tokenStream)
    {
        return _stringLiteralParsingStrategy.Parse(tokenStream);
    }

    private NewExpression ParseNewExpression(IParser parser, ITokenStream tokenStream)
    {
        return _newExpressionParsingStrategy.Parse(parser, tokenStream);
    }

    private Expression ParseParenthesisExpression(IParser parser, ITokenStream tokenStream)
    {
        return _parenthesisParsingStrategy.Parse(parser, tokenStream);
    }

    private UnaryExpression ParseUnaryExpression(IParser parser, ITokenStream tokenStream)
    {
        return _unaryExpressionParsingStrategy.Parse(parser, tokenStream);
    }

    private VariableReferenceExpression ParseVariableReferenceExpression(IParser parser, ITokenStream tokenStream)
    {
        return _variableReferenceParsingStrategy.Parse(parser, tokenStream);
    }
}
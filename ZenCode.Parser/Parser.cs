using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions.Strategies;
using ZenCode.Parser.Abstractions.Statements.Strategies;
using ZenCode.Parser.Abstractions.Types.Strategies;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Model.Types;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser;

public class Parser : IParser
{
    private readonly IDictionary<TokenType, IInfixExpressionParsingStrategy> _infixExpressionParsingStrategies =
        new Dictionary<TokenType, IInfixExpressionParsingStrategy>();

    private readonly IDictionary<TokenType, IPrefixExpressionParsingStrategy> _prefixExpressionParsingStrategies =
        new Dictionary<TokenType, IPrefixExpressionParsingStrategy>();
    
    private readonly IDictionary<TokenType, IStatementParsingStrategy> _statementParsingStrategies =
        new Dictionary<TokenType, IStatementParsingStrategy>();
    
    private readonly IDictionary<TokenType, IPrefixTypeParsingStrategy> _prefixTypeParsingStrategies =
        new Dictionary<TokenType, IPrefixTypeParsingStrategy>();

    public Program ParseProgram(ITokenStream tokenStream)
    {
        var statements = new List<Statement>();

        while (tokenStream.Peek(0) != null)
        {
            statements.Add(ParseStatement(tokenStream));
        }

        return new Program(statements);
    }
    
    public Expression ParseExpression(ITokenStream tokenStream, int precedence = 0)
    {
        var lExpression = ParsePrefixExpression(tokenStream);

        while (precedence < GetExpressionPrecedence(tokenStream))
        {
            lExpression = ParseInfixExpression(tokenStream, lExpression);
        }

        return lExpression;
    }

    public ExpressionList ParseExpressionList(ITokenStream tokenStream)
    {
        var expressions = new List<Expression>();

        while (true)
        {
            expressions.Add(ParseExpression(tokenStream));

            if (!tokenStream.Match(TokenType.Comma))
            {
                break;
            }

            tokenStream.Consume(TokenType.Comma);
        }

        return new ExpressionList { Expressions = expressions };
    }
    
    public Statement ParseStatement(ITokenStream tokenStream)
    {
        var token = tokenStream.Current;

        if (!_statementParsingStrategies.TryGetValue(token.Type, out var statementParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return statementParsingStrategy.Parse(tokenStream);
    }

    public Scope ParseScope(ITokenStream tokenStream)
    {
        tokenStream.Consume(TokenType.LeftBrace);

        var statements = new List<Statement>();

        while (true)
        {
            if (tokenStream.Match(TokenType.RightBrace))
            {
                tokenStream.Consume(TokenType.RightBrace);
                break;
            }
                
            statements.Add(ParseStatement(tokenStream));
        }

        return new Scope { Statements = statements };
    }

    public ConditionScope ParseConditionScope(ITokenStream tokenStream)
    {
        var condition = ParseExpression(tokenStream);
        var scope = ParseScope(tokenStream);

        return new ConditionScope(condition, scope);
    }

    public Type ParseType(ITokenStream tokenStream, int precedence = 0)
    {
        var type = ParsePrefixType(tokenStream);

        while (tokenStream.Peek(0)?.Type == TokenType.LeftBracket && tokenStream.Peek(1)?.Type == TokenType.RightBracket)
        {
            tokenStream.Consume(TokenType.LeftBracket);
            tokenStream.Consume(TokenType.RightBracket);
            
            type = new ArrayType(type);
        }

        return type;
    }

    public ParameterList ParseParameterList(ITokenStream tokenStream)
    {
        var parameters = new List<Parameter>();

        while (true)
        {
            var identifier = tokenStream.Consume(TokenType.Identifier);

            tokenStream.Consume(TokenType.Colon);

            var type = ParseType(tokenStream);

            parameters.Add(new Parameter(identifier, type));

            if (!tokenStream.Match(TokenType.Comma))
            {
                break;
            }

            tokenStream.Consume(TokenType.Comma);
        }

        return new ParameterList { Parameters = parameters };
    }

    public void SetPrefixExpressionParsingStrategy(TokenType tokenType,
        IPrefixExpressionParsingStrategy parsingStrategy)
    {
        _prefixExpressionParsingStrategies[tokenType] = parsingStrategy;
    }

    public void SetInfixExpressionParsingStrategy(TokenType tokenType, IInfixExpressionParsingStrategy parsingStrategy)
    {
        _infixExpressionParsingStrategies[tokenType] = parsingStrategy;
    }
    
    public void SetStatementParsingStrategy(TokenType tokenType, IStatementParsingStrategy parsingStrategy)
    {
        _statementParsingStrategies[tokenType] = parsingStrategy;
    }
    
    public void SetPrefixTypeParsingStrategy(TokenType tokenType, IPrefixTypeParsingStrategy parsingStrategy)
    {
        _prefixTypeParsingStrategies[tokenType] = parsingStrategy;
    }

    private Expression ParsePrefixExpression(ITokenStream tokenStream)
    {
        var token = tokenStream.Current;

        if (!_prefixExpressionParsingStrategies.TryGetValue(token.Type, out var prefixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return prefixExpressionParsingStrategy.Parse(tokenStream);
    }

    private Expression ParseInfixExpression(ITokenStream tokenStream, Expression lOperand)
    {
        var operatorToken = tokenStream.Current;

        if (!_infixExpressionParsingStrategies.TryGetValue(operatorToken.Type, out var infixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return infixExpressionParsingStrategy.Parse(tokenStream, lOperand);
    }

    private int GetExpressionPrecedence(ITokenStream tokenStream)
    {
        var currentToken = tokenStream.Peek(0);

        if (currentToken == null)
        {
            return 0;
        }

        return !_infixExpressionParsingStrategies.TryGetValue(currentToken.Type, out var parsingStrategy)
            ? 0
            : parsingStrategy.Precedence;
    }
    
    private Type ParsePrefixType(ITokenStream tokenStream)
    {
        var token = tokenStream.Current;

        if (!_prefixTypeParsingStrategies.TryGetValue(token.Type, out var prefixExpressionParsingStrategy))
        {
            throw new UnexpectedTokenException();
        }

        return prefixExpressionParsingStrategy.Parse(tokenStream);
    }
}
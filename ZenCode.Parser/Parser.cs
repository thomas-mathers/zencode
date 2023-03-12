using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Expressions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Abstractions.Types;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser;

public class Parser : IParser
{
    private readonly IExpressionParser _expressionParser;
    private readonly IStatementParser _statementParser;
    private readonly ITypeParser _typeParser;

    public Parser(
        IExpressionParser expressionParser,
        IStatementParser statementParser,
        ITypeParser typeParser)
    {
        _expressionParser = expressionParser;
        _statementParser = statementParser;
        _typeParser = typeParser;
    }
    
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
        return _expressionParser.ParseExpression(this, tokenStream, precedence);
    }
    
    public AssignmentStatement ParseAssignmentStatement(ITokenStream tokenStream)
    {
        return _statementParser.ParseAssignmentStatement(this, tokenStream);
    }
    
    public VariableDeclarationStatement ParseVariableDeclarationStatement(ITokenStream tokenStream)
    {
        return _statementParser.ParseVariableDeclarationStatement(this, tokenStream);
    }
    
    public Statement ParseStatement(ITokenStream tokenStream)
    {
        return _statementParser.ParseStatement(this, tokenStream);
    }

    public Type ParseType(ITokenStream tokenStream, int precedence = 0)
    {
        return _typeParser.ParseType(tokenStream);
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
}
using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model.Grammar.Statements;

namespace ZenCode.Parser.Statements;

public class StatementParser : IStatementParser
{
    private readonly IAssignmentStatementParsingStrategy _assignmentStatementParsingStrategy;
    private readonly IBreakStatementParsingStrategy _breakStatementParsingStrategy;
    private readonly IContinueStatementParsingStrategy _continueStatementParsingStrategy;
    private readonly IForStatementParsingStrategy _forStatementParsingStrategy;
    private readonly IFunctionDeclarationStatementParsingStrategy _functionDeclarationStatementParsingStrategy;
    private readonly IIfStatementParsingStrategy _ifStatementParsingStrategy;
    private readonly IPrintStatementParsingStrategy _printStatementParsingStrategy;
    private readonly IReadStatementParsingStrategy _readStatementParsingStrategy;
    private readonly IReturnStatementParsingStrategy _returnStatementParsingStrategy;
    private readonly IVariableDeclarationStatementParsingStrategy _variableDeclarationStatementParsingStrategy;
    private readonly IWhileStatementParsingStrategy _whileStatementParsingStrategy;

    public StatementParser
    (
        IAssignmentStatementParsingStrategy assignmentStatementParsingStrategy,
        IBreakStatementParsingStrategy breakStatementParsingStrategy,
        IContinueStatementParsingStrategy continueStatementParsingStrategy,
        IForStatementParsingStrategy forStatementParsingStrategy,
        IFunctionDeclarationStatementParsingStrategy functionDeclarationStatementParsingStrategy,
        IIfStatementParsingStrategy ifStatementParsingStrategy,
        IPrintStatementParsingStrategy printStatementParsingStrategy,
        IReadStatementParsingStrategy readStatementParsingStrategy,
        IReturnStatementParsingStrategy returnStatementParsingStrategy,
        IVariableDeclarationStatementParsingStrategy variableDeclarationStatementParsingStrategy,
        IWhileStatementParsingStrategy whileStatementParsingStrategy
    )
    {
        _assignmentStatementParsingStrategy = assignmentStatementParsingStrategy;
        _breakStatementParsingStrategy = breakStatementParsingStrategy;
        _continueStatementParsingStrategy = continueStatementParsingStrategy;
        _forStatementParsingStrategy = forStatementParsingStrategy;
        _functionDeclarationStatementParsingStrategy = functionDeclarationStatementParsingStrategy;
        _ifStatementParsingStrategy = ifStatementParsingStrategy;
        _printStatementParsingStrategy = printStatementParsingStrategy;
        _readStatementParsingStrategy = readStatementParsingStrategy;
        _returnStatementParsingStrategy = returnStatementParsingStrategy;
        _variableDeclarationStatementParsingStrategy = variableDeclarationStatementParsingStrategy;
        _whileStatementParsingStrategy = whileStatementParsingStrategy;
    }

    public Statement ParseStatement(IParser parser, ITokenStream tokenStream)
    {
        if (tokenStream.Current == null)
        {
            throw new UnexpectedTokenException();
        }
        
        return tokenStream.Current.Type switch
        {
            TokenType.Identifier => ParseAssignmentStatement(parser, tokenStream),
            TokenType.Break => ParseBreakStatement(tokenStream),
            TokenType.Continue => ParseContinueStatement(tokenStream),
            TokenType.For => ParseForStatement(parser, tokenStream),
            TokenType.Function => ParseFunctionDeclarationStatement(parser, tokenStream),
            TokenType.If => ParseIfStatement(parser, tokenStream),
            TokenType.Print => ParsePrintStatement(parser, tokenStream),
            TokenType.Read => ParseReadStatement(parser, tokenStream),
            TokenType.Return => ParseReturnStatement(parser, tokenStream),
            TokenType.Var => ParseVariableDeclarationStatement(parser, tokenStream),
            TokenType.While => ParseWhileStatement(parser, tokenStream),
            _ => throw new UnexpectedTokenException(tokenStream.Current.Type)
        };
    }

    public AssignmentStatement ParseAssignmentStatement(IParser parser, ITokenStream tokenStream)
    {
        return _assignmentStatementParsingStrategy.Parse(parser, tokenStream);
    }

    public VariableDeclarationStatement ParseVariableDeclarationStatement(IParser parser, ITokenStream tokenStream)
    {
        return _variableDeclarationStatementParsingStrategy.Parse(parser, tokenStream);
    }

    private BreakStatement ParseBreakStatement(ITokenStream tokenStream)
    {
        return _breakStatementParsingStrategy.Parse(tokenStream);
    }

    private ContinueStatement ParseContinueStatement(ITokenStream tokenStream)
    {
        return _continueStatementParsingStrategy.Parse(tokenStream);
    }

    private ForStatement ParseForStatement(IParser parser, ITokenStream tokenStream)
    {
        return _forStatementParsingStrategy.Parse(parser, tokenStream);
    }

    private FunctionDeclarationStatement ParseFunctionDeclarationStatement(IParser parser, ITokenStream tokenStream)
    {
        return _functionDeclarationStatementParsingStrategy.Parse(parser, tokenStream);
    }

    private IfStatement ParseIfStatement(IParser parser, ITokenStream tokenStream)
    {
        return _ifStatementParsingStrategy.Parse(parser, tokenStream);
    }

    private PrintStatement ParsePrintStatement(IParser parser, ITokenStream tokenStream)
    {
        return _printStatementParsingStrategy.Parse(parser, tokenStream);
    }

    private ReadStatement ParseReadStatement(IParser parser, ITokenStream tokenStream)
    {
        return _readStatementParsingStrategy.Parse(parser, tokenStream);
    }

    private ReturnStatement ParseReturnStatement(IParser parser, ITokenStream tokenStream)
    {
        return _returnStatementParsingStrategy.Parse(parser, tokenStream);
    }

    private WhileStatement ParseWhileStatement(IParser parser, ITokenStream tokenStream)
    {
        return _whileStatementParsingStrategy.Parse(parser, tokenStream);
    }
}

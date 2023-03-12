using ZenCode.Lexer.Abstractions;
using ZenCode.Lexer.Exceptions;
using ZenCode.Lexer.Model;
using ZenCode.Parser.Abstractions;
using ZenCode.Parser.Abstractions.Statements;
using ZenCode.Parser.Model.Grammar.Statements;
using ZenCode.Parser.Statements.Strategies;

namespace ZenCode.Parser.Statements;

public class StatementParser : IStatementParser
{
    private readonly AssignmentStatementParsingStrategy _assignmentStatementParsingStrategy;
    private readonly ForStatementParsingStrategy _forStatementParsingStrategy;
    private readonly FunctionDeclarationStatementParsingStrategy _functionDeclarationStatementParsingStrategy;
    private readonly IfStatementParsingStrategy _ifStatementParsingStrategy;
    private readonly PrintStatementParsingStrategy _printStatementParsingStrategy;
    private readonly ReturnStatementParsingStrategy _returnStatementParsingStrategy;
    private readonly VariableDeclarationStatementParsingStrategy _variableDeclarationStatementParsingStrategy;
    private readonly WhileStatementParsingStrategy _whileStatementParsingStrategy;
    
    public StatementParser(
        AssignmentStatementParsingStrategy assignmentStatementParsingStrategy,
        ForStatementParsingStrategy forStatementParsingStrategy,
        FunctionDeclarationStatementParsingStrategy functionDeclarationStatementParsingStrategy,
        IfStatementParsingStrategy ifStatementParsingStrategy,
        PrintStatementParsingStrategy printStatementParsingStrategy,
        ReturnStatementParsingStrategy returnStatementParsingStrategy,
        VariableDeclarationStatementParsingStrategy variableDeclarationStatementParsingStrategy, 
        WhileStatementParsingStrategy whileStatementParsingStrategy)
    {
        _assignmentStatementParsingStrategy = assignmentStatementParsingStrategy;
        _forStatementParsingStrategy = forStatementParsingStrategy;
        _functionDeclarationStatementParsingStrategy = functionDeclarationStatementParsingStrategy;
        _ifStatementParsingStrategy = ifStatementParsingStrategy;
        _printStatementParsingStrategy = printStatementParsingStrategy;
        _returnStatementParsingStrategy = returnStatementParsingStrategy;
        _variableDeclarationStatementParsingStrategy = variableDeclarationStatementParsingStrategy;
        _whileStatementParsingStrategy = whileStatementParsingStrategy;
    }
    
    public Statement ParseStatement(IParser parser, ITokenStream tokenStream) =>
        tokenStream.Current.Type switch
        {
            TokenType.Identifier => ParseAssignmentStatement(parser, tokenStream),
            TokenType.For => ParseForStatement(parser, tokenStream),
            TokenType.Function => ParseFunctionDeclarationStatement(parser, tokenStream),
            TokenType.If => ParseIfStatement(parser, tokenStream),
            TokenType.Print => ParsePrintStatement(parser, tokenStream),
            TokenType.Return => ParseReturnStatement(parser, tokenStream),
            TokenType.Var => ParseVariableDeclarationStatement(parser, tokenStream),
            TokenType.While => ParseWhileStatement(parser, tokenStream),
            _ => throw new UnexpectedTokenException()
        };
    
    public AssignmentStatement ParseAssignmentStatement(IParser parser, ITokenStream tokenStream)
    {
        return _assignmentStatementParsingStrategy.Parse(parser, tokenStream);
    }
    
    public VariableDeclarationStatement ParseVariableDeclarationStatement(IParser parser, ITokenStream tokenStream)
    {
        return _variableDeclarationStatementParsingStrategy.Parse(parser, tokenStream);
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

    private ReturnStatement ParseReturnStatement(IParser parser, ITokenStream tokenStream)
    {
        return _returnStatementParsingStrategy.Parse(parser, tokenStream);
    }

    private WhileStatement ParseWhileStatement(IParser parser, ITokenStream tokenStream)
    {
        return _whileStatementParsingStrategy.Parse(parser, tokenStream);
    }
}
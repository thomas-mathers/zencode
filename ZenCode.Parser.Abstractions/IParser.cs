using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.Parser.Abstractions;

public interface IParser
{
    VariableReferenceExpression ParseVariableReferenceExpression(ITokenStream tokenStream);
    Expression ParseExpression(ITokenStream tokenStream, int precedence = 0);
    ExpressionList ParseExpressionList(ITokenStream tokenStream);
    AssignmentStatement ParseAssignmentStatement(ITokenStream tokenStream);
    VariableDeclarationStatement ParseVariableDeclarationStatement(ITokenStream tokenStream);
    Statement ParseStatement(ITokenStream tokenStream);
    Type ParseType(ITokenStream tokenStream);
    TypeList ParseTypeList(ITokenStream tokenStream);
    ConditionScope ParseConditionScope(ITokenStream tokenStream);
    ParameterList ParseParameterList(ITokenStream tokenStream);
    Scope ParseScope(ITokenStream tokenStream);
    Program ParseProgram(ITokenStream tokenStream);
}
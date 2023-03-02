﻿using ZenCode.Lexer.Abstractions;
using ZenCode.Parser.Model;
using ZenCode.Parser.Model.Grammar;
using ZenCode.Parser.Model.Grammar.Expressions;
using ZenCode.Parser.Model.Grammar.Statements;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Abstractions;

public interface IParser
{
    Program ParseProgram(ITokenStream tokenStream);
    Expression ParseExpression(ITokenStream tokenStream, int precedence = 0);
    IReadOnlyList<Expression> ParseExpressionList(ITokenStream tokenStream);
    Statement ParseStatement(ITokenStream tokenStream);
    Scope ParseScope(ITokenStream tokenStream);
    ConditionScope ParseConditionScope(ITokenStream tokenStream);
    Type ParseType(ITokenStream tokenStream, int precedence = 0);
    IReadOnlyList<Parameter> ParseParameterList(ITokenStream tokenStream);
}
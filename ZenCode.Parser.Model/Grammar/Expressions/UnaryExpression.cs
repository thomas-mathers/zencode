﻿using ZenCode.Lexer.Model;

namespace ZenCode.Parser.Model.Grammar.Expressions;

public record UnaryExpression(Token Operator, Expression Expression) : Expression
{
    public override string ToString()
    {
        return $"{Operator} {Expression}";
    }
}

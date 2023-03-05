﻿namespace ZenCode.Lexer.Model;

public enum TokenType
{
    Any = 0,
    Addition,
    And,
    Assignment,
    Boolean,
    BooleanLiteral,
    Colon,
    Comma,
    Division,
    Else,
    ElseIf,
    Equals,
    Exponentiation,
    Float,
    FloatLiteral,
    Function,
    GreaterThan,
    GreaterThanOrEqual,
    Identifier,
    If,
    Integer,
    IntegerLiteral,
    LeftBrace,
    LeftBracket,
    LeftParenthesis,
    LessThan,
    LessThanOrEqual,
    Modulus,
    Multiplication,
    Not,
    NotEquals,
    Or,
    Print,
    Return,
    RightArrow,
    RightBrace,
    RightBracket,
    RightParenthesis,
    String,
    StringLiteral,
    Subtraction,
    Var,
    Void,
    While
}
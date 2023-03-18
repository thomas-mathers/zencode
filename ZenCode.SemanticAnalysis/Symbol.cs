using ZenCode.Lexer.Model;
using Type = ZenCode.Parser.Model.Grammar.Types.Type;

namespace ZenCode.SemanticAnalysis;

public record Symbol(Token Token, Type Type);
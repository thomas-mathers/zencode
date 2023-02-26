using ZenCode.Lexer.Model;
using Type = ZenCode.Parser.Model.Types.Type;

namespace ZenCode.Parser.Model;

public record Parameter(Token Identifier, Type Type);
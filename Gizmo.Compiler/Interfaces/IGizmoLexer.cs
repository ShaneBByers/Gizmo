using Gizmo.Compiler.Entities;

namespace Gizmo.Compiler.Interfaces;

public interface IGizmoLexer
{
    IList<LineProgramTokens> GetTokens(string text);
}

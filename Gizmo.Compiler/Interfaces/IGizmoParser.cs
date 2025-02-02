using Gizmo.Compiler.Entities;

namespace Gizmo.Compiler.Interfaces;

public interface IGizmoParser
{
    IList<SyntaxTree> GetSyntaxTrees(string fileName, IList<LineProgramTokens> lines);
}

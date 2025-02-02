using Gizmo.Compiler.Entities;

namespace Gizmo.Compiler.Interfaces;

public interface IGizmoCompiler
{
    bool TryCompileFiles(IList<GizmoFile> files, out string compiledFileContents);
}

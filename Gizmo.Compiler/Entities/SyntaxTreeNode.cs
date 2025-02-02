using Gizmo.Compiler.Interfaces;

namespace Gizmo.Compiler.Entities;

public record SyntaxTreeNode<T>(
    T Value,
    IList<ISyntaxTreeNode> Children
);
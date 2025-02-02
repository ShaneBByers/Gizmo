using Gizmo.Compiler.Entities;
using Gizmo.Compiler.Interfaces;

namespace Gizmo.Compiler;

public record SyntaxTree(
    SyntaxTreeNode<ISyntaxTreeNode> Root
);
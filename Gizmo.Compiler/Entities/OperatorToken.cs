using Gizmo.Compiler.Entities.Enums;

namespace Gizmo.Compiler.Entities;

public record OperatorToken(
    OperatorType Type
) : ProgramTokenBase
{
    public override string ToString() => $"Operator: {Type}";
}

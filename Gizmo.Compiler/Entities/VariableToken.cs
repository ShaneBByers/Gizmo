namespace Gizmo.Compiler.Entities;

public record VariableToken(
    string Name
) : ProgramTokenBase
{
    public override string ToString() => $"Variable: {Name}";
}

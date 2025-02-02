namespace Gizmo.Compiler.Entities;

public record NumberToken(
    double Number
) : ProgramTokenBase
{
    public override string ToString() => $"Number  : {Number}";
}

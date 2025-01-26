namespace Gizmo.Shared;

public record OperatorToken(
    OperatorType Type
) : IProgramToken;

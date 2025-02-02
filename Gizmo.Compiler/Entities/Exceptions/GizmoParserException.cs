namespace Gizmo.Compiler.Entities.Exceptions;

public class GizmoParserException(
    string fileName,
    int lineNumber,
    string message
) : Exception($"Parser Exception in {fileName} on line {lineNumber}: {message}");
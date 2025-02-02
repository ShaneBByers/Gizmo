using Gizmo.Compiler.Entities;
using Gizmo.Compiler.Entities.Enums;
using Gizmo.Compiler.Interfaces;

namespace Gizmo.Compiler.Services;

public class GizmoSemanticAnalyzer : IGizmoSemanticAnalyzer
{
    public static bool DoLinesHaveValidSemantics(IList<LineProgramTokens> lines)
    {
        // Verify variables are assigned before being used
        IList<string> assignedVariables = [];
        foreach (var line in lines)
        {
            var tokens = line.Tokens;
            for (int i = 0; i < tokens.Count; i++)
            {
                if (tokens[i] is VariableToken assignedVariable
                    && i < tokens.Count - 1
                    && tokens[i + 1] is OperatorToken { Type: OperatorType.Assign })
                {
                    assignedVariables.Add(assignedVariable.Name);
                }
                else if (tokens[i] is VariableToken usedVariable
                    && !assignedVariables.Contains(usedVariable.Name))
                {
                    return false;
                }
            }
        }
        
        return true;
    }
}

using Gizmo.Compiler.Entities;
using Gizmo.Compiler.Entities.Enums;
using Gizmo.Compiler.Entities.Exceptions;
using Gizmo.Compiler.Interfaces;

namespace Gizmo.Compiler.Services;

public class GizmoParser : IGizmoParser
{
    public IList<SyntaxTree> GetSyntaxTrees(string fileName, IList<LineProgramTokens> lines)
    {
        IList<SyntaxTree> syntaxTree = [];
        for (int i = 0; i < lines.Count; i++)
        {
            var line = lines[i];
            if (!DoesLineHaveValidSyntax(line, out string errorMessage))
                throw new GizmoParserException(fileName, i + 1, errorMessage);
        }

        return syntaxTree;
    }

    private static bool DoesLineHaveValidSyntax(LineProgramTokens line, out string errorMessage)
    {
        errorMessage = string.Empty;

        // Check if we're assigning a value to a variable
        if (line.Tokens.First() is not VariableToken variable)
        {
            errorMessage = "Not assigning a value to a variable";
            return false;
        }

        // Verify there's only one Assign
        if (line.Tokens.OfType<OperatorToken>().Count(x => x.Type == OperatorType.Assign) != 1)
        {
            errorMessage = "Not exactly one assign statment";
            return false;
        }
        
        // Verify there aren't two Operators consecutively
        for (int i = 0; i < line.Tokens.Count; i++)
        {
            if (line.Tokens[i] is OperatorToken
                && i < line.Tokens.Count - 1
                && line.Tokens[i + 1] is OperatorToken)
            {
                errorMessage = "Two consecutive operators detected";
                return false;
            }
        }

        return true;
    }
}

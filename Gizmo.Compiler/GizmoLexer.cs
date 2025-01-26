using Gizmo.Shared;

namespace Gizmo.Compiler;

public static class GizmoLexer
{
    public static IList<LineProgramTokens> GetTokens(string text)
    {
        IList<string> lines = text.Split(Environment.NewLine);

        return [.. lines.Select(GetTokensForLine)];
    }

    private static LineProgramTokens GetTokensForLine(string line)
    {
        var programTokens = new List<IProgramToken>();

        for (int currentPosition = 0; currentPosition < line.Length; currentPosition++)
        {
            if (TryParseNumber(line, currentPosition, out int numberStringLength, out double? number)
                && number.HasValue)
            {
                currentPosition += numberStringLength - 1;
                programTokens.Add(new NumberToken(number.Value));
            }
            else if (TryParseOperator(line, currentPosition, out OperatorType? operatorType)
                && operatorType.HasValue)
            {
                programTokens.Add(new OperatorToken(operatorType.Value));
            }
            else if (TryParseVariableName(line, currentPosition, out string? variableName)
                && !string.IsNullOrWhiteSpace(variableName))
            {
                currentPosition += variableName.Length - 1;
                programTokens.Add(new VariableToken(variableName));
            }
        }

        return new(programTokens);
    }

    private static bool TryParseNumber(string text, int currentPosition, out int numberStringLength, out double? number)
    {
        numberStringLength = 0;
        number = null;

        if (!IsIndexWithinString(currentPosition, text)
            || !char.IsDigit(text[currentPosition]))
            return false;

        string numberString = new([.. text
            .Skip(currentPosition)
            .TakeWhile(c => char.IsDigit(c) || c == Constants.Period)]);

        if (!double.TryParse(numberString, out double parsedNumber))
            return false;

        numberStringLength = numberString.Length;
        number = parsedNumber;
        return true;
    }

    private static bool TryParseOperator(string text, int currentPosition, out OperatorType? operatorType)
    {
        operatorType = null;

        if (!IsIndexWithinString(currentPosition, text))
            return false;
            
        operatorType = text[currentPosition] switch
        {
            Constants.Plus => OperatorType.Add,
            Constants.Minus => OperatorType.Subtract,
            Constants.Asterisk => OperatorType.Multiply,
            Constants.ForwardSlash => OperatorType.Divide,
            Constants.Equal => OperatorType.Assign,
            _ => null
        };

        if (!operatorType.HasValue)
            return false;

        return true;
    }

    private static bool TryParseVariableName(string text, int currentPosition, out string? variableName)
    {
        variableName = null;

        if (!IsIndexWithinString(currentPosition, text)
            || !char.IsLetter(text[currentPosition]))
            return false;

        variableName = new([.. text
            .Skip(currentPosition)
            .TakeWhile(char.IsLetter)]);
        
        return true;
    }

    private static bool IsIndexWithinString(int index, string text) => index < text.Length;
}
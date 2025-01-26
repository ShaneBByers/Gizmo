using Gizmo.Compiler;

using StreamReader streamReader = new("TestProgram.txt");
string program = streamReader.ReadToEnd();

var lines = GizmoLexer.GetTokens(program);

foreach (var line in lines)
{
    foreach (var token in line.Tokens)
    {
        Console.WriteLine(token);
    }
    Console.WriteLine();
}
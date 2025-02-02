using Gizmo.Compiler.Entities;
using Gizmo.Compiler.Services;

IList<string> programFileNames = [
    "TestProgram.txt"
];

var gizmoCompiler = new GizmoCompiler();

IList<GizmoFile> files = [];

foreach (var programFileName in programFileNames)
{
    using StreamReader streamReader = new(programFileName);
    string programContents = streamReader.ReadToEnd();
    files.Add(new(programFileName, programContents));
}

bool wereFilesCompiled = gizmoCompiler.TryCompileFiles(files, out string compiledFileContents);

Console.WriteLine($"Compilation: {(wereFilesCompiled ? "SUCCESS": "FAILED")}");
Console.WriteLine($"Contents: {compiledFileContents}");
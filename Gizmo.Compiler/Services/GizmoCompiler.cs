using Gizmo.Compiler.Entities;
using Gizmo.Compiler.Entities.Exceptions;
using Gizmo.Compiler.Interfaces;
using Microsoft.Extensions.Logging;

namespace Gizmo.Compiler.Services;

public class GizmoCompiler(
    ILoggerFactory loggerFactory) : IGizmoCompiler
{
    private readonly ILogger<GizmoCompiler> _logger = loggerFactory.CreateLogger<GizmoCompiler>();
    private readonly IGizmoLexer _gizmoLexer = new GizmoLexer();
    private readonly IGizmoParser _gizmoParser = new GizmoParser();

    public bool TryCompileFiles(
        IList<GizmoFile> files,
        out string compiledFileContents)
    {
        compiledFileContents = string.Empty;
        IList<string> compiledFiles = [];
        
        foreach (var file in files)
        {
            if (!TryCompileFile(file, out string singleCompiledFile))
                return false;
            
            compiledFiles.Add(singleCompiledFile);
        }

        compiledFileContents = string.Join(Environment.NewLine, compiledFiles);
        return true;
    }

    private bool TryCompileFile(GizmoFile file, out string compiledFile)
    {
        compiledFile = string.Empty;

        try
        {
            var lines = _gizmoLexer.GetTokens(file.Contents);

            var syntaxTrees = _gizmoParser.GetSyntaxTrees(file.Name, lines);
        }
        catch (GizmoParserException parserEx)
        {
            _logger.LogError(parserEx, "An error occurred while parsing the file.");
            return false;
        }

        return true;
    }
}

// Code Generator - Turns LineProgramTokens into lines of Gizmo machine code
// Assembler - 

// A compiler typically consists of several components, each responsible for a specific part of the compilation process. Here are the main components:

// Lexical Analyzer (Lexer):
// Converts the source code into tokens.
// Removes whitespace and comments.
// Identifies keywords, operators, and identifiers.

// Syntax Analyzer (Parser):
// Analyzes the token sequence to ensure it follows the grammatical structure of the language.
// Constructs a parse tree or abstract syntax tree (AST).

// Semantic Analyzer:
// Checks for semantic errors (e.g., type checking, scope resolution).
// Ensures that the parse tree adheres to the language's semantic rules.

// Intermediate Code Generator:
// Translates the parse tree or AST into an intermediate representation (IR).
// The IR is often easier to optimize and translate into machine code.

// Optimizer:
// Improves the intermediate code for performance (e.g., reducing instruction count, improving memory usage).
// Can perform both machine-independent and machine-dependent optimizations.

// Code Generator:
// Converts the optimized intermediate code into target machine code.
// Allocates registers and manages memory.

// Assembler:
// Converts the machine code into an object file (binary format).

// Linker:
// Combines multiple object files into a single executable.
// Resolves external references and addresses.

// Loader:
// Loads the executable into memory for execution.
// Sets up the execution environment.
// These components work together to transform high-level source code into executable machine code.
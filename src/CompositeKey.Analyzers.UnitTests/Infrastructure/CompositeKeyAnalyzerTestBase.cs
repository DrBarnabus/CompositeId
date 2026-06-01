using Microsoft.CodeAnalysis.CSharp.Testing;
using Microsoft.CodeAnalysis.Diagnostics;
using Microsoft.CodeAnalysis.Testing;

namespace CompositeKey.Analyzers.UnitTests.Infrastructure;

/// <summary>
/// Base class for CompositeKey analyzer tests providing common test infrastructure,
/// diagnostic verification utilities, and test data management.
/// </summary>
/// <typeparam name="TAnalyzer">The type of analyzer being tested.</typeparam>
public abstract class CompositeKeyAnalyzerTestBase<TAnalyzer> : CSharpAnalyzerTest<TAnalyzer, DefaultVerifier>
    where TAnalyzer : DiagnosticAnalyzer, new()
{
    /// <summary>
    /// Initializes the test with default configuration.
    /// </summary>
    protected CompositeKeyAnalyzerTestBase()
    {
        // Add CompositeKey reference by default
        TestState.AdditionalReferences.Add(MetadataReferences.CompositeKeyReference);

        // Set default language version to C# 12
        TestState.AdditionalFiles.Add(("Directory.Build.props", CreateDirectoryBuildProps()));

        // Match reference assemblies to the running TFM; the loaded CompositeKey may be built for it.
        ReferenceAssemblies =
#if NET10_0_OR_GREATER
            ReferenceAssemblies.Net.Net100;
#elif NET9_0
            ReferenceAssemblies.Net.Net90;
#else
            ReferenceAssemblies.Net.Net80;
#endif
    }

    /// <summary>
    /// Creates a Directory.Build.props file content for test projects.
    /// </summary>
    private static string CreateDirectoryBuildProps()
    {
        return """
            <Project>
                <PropertyGroup>
                    <LangVersion>12</LangVersion>
                    <ImplicitUsings>enable</ImplicitUsings>
                    <Nullable>enable</Nullable>
                </PropertyGroup>
            </Project>
            """;
    }
}

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Symbols;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.CodeAnalysis.MSBuild;
using Microsoft.CodeAnalysis.Text;

namespace UnityEngineAnalyzer.CMD
{
    class Program
    {
        static void Main(string[] args)
        {
            var ws = MSBuildWorkspace.Create();
            var soln = ws.OpenSolutionAsync(@"E:\Bitbucket\Work\commanders\commanders.sln").Result;
            var proj = soln.Projects.Single();
            var compilation = proj.GetCompilationAsync().Result;

            string TEST_ATTRIBUTE_METADATA_NAME = "System.Windows.Forms.Form";
            var testAttributeType = compilation.GetTypeByMetadataName(TEST_ATTRIBUTE_METADATA_NAME);

            foreach (var tree in compilation.SyntaxTrees)
            {
                var classes = tree.GetRoot().DescendantNodesAndSelf().Where(x => x.IsKind(SyntaxKind.ClassDeclaration));
                foreach (var c in classes)
                {
                    var classDec = (ClassDeclarationSyntax)c;
                    var bases = classDec.BaseList;

                    if (bases?.Types != null)
                    {
                        foreach (var b in bases.Types)
                        {
                            var nodeType = compilation.GetSemanticModel(tree).GetTypeInfo(b.Type);

                            // Is the node a System.Windows.Forms.Form?
                            if (nodeType.Type.Equals(testAttributeType))
                            {
                                Console.WriteLine(classDec.Identifier.Text);
                            }
                        }
                    }
                }
            }
            Console.ReadKey();
        }
    }
}

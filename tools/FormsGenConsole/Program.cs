using System;
using System.IO;
using FormsGen;

namespace FormsGenConsole
{
    class MainClass
    {
        public static void Main(string[] args)
        {
            //need project file --in
            //need project path --path

            string ProjectPath = null;
            Project Project = null;

            string ProjectFile = "/Users/PhilOchu/Sandbox/relief/src/Relief.Pharmacy.Core/FormsGen.xml";

            Console.WriteLine("Inputs:");
            Console.WriteLine(ProjectFile);

            //get the project path
            ProjectPath = Path.GetDirectoryName(ProjectFile);

            //load the project file
            Project = ProjectBuilder.LoadProjectFile(ProjectFile);

            //build Pages Project
            var builder = new ProjectBuilder();
            builder.Build(ProjectPath, Project);


        }

    }
}

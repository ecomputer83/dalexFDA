using System;
using System.IO;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;

namespace FormsGen
{
    public class ProjectBuilderTask : Task, ITask
    {

        const string tag = "FORMGEN";
       
        [Required]
        public string ProjectFile { get; set; }

        public override bool Execute()
        {
            bool retVal = true;


            try
            {
                Log.LogMessage(MessageImportance.Normal, $"{tag}- Generating code for project file: {ProjectFile}");


                string ProjectPath = Path.GetDirectoryName(ProjectFile);

                Log.LogMessage(MessageImportance.Normal, $"{tag}- project path: {ProjectPath}");

                Project Project = ProjectBuilder.LoadProjectFile(ProjectFile);

                Log.LogMessage(MessageImportance.Normal, $"{tag}- Project loaded: {Project.Namespace}");

                var builder = new ProjectBuilder();

                builder.Log = this.Log;

                builder.Build(ProjectPath, Project);

            }
            catch(Exception ex)
            {
                Log.LogError("{0} - {1}", tag, ex.Message);

                retVal = false;
            }

            return retVal;
        }
    }
}

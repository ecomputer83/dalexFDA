using System;
using System.IO;
using System.Xml.Linq;
using System.Xml.Serialization;
using Microsoft.Build.Utilities;

namespace FormsGen
{
    public class ProjectBuilder
    {
        public string PagesFolderName { get; set; } = "Pages";
        public string ProjectPath { get; set; }
        public Project Project { get; set; }
        public TaskLoggingHelper Log { get; internal set; }

        public void Build(string projectPath, Project project)
        {


            this.ProjectPath = projectPath;
            this.Project = project;

            LogMessage("Building project with {0} pages", project.Pages.Count);
            ProcessPages();
        }



        private void ProcessPages()
        {
            LogMessage("ProcessPage");

            if (this.Project != null)
            {


                foreach (var page in this.Project.Pages)
                {
                    string pageFolder = Path.Combine(this.ProjectPath, PagesFolderName, page.Folder, page.Name);

                    //check that folder for page exists if not create it
                    if (!Directory.Exists(pageFolder))
                    {
                        Directory.CreateDirectory(pageFolder);
                        LogMessage("Created directory for {0} - {1}", page.Name, pageFolder);
                    }
                    else
                    {
                        LogMessage("Directory for {0} already exists - {1}", page.Name, pageFolder);
                    }

                    ProcessPage(pageFolder, page);

                }

            }
        }

        private void ProcessPage(string pageFolder, Page page)
        {
            LogMessage("ProcessPage - {0}", page.Name);

            //create viewmodel file
            GenerateViewModel(pageFolder, page);

            //create viewmodel - generated properties,services & commands file
            GenerateViewModelGen(pageFolder, page);

            if (page.Name.ToLower().EndsWith("list"))
            {
                //if page name ends in list then generate the list item view model as well

                //create viewmodel file
                GenerateItemViewModel(pageFolder, page);

                //create viewmodel - generated properties,services & commands file
                GenerateItemViewModelGen(pageFolder, page);
            }

        }

        private void GenerateViewModelGen(string pageFolder, Page page)
        {
            LogMessage("GenerateViewModelGen - {0}", page.Name);

            string generatedViewModelFileName = Path.Combine(pageFolder, $"{page.Name}ViewModel.generated.cs");
            string actualViewModelFileName = Path.Combine(pageFolder, $"{page.Name}ViewModel.cs");
            var generatedViewModel = new vmgen();

            generatedViewModel.Page = page;
            generatedViewModel.Project = this.Project;
            generatedViewModel.ViewModelFilePath = actualViewModelFileName;


            var generatedViewModelContent = generatedViewModel.TransformText();
            System.IO.File.WriteAllText(generatedViewModelFileName, generatedViewModelContent);
        }

        private void GenerateItemViewModelGen(string pageFolder, Page page)
        {
            LogMessage("GenerateItemViewModelGen - {0}", page.Name);

            string generatedViewModelFileName = Path.Combine(pageFolder, $"{page.Name}ItemViewModel.generated.cs");
            string actualViewModelFileName = Path.Combine(pageFolder, $"{page.Name}ItemViewModel.cs");
            var generatedViewModel = new vmgenitem();

            generatedViewModel.Page = page;
            generatedViewModel.Project = this.Project;
            generatedViewModel.ViewModelFilePath = actualViewModelFileName;


            var generatedViewModelContent = generatedViewModel.TransformText();
            System.IO.File.WriteAllText(generatedViewModelFileName, generatedViewModelContent);
        }

        private void GenerateViewModel(string pageFolder, Page page)
        {
            LogMessage("GenerateViewModel - {0}", page.Name);

            string actualViewModelFileName = Path.Combine(pageFolder, $"{page.Name}ViewModel.cs");

            if (!File.Exists(actualViewModelFileName))
            {

                var actualViewModel = new vm();

                actualViewModel.Page = page;
                actualViewModel.Project = this.Project;


                var actualViewModelContent = actualViewModel.TransformText();
                System.IO.File.WriteAllText(actualViewModelFileName, actualViewModelContent);
            }
        }

        private void GenerateItemViewModel(string pageFolder, Page page)
        {
            LogMessage("GenerateItemViewModel - {0}", page.Name);

            string actualViewModelFileName = Path.Combine(pageFolder, $"{page.Name}ItemViewModel.cs");

            if (!File.Exists(actualViewModelFileName))
            {

                var actualViewModel = new vmitem();

                actualViewModel.Page = page;
                actualViewModel.Project = this.Project;


                var actualViewModelContent = actualViewModel.TransformText();
                System.IO.File.WriteAllText(actualViewModelFileName, actualViewModelContent);

            }
        }

        public static Project LoadProjectFile(string ProjectFile)
        {
           

            try
            {
                XDocument doc = XDocument.Load(ProjectFile);

                XmlSerializer serializer = new XmlSerializer(typeof(Project));
                System.Xml.XmlReader reader = doc.CreateReader();
                reader.MoveToContent();

                return (Project)serializer.Deserialize(reader);
            }
            catch (Exception ex)
            {
                throw new ApplicationException(String.Format("Error occurred while loading project file - {0}", ProjectFile), ex);
            }
        }

        private void LogMessage(string message, params object[] messageArgs)
        {
            if (Log != null)
            {
                Log.LogMessage(message, messageArgs);
            }
            else
            {
                Console.WriteLine(message, messageArgs);
            }
        }
    }
}

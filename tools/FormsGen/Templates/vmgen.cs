﻿// ------------------------------------------------------------------------------
//  <autogenerated>
//      This code was generated by a tool.
//      Mono Runtime Version: 4.0.30319.42000
// 
//      Changes to this file may cause incorrect behavior and will be lost if 
//      the code is regenerated.
//  </autogenerated>
// ------------------------------------------------------------------------------

namespace FormsGen {
    using System.Linq;
    using System.Text;
    using System.Collections.Generic;
    using System;
    
    
    public partial class vmgen : vmgenBase {
        
        public virtual string TransformText() {
            this.GenerationEnvironment = null;
            
            #line 6 ""
            this.Write("// ------------------------------------------------------------------------------\n//  <autogenerated>\n//      This code was generated by a tool.\n//      Changes to this file may cause incorrect behavior and will be lost if \n//      the code is regenerated.\n//  </autogenerated>\n// ------------------------------------------------------------------------------\nusing System;\nusing System.Collections.Generic;\nusing System.Collections.ObjectModel;\nusing System.Threading.Tasks;\nusing PropertyChanged;\nusing Xamarin.Forms;\n\nnamespace ");
            
            #line default
            #line hidden
            
            #line 20 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( Project.Namespace ));
            
            #line default
            #line hidden
            
            #line 20 ""
            this.Write("\n{\n    [AddINotifyPropertyChangedInterface]\n    public partial class ");
            
            #line default
            #line hidden
            
            #line 23 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( Page.Name ));
            
            #line default
            #line hidden
            
            #line 23 ""
            this.Write("ViewModel\n    {\n        //default services\n");
            
            #line default
            #line hidden
            
            #line 26 ""
 foreach (var service in Project.Services.Where(s => s.IsDefault)) 
{ 
            
            #line default
            #line hidden
            
            #line 28 ""
            this.Write("        internal readonly ");
            
            #line default
            #line hidden
            
            #line 28 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( service.FullTypeName ));
            
            #line default
            #line hidden
            
            #line 28 ""
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 28 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( service.DisplayName ));
            
            #line default
            #line hidden
            
            #line 28 ""
            this.Write(";\n");
            
            #line default
            #line hidden
            
            #line 29 ""
 } 
            
            #line default
            #line hidden
            
            #line 30 ""
            this.Write("\n        //other services\n");
            
            #line default
            #line hidden
            
            #line 32 ""
 foreach (var pageService in Page.Services) { var service = Page.GetService(Project,pageService); 
  
            
            #line default
            #line hidden
            
            #line 34 ""
            this.Write("        internal readonly ");
            
            #line default
            #line hidden
            
            #line 34 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( service.FullTypeName ));
            
            #line default
            #line hidden
            
            #line 34 ""
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 34 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( service.DisplayName ));
            
            #line default
            #line hidden
            
            #line 34 ""
            this.Write(";\n");
            
            #line default
            #line hidden
            
            #line 35 ""
 } 
            
            #line default
            #line hidden
            
            #line 36 ""
            this.Write("\n        //commands\n");
            
            #line default
            #line hidden
            
            #line 38 ""
 foreach (var command in Page.Commands.Where(c=>!c.ForListItem)) 
{ 
            
            #line default
            #line hidden
            
            #line 40 ""
            this.Write("        public Command ");
            
            #line default
            #line hidden
            
            #line 40 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( command.Name ));
            
            #line default
            #line hidden
            
            #line 40 ""
            this.Write(" { get; private set; }\n");
            
            #line default
            #line hidden
            
            #line 41 ""
 } 
            
            #line default
            #line hidden
            
            #line 42 ""
            this.Write("\n        //properties\n");
            
            #line default
            #line hidden
            
            #line 44 ""
 foreach (var property in Page.Properties.Where(c=>!c.ForListItem)) 
{ 
            
            #line default
            #line hidden
            
            #line 46 ""
            this.Write("        public ");
            
            #line default
            #line hidden
            
            #line 46 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( property.TypeName ));
            
            #line default
            #line hidden
            
            #line 46 ""
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 46 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( property.Name ));
            
            #line default
            #line hidden
            
            #line 46 ""
            this.Write(" { get; set; }\n");
            
            #line default
            #line hidden
            
            #line 47 ""
 } 
            
            #line default
            #line hidden
            
            #line 48 ""
            this.Write("\n        public ");
            
            #line default
            #line hidden
            
            #line 49 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( Page.Name ));
            
            #line default
            #line hidden
            
            #line 49 ""
            this.Write("ViewModel\n        (\n");
            
            #line default
            #line hidden
            
            #line 51 ""
 
        string sep = " ";
        foreach (var service in Project.Services.Where(s => s.IsDefault)) { 

            
            #line default
            #line hidden
            
            #line 55 ""
            this.Write("          ");
            
            #line default
            #line hidden
            
            #line 55 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( sep));
            
            #line default
            #line hidden
            
            #line 55 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( service.FullTypeName ));
            
            #line default
            #line hidden
            
            #line 55 ""
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 55 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( service.DisplayName ));
            
            #line default
            #line hidden
            
            #line 55 ""
            this.Write("\n");
            
            #line default
            #line hidden
            
            #line 56 ""
 sep = ",";} 

          foreach (var pageService in Page.Services) { var service = Page.GetService(Project,pageService);

            
            #line default
            #line hidden
            
            #line 60 ""
            this.Write("          ");
            
            #line default
            #line hidden
            
            #line 60 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( sep));
            
            #line default
            #line hidden
            
            #line 60 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( service.FullTypeName ));
            
            #line default
            #line hidden
            
            #line 60 ""
            this.Write(" ");
            
            #line default
            #line hidden
            
            #line 60 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( service.DisplayName ));
            
            #line default
            #line hidden
            
            #line 60 ""
            this.Write("\n");
            
            #line default
            #line hidden
            
            #line 61 ""
 sep = ",";} 

            
            #line default
            #line hidden
            
            #line 63 ""
            this.Write("        )\n        {\n            //setup default services\n");
            
            #line default
            #line hidden
            
            #line 66 ""
 foreach (var service in Project.Services.Where(s => s.IsDefault)) 
{ 
            
            #line default
            #line hidden
            
            #line 68 ""
            this.Write("            this.");
            
            #line default
            #line hidden
            
            #line 68 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( service.DisplayName ));
            
            #line default
            #line hidden
            
            #line 68 ""
            this.Write(" = ");
            
            #line default
            #line hidden
            
            #line 68 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( service.DisplayName ));
            
            #line default
            #line hidden
            
            #line 68 ""
            this.Write(";\n");
            
            #line default
            #line hidden
            
            #line 69 ""
 } 
            
            #line default
            #line hidden
            
            #line 70 ""
            this.Write("\n            //setup other services\n");
            
            #line default
            #line hidden
            
            #line 72 ""
 foreach (var pageService in Page.Services) { var service = Page.GetService(Project,pageService); 
  
            
            #line default
            #line hidden
            
            #line 74 ""
            this.Write("            this.");
            
            #line default
            #line hidden
            
            #line 74 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( service.DisplayName ));
            
            #line default
            #line hidden
            
            #line 74 ""
            this.Write(" = ");
            
            #line default
            #line hidden
            
            #line 74 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( service.DisplayName ));
            
            #line default
            #line hidden
            
            #line 74 ""
            this.Write(";\n");
            
            #line default
            #line hidden
            
            #line 75 ""
 } 
            
            #line default
            #line hidden
            
            #line 76 ""
            this.Write("\n            //setup commands\n");
            
            #line default
            #line hidden
            
            #line 78 ""
 foreach (var command in Page.Commands.Where(c=>!c.ForListItem)) 
{ 
            
            #line default
            #line hidden
            
            #line 80 ""
            this.Write("            ");
            
            #line default
            #line hidden
            
            #line 80 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( command.Name ));
            
            #line default
            #line hidden
            
            #line 80 ""
            this.Write(" = new Command(async () => await Execute");
            
            #line default
            #line hidden
            
            #line 80 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( command.Name ));
            
            #line default
            #line hidden
            
            #line 80 ""
            this.Write("());\n");
            
            #line default
            #line hidden
            
            #line 81 ""
 } 
            
            #line default
            #line hidden
            
            #line 82 ""
            this.Write("  \n            Setup();\n        }\n\n");
            
            #line default
            #line hidden
            
            #line 86 ""
 
bool writeCommandCode = true;
foreach (var command in Page.Commands.Where(c=>!c.ForListItem)) 
{ 
    writeCommandCode = !FileContainsText(ViewModelFilePath, String.Format("Execute{0}",command.Name));
    if(writeCommandCode)
    {    

            
            #line default
            #line hidden
            
            #line 94 ""
            this.Write("            \n        private async Task Execute");
            
            #line default
            #line hidden
            
            #line 95 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( command.Name ));
            
            #line default
            #line hidden
            
            #line 95 ""
            this.Write("()\n        {\n            try\n            {\n                await this.CoreMethods.DisplayAlert(\"");
            
            #line default
            #line hidden
            
            #line 99 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( command.Name ));
            
            #line default
            #line hidden
            
            #line 99 ""
            this.Write("\",\"");
            
            #line default
            #line hidden
            
            #line 99 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( command.Name ));
            
            #line default
            #line hidden
            
            #line 99 ""
            this.Write("\",\"OK\");\n            }\n            catch(Exception ex)\n            {\n                await ErrorManager.DisplayErrorMessageAsync(ex);\n            }\n        }\n");
            
            #line default
            #line hidden
            
            #line 106 ""
 }
} 
            
            #line default
            #line hidden
            
            #line 108 ""
            this.Write(" \n\n");
            
            #line default
            #line hidden
            
            #line 110 ""
 if(Page.Name.ToLower().EndsWith("list")) 
{
    bool writeGetData = true;
    writeGetData = !FileContainsText(ViewModelFilePath, "GetData");
    if(writeGetData)
{
            
            #line default
            #line hidden
            
            #line 116 ""
            this.Write("  \n        public async Task<ObservableCollection<");
            
            #line default
            #line hidden
            
            #line 117 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( Page.Name ));
            
            #line default
            #line hidden
            
            #line 117 ""
            this.Write("ItemViewModel>> GetData()\n        {\n\n            List<");
            
            #line default
            #line hidden
            
            #line 120 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( Page.Name ));
            
            #line default
            #line hidden
            
            #line 120 ""
            this.Write("ItemViewModel> list = new List<");
            
            #line default
            #line hidden
            
            #line 120 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( Page.Name ));
            
            #line default
            #line hidden
            
            #line 120 ""
            this.Write("ItemViewModel>();\n\n            //TODO - set listItems to data source\n            dynamic listItems = null;\n           \n            foreach (var item in listItems)\n            {\n                var model = new ");
            
            #line default
            #line hidden
            
            #line 127 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( Page.Name ));
            
            #line default
            #line hidden
            
            #line 127 ""
            this.Write("ItemViewModel(this);\n                await model.Init(item);\n                list.Add(model);\n\n            }\n\n            ObservableCollection<");
            
            #line default
            #line hidden
            
            #line 133 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( Page.Name ));
            
            #line default
            #line hidden
            
            #line 133 ""
            this.Write("ItemViewModel> retVal = new ObservableCollection<");
            
            #line default
            #line hidden
            
            #line 133 ""
            this.Write(this.ToStringHelper.ToStringWithCulture( Page.Name ));
            
            #line default
            #line hidden
            
            #line 133 ""
            this.Write("ItemViewModel>(list);\n\n            return retVal;\n\n        }\n");
            
            #line default
            #line hidden
            
            #line 138 ""
}}
            
            #line default
            #line hidden
            
            #line 139 ""
            this.Write("    }\n}\n");
            
            #line default
            #line hidden
            return this.GenerationEnvironment.ToString();
        }
        
        public virtual void Initialize() {
        }
    }
    
    public class vmgenBase {
        
        private global::System.Text.StringBuilder builder;
        
        private global::System.Collections.Generic.IDictionary<string, object> session;
        
        private global::System.CodeDom.Compiler.CompilerErrorCollection errors;
        
        private string currentIndent = string.Empty;
        
        private global::System.Collections.Generic.Stack<int> indents;
        
        private ToStringInstanceHelper _toStringHelper = new ToStringInstanceHelper();
        
        public virtual global::System.Collections.Generic.IDictionary<string, object> Session {
            get {
                return this.session;
            }
            set {
                this.session = value;
            }
        }
        
        public global::System.Text.StringBuilder GenerationEnvironment {
            get {
                if ((this.builder == null)) {
                    this.builder = new global::System.Text.StringBuilder();
                }
                return this.builder;
            }
            set {
                this.builder = value;
            }
        }
        
        protected global::System.CodeDom.Compiler.CompilerErrorCollection Errors {
            get {
                if ((this.errors == null)) {
                    this.errors = new global::System.CodeDom.Compiler.CompilerErrorCollection();
                }
                return this.errors;
            }
        }
        
        public string CurrentIndent {
            get {
                return this.currentIndent;
            }
        }
        
        private global::System.Collections.Generic.Stack<int> Indents {
            get {
                if ((this.indents == null)) {
                    this.indents = new global::System.Collections.Generic.Stack<int>();
                }
                return this.indents;
            }
        }
        
        public ToStringInstanceHelper ToStringHelper {
            get {
                return this._toStringHelper;
            }
        }
        
        public void Error(string message) {
            this.Errors.Add(new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message));
        }
        
        public void Warning(string message) {
            global::System.CodeDom.Compiler.CompilerError val = new global::System.CodeDom.Compiler.CompilerError(null, -1, -1, null, message);
            val.IsWarning = true;
            this.Errors.Add(val);
        }
        
        public string PopIndent() {
            if ((this.Indents.Count == 0)) {
                return string.Empty;
            }
            int lastPos = (this.currentIndent.Length - this.Indents.Pop());
            string last = this.currentIndent.Substring(lastPos);
            this.currentIndent = this.currentIndent.Substring(0, lastPos);
            return last;
        }
        
        public void PushIndent(string indent) {
            this.Indents.Push(indent.Length);
            this.currentIndent = (this.currentIndent + indent);
        }
        
        public void ClearIndent() {
            this.currentIndent = string.Empty;
            this.Indents.Clear();
        }
        
        public void Write(string textToAppend) {
            this.GenerationEnvironment.Append(textToAppend);
        }
        
        public void Write(string format, params object[] args) {
            this.GenerationEnvironment.AppendFormat(format, args);
        }
        
        public void WriteLine(string textToAppend) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendLine(textToAppend);
        }
        
        public void WriteLine(string format, params object[] args) {
            this.GenerationEnvironment.Append(this.currentIndent);
            this.GenerationEnvironment.AppendFormat(format, args);
            this.GenerationEnvironment.AppendLine();
        }
        
        public class ToStringInstanceHelper {
            
            private global::System.IFormatProvider formatProvider = global::System.Globalization.CultureInfo.InvariantCulture;
            
            public global::System.IFormatProvider FormatProvider {
                get {
                    return this.formatProvider;
                }
                set {
                    if ((value != null)) {
                        this.formatProvider = value;
                    }
                }
            }
            
            public string ToStringWithCulture(object objectToConvert) {
                if ((objectToConvert == null)) {
                    throw new global::System.ArgumentNullException("objectToConvert");
                }
                global::System.Type type = objectToConvert.GetType();
                global::System.Type iConvertibleType = typeof(global::System.IConvertible);
                if (iConvertibleType.IsAssignableFrom(type)) {
                    return ((global::System.IConvertible)(objectToConvert)).ToString(this.formatProvider);
                }
                global::System.Reflection.MethodInfo methInfo = type.GetMethod("ToString", new global::System.Type[] {
                            iConvertibleType});
                if ((methInfo != null)) {
                    return ((string)(methInfo.Invoke(objectToConvert, new object[] {
                                this.formatProvider})));
                }
                return objectToConvert.ToString();
            }
        }
    }
}

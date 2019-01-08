using System;
using System.Collections.Generic;
using FreshMvvm;

namespace Zenith
{
    public class PageModelMapper : IFreshPageModelMapper
    {
        readonly Dictionary<string, string> viewmodels = new Dictionary<string, string>();

        public PageModelMapper()
        {
            
        }

        public string GetPageTypeName(Type pageModelType)
        {
            string page = pageModelType.AssemblyQualifiedName.Replace("ViewModel", "Page");

            if (viewmodels.ContainsKey(pageModelType.AssemblyQualifiedName))
            {
                page = viewmodels[pageModelType.AssemblyQualifiedName];
            }

            return page;
        }
    }
}

using Sefacan.Core.Entities;
using System.Collections.Generic;

namespace Sefacan.Service
{
    public interface IProjectService
    {
        IEnumerable<Project> GetProjects();
    }
}
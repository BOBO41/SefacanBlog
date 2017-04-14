using Sefacan.Core.Entities;
using Sefacan.Data;
using System.Collections.Generic;
using System.Linq;

namespace Sefacan.Service
{
    public class ProjectService : IProjectService
    {
        #region Fields
        private readonly IRepository<Project> projectRepository;
        #endregion

        #region Ctor
        public ProjectService(IRepository<Project> _projectRepository)
        {
            projectRepository = _projectRepository;
        }
        #endregion

        #region Methods
        public IEnumerable<Project> GetProjects()
        {
            return (from p in projectRepository.TableNoTracking
                    select p).ToList();
        }
        #endregion
    }
}
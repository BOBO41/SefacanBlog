using Sefacan.Core.Entities;
using Sefacan.Data;
using System.Linq;

namespace Sefacan.Service
{
    public class LocalService : ILocalService
    {
        #region Fields
        private readonly IRepository<LocalString> localRepository;
        #endregion

        #region Ctor
        public LocalService(IRepository<LocalString> _localRepository)
        {
            localRepository = _localRepository;
        }
        #endregion

        #region Methods
        public string GetByName(string name)
        {
            name = name.ToLower().Trim();
            return (from l in localRepository.TableNoTracking
                    where l.Name == name
                    select l.Value).FirstOrDefault();
        }
        #endregion
    }
}
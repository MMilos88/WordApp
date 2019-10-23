using Microsoft.EntityFrameworkCore;
using WordApp.DataModel.Entities;
using WordApp.RepositoryInterface;

namespace WordApp.Repository
{
    public class TextRepository : GenericRepository<Text>, ITextRepository
    {
        #region Public constructors
        public TextRepository(DbContext dbContext) : base(dbContext)
        {
        }
        #endregion
    }
}

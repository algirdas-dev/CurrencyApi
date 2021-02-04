using Currency.DB;
using Currency.Helpers.Connections;

namespace Currency.Infrastructure.Repositories
{
    public abstract class BaseRepository
    {
        protected readonly IDatabaseConnectionFactory ConnectionFactory;
        protected readonly ProductContext Context;
        public BaseRepository(IDatabaseConnectionFactory connectionFactory = null, ProductContext context = null) {
            ConnectionFactory = connectionFactory;
            Context = context;
        }
    }
}

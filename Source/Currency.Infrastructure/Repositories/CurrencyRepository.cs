using Currency.DB;
using Currency.DB.Models;
using Currency.Helpers.Connections;
using Currency.Infrastructure.IRepositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Transactions;

namespace Currency.Infrastructure.Repositories
{
    public class CurrencyRepository : BaseRepository, ICurrencyRepository
    {
         
        public CurrencyRepository(IDatabaseConnectionFactory connectionFactory, ProductContext context) :base(connectionFactory: connectionFactory, context:context) {
        }

       
        public void ResetCurrencyRates(List<CurrencyRate> rate, string updatedFrom)
        {
            using (TransactionScope trans = new TransactionScope())
            {
                Context.CurrencyRateUpdateDates.Add(new CurrencyRateUpdateDate
                {
                    UpdatesFrom = updatedFrom,
                    UpdateDate = DateTimeOffset.Now,
                    Updated = true
                });

                Context.CurrencyRates.BulkDelete(Context.CurrencyRates.AsNoTracking().ToList());
                Context.BulkInsert(rate);
                Context.BulkSaveChanges();
                trans.Complete();
            }
        }

        public async Task<List<CurrencyRate>> GetCurrencyRates()
        {
            Context.CurrencyRates.AsNoTracking();
            return await Context.CurrencyRates.ToListAsync().ConfigureAwait(false);
        }
    }
}

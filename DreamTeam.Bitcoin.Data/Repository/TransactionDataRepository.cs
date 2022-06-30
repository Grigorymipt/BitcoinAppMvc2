using BitcoinAppMvc.Data;
using BitcoinAppMvc.Models;
using System.Collections.Generic;
using System.Linq;

namespace DreamTeam.Bitcoin.Data.Repository
{
    public class TransactionDataRepository
        :BaseRepository<TransactionData,BitcoinAppMvcContext>
    {
        public TransactionDataRepository(BitcoinAppMvcContext context)
            :base(context)
        { }

        public IEnumerable<TransactionData>GetByStatus(PublicationStatus status) {
            
            return Set()
                .Where(t => t.PublicationStatus == status).ToList();
        }

    }
}

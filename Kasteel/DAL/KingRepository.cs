using Kasteel.Models;
using Microsoft.EntityFrameworkCore;

namespace Kasteel.DAL
{
    public class KingRepository : IKingRepository
    {
        private readonly KasteelContext context;

        public KingRepository(KasteelContext context)
        {
            this.context = context;
            this.context.Database.EnsureCreated();
        }

        public Task<List<King>> GetAll()
            => context.Kings.ToListAsync();

        public ValueTask<King?> GetByID(int id)
            => context.Kings.FindAsync(id);

        public async Task Insert(King model)
            => await context.Kings.AddAsync(model);

        public void Update(King model)
            => context.Entry(model).State = EntityState.Modified;

        public async Task<bool> Delete(int id)
        {
            var entity = await context.Kings.FindAsync(id);
            if (entity == null) return false;
            context.Kings.Remove(entity);
            return true;
        }

        public async Task Save() => await context.SaveChangesAsync();
    }
}

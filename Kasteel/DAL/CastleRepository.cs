using Kasteel.DAL.Interfaces;
using Kasteel.Models;
using Microsoft.EntityFrameworkCore;

namespace Kasteel.DAL
{
    public class CastleRepository : ICastleRepository
    {
        private readonly KasteelContext context;

        public CastleRepository(KasteelContext context)
        {
            this.context = context;
            this.context.Database.EnsureCreated();
        }

        public Task<List<Castle>> GetAll()
            => context.Castles
                .Include(i => i.Kings)
                .ToListAsync();

        public Task<Castle?> GetByID(int id)
            => context.Castles
                .Include(i => i.Kings)
                .FirstOrDefaultAsync(x => x.Id == id);

        public async Task Insert(Castle model)
            => await context.Castles.AddAsync(model);

        public void Update(Castle model)
            => context.Entry(model).State = EntityState.Modified;

        public async Task<bool> Delete(int id)
        {
            var entity = await context.Castles.FindAsync(id);
            if (entity == null) return false;
            context.Castles.Remove(entity);
            return true;
        }

        public async Task Save() => await context.SaveChangesAsync();
    }
}

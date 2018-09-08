using CactusGuru.Infrastructure.Persistance;
using CactusGuru.Infrastructure.Utils;
using CactusGuru.Persistance.Entities;

namespace CactusGuru.Persistance
{
    public class UnitOfWork : IUnitOfWork
    {
        public UnitOfWork(CactusGuruEntities context)
        {
            ArgumentChecker.CheckNull(context);
            _context = context;
        }

        private readonly CactusGuruEntities _context;

        public void SaveChanges()
        {
            _context.SaveChanges();
        }
    }
}

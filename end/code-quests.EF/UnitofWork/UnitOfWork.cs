using code_quests.Core.Interfaces;
using code_quests.EF.Models.DATA;
using code_quests.EF.Repository;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace code_quests.EF.UnitofWork
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext context;

        public UnitOfWork(AppDbContext _context)
        {
            context = _context;
        }
        public void Dispose()
        {
            context.Dispose();
        }

        public IBaseRepo<T> Repository<T>() where T : class
        {
            return new BaseRepo<T>(context);
        }

        public int SaveChanges()
        {
            return context.SaveChanges();
        }
    }
}

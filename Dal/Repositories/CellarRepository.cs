using Dal.IRepositories;
using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.Repositories
{
    public class CellarRepository : ICellarRepository
    {
        private readonly CellarContext context;

        public CellarRepository(CellarContext context)
        {
            this.context = context;
        }

        public List<Cellar> GetAllCellars()
        {
            return context.Cellars.ToList();
        }

        public Cellar GetCellarById(int id)
        {
            return context.Cellars.Find(id);
        }

        public List<Cellar> SearchCellarsByName(string name)
        {
            return context.Cellars
                .Where(c => c.CellarName.Contains(name))
                .ToList();
        }

        public List<Cellar> GetCellarsByModel(int modelId)
        {
            return context.Cellars.Where(c => c.CellarModelId == modelId).ToList();
        }

        public List<Cellar> GetCellarsByCategory(int categoryId)
        {
            return context.Cellars.Where(c => c.CellarCategoryId == categoryId).ToList();
        }

        public void AddCellar(Cellar cellar)
        {
            context.Cellars.Add(cellar);
            context.SaveChanges();
        }

        public void UpdateCellar(Cellar cellar)
        {
            context.Cellars.Update(cellar);
            context.SaveChanges();
        }

        public void RemoveCellar(Cellar cellar)
        {
            context.Cellars.Remove(cellar);
            context.SaveChanges();
        }
    }
}

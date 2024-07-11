using DomainModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dal.IRepositories
{
    public interface ICellarRepository
    {
        List<Cellar> GetAllCellars();
        Cellar GetCellarById(int id);
        List<Cellar> SearchCellarsByName(string name);
        List<Cellar> GetCellarsByModel(int modelId);
        List<Cellar> GetCellarsByCategory(int categoryId);
        void AddCellar(Cellar cellar);
        void UpdateCellar(Cellar cellar);
        void RemoveCellar(Cellar cellar);
    }
}

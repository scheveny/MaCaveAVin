﻿using DomainModel;
using Dal;
using System.Linq;

namespace Dal.Services
{
    public class BottlePositionService : IBottlePositionService
    {
        private readonly CellarContext context;

        public BottlePositionService(CellarContext context)
        {
            this.context = context;
        }

        public (int, int)? FindFirstAvailablePosition(int cellarId)
        {
            var cellar = context.Cellars.Find(cellarId);
            if (cellar == null)
                return null;

            int maxDrawers = cellar.NbRow;
            int maxStacksPerDrawer = cellar.NbStackRow;

            for (int drawer = 1; drawer <= maxDrawers; drawer++)
            {
                for (int stack = 1; stack <= maxStacksPerDrawer; stack++)
                {
                    bool isOccupied = context.Bottles.Any(b => b.DrawerNb == drawer && b.StackInDrawerNb == stack && b.CellarId == cellarId);
                    if (!isOccupied)
                    {
                        return (drawer, stack);
                    }
                }
            }

            return null; // No available position found
        }
    }
}

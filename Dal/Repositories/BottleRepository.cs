﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dal;
using DomainModel;
using Dal.Repositories;
using Dal.IRepositories;
using Microsoft.EntityFrameworkCore;


namespace Dal.Repositories
{
    public class BottleRepository(CellarContext context) : IBottleRepository
    {
        public async Task<List<Bottle>> GetAllAsync()
        {
            return await context.Bottles.ToListAsync();
        }

        public async Task<Bottle> GetByIdAsync(int id)
        {
            return await context.Bottles.FindAsync(id);
        }

        public async Task<List<Bottle>> GetBottlesByCellarIdAsync(int cellarId)
        {
            return await context.Bottles.Where(b => b.CellarId == cellarId).ToListAsync();
        }

        public async Task<Bottle> PostAsync(Bottle bottle)
        {
            context.Bottles.Add(bottle);
            await context.SaveChangesAsync();

            return bottle;
        }

        public async Task<List<Bottle>> GetBottlesByUserIdAsync(int userId)
        {
            var userCellars = await context.Cellars.Where(c => c.User.UserId == userId).Select(c => c.CellarId).ToListAsync();
            return await context.Bottles.Where(b => userCellars.Contains(b.CellarId)).ToListAsync();
        }


        public async Task<Bottle> UpdateAsync(Bottle bottle)
        {
            context.Bottles.Update(bottle);
            await context.SaveChangesAsync();
            return bottle;
        }

        public async Task<Bottle> RemoveAsync(int id)
        {
            var bottle = await context.Bottles.FindAsync(id);
            if (bottle != null)
            {
                context.Bottles.Remove(bottle);
                await context.SaveChangesAsync();
            }
            return bottle;
        }

    }
}
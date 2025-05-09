﻿using FinanceApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApp.Data.Service
{
    public class ExpensesService : IExpensesService
    {
        private readonly FinanceAppContext _context;

        public ExpensesService(FinanceAppContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Expense>> GetAll()
        {
            return await _context.Expenses.ToListAsync();
        }

        public async Task<Expense> GetById(int id)
        {
            return await _context.Expenses.FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Add(Expense expense)
        {
            _context.Expenses.Add(expense);
            await _context.SaveChangesAsync();
        }

        public async Task Update(Expense expense)
        {
            _context.Expenses.Update(expense);
            await _context.SaveChangesAsync();
        }


        public async Task Delete(int id)
        {
            var expense = await _context.Expenses.FindAsync(id);
            if (expense != null)
            {
                _context.Expenses.Remove(expense);
                await _context.SaveChangesAsync();
            }
        }

        public IQueryable<object> GetChartData()
        {
            return _context.Expenses
                .GroupBy(e => e.Category)
                .Select(g => new
                {
                    Category = g.Key,
                    TotalAmount = g.Sum(e => e.Amount)
                });
        }

    }
}

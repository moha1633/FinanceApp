using FinanceApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace FinanceApp.Data.Service
{
    public interface IExpensesService
    {
        Task<IEnumerable<Expense>> GetAll();
        Task<Expense> GetById(int id);
        Task Add(Expense expense);
        Task Update(Expense expense);
        Task Delete(int id);

        // Om du använder diagram
        IQueryable<object> GetChartData();
    }
}

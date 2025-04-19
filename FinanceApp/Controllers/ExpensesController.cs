using FinanceApp.Data.Service;
using FinanceApp.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FinanceApp.Controllers
{
    public class ExpensesController : Controller
    {
        private readonly IExpensesService _expensesService;

        public ExpensesController(IExpensesService expensesService)
        {
            _expensesService = expensesService;
        }

        // GET: Index
        public async Task<IActionResult> Index()
        {
            var expenses = await _expensesService.GetAll();
            return View(expenses);
        }

        // GET: Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Expense expense)
        {
            if (ModelState.IsValid)
            {
                await _expensesService.Add(expense);
                return RedirectToAction("Index");
            }
            return View(expense);
        }

        // GET: Edit
        public async Task<IActionResult> Edit(int id)
        {
            var expense = await _expensesService.GetById(id);
            if (expense == null)
            {
                return NotFound();
            }
            return View(expense);
        }

        // POST: Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Expense expense)
        {
            if (id != expense.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _expensesService.Update(expense);
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ExpenseExists(expense.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }

                // Efter uppdatering, navigera tillbaka till Index-sidan
                return RedirectToAction("Index");
            }

            return View(expense);
        }

        // DELETE: Bekräftelsesida
        public async Task<IActionResult> Delete(int id)
        {
            var expense = await _expensesService.GetById(id);
            if (expense == null) return NotFound();

            return View(expense);
        }

        // DELETE: POST
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _expensesService.Delete(id);
            return RedirectToAction(nameof(Index));
        }

        // Kontrollera om en utgift med det angivna id finns
        private bool ExpenseExists(int id)
        {
            return _expensesService.GetAll().Result.Any(e => e.Id == id);
        }
    }
}

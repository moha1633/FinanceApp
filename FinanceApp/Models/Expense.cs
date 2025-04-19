using System.ComponentModel.DataAnnotations;

namespace FinanceApp.Models
{
    public class Expense
    {
        public int Id { get; set; }
        [Required]
      
        public string Description { get; set; } = null!;
        [Required]
        [Range(0.01 , double.MaxValue, ErrorMessage ="Beloppet måste vara större än  0")]
        
        public decimal Amount { get; set; }

        public string Category { get; set; } = null!;
        public DateTime Date { get; set; }= DateTime.Now;
    }

}

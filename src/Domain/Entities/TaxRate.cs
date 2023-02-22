using System.ComponentModel.DataAnnotations;

namespace Domain.Entities
{
    public class TaxRate
    {
        [Key]
        public int Id { get; set; }
        public double Rate { get; set; }
        public int LimitInMonths { get; set; }
    }
}

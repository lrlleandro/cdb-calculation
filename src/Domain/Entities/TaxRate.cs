using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class TaxRate
    {
        [Key]
        public int Id { get; set; }
        public double Rate { get; set; }
        public int LimitInMonths { get; set; }
    }
}

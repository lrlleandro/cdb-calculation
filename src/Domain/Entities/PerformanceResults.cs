using System.Diagnostics.CodeAnalysis;

namespace Domain.Entities
{
    [ExcludeFromCodeCoverage]
    public class PerformanceResults
    {
        public double Value { get; set; }
        public double ValueWithTax { get; set; }
    }
}

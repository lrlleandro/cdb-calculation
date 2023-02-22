using FluentValidation;
using System.Runtime.ConstrainedExecution;

namespace Application.Features.SimulatePerformance
{
    public class SimulatePerformanceCommandValidator : AbstractValidator<SimulatePerformanceCommand>
    {
        public SimulatePerformanceCommandValidator()
        {
            RuleFor(v => v.InitialValue)
                .GreaterThan(0)
                .WithMessage("O Valor inicial deve ser superior a '0'.");

            RuleFor(v => v.TermInMonths)
                .GreaterThan(1)
                .WithMessage("O Prazo deve ser superior a '1'.");
        }
    }
}
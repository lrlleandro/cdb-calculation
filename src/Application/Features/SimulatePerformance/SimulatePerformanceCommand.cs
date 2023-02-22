using Application.Common.Exceptions;
using Application.Common.Interfaces;
using Domain.Entities;
using FluentValidation;
using MediatR;
using ValidationException = Application.Common.Exceptions.ValidationException;

namespace Application.Features.SimulatePerformance
{
    public record SimulatePerformanceCommand: IRequest<PerformanceResults>
    {
        public int InitialValue { get; init; }
        public int TermInMonths { get; init; }
    }

    public class SimulatePerformanceCommandHandler : IRequestHandler<SimulatePerformanceCommand, PerformanceResults>
    {
        private readonly IApplicationDbContext _context;
        private readonly IValidator<SimulatePerformanceCommand> _validator;

        private const double CDI_RATE = -0.009;
        private const double PERCENT_CDI_PAID_BANK = -1.08;


        public SimulatePerformanceCommandHandler(IApplicationDbContext context, IValidator<SimulatePerformanceCommand> validator)
        {
            _context = context;
            _validator = validator;
        }

        public Task<PerformanceResults> Handle(SimulatePerformanceCommand request, CancellationToken cancellationToken)
        {
            var validationResults = _validator.Validate(request);
            if (!validationResults.IsValid)
                throw new ValidationException(validationResults.Errors);

            var value = request.InitialValue * Math.Pow((1 + (CDI_RATE * PERCENT_CDI_PAID_BANK)), request.TermInMonths);
            var taxRate = _context.TaxRates
                .OrderBy(x => x.LimitInMonths)
                .FirstOrDefault(x => request.TermInMonths <= x.LimitInMonths);

            var tax = (value - request.InitialValue) * taxRate?.Rate ?? 1;

            return Task.FromResult(new PerformanceResults
            {
                Value = value,
                ValueWithTax = value - tax
            });
        }
    }
}

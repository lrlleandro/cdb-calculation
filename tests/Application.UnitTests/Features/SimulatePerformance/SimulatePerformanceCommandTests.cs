using Application.Common.Interfaces;
using Application.Features.SimulatePerformance;
using Domain.Entities;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.EntityFrameworkCore;
using Moq;
using NUnit.Framework;
using ValidationException = Application.Common.Exceptions.ValidationException;

namespace Application.UnitTests.Features.SimulatePerformance
{
    [TestFixture]
    public class SimulatePerformanceCommandHandlerTests
    {
        private TaxRate[] _taxRates;
        private Mock<IApplicationDbContext> _contextMock;
        private Mock<IValidator<SimulatePerformanceCommand>> _validatorMock;
        private SimulatePerformanceCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _taxRates = new TaxRate[]
                {
                    new TaxRate { Rate = 0.225, LimitInMonths = 6 },
                    new TaxRate { Rate = 0.2, LimitInMonths = 12 },
                    new TaxRate { Rate = 0.175, LimitInMonths = 24 },
                    new TaxRate { Rate = 0.15, LimitInMonths = int.MaxValue }
                };
            _contextMock = new Mock<IApplicationDbContext>();
            _validatorMock = new Mock<IValidator<SimulatePerformanceCommand>>();
            _handler = new SimulatePerformanceCommandHandler(_contextMock.Object, _validatorMock.Object);
        }

        [Test]
        public void HandleWhenValidationFailsShouldThrowValidationException()
        {
            // Arrange
            var command = new SimulatePerformanceCommand();
            var validationResults = new ValidationResult(new[] { new ValidationFailure("InitialValue", "error message") });
            _validatorMock.Setup(x => x.Validate(command)).Returns(validationResults);

            // Act
            var ex = Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

            //Assert
            Assert.AreEqual(ex?.Errors[nameof(command.InitialValue)][0], validationResults.Errors[0].ErrorMessage);
        }

        [Test]
        public async Task HandleWhenTermInMonthIs6ShouldReturnPerformanceResults()
        {
            // Arrange
            var command = new SimulatePerformanceCommand { InitialValue = 1000, TermInMonths = 6 };
            _validatorMock.Setup(x => x.Validate(command)).Returns(new ValidationResult());
            _contextMock.Setup(x => x.TaxRates).Returns(GetDbSetMock(_taxRates).Object);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            var expectedValue = 1059.7556770148981;
            var expectedValueWithTax = 1046.310649686546;
            Assert.AreEqual(result.Value, expectedValue);
            Assert.AreEqual(result.ValueWithTax, expectedValueWithTax);
        }

        [Test]
        public async Task HandleWhenTermInMonthIs12ShouldReturnPerformanceResults()
        {
            // Arrange
            var command = new SimulatePerformanceCommand { InitialValue = 1000, TermInMonths = 12 };
            _validatorMock.Setup(x => x.Validate(command)).Returns(new ValidationResult());
            _contextMock.Setup(x => x.TaxRates).Returns(GetDbSetMock(_taxRates).Object);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            var expectedValue = 1123.082094965305;
            var expectedValueWithTax = 1098.465675972244;
            Assert.AreEqual(result.Value, expectedValue);
            Assert.AreEqual(result.ValueWithTax, expectedValueWithTax);
        }

        [Test]
        public async Task HandleWhenTermInMonthIs24ShouldReturnPerformanceResults()
        {
            // Arrange
            var command = new SimulatePerformanceCommand { InitialValue = 1000, TermInMonths = 24 };
            _validatorMock.Setup(x => x.Validate(command)).Returns(new ValidationResult());
            _contextMock.Setup(x => x.TaxRates).Returns(GetDbSetMock(_taxRates).Object);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            var expectedValue = 1261.3133920316586;
            var expectedValueWithTax = 1215.5835484261183;
            Assert.AreEqual(result.Value, expectedValue);
            Assert.AreEqual(result.ValueWithTax, expectedValueWithTax);
        }

        [Test]
        public async Task HandleWhenTermInMonthIsGreaterThan24ShouldReturnPerformanceResults()
        {
            // Arrange
            var command = new SimulatePerformanceCommand { InitialValue = 1000, TermInMonths = 48 };
            _validatorMock.Setup(x => x.Validate(command)).Returns(new ValidationResult());
            _contextMock.Setup(x => x.TaxRates).Returns(GetDbSetMock(_taxRates).Object);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            var expectedValue = 1590.9114729184084;
            var expectedValueWithTax = 1502.2747519806471;
            Assert.AreEqual(result.Value, expectedValue);
            Assert.AreEqual(result.ValueWithTax, expectedValueWithTax);
        }

        private static Mock<DbSet<T>> GetDbSetMock<T>(IEnumerable<T> data) where T : class
        {
            var queryable = data.AsQueryable();
            var dbSetMock = new Mock<DbSet<T>>();

            dbSetMock.As<IQueryable<T>>().Setup(m => m.Provider).Returns(queryable.Provider);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.Expression).Returns(queryable.Expression);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.ElementType).Returns(queryable.ElementType);
            dbSetMock.As<IQueryable<T>>().Setup(m => m.GetEnumerator()).Returns(() => queryable.GetEnumerator());

            return dbSetMock;
        }
    }
}
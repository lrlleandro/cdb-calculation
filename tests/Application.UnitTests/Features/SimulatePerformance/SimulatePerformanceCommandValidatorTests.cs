using Application.Features.SimulatePerformance;
using NUnit.Framework;

namespace Application.UnitTests.Features.SimulatePerformance
{
    [TestFixture]
    public class SimulatePerformanceCommandValidatorTests
    {
        private SimulatePerformanceCommandValidator _validator;

        [SetUp]
        public void SetUp()
        {
            _validator = new SimulatePerformanceCommandValidator();
        }

        [Test]
        public void InitialValueShouldBeGreaterThanZero()
        {
            // Arrange
            var command = new SimulatePerformanceCommand { InitialValue = 0 };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors[0].ErrorMessage, "O Valor inicial deve ser superior a '0'.");
        }

        [Test]
        public void TermInMonths_ShouldBeGreaterThanOne()
        {
            // Arrange
            var command = new SimulatePerformanceCommand { InitialValue = 1, TermInMonths = 1 };

            // Act
            var result = _validator.Validate(command);

            // Assert
            Assert.IsFalse(result.IsValid);
            Assert.AreEqual(result.Errors[0].ErrorMessage, "O Prazo deve ser superior a '1'.");
        }
    }
}

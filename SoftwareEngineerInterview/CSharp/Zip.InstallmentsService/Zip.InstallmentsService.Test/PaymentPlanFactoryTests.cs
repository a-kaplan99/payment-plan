using Shouldly;
using System;
using System.Linq;
using Xunit;

namespace Zip.InstallmentsService.Test
{
    public class PaymentPlanFactoryTests
    {
        [Fact]
        public void WhenCreatePaymentPlanWithValidOrderAmount_ShouldReturnValidPaymentPlan()
        {
            // Arrange
            var paymentPlanFactory = new PaymentPlanFactory();
            
            // Act
            var paymentPlan = PaymentPlanFactory.CreatePaymentPlan(123.45M);

            // Assert
            paymentPlan.ShouldNotBeNull();
        }

        [Fact]
        public void WhenCreatePaymentPlanWithInvalidOrderAmount_ShouldReturnNull()
        {
            // Arrange
            var paymentPlanFactory = new PaymentPlanFactory();

            // Act
            var paymentPlan1 = PaymentPlanFactory.CreatePaymentPlan(-123.45M);
            var paymentPlan2 = PaymentPlanFactory.CreatePaymentPlan(0);

            // Assert
            paymentPlan1.ShouldBeNull();
            paymentPlan2.ShouldBeNull();
        }

        [Fact]
        public void WhenCreatePaymentPlanWithValidOrderAmount_ShouldReturnPaymentPlanWithDEfaultPurchaseAmountAndInstallments()
        {
            // Arrange
            var paymentPlanFactory = new PaymentPlanFactory();
            var expectedAmount = 200M;
            var expecedNumInstallments = 4;
            var expectedFrequency = 21;

            // Act
            var paymentPlan = PaymentPlanFactory.CreatePaymentPlan(expectedAmount);

            // Assert
            paymentPlan.ShouldNotBeNull();
            paymentPlan.PurchaseAmount.ShouldBe(expectedAmount);
            paymentPlan.Installments.Length.ShouldBe(expecedNumInstallments);
            paymentPlan.Installments.Sum(i  => i.Amount).ShouldBe(expectedAmount);

            // Assert Installments
            var now = DateTime.Today;
            for (var i = 1; i <= expecedNumInstallments; i++)
            {
                var installment = paymentPlan.Installments[i - 1];
                installment.DueDate.ShouldBe(now.AddDays(expectedFrequency * i));
            }
        }
    }
}

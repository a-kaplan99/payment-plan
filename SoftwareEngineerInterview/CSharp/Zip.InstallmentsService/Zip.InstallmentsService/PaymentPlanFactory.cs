using System;

namespace Zip.InstallmentsService
{
    /// <summary>
    /// This class is responsible for building the PaymentPlan according to the Zip product definition.
    /// </summary>
    public class PaymentPlanFactory
    {
        // Default Settings for payment plan
        const int FREQUENCY = 14;
        const int NUM_INSTALLMENTS = 4;

        /// <summary>
        /// Builds the PaymentPlan instance.
        /// </summary>
        /// <param name="purchaseAmount">The total amount for the purchase that the customer is making.</param>
        /// <returns>The PaymentPlan created with all properties set.</returns>
        public static PaymentPlan CreatePaymentPlan(decimal purchaseAmount)
        {
            // Error scenario
            if (purchaseAmount <= 0)
            {
                Console.WriteLine("Cannot compute payment plan for amount less than or equal to 0.");
                return null;
            }

            return new PaymentPlan
            {
                Id = Guid.NewGuid(),
                PurchaseAmount = purchaseAmount,
                Installments = GenerateInstallments(purchaseAmount, NUM_INSTALLMENTS, FREQUENCY)
            }; ;
        }

        // Helper Methods

        private static Installment[] GenerateInstallments(decimal purchaseAmount, int numInstallments, int frequency)
        {
            // One-time computed values
            var installments = new Installment[numInstallments];
            var startDate = DateTime.Today;
            var amountPerInstallment = purchaseAmount / numInstallments;

            // Compute and add installments
            for (int i = 1; i <= numInstallments; i++)
            {
                installments[i - 1] = new Installment
                {
                    Id = Guid.NewGuid(),
                    Amount = amountPerInstallment,
                    DueDate = startDate.AddDays(frequency * i)
                };
            }

            return installments;
        }
    }
}

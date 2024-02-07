using System;

namespace Zip.InstallmentsService
{
    /// <summary>
    /// This class is responsible for building the PaymentPlan according to the Zip product definition.
    /// </summary>
    public class PaymentPlanFactory
    {
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

            return new PaymentPlan(purchaseAmount);
        }
    }
}

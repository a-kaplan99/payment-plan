using System;

namespace Zip.InstallmentsService
{
    /// <summary>
    /// Data structure which defines all the properties for a purchase installment plan.
    /// </summary>
    public class PaymentPlan
    {
        public Guid Id { get; set; }

		public decimal PurchaseAmount { get; set; }

        public Installment[] Installments { get; set; }

        // Private Fields

        private int NumInstallments = 4;

        private int Frequency = 21; // in days

        public PaymentPlan(decimal purchaseAmount) {
            Id = Guid.NewGuid();
            PurchaseAmount = purchaseAmount;
            Installments = new Installment[NumInstallments];
            GenerateInstallments();
        }

        private void GenerateInstallments()
        {
            var now = DateTime.Today;
            var amountPerInstallment = Math.Round(PurchaseAmount / NumInstallments, 2, MidpointRounding.ToZero);
            decimal remainder = PurchaseAmount % (amountPerInstallment * NumInstallments);

            for (int i = 0; i < NumInstallments; i++)
            {
                now = now.AddDays(Frequency);

                Installments[i] = i == 0 
                    ? new Installment(amountPerInstallment + remainder, now) // For first installment, add remainder lost during division
                    : new Installment(amountPerInstallment, now);
            }
        }
    }
}

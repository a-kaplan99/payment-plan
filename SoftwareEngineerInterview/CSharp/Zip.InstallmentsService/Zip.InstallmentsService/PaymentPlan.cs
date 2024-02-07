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

        private int Frequency = 14; // in days

        public PaymentPlan(decimal purchaseAmount) {
            Id = Guid.NewGuid();
            PurchaseAmount = purchaseAmount;
            Installments = new Installment[NumInstallments];
            GenerateInstallments();
        }

        private void GenerateInstallments()
        {
            var now = DateTime.Today;
            var amountPerInstallment = PurchaseAmount / NumInstallments;
            for (int i = 0; i < NumInstallments; i++)
            {
                now = now.AddDays(Frequency);
                Installments[i] = new Installment(amountPerInstallment, now);
            }
        }
    }
}

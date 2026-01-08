using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Net.Mail;
using System.Reflection.Emit;
using System.Text;

namespace OrderingDomain.ValueObjects
{
    public record Payment
    {
        public string? CardName { get; } = default!;
        public string CardNumber { get; } = default!;
        public string Expiration { get; } = default!;
        public string CVV { get; } = default!;
        public int PaymentMethod { get; } = default!;

        //Required for EF Core
        protected Payment()
        {

        }

        private Payment(string cardName, string cardNumber, string exp, string cvv, int method)
        {
            CardName = cardName;
            CardNumber = cardNumber;
            Expiration = exp;
            CVV = cvv;
            PaymentMethod = method;
        }

        public static Payment Of(string cardName, string cardNumber, string exp, string cvv, int method)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(cardName);
            ArgumentException.ThrowIfNullOrWhiteSpace(cardNumber);
            ArgumentException.ThrowIfNullOrWhiteSpace(cvv);
            ArgumentOutOfRangeException.ThrowIfGreaterThan(cvv.Length, 3);

            return new Payment(cardName, cardNumber, exp, cvv, method);
        }
    }
}

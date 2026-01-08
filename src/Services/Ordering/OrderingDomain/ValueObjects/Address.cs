using System;
using System.Collections.Generic;
using System.Text;

namespace OrderingDomain.ValueObjects
{
    public record Address
    {
        public string FirstName { get; } = default!;
        public string LastName { get; } = default!;
        public string? EmailAddress { get; } = default;
        public string AddressLine { get; } = default!;
        public string Country { get; } = default!;
        public string State { get; } = default!;
        public string ZipCode { get; } = default!;

        //Required for EF Core
        protected Address()
        {
            
        }

        private Address(string firstName, string lastName, string email, string addr, string country, string state, string zip)
        {
            FirstName = firstName;
            LastName = lastName;
            EmailAddress = email;
            AddressLine = addr;
            Country = country;
            State = state;
            ZipCode = zip;
        }

        public static Address Of(string firstName, string lastName, string email, string addr, string country, string state, string zip)
        {
            ArgumentException.ThrowIfNullOrWhiteSpace(email);
            ArgumentException.ThrowIfNullOrWhiteSpace(addr);

            return new Address(firstName, lastName, email, addr, country, state, zip);
        }
    }
}

using Altkom.DotnetCore.Models;
using Bogus;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace Altkom.DotnetCore.FakeServices
{
    // add package Bogus
    public class CustomerFaker : Faker<Customer>
    {
        public CustomerFaker()
        {
            StrictMode(true);
            RuleFor(p => p.Id, f => f.IndexFaker);
            RuleFor(p => p.Gender, f => f.PickRandom<Gender>());
            RuleFor(p => p.FirstName, (f, p) => f.Name.FirstName((Bogus.DataSets.Name.Gender) p.Gender));
            RuleFor(p => p.LastName, (f, p) => f.Name.LastName((Bogus.DataSets.Name.Gender)p.Gender));
            RuleFor(p => p.EMail, (f, p) => $"{p.FirstName}.{p.LastName}@{f.Internet.DomainName()}");
            RuleFor(p => p.IsDeleted, f => f.Random.Bool(0.8f));
            Ignore(p => p.IsSelected);
            FinishWith((f, c) => Trace.WriteLine($"Customer {c.FirstName} {c.LastName} {c.Gender} <{c.EMail}>) was created."));


        }
    }
}

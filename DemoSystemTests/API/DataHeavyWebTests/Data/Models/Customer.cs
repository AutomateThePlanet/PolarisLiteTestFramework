﻿namespace DemoSystemTests.Builder;
public class Customer
{
    public Customer() => Invoices = new HashSet<Invoice>();

    public int CustomerId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Company { get; set; }
    public string Address { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string Country { get; set; }
    public string PostalCode { get; set; }
    public string Phone { get; set; }
    public string Fax { get; set; }
    public string Email { get; set; }
    public long? SupportRepId { get; set; }

    public Employee SupportRep { get; set; }
    public ICollection<Invoice> Invoices { get; set; }


    public override string ToString()
    {
        var supportRepName = SupportRep != null ? SupportRep.FirstName + " " + SupportRep.LastName : "None";
        return $"CustomerId: {CustomerId}, Name: {FirstName} {LastName}, Company: {Company}, " +
               $"Address: {Address}, City: {City}, State: {State}, Country: {Country}, PostalCode: {PostalCode}, " +
               $"Phone: {Phone}, Fax: {Fax}, Email: {Email}, SupportRep: {supportRepName}, Invoices Count: {Invoices.Count}";
    }
}

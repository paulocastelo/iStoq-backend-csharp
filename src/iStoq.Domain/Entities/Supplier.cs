using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iStoq.Domain.Entities;

public class Supplier
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string CNPJ { get; set; }
    public string Email { get; set; }
    public string Phone { get; set; }

    public Supplier() { }
    public Supplier(string name, string cnpj, string email, string phone)
    {
        Name = name;
        CNPJ = cnpj;
        Email = email;
        Phone = phone;
    }

    public void Update(string name, string cnpj, string email, string phone)
    {
        Name = name;
        CNPJ = cnpj;
        Email = email;
        Phone = phone;
    }
}

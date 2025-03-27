using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iStoq.Domain.Entities;

public class Supplier
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string CNPJ { get; private set; }
    public string Email { get; private set; }
    public string Phone { get; private set; }

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

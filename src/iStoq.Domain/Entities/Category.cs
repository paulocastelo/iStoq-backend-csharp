using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iStoq.Domain.Entities;

public class Category
{
    public Guid Id { get; private set; } = Guid.NewGuid();
    public string Name { get; private set; }
    public string Description { get; private set; }

    public Category(string name, string description)
    {
        Name = name;
        Description = description;
    }

    public Category() { }
    public void Update(string name, string description)
    {
        Name = name;
        Description = description;
    }
}

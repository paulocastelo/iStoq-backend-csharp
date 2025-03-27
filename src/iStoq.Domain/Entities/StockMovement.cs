using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace iStoq.Domain.Entities;

public class StockMovement
{
    public StockMovement(Guid productId, int quantity, string type, string notes)
    {
        ProductId = productId;
        Quantity = quantity;
        Type = type;
        Notes = notes;
    }

    public Guid Id { get; private set; } = Guid.NewGuid();
    public Guid ProductId { get; private set; }
    public int Quantity { get; private set; }
    public string Type { get; private set; } // IN or OUT
    public string Notes { get; private set; }
    public DateTime Date { get; private set; } = DateTime.UtcNow;

    public StockMovement() { }
    public void Update(Guid productId, int quantity, string type, string notes)
    {
        ProductId = productId;
        Quantity = quantity;
        Type = type;
        Notes = notes;
    }
}

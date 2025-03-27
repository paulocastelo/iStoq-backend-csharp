namespace iStoq.Domain.Entities;

public class StockMovement
{
    public Guid Id { get; set; }
    public Guid ProductId { get; set; }
    public Product Product { get; set; } = null!;
    public int Quantity { get; set; }
    public string Type { get; set; } = string.Empty;
    public string Notes { get; set; } = string.Empty;
    public DateTime Date { get; set; }

    // Construtor vazio para EF
    public StockMovement() { }

    // Construtor para uso no código
    public StockMovement(Guid productId, int quantity, string type, string notes, DateTime date)
    {
        ProductId = productId;
        Quantity = quantity;
        Type = type;
        Notes = notes;
        Date = date;
    }

    public StockMovement(Guid productId, int quantity, string type, string notes)
    {
        ProductId = productId;
        Quantity = quantity;
        Type = type;
        Notes = notes;
    }

    // Atualização controlada
    public void Update(Guid productId, int quantity, string type, string notes)
    {
        ProductId = productId;
        Quantity = quantity;
        Type = type;
        Notes = notes;
    }
}

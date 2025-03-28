namespace iStoq.Application.DTOs
{
    public class CategoryDto
    {
        public Guid? Id { get; set; } // Pode ser nulo em criação
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
    }
}

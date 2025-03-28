using iStoq.Application.DTOs;

namespace iStoq.Tests.API.Controllers
{
    internal class StockMovementUpdateDto : StockMovementDto
    {
        public int Quantity { get; set; }
        public string Type { get; set; }
        public string Notes { get; set; }
    }
}
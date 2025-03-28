﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iStoq.Application.DTOs
{
    public class StockMovementDto
    {
        public Guid? Id { get; set; } // Pode ser nulo na criação
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public string Type { get; set; } = string.Empty;
        public string Notes { get; set; } = string.Empty;
        public DateTime? Date { get; set; } // Pode ser nulo na criação
    }
}

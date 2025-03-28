using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iStoq.Application.DTOs
{
    public class SupplierDto
    {
        public Guid? Id { get; set; } // Opcional na criação
        public string Name { get; set; } = string.Empty;
        public string CNPJ { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
    }
}

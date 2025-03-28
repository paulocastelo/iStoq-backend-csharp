using iStoq.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iStoq.Application.Interfaces
{
    public interface IUserService
    {
        Task<User?> Authenticate(string username, string password);
        Task<User> Register(UserDto dto);
        Task<IEnumerable<UserDto>> GetAllAsync();
        Task<UserDto?> UpdateAsync(Guid id, UserDto dto);
        Task<bool> DeleteAsync(Guid id);
        Task<UserDto?> GetByIdAsync(Guid id);
    }
}
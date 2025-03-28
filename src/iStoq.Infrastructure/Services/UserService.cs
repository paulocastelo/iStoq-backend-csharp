using AutoMapper;
using iStoq.Application.DTOs;
using iStoq.Application.Interfaces;
using iStoq.Domain.Entities;
using iStoq.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

public class UserService : IUserService
{
    private readonly AppDbContext _context;
    private readonly IMapper _mapper;

    public UserService(AppDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<User?> Authenticate(string username, string password)
    {
        var user = await _context.Users.FirstOrDefaultAsync(u => u.Username == username);
        if (user is null || !user.ValidatePassword(password)) return null;
        return user;
    }

    public async Task<User> Register(UserDto dto)
    {
        var user = _mapper.Map<User>(dto);
        user.SetPassword(dto.Password);

        _context.Users.Add(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<IEnumerable<UserDto>> GetAllAsync()
    {
        var users = await _context.Users.ToListAsync();

        // Mapeia a lista de entidades para a lista de DTOs
        return _mapper.Map<IEnumerable<UserDto>>(users);
    }

    public async Task<UserDto?> UpdateAsync(Guid id, UserDto dto)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return null;

        user.Username = dto.Username;
        user.Email = dto.Email;

        if (!string.IsNullOrWhiteSpace(dto.Password))
        {
            user.SetPassword(dto.Password);
        }

        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return _mapper.Map<UserDto>(user);
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null) return false;

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<UserDto?> GetByIdAsync(Guid id)
    {
        var user = await _context.Users.FindAsync(id);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

}

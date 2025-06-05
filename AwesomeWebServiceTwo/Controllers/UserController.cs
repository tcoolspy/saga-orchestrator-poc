using AwesomeWebServiceTwo.Data;
using AwesomeWebServiceTwo.Models;
using AwesomeWebServiceTwo.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AwesomeWebServiceTwo.Controllers;

[ApiController]
[Route("[controller]")]
public class UserController(UserDbContext context, ILogger<UserController> logger) : ControllerBase
{
    [HttpGet]
    [Route("api/user")]
    public async Task<IEnumerable<BlogUser?>> GetUsers()
    {
        return await Task.FromResult(context.BlogUsers);
    }

    [HttpGet]
    [Route("api/user/{id}")]
    public async Task<BlogUser?> GetUserById(int id)
    {
        return await context.BlogUsers.FindAsync(id);
    }

    [HttpPost]
    [Route("api/user")]
    public async Task<bool> AddUser(AddUserRequestDto requestDto)
    {
        var newUser = new BlogUser
        {
            FirstName = requestDto.FirstName, LastName = requestDto.LastName, Email = requestDto.Email
        };

        await context.BlogUsers.AddAsync(newUser);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }

    [HttpPut]
    [Route("api/user")]
    public async Task<bool> UpdateUser(UpdateUserRequestDto requestDto)
    {
        var user = await context.BlogUsers.FirstOrDefaultAsync(u => u.Email == requestDto.Email);
        if (user == null) return false;

        user.FirstName = requestDto.FirstName ?? user.FirstName;
        user.LastName = requestDto.LastName ?? user.LastName;
        user.Email = requestDto.Email ?? user.Email;

        context.BlogUsers.Update(user);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }

    [HttpDelete]
    [Route("api/user")]
    public async Task<bool> DeleteUser(int id)
    {
        var user = await context.BlogUsers.FindAsync(id);
        if (user == null) return false;

        context.BlogUsers.Remove(user);
        var changes = await context.SaveChangesAsync();
        return changes > 0;
    }
}
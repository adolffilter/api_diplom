using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Diploma.Auth;
using Diploma.Database;
using Diploma.model.user;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace Diploma.Controllers;

[Route("api/[controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private EfModel _efModel;

    public UserController(EfModel model)
    {
        _efModel = model;
    }

    [Authorize]
    [HttpGet]
    public async Task<ActionResult<User>> Get()
    {
        if (HttpContext.User.Identity is not ClaimsIdentity identity)
            return NotFound();
            
        var id = Convert.ToInt32(identity.FindFirst("Id")?.Value);

        var user = await _efModel.Users
            .Include(u => u.Specialization)
            .FirstOrDefaultAsync(u => u.Id == id);

        return Ok(user);
    }

    [Authorize]
    [HttpPost("Balance")]
    public async Task<ActionResult<int>> PostBalance(int hours)
    {
        if (HttpContext.User.Identity is not ClaimsIdentity identity)
            return NotFound();
            
        var id = Convert.ToInt32(identity.FindFirst("Id")?.Value);

        var user = await _efModel.Users
            .Include(u => u.Specialization)
            .FirstOrDefaultAsync(u => u.Id == id);

        user.Balance += hours * user.Specialization.Salary;
        user.Hourse += hours;
        _efModel.Entry(user).State = EntityState.Modified;
        await _efModel.SaveChangesAsync();
        
        return Ok(user.Balance);
    }

    [HttpPost("/api/Registration")]
    public async Task<ActionResult> PostRegistration(RegistrationDTO userDTO)
    {
        if (userDTO == null)
            return BadRequest();

        var specialization = await _efModel.Specializations.FindAsync(userDTO.SpecializationId);

        if (specialization == null)
            return NotFound();

        _efModel.Users.Add(new User
        {
            Password = userDTO.Password,
            FirstName = userDTO.FirstName,
            LastName = userDTO.LastName,
            Specialization = specialization
        });

        await _efModel.SaveChangesAsync();

        return Ok();
    }
    
    [HttpPost("/api/Authorization")]
    public ActionResult<object> Token(AuthorizationDTO authorization)
    {
        var indentity = GetIdentity(authorization.FirstName, authorization.Password, authorization.LastName);

        if (indentity == null)
        {
            return BadRequest();
        }

        var now = DateTime.UtcNow;
        var jwt = new JwtSecurityToken(
            audience: TokenBaseOptions.AUDIENCE,
            issuer: TokenBaseOptions.ISSUER,
            notBefore: now,
            claims: indentity.Claims,
            expires: now.Add(TimeSpan.FromDays(TokenBaseOptions.LIFETIME)),
            signingCredentials: new SigningCredentials(TokenBaseOptions.GetSymmetricSecurityKey(),
                SecurityAlgorithms.HmacSha256));

        var encodedJwt = new JwtSecurityTokenHandler().WriteToken(jwt);

        var response = new
        {
            access_token = encodedJwt,
            username = indentity.Name,
        };

        return response;
    }
    
    [NonAction]
    public ClaimsIdentity? GetIdentity(string firstName, string password, string lastName)
    {
        var user = _efModel.Users.FirstOrDefault(
            x => x != null && x.FirstName == firstName && x.Password == password && x.LastName == lastName);

        if (user != null)
        {
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, user.FirstName),
                new Claim(ClaimsIdentity.DefaultRoleClaimType, user.Role),
                new Claim("Id", user.Id.ToString())
            };

            ClaimsIdentity claimsIdentity =
                new ClaimsIdentity(claims, "Token", ClaimsIdentity.DefaultNameClaimType,
                    ClaimsIdentity.DefaultRoleClaimType);

            return claimsIdentity;
        }
        
        return null;
    }
}
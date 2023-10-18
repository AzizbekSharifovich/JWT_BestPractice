using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Contracts;
using Entities.DTO.Options; 
using Entities.DTO.User;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace JWT_WebAPI.Controllers;

[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IRepositoryManager repositoryManager;
    private readonly IOptions<AppOption> appOptions;
    private readonly IMapper mapper;
    private readonly JwtSecurityTokenHandler securityTokenHandler;

    public UserController(IRepositoryManager repositoryManager, IOptions<AppOption> appOptions, IMapper mapper)
    {
        this.repositoryManager = repositoryManager ??
            throw new ArgumentNullException(nameof(repositoryManager));
        this.appOptions = appOptions ??
            throw new ArgumentNullException(nameof(repositoryManager));
        this.securityTokenHandler = new JwtSecurityTokenHandler();
        this.mapper = mapper ?? throw new ArgumentNullException(nameof(mapper));
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<ActionResult<UserAuthInfoDTO>> LoginAsync(
        [FromBody] UserCredentials userCredentials,
        CancellationToken cancellationToken)
    {
        var userAuthInfoDTO = new UserAuthInfoDTO();

        if (userCredentials == null)
        {
            return BadRequest("No data");
        }

        var user = await repositoryManager.User
            .LoginAsync( userCredentials.Login, userCredentials.Password,false, cancellationToken);
        
        if(user != null)
        {
            var key = Encoding.ASCII.GetBytes(appOptions.Value.SecretKey);

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(
                    new Claim[]
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                        new Claim(ClaimTypes.GivenName, user.FirstName),
                        new Claim(ClaimTypes.Name, user.LastName),
                        new Claim(ClaimTypes.Role, user.Roles.ToString())
                    }),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials =  new SigningCredentials(
                    new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
                   
            };

            var securityToken = securityTokenHandler.CreateToken(tokenDescriptor);
            userAuthInfoDTO.Token = securityTokenHandler.WriteToken(securityToken);
            userAuthInfoDTO.UserDetails = mapper.Map<UserDTO>(user);
        }

        if (string.IsNullOrEmpty(userAuthInfoDTO?.Token))
        {
            return Unauthorized("Error! The user is not registered.");
        }

        return Ok(userAuthInfoDTO);
    }
}

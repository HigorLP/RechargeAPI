using Microsoft.AspNetCore.Mvc;
using Recharge.Application.DTOs.Users;
using Recharge.Application.Interfaces.Users;

namespace Recharge.API.Controllers.Users;

[Route("Users")]
[ApiController]
public class UserController : ControllerBase {
    private readonly IUserService _userService;

    public UserController(IUserService userService) {
        _userService = userService;
    }

    [HttpPost("register")]
    public async Task<ActionResult> RegisterUser([FromBody] RegisterUserDTO userDTO) {
        var result = await _userService.RegisterUser(userDTO);

        if (result.isSucess) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult> GetUserById(Guid id) {
        var result = await _userService.GetUserById(id);

        if (result.isSucess) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpGet("email/{email}")]
    public async Task<ActionResult> GetUserByEmail(string email) {
        var result = await _userService.GetUserByEmail(email);

        if (result.isSucess) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpGet("cpf/{cpf}")]
    public async Task<ActionResult> GetUserByDocument(string cpf) {
        var result = await _userService.GetUserByDocument(cpf);

        if (result.isSucess) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpGet]
    public async Task<ActionResult> GetAllUsers() {
        var result = await _userService.GetAllUsers();

        if (result.isSucess) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPut("complete/{id}")]
    public async Task<ActionResult> CompleteRegister(Guid id, [FromBody] UserUpdateDTO userDTO) {
        var result = await _userService.CompleteRegister(id, userDTO);

        if (result.isSucess) {
            return Ok(result);
        }

        return BadRequest(result);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateUser(Guid id, [FromBody] UserUpdateDTO userDTO) {
        var result = await _userService.UpdateUser(id, userDTO);

        if (result.isSucess) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteUser(Guid id) {
        var result = await _userService.DeleteUser(id, null);

        if (result.isSucess) {
            return Ok(result);
        }

        return NotFound(result);
    }

    [HttpPost("Login")]
    public async Task<ActionResult> Login([FromBody] LoginDTO loginDTO) {
        var result = await _userService.LogIn(loginDTO);

        if (result != null) {
            return Ok(result);
        }

        return BadRequest(result);
    }
}
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Registration.UserRegistrationEnterpriseExample.Application.Users.GetUser;
using Registration.UserRegistrationEnterpriseExample.Application.Users.Login;
using Registration.UserRegistrationEnterpriseExample.Application.Users.SaveUser;
using Registration.UserRegistrationEnterpriseExample.Application.Users.Vinculate;

namespace Registration.UserRegistrationEnterpriseExample.Presentation.Controllers;

public class UserController : ApiController
{
    private readonly IMediator _mediator;

    public UserController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("/api/new-user")]
    public async Task<SaveUserViewModel> InsertUser([FromBody] SaveUserDto user)
    {
        var request = new SaveUserRequest
        {
            Document = user.Document,
            Name = user.Name,
            UserType = user.UserType,
            Password = user.Password,
            Email = user.Email
        };
        return await _mediator.Send(request);
    }
    
    [HttpGet]
    [Route("/api/login")]
    public async Task<LoginUserViewModel> Login([FromQuery] string email, string password)
    {
        return await _mediator.Send(new LoginUserRequest{Email = email, Password = password});
    }
    
    [HttpGet]
    [Route("/api/get-user")]
    public async Task<GetUserViewModel> GetUser([FromQuery] string email)
    {
        return await _mediator.Send(new GetUserRequest{Email = email});
    }
    
    [HttpPost]
    [Route("/api/vinculate")]
    public async Task<Unit> VinculateWorkshop([FromQuery] string userId, string workshopId)
    {
        return await _mediator.Send(new VinculateWorkshopToUserRequest
        {
            UserId = Guid.Parse(userId),
            WorkshopId = Guid.Parse(workshopId)
        });
    }
}
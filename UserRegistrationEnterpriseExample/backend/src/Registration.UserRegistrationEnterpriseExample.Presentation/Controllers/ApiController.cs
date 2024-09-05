using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Registration.UserRegistrationEnterpriseExample.Presentation.Controllers;

[ApiController]
public abstract class ApiController : ControllerBase
{
    private IMediator _mediator;

    protected IMediator Mediator => _mediator ??= HttpContext.RequestServices.GetService<IMediator>();
}
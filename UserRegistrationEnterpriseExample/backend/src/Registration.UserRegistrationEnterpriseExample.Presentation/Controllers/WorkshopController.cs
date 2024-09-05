using MediatR;
using Microsoft.AspNetCore.Mvc;
using Registration.UserRegistrationEnterpriseExample.Application.Workshops.Get;
using Registration.UserRegistrationEnterpriseExample.Application.Workshops.Persistir;

namespace Registration.UserRegistrationEnterpriseExample.Presentation.Controllers;

public class WorkshopController : ApiController
{
    private readonly IMediator _mediator;

    public WorkshopController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost]
    [Route("/new-workshop")]
    public async Task<ActionResult> InsertWorkshop([FromBody] SaveWorkshopDto input)
    {
        var request = new SaveWorkshopRequest
        {
            Address = input.Address,
            Date = input.Date,
            Description = input.Description,
            Image = input.Image,
            Name = input.Name,
            IdCreator = input.IdCreator
        };
        
        await _mediator.Send(request);
        return Ok();
    }
    
    [HttpGet]
    [Route("/get-workshop")]
    public async Task<GetWorkshopViewModel> GetWorkshop([FromQuery] GetWorkshopRequest request)
    {
        return await _mediator.Send(request);
    }
}
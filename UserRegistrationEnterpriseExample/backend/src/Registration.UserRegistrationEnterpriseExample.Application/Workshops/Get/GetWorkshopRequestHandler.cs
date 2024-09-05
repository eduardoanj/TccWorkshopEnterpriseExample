using System.Globalization;
using MediatR;
using Registration.UserRegistrationEnterpriseExample.Application.Common.Interfaces;

namespace Registration.UserRegistrationEnterpriseExample.Application.Workshops.Get;

public class GetWorkshopRequestHandler : IRequestHandler<GetWorkshopRequest, GetWorkshopViewModel>
{
    private readonly IWorkshops _workshops;

    public GetWorkshopRequestHandler(IWorkshops workshops)
    {
        _workshops = workshops;
    }

    public async Task<GetWorkshopViewModel> Handle(GetWorkshopRequest request, CancellationToken cancellationToken)
    {
        var (workshops, totalItems) = await _workshops.ObterWorkshopsFiltrados(request);

        return new GetWorkshopViewModel
        {
            TotalItems = totalItems.ToString(),
            Workshops = workshops.Select(x => new WorkShopModel
            {
                Id = x.Id.ToString(),
                Description = x.Description,
                Date = x.Date.ToString(CultureInfo.CurrentCulture),
                Name = x.Name,
                Address = x.Address,
                Image = x.Image,
                ImageCreator = x.ImageCreator,
                IdCreator = x.IdCreator
            }).ToList()
        };
    }
}
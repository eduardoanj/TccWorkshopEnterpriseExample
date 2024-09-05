using MediatR;

namespace Registration.UserRegistrationEnterpriseExample.Application.Workshops.Get;

public class GetWorkshopRequest : IRequest<GetWorkshopViewModel>
{
    public int Pagina { get; set; }
    public int QtdePorPagina { get; set; }
    public string IdCreator { get; set; }
    public DateTime DataInicioFiltro { get; set; }
    public DateTime DataFinalFiltro { get; set; }
}
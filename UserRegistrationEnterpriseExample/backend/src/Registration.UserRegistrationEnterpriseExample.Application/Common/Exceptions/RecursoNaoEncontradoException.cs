using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace Registration.UserRegistrationEnterpriseExample.Application.Common.Exceptions;

[ExcludeFromCodeCoverage]
[Serializable]
public class RecursoNaoEncontradoException : Exception, INegocioException
{
    public RecursoNaoEncontradoException(string mensagem)
        : base(mensagem)
    {
    }

    protected RecursoNaoEncontradoException(SerializationInfo info, StreamingContext context)
        : base(info, context)
    {
    }
}
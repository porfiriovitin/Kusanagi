using Kusanagi.Communication.Responses;

namespace Kusanagi.Application.UseCases.User;

public interface IRegisterUserAccountUseCase
{
    public Task<ResponseTokens> Execute(RequestRegisterAccount user) 
}

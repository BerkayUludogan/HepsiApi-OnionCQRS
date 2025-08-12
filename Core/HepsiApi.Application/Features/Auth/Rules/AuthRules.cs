using HepsiApi.Application.Bases;
using HepsiApi.Application.Features.Auth.Exceptions;
using HepsiApi.Domain.Entities; 

namespace HepsiApi.Application.Features.Auth.Rules
{
    public class AuthRules : BaseRules
    {
        public Task UserShouldNotBeExist(User? user)
        {
            if (user is not null) throw new UserAlreadyExistException();
            return Task.CompletedTask;
        }
    }
}

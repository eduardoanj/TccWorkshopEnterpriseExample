using FluentAssertions.Extensions;
using Registration.UserRegistrationEnterpriseExample.Application.Users.SaveUser;
using Registration.UserRegistrationEnterpriseExample.Domain.Enums;

namespace Registration.UserRegistrationEnterpriseExample.Tests.Builders.Application.Users.PersistirUser;

public class SaveUserRequestBuilder
{
    private string _document;
    private string _name;
    private string _userType;
    private string _email;
    private string _password;

    public SaveUserRequest Build()
    {
        return new SaveUserRequest
        {
            Document = _document ?? "10579555000193",
            Name = _name ?? "Batman de Souza",
            UserType = _userType ?? string.Empty,
            Email = _email ?? string.Empty,
            Password = _password ?? "1234"
        };
    }

    public SaveUserRequestBuilder WithName(string name)
    {
        _name = name;
        return this;
    }
    
    public SaveUserRequestBuilder WithEmail(string email)
    {
        _email = email;
        return this;
    }
    
    public SaveUserRequestBuilder WithPassword(string password)
    {
        _password = password;
        return this;
    }
    
    public SaveUserRequestBuilder WithUserType(string userType)
    {
        _userType = userType;
        return this;
    }

    public SaveUserRequestBuilder WithDocument(string document)
    {
        _document = document;
        return this;
    }
}
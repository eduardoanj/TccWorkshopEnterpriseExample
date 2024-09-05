using FluentAssertions;
using FluentAssertions.Extensions;
using Microsoft.EntityFrameworkCore;
using Registration.UserRegistrationEnterpriseExample.Application.Users.SaveUser;
using Registration.UserRegistrationEnterpriseExample.Domain.Entidades;
using Registration.UserRegistrationEnterpriseExample.Domain.Enums;
using Registration.UserRegistrationEnterpriseExample.Tests.Builders.Application.Users.PersistirUser;
using Xunit;

namespace Registration.UserRegistrationEnterpriseExample.Tests.Application.Users.PersistirUser;

[Collection("Sequential")]
public class SaveUserRequestTests : IntegrationTestBase
{
    private readonly SaveUserRequest _userRequest;
    
    public SaveUserRequestTests()
    {
        _userRequest = new SaveUserRequestBuilder()
            .WithDocument("6768757576")
            .WithName("Jaspion da Silva")
            .WithUserType(UserType.NormalUser.ToString())
            .WithEmail("teste@teste.com")
            .WithPassword("123")
            .Build();
    }

    [Fact]
    public async void It_should_insert_a_new_user()
    {
        var viewModel = await Handle<SaveUserRequest, SaveUserViewModel>(_userRequest);
        
        var user = await GetByIdAsync<User>(viewModel.Id);
        user.OriginTimestampUtc.Should().Be(IntegrationTestClock.Now);
        user.Document.Should().Be(_userRequest.Document);
    }
}
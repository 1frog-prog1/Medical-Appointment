using Xunit;
using domain.models.user.usecase;
using Moq;
using domain.models.user;
using domain.models.user.model;
using domain;
using domain.models;

namespace tests;

// а точно ли все проверки должны храниться в одном классе?

public class UserUsecasesTests
{
    private readonly UserUsecases usecases;
    private readonly Mock<IUserRepository> repository;

    private User createUser(string login) {
        return new User(1, login, "1111", "88005553535", 
            "Ванька Дурачок Петрович", Role.Patient);
    }

    public UserUsecasesTests() {
        repository = new Mock<IUserRepository>();
        usecases = new UserUsecases(repository.Object);
    }

    [Fact]
    public async void signInUserEmptyFieldTest_Fail()
    {
        // arrange
        var empty_data = new loginData("", "something");
        
        // act
        var res = await usecases.signInUser(empty_data);

        //assert
        Assert.False(res.Success);
        Assert.Equal(res.Error, "There must be no empty fields");
    }

    [Fact]
    public async void signInUserNullFieldTest_Fail()
    {
        // arrange
        var null_data = new loginData(null, null);
        
        // act
        var res = await usecases.signInUser(null_data);

        //assert
        Assert.False(res.Success);
        Assert.Equal(res.Error, "There must be no empty fields");
    }

    [Fact]
    public async void signInUserIncorrectLoginDataTest_Fail()
    {
        // arrange
        var incorrect_data = new loginData("user", "incorrect password");
        repository.Setup(rep=>rep.checkAccount(incorrect_data)).ReturnsAsync(false);
        
        // act
        var res = await usecases.signInUser(incorrect_data);
        
        // assert
        Assert.False(res.Success);
        Assert.Equal(res.Error, "Error. Check your login or password");

    }

    [Fact]
    public async void signInUserCorrectLoginDataTest_Ok()
    {
        // arrange
        var incorrect_data = new loginData("user", "incorrect password");
        repository.Setup(rep=>rep.checkAccount(incorrect_data)).ReturnsAsync(true);
        
        // act
        var res = await usecases.signInUser(incorrect_data);
        
        // assert
        Assert.True(res.Success);
        Assert.Equal(res.Error, "");
    }

    [Fact]
    public async void signInUserEmptyFielsTest_Fail()
    {
        // arrange
        var empty_user = createUser("");
        
        // act
        var res = await usecases.signUpUser(empty_user);
        
        // assert
        Assert.False(res.Success);
        Assert.Equal(res.Error, "There must be no empty fields");
    }

     [Fact]
    public async void signInLoginAlreadyTakenTest_Fail()
    {
        // arrange
        var empty_user = createUser("abcabc");
        repository.Setup(rep => rep.isLoginExist(empty_user.login)).ReturnsAsync(true);
        
        // act
        var res = await usecases.signUpUser(empty_user);
        
        // assert
        Assert.False(res.Success);
        Assert.Equal(res.Error, "This login already exists");
    }

    [Fact]
    public async void signInOkDataTest_Ok()
    {
        // arrange
        var empty_user = createUser("abcabc");
        repository.Setup(rep => rep.isLoginExist(empty_user.login)).ReturnsAsync(false);
        
        // act
        var res = await usecases.signUpUser(empty_user);
        
        // assert
        Assert.True(res.Success);
        Assert.Equal(res.Error, "");
    }
}
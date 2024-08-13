namespace StudentManager.test.Controllers;

using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using StudentManager.Controllers;
using StudentManager.Data;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using StudentManager.test; // Add this using directive

public class AccountsControllerTests
{
    //private DbSetMockingExtensions _dbSetMockingExtensions;
    private readonly Mock<StudentManagerContext> _mockContext;
    private readonly AccountsController _controller;

    public AccountsControllerTests()
    {
        _mockContext = new Mock<StudentManagerContext>();
        _controller = new AccountsController(_mockContext.Object);
    }

    [Fact]
    public async Task Index_ReturnsViewResult_WithListOfAccounts()
    {
        // Arrange
        var role = new Role { Id = 3, Name = "Admin" ,Status = 1,Slug = "admin",Description = "admin"};
        var user = new User
        {
            Id = 1, LastName = "Test", FirstName = "User", FullName = "Test User", Email = "test@gmail.com", Phone = "1234567890", Address = "123 Test St", Status = 1, CreatedAt = DateTime.UtcNow, Gender = 1, ExtraCode = "01"
        };

        _mockContext.Setup(c => c.Users.FindAsync(1)).ReturnsAsync(user);
        _mockContext.Setup(c => c.Roles.FindAsync(1)).ReturnsAsync(role);
        
        // Arrange
        var accounts = new List<Account>
        {
            new Account
            {
                Username = "newuser", Password = "password", Role = role, RoleId = 3, User = user, UserId = 1, Status = 1, IpClient = "BH00625"
            },
            new Account
            {
                Username = "newuser2", Password = "password2", Role = role, RoleId = 3, User = user, UserId = 1, Status = 1, IpClient = "BH006252"
            }
        }.AsQueryable();

        // Create the mock DbSet
        var mockSet = new Mock<DbSet<Account>>();
        mockSet.As<IQueryable<Account>>().Setup(m => m.Provider).Returns(accounts.Provider);
        mockSet.As<IQueryable<Account>>().Setup(m => m.Expression).Returns(accounts.Expression);
        mockSet.As<IQueryable<Account>>().Setup(m => m.ElementType).Returns(accounts.ElementType);
        mockSet.As<IQueryable<Account>>().Setup(m => m.GetEnumerator()).Returns(accounts.GetEnumerator());
        mockSet.As<IAsyncEnumerable<Account>>().Setup(m => m.GetAsyncEnumerator(default)).Returns(new TestAsyncEnumerator<Account>(accounts.GetEnumerator()));

        // This mocks the Include method to return the mocked DbSet itself
        //mockSet.Setup(m => m.Include(It.IsAny<Expression<Func<Account, object>>>())).Returns(mockSet.Object);

        // Setup the context to return the mock DbSet
        _mockContext.Setup(c => c.Accounts).Returns(mockSet.Object);

        // Act
        var result = await _controller.Index("admin");

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<IEnumerable<Account>>(viewResult.ViewData.Model);
        Assert.Equal(2, model.Count());
    }

    /*[Fact]
    public async Task Details_ReturnsNotFound_WhenIdIsNull()
    {
        // Act
        var result = await _controller.Details(null);

        // Assert
        Assert.IsType<NotFoundResult>(result);
    }

    [Fact]
    public async Task Details_ReturnsViewResult_WithAccount()
    {
        // Arrange
        var account = new Account { Id = 1, Username = "user1", Password = "password1" };
        _mockContext.Setup(c => c.Accounts.FindAsync(1)).ReturnsAsync(account);

        // Act
        var result = await _controller.Details(1);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Account>(viewResult.ViewData.Model);
        Assert.Equal("user1", model.Username);
    }*/

    [Fact]
    public async Task Create_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        // Arrange
        var role = new Role { Id = 3, Name = "Admin" ,Status = 1,Slug = "admin",Description = "admin"};
        var user = new User
        {
            Id = 1, 
            LastName = "Test",
            FirstName = "User",
            FullName = "Test User",
            Email = "test@gmail.com",
            Phone = "1234567890",
            Address = "123 Test St",
            Status = 1,
            CreatedAt = DateTime.UtcNow,
            Gender = 1,
            ExtraCode = "01"
        };

        _mockContext.Setup(c => c.Users.FindAsync(1)).ReturnsAsync(user);
        _mockContext.Setup(c => c.Roles.FindAsync(1)).ReturnsAsync(role);

        var account = new Account
        {
            Username = "newuser",
            Password = "password",
            Role = role,
            RoleId = 3,
            User = user,
            UserId = 1,
            Status = 1,
            CreatedAt = DateTime.UtcNow,
            IpClient = "BH00625"
        };

        // Act
        var result = await _controller.Create(account);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(AccountsController.Index), redirectToActionResult.ActionName);
        _mockContext.Verify(c => c.Add(account), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
    }

    [Fact]
    public async Task Create_Post_ReturnsViewResult_WhenModelStateIsInvalid()
    {
        // Arrange
        _controller.ModelState.AddModelError("Username", "Required");

        var account = new Account
        {
            Password = "password",
            RoleId = 1,
            UserId = 1,
            Status = 1,
            CreatedAt = DateTime.UtcNow,
            IpClient = "BH00625"
        };

        // Act
        var result = await _controller.Create(account);

        // Assert
        var viewResult = Assert.IsType<ViewResult>(result);
        var model = Assert.IsAssignableFrom<Account>(viewResult.ViewData.Model);
        Assert.Equal(account, model);
        Assert.False(_controller.ModelState.IsValid);
    }
    
    [Fact]
    public async Task Edit_Post_ReturnsRedirectToActionResult_WhenModelStateIsValid()
    {
        // Arrange
        var account = new Account
        {
            Id = 1,
            Username = "updateduser",
            Password = "updatedpassword",
            Status = 1,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow,
            IpClient = "BH00625"
        };

        _mockContext.Setup(c => c.Accounts.FindAsync(1)).ReturnsAsync(account);

        // Act
        var result = await _controller.Edit(1, account);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(AccountsController.Index), redirectToActionResult.ActionName);
        _mockContext.Verify(c => c.Update(account), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
    }
    
    [Fact]
    public async Task DeleteConfirmed_ReturnsRedirectToActionResult()
    {
        // Arrange
        var account = new Account
        {
            Id = 1,
            Username = "user1",
            Password = "password1",
            RoleId = 3,
            UserId = 1,
            Status = 1,
            IpClient = "BH00625",
            UpdatedAt = DateTime.UtcNow
        };

        _mockContext.Setup(c => c.Accounts.FindAsync(1)).ReturnsAsync(account);

        // Act
        var result = await _controller.DeleteConfirmed(1);

        // Assert
        var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
        Assert.Equal(nameof(AccountsController.Index), redirectToActionResult.ActionName);
        _mockContext.Verify(c => c.Remove(account), Times.Once);
        _mockContext.Verify(c => c.SaveChangesAsync(default), Times.Once);
    }
}

public class TestAsyncEnumerator<T> : IAsyncEnumerator<T>
{
    private readonly IEnumerator<T> _inner;

    public TestAsyncEnumerator(IEnumerator<T> inner)
    {
        _inner = inner;
    }

    public ValueTask DisposeAsync()
    {
        _inner.Dispose();
        return ValueTask.CompletedTask;
    }

    public ValueTask<bool> MoveNextAsync()
    {
        return new ValueTask<bool>(_inner.MoveNext());
    }

    public T Current => _inner.Current;
}
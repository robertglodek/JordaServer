using Jorda.Application.Common.Interfaces;
using Jorda.Application.CQRS.Goal.Commands.CreateGoal;
using Moq;

namespace Application.UnitTests.CQRS.Goal.Commands;

public class CreateGoalCommandTests
{
    private Mock<IApplicationDbContext> _contextMock;
    private Mock<ICurrentUserService> _currentUserServiceMock;

    public CreateGoalCommandTests()
    {
        _contextMock = new();
        _currentUserServiceMock = new();
    }

    [Theory]
    [InlineData("ss","22","22")]
    public async Task AddGoal_WhenModelNotValid_ShouldFail(string name, string? colourHex, string? description)
    {
        //Arrange
        var command = new CreateGoalCommand() { Name = name, ColourHex = colourHex, Description = description };

        _currentUserServiceMock.Setup(m => m.UserRole)
            .Returns("Admin");

        var handler = new CreateGoalCommandHandler(_contextMock.Object,_currentUserServiceMock.Object);
        //Act

        var result = await handler.Handle(command, default);

        //Assert

    }
}

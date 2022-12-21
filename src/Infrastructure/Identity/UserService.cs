using AutoMapper;
using FluentValidation;
using Jorda.Application.Common.Exceptions;
using Jorda.Application.Common.Models;
using Jorda.Server.Application.Common.Interfaces;
using Jorda.Server.Application.Common.Models.Identity.Requests;
using Jorda.Server.Application.Common.Models.Identity.Responses;
using Jorda.Server.Domain.Enums;
using Jorda.Server.Infrastructure.Common.Exceptions;
using Jorda.Server.Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System.Linq.Expressions;

namespace Jorda.Server.Infrastructure.Identity;

public class UserService : BaseService, IUserService
{
    private readonly UserManager<JordaUser> _userManager;
    private readonly RoleManager<JordaRole> _roleManager;
    private readonly SignInManager<JordaUser> _signInManager;
    private readonly IServiceProvider _serviceProvider;
    private readonly IMapper _mapper;

    public UserService(UserManager<JordaUser> userManager, RoleManager<JordaRole> roleManager,
        SignInManager<JordaUser> signInManager, IServiceProvider serviceProvider, IMapper mapper)
    {
        _userManager = userManager;
        _roleManager = roleManager;
        _signInManager = signInManager;
        _serviceProvider = serviceProvider;
        _mapper = mapper;
    }
    public async Task<UserResponse> GetUserAsync(string userId)
    {
        var user = await _userManager.Users.FirstOrDefaultAsync(n => n.Id == userId);

        if(user==null)
        {
            throw new NotFoundException(nameof(JordaUser), userId);
        }
        return _mapper.Map<UserResponse>(user);
    }
    public async Task LockUserAsync(UserLockoutRequest request)
    {
        await ValidateAsync(_serviceProvider.GetService<IEnumerable<IValidator<UserLockoutRequest>>>()!, request);

        var user = await _userManager.FindByIdAsync(request.Id);
        if (user == null)
        {
            throw new NotFoundException(nameof(JordaUser), request.Id);
        }
        user.LockoutMessage = request.Message;
        var result = await _userManager.SetLockoutEndDateAsync(user, request.LockoutEnd);
        if (!result.Succeeded)
        {
            throw new IdentityErrorException(result.Errors.Select(n => n.Description).ToArray());
        }
    }

    public async Task<string> GetUserNameAsync(string userId)
    {
        var user = await _userManager.Users.FirstAsync(u => u.Id == userId);
        if (user == null)
        {
            throw new NotFoundException(nameof(JordaUser), userId);
        }
        return user.UserName!;
    }
    public async Task<bool> UserExists(string email) => await _userManager.Users.AnyAsync(n => n.Email == email);

    public async Task<PagedResult<UserResponse>> GetUsersAsync(GetUsersRequest request)
    {
        var baseQuery = _userManager.Users.Where(n => request.SearchPhrase == null);

        if (!string.IsNullOrEmpty(request.SortBy))
        {
            var columnsSelectors = new Dictionary<string, Expression<Func<JordaUser, object>>>
                {
                   { nameof(JordaUser.Email), n => n.Email! },
                   { nameof(JordaUser.Score), n => n.Score },
                };
            var selectedColumn = columnsSelectors[request.SortBy];
            baseQuery = request.SortDirection == SortDirection.Ascending
                ? baseQuery.OrderBy(selectedColumn)
                : baseQuery.OrderByDescending(selectedColumn);
        }
        var entities = await baseQuery
            .Skip(request.PageSize * (request.PageNumber - 1))
            .Take(request.PageSize)
            .ToListAsync();

        var totalItemsCount = baseQuery.Count();


        return new PagedResult<UserResponse>(_mapper.Map<List<UserResponse>>(entities),
            totalItemsCount, request.PageSize, request.PageNumber);
    }
}

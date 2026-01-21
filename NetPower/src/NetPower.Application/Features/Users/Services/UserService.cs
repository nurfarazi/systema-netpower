using AutoMapper;
using FluentValidation;
using NetPower.Application.Common.Models;
using NetPower.Application.Features.Users.DTOs;
using NetPower.Application.Features.Users.Validators;
using NetPower.Domain.Entities;
using NetPower.Domain.Exceptions;
using NetPower.Domain.Interfaces.Repositories;

namespace NetPower.Application.Features.Users.Services;

/// <summary>
/// Service for managing user operations.
/// </summary>
public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly GetUsersQueryValidator _queryValidator;
    private readonly CreateUserValidator _createValidator;

    public UserService(
        IUserRepository userRepository,
        IMapper mapper,
        GetUsersQueryValidator queryValidator,
        CreateUserValidator createValidator)
    {
        _userRepository = userRepository;
        _mapper = mapper;
        _queryValidator = queryValidator;
        _createValidator = createValidator;
    }

    public async Task<PagedResult<UserDto>> GetUsersAsync(
        GetUsersQuery query,
        CancellationToken cancellationToken = default)
    {
        // Validate query parameters
        var validationResult = await _queryValidator.ValidateAsync(query, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new DomainException(
                string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        // Get paginated users using EF Core
        var users = await _userRepository.GetPagedAsync(
            query.Search,
            query.IsActive,
            query.Page,
            query.PageSize,
            cancellationToken);

        // Get total count using ADO.NET (optimized count query)
        var totalCount = await _userRepository.GetCountAsync(
            query.Search,
            query.IsActive,
            cancellationToken);

        // Map to DTOs
        var userDtos = _mapper.Map<IEnumerable<UserDto>>(users);

        return new PagedResult<UserDto>
        {
            Items = userDtos,
            Page = query.Page,
            PageSize = query.PageSize,
            TotalCount = totalCount
        };
    }

    public async Task<UserDto?> GetByIdAsync(int id, CancellationToken cancellationToken = default)
    {
        var user = await _userRepository.GetByIdAsync(id, cancellationToken);
        return user == null ? null : _mapper.Map<UserDto>(user);
    }

    public async Task<int> CreateAsync(CreateUserDto dto, CancellationToken cancellationToken = default)
    {
        // Validate input
        var validationResult = await _createValidator.ValidateAsync(dto, cancellationToken);
        if (!validationResult.IsValid)
        {
            throw new DomainException(
                string.Join(", ", validationResult.Errors.Select(e => e.ErrorMessage)));
        }

        // Map DTO to entity
        var user = _mapper.Map<User>(dto);

        // Add to database
        await _userRepository.AddAsync(user, cancellationToken);
        await _userRepository.SaveChangesAsync(cancellationToken);

        return user.Id;
    }
}

﻿using Budgetify.Enums;
using Budgetify.Models;
using Budgetify.Models.DTOs;
using Budgetify.Models.Request;
using Budgetify.Models.Response;
using Budgetify.Repositories;
using Budgetify.Validators;

namespace Budgetify.Services;

public class UserService: IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly AppValidator _appValidator;
    private readonly IJwtService _jwtService;
    public UserService(IUserRepository userRepository, AppValidator appValidator, IJwtService jwtService)
    {
        _userRepository = userRepository;
        _appValidator = appValidator;
        _jwtService = jwtService;
    }
    public BaseApiResponse<UserDto> UserRegister(RegisterRequest request)
    {
        var validationMessages = new List<string>();
    
        if (!_appValidator.IsValidUsername(request.Username))
            validationMessages.Add("Invalid username format");
        
        if (!_appValidator.IsValidPassword(request.Password))
            validationMessages.Add("Invalid password format");
        
        if (!_appValidator.IsValidEmail(request.Email))
            validationMessages.Add("Invalid email format");
    
        if (validationMessages.Any())
            return new BaseApiResponse<UserDto>((int)EnumErrorCode.ValidationFailed, string.Join(", ", validationMessages));
    
        var users = _userRepository.GetAllUsers().Data;
        if (users.Any(x => x.Username == request.Username || x.Email == request.Email))
            return new BaseApiResponse<UserDto>((int)EnumErrorCode.DuplicateRecord, "Username or Email is already taken");
    
        var result = _userRepository.UserRegister(request);
        return new BaseApiResponse<UserDto>((int)EnumErrorCode.Success, "Registration successful", result);
    }


    public BaseApiResponse<List<GetAllUser>> GetAllUsers()
    {
        return _userRepository.GetAllUsers();
    }

    public BaseApiResponse<LoginResponse> Login(LoginRequest request)
    {
        var result = _userRepository.Login(request);
        var token = _jwtService.GenerateToken(result);
        result.Token = token;
        return new BaseApiResponse<LoginResponse>((int)EnumErrorCode.Success, "Login successful", result);
    }
}
﻿using Microsoft.AspNetCore.Mvc;
using Project_2_Web_Api.DTO;

namespace Project_2_Web_Api.Service;

public interface AuthService
{
	Task<IActionResult> Login(UserAccessorDTO userAccessorDTO);
	Task<string> RefreshTokenAsync(RefreshTokenRequest refreshTokenRequest);
}

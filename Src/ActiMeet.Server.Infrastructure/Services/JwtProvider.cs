﻿using ActiMeet.Server.Application.Services;
using ActiMeet.Server.Domain.Users;
using ActiMeet.Server.Infrastructure.Options;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ActiMeet.Server.Infrastructure.Services;
public class JwtProvider(
	IOptions<JwtOptions> options) : IJwtProvider
{
	public Task<string> CreateTokenAsync(AppUser user, CancellationToken cancellationToken = default)
	{
		List<Claim> claims = new()
		{
			new Claim("user-id", user.Id.ToString())
		};


		var expires = DateTime.Now.AddMonths(1);

		SymmetricSecurityKey securityKey = new(Encoding.UTF8.GetBytes(options.Value.SecretKey));
		SigningCredentials signingCredentials = new(securityKey, SecurityAlgorithms.HmacSha512);

		JwtSecurityToken securityToken = new(
			issuer: options.Value.Issuer,
			audience: options.Value.Audience,
			claims: claims,
			notBefore: DateTime.Now,
			expires: expires,
			signingCredentials: signingCredentials);

		JwtSecurityTokenHandler tokenHandler = new();

		string token = tokenHandler.WriteToken(securityToken);

		return Task.FromResult(token);
	}
}

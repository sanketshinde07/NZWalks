using System;
using Microsoft.AspNetCore.Identity;

namespace NZWalks.Repositories
{
	public interface ITokenInterface
	{
		string CreateJWTToken(IdentityUser user,List<string> roles);
	}
}


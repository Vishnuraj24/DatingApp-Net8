using System;
using System.Collections.Generic;
using API.Entities;

namespace DatingApp.API.Interfaces
{

    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }


}
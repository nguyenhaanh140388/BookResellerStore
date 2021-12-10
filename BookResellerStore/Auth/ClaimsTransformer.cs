using Microsoft.AspNetCore.Authentication;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using static BookResellerStore.Common.Constants;

namespace BookResellerStore.Auth
{
    public class ClaimsTransformer : IClaimsTransformation
    {
        // Rights 
        private readonly List<string> Rights = new List<string>() { RightName.ViewOrder, RightName.CreateOrder, RightName.ExportOrder };

        public async Task<ClaimsPrincipal> TransformAsync(ClaimsPrincipal principal)
        {
            var existingClaimsIdentity = (ClaimsIdentity)principal.Identity;

            // Initialize a new list of claims for the new identity
            var claims = new List<Claim>
            {
                new Claim(PermissionType.Right,String.Join(",", Rights) ),
            };

            claims.AddRange(existingClaimsIdentity.Claims);

            var newClaimsIdentity = new ClaimsIdentity(claims, existingClaimsIdentity.AuthenticationType);
            return new ClaimsPrincipal(newClaimsIdentity);
        }
    }
}

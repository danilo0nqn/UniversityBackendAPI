using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using University.Models.DataModels;

namespace University.Helpers
{
    public static class JwtHelpers
    {
        public static IEnumerable<Claim> GetClaims(this userTokens userAccounts, Guid id)
        {
            List<Claim> claims = new List<Claim>
            {
                new Claim("id", userAccounts.Id.ToString()),
                new Claim(ClaimTypes.Name, userAccounts.UserName),
                new Claim(ClaimTypes.Email, userAccounts.EmailId),
                new Claim(ClaimTypes.NameIdentifier, id.ToString()),
                new Claim(ClaimTypes.Expiration, DateTime.UtcNow.AddDays(1).ToString("MMM ddd dd yyy HH:mm:ss tt")),
            };

            if (userAccounts.UserName == "Admin")
            {
                claims.Add(new Claim(ClaimTypes.Role, "Administrator"));
            } else if(userAccounts.UserName == "User 1")
            {
                claims.Add(new Claim(ClaimTypes.Role, "User"));
                claims.Add(new Claim("UserOnly", "User 1"));
            }

            return claims;
        }

        public static IEnumerable<Claim> GetClaims(this userTokens userAccounts, out Guid id)
        {
            id = Guid.NewGuid();
            return GetClaims(userAccounts, id);
        }

        public static userTokens GenTokenKey(userTokens model, JwtSettings jwtSettings)
        {
            try
            {
                var userToken = new userTokens();
                if (model == null)
                {
                    throw new ArgumentNullException(nameof(model));
                }

                //Obtain SECRET KEY
                var key = System.Text.Encoding.ASCII.GetBytes(jwtSettings.IssuerSigningKey);

                Guid Id;

                //Expires in 1 Day
                DateTime expireTime = DateTime.UtcNow.AddDays(1);

                //Validity of our token
                userToken.Validity = expireTime.TimeOfDay;

                //GENERATE OUR JWT
                var jwToken = new JwtSecurityToken
                    (
                    issuer: jwtSettings.ValidIssuer,
                    audience: jwtSettings.ValidAudience,
                    claims: GetClaims(model, out Id),
                    notBefore: new DateTimeOffset(DateTime.Now).DateTime,
                    expires: new DateTimeOffset(expireTime).DateTime,
                    signingCredentials: new SigningCredentials
                        (
                            new SymmetricSecurityKey(key),
                            SecurityAlgorithms.HmacSha256
                        )
                    );

                userToken.Token = new JwtSecurityTokenHandler().WriteToken(jwToken);
                userToken.UserName = model.UserName;
                userToken.Id = model.Id;
                userToken.GuidId = Id;

                return userToken;
            }
            catch (Exception ex)
            {
                throw new Exception("Error generating JWT", ex);
            }
        }
    }
}

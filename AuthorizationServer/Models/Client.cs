using System;

namespace AuthorizationServer.Models
{
    public class Client
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string ClientSecretHash { get; set; }
        public OAuthGrant AllowedGrant { get; set; }

        public DateTimeOffset CreatedOn { get; set; }
    }
}
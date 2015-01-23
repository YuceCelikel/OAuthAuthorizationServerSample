using System;
using System.Collections.Generic;

namespace AuthorizationServer.Models
{
    public class DataStore
    {
        public static readonly DataStore Store = new DataStore();

        public List<Client> Clients { get; set; }

        public DataStore()
        {
            Clients = new List<Client>();

            Clients.Add(new Client()
            {
                Id = "yuce.celikel",
                Name = "yuce.celikel",
                ClientSecretHash = "123456",
                AllowedGrant = OAuthGrant.ResourceOwner,
                CreatedOn = DateTimeOffset.UtcNow
            });
        }
    }
}
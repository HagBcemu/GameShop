﻿namespace CatalogGame.Host.Models.Requests
{
    public class CreateGameRequest
    {
        public string Name { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string PictureFileName { get; set; } = null!;

        public string CompanyName { get; set; } = null!;
    }
}

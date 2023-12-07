using System;
namespace ProvaAPI_Authentication.DTO
{
	public class BookDTO
	{
        public int? IdBook { get; set; }

        public string? Title { get; set; }

        public string? Author { get; set; }

        public DateTime PublicationDate { get; set; }

        public int? IdUser { get; set; }
    }
}


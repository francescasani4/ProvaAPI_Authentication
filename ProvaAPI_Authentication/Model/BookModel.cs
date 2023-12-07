using System;
namespace ProvaAPI_Authentication.Model
{
	public class BookModel
	{
        public int IdBook { get; set; }

        public string Title { get; set; }

        public string Author { get; set; }

        public DateTime PublicationDate { get; set; }

        public UserModel? User { get; set; }
    }
}


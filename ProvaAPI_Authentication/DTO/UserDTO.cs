using System;
namespace ProvaAPI_Authentication.DTO
{
	public class UserDTO
	{
        public int? IdUser { get; set; }

        public string? UserName { get; set; }

        public string? Password { get; set; }

        public string? Name { get; set; }

        public string? Surname { get; set; }
    }
}


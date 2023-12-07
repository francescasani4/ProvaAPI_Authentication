using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProvaAPI_Authentication.Entity
{
    [Table("user")]
	public class UserEntity
	{
        [Key]
        public int IdUser { get; set; }

        public string UserName { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Surname { get; set; }
    }
}


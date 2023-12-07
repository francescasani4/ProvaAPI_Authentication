using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ProvaAPI_Authentication.Database;
using ProvaAPI_Authentication.DTO;
using ProvaAPI_Authentication.Entity;
using ProvaAPI_Authentication.Model;

namespace ProvaAPI_Authentication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly MyDbContext _dbContext;

        public UsersController(MyDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        [Route("{idUser}")]
        public IActionResult GetUserById(int idUser)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.IdUser == idUser);

            if (user == null)
                return NotFound();

            var u = MapUserEntityToUserModel(user);

            return Ok(u);
        }

        [HttpGet]
        public IActionResult GetAllUsers(string? name, string? surname)
        {
            IQueryable<UserEntity> query = _dbContext.Users;

            if(!string.IsNullOrEmpty(name))
            {
                query = query.Where(user => user.Name.Contains(name));
            }

            if(!string.IsNullOrEmpty(surname))
            {
                query = query.Where(user => user.Surname.Contains(surname));
            }

            var users = query
                .Select(MapUserEntityToUserModel)
                .ToList();

            if(users.Count == 0)
            {
                return NotFound();
            }

            return Ok(users);
        }

        [HttpPost]
        public IActionResult AddUser([FromBody] UserDTO userDTO)
        {
            var user = new UserEntity
            {
                UserName = userDTO.UserName,
                Password = userDTO.Password,
                Name = userDTO.Name,
                Surname = userDTO.Surname
            };

            _dbContext.Add(user);
            _dbContext.SaveChanges();

            return Ok(user);
        }

        [HttpPut]
        [Route("{idUser}")]
        public IActionResult UpdateUser(UserDTO userDTO, int idUser)
        {
            var existingUser = _dbContext.Users.FirstOrDefault(u => u.IdUser == idUser);

            if (existingUser != null)
            {
                existingUser.UserName = userDTO.UserName;
                existingUser.Password = userDTO.Password;
                existingUser.Name = userDTO.Name;
                existingUser.Surname = userDTO.Surname;

                _dbContext.SaveChanges();

                return Ok(idUser);
            }

            return NotFound();
        }

        [HttpDelete]
        [Route("{idUser}")]
        public IActionResult DeleteUser(int idUser)
        {
            var user = _dbContext.Users.FirstOrDefault(u => u.IdUser == idUser);

            if (user != null)
            {
                _dbContext.Users.Remove(user);
                _dbContext.SaveChanges();

                return Ok(idUser);
            }

            return NotFound();

        }

        private UserModel MapUserEntityToUserModel(UserEntity user)
        {
            return new UserModel
            {
                IdUser = user.IdUser,
                UserName = user.UserName,
                Password = user.Password,
                Name = user.Name,
                Surname = user.Surname
            };
        }
    }
}


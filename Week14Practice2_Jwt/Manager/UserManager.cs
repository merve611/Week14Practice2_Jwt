using Microsoft.EntityFrameworkCore;
using Week14Practice2_Jwt.Context;
using Week14Practice2_Jwt.Entities;
using Week14Practice2_Jwt.Models;
using Week14Practice2_Jwt.Services;
using Week14Practice2_Jwt.Type;

namespace Week14Practice2_Jwt.Manager
{
    public class UserManager : IUserService
    {
        private readonly PatikaDbContext _db;
        public UserManager(PatikaDbContext db)
        {
            _db = db;   
        }

        public async Task<ServiceMessage> AddUser(AddUserDto user)
        {
            var newUser = new UserEntity
            {
                Email = user.Email,      //AddUserDto daki email aktarıldı
                Password = user.Password,
            };

            _db.Users.Add(newUser);
            _db.SaveChanges();

            return new ServiceMessage
            {
                IsSucced = true,
                Message = "Kayıt Başarı ile oluşturuldu"
            };
        }


        public async Task<ServiceMessage<UserInfoDto>> LoginUser(LoginUserDto user)
        {
            //LoginUserDto ya gelen kullanıcı db de var mı yani kayıtlı mı
            var userEntity = _db.Users.Where(x => x.Email.ToLower() == user.Email.ToLower()).FirstOrDefault();

            if (userEntity is null)      //kayıtlı değilse hata mesajı
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı veya şifre hatalı"
                };
            }
            //kullanıcı var ise şifre kontolü yapılır;
            if (userEntity.Password == user.Password)
            {
                //dığru ise mesaj yollanır
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = true,
                    Data = new UserInfoDto
                    {
                        Id = userEntity.Id,
                        Email = userEntity.Email,
                        UserType = userEntity.UserType,
                    }

                };

            }
            //şifre yanlış ise 
            else
            {
                return new ServiceMessage<UserInfoDto>
                {
                    IsSucceed = false,
                    Message = "Kullanıcı adı ve şifre hatalı"
                };
            }
        }
    }
}

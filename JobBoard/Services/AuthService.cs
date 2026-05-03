using JobBoard.DTOs;
using JobBoard.DTOs.AuthDTOs;
using JobBoard.Data;
using JobBoard.Helpers;
using JobBoard.Models;
using Microsoft.EntityFrameworkCore;

//chekc the database for email a for login and registration, make sure user is unique
//validatinh format, length, characters of name, passwrod, email is not! needed, dto handles that
//hash the password for registration and store user in db. 
//generate token 
//create respnse DTO, assign it token and return it to be sent with OK response.

namespace JobBoard.Services
{
    public class AuthService
    {
        // injected dependencies 
        private readonly AppDbContext _db;
        private readonly JwtHelper _jwtHelper;

        // constructor 
        public AuthService(AppDbContext db, JwtHelper jwtHelper)
        {
            _db = db;
            _jwtHelper = jwtHelper;
        }

        //------------SEEKER REGISTRATION------------------//
        public async Task<AuthResponseDto> SeekerRegister(SeekerRegisterDto dto)
        {
            // check if email already exists
            var existing = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existing != null) throw new Exception("Email already taken");

            // hash password
            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // create user object for seeker
            var user = new User
            {
                Email = dto.Email,
                PasswordHash = hash,
                FullName = dto.FullName,
                Role = Role.Seeker,
                // create profile for seeker
                SeekerProfile = new SeekerProfile()
            };
            // save to database
            _db.Users.Add(user);
            await _db.SaveChangesAsync();

            // generate token
            var token = _jwtHelper.GenerateToken(user);

            // create and return AuthResponseDto
            return new AuthResponseDto
            {
                FullName = user.FullName,
                Role = user.Role,
                Token = token
            };
        }

        //--------EMPLOYER REGOSTRATION--------------//
        public async Task<AuthResponseDto> EmployerRegister(EmployerRegisterDto dto)
        {
            // check if email already exists
            var existing = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (existing != null) throw new Exception("Email already taken");

            // 2.hash password
            var hash = BCrypt.Net.BCrypt.HashPassword(dto.Password);

            // create user object for seeker
            var user = new User
            {
                Email = dto.Email,
                PasswordHash = hash,
                FullName = dto.FullName,
                Role = Role.Employer,
                // create profile for seeker
                EmployerProfile = new EmployerProfile
                {
                    CompanyName = dto.CompanyName
                },
            };

            // save to database
            _db.Users.Add(user);
            await _db.SaveChangesAsync();
            // generate token
            var token = _jwtHelper.GenerateToken(user);

            // create and return AuthResponseDto
            return new AuthResponseDto
            {
                FullName=user.FullName,
                Role=user.Role,
                Token=token
            };
        }

        //----for login, same for both roles------//
        public async Task<AuthResponseDto> Login(LoginDto dto)
        {
            // find user by email
            var user = await _db.Users.FirstOrDefaultAsync(u => u.Email == dto.Email);
            if (user == null) throw new Exception("Invalid credentials");

            // verify password
            bool validPassword = BCrypt.Net.BCrypt.Verify(dto.Password, user.PasswordHash);
            if (!validPassword) throw new Exception("Invalid credentials");

            // generate token
            var token = _jwtHelper.GenerateToken(user);

            // create and return AuthResponseDto
            return new AuthResponseDto
            {
                FullName = user.FullName,
                Role = user.Role,
                Token = token
            };
        }

        //---------------GET return profile to users------------------//
        public async Task<UserResponseDto> ReturnProfile(int userId)
        {
            var user = await _db.Users
                .Include(u => u.EmployerProfile)
                .FirstOrDefaultAsync(u => u.Id == userId);

            if (user == null) throw new Exception("user profile not found");

            return new UserResponseDto
            {
                Id = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = user.Role,
                CompanyName = user.Role == Role.Employer//if user has emplyer profile, thne return comany name too. 
            ? user.EmployerProfile?.CompanyName
            : null
            };

        }
    }
}
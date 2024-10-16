﻿namespace API_GISSA.Entities
{
    public class UserEnt
    {
        public long IdUser { get; set; }
        public long IdRol { get; set; }
        public string Token { get; set; } = string.Empty;
        public long IdNationality { get; set; }
        //public long IdSkills { get; set; }
        public string Identification { get; set; } = string.Empty;
        public string NameUser { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordUser { get; set; } = string.Empty;
        public string ConfPassUser { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        public string? SecondPhone { get; set; } = string.Empty;
        public string SoftSkills { get; set; } = string.Empty;
        public bool State { get; set; }
        public string TempKey { get; set; } = string.Empty;
        public string TempCode { get; set; } = string.Empty;
    }
}

﻿namespace MovieApi.Requests
{
    public class RegistrationRequestModel
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int ID_Role { get; set; }
        public string login { get; set; }
        public string password { get; set; }
    }
}

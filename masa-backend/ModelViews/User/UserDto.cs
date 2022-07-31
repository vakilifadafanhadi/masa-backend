namespace masa_backend.ModelViews
{
    public class UserDto:BaseEntityDto
    {
        public string Token { get; set; } = default!;
        public string UserName { get; set; } = default!;
        public string Key { get; set; } = default!;
        public string Pass { get; set; } = default!;
        public PersonalInformationDto PersonalInformation { get; set; }
        public UserDto()
        {
            PersonalInformation = new PersonalInformationDto();
        }
    }
}

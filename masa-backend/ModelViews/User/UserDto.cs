using System.ComponentModel.DataAnnotations;

namespace masa_backend.ModelViews
{
    public class UserDto:BaseEntityDto
    {
        public int Type { get; set; }
        public string? Token { get; set; }
        public string? UserName { get; set; } = default!;
        public string? Key { get; set; }
        public string? Pass { get; set; }
        public Guid PersonId { get; set; }
        public PersonalInformationDto? PersonalInformation { get; set; }
        //public UserDto()
        //{
        //    PersonalInformation = new PersonalInformationDto();
        //}
    }
}

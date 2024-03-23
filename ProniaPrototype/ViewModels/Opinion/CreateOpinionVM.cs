namespace ProniaPrototype.ViewModels
{
    public class CreateOpinionVM
    {
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public string? Role { get; set; }
        public string? Description { get; set; }
        public IFormFile? Image { get; set; }
    }
}

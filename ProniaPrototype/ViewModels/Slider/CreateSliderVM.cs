namespace ProniaPrototype.ViewModels
{
    public class CreateSliderVM
    {
        public string? Name { get; set; }
        public string? Description { get; set; }
        public byte? DiscountPercent { get; set; }
        public int? Order { get; set; }
        public IFormFile? Image { get; set; }
    }
}

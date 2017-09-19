namespace ygo.application.Dto
{
    public class TrapCardDto
    {
        public long Id { get; set; }
        public string CardNumber { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string ImageUrl { get; set; }
        public SubCategoryDto SubCategory { get; set; }
    }
}
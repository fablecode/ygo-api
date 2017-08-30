namespace ygo.domain.Models
{
    public class CardSubCategory
    {
        public long SubCategoryId { get; set; }
        public long CardId { get; set; }

        public Card Card { get; set; }
        public SubCategory SubCategory { get; set; }
    }
}
namespace ygo.application.Queries.CardImageByName
{
    public class CardImageByNameResult
    {
        public bool IsSuccessful { get; set; }
        public string Name { get; set; }
        public string FilePath { get; set; }
        public string ContentType { get; set; }
        public string Extension { get; set; }
    }
}
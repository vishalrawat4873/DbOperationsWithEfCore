namespace DbOperationWithEfCoreApp.Data
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int NoOfPages { get; set; }
        public string Description { get; set; }
        public bool IsActive { get; set; }
        public DateTime CreatedON { get; set; }
        public int? LanguageId { get; set; } //Foregin Key
        public int? AuthorId { get; set; }

        public Author? Author { get; set; } //navigation Property
        public Language? Language { get; set; } //Foregin Key
    }
}

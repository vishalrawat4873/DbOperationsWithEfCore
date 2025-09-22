namespace DbOperationWithEfCoreApp.Data
{
    public class Language
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Description { get; set; }


        public ICollection<Book> Books { get; set; } /* This is a one to many Relationship. This Book table hav
                                                      * Relaship with Language Table */

    }
}

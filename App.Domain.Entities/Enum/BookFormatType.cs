using App.Domain.Entities.Framework;

namespace App.Domain.Entities.Enum
{
    public class BookFormatType : Enumeration
    {
        public static readonly BookFormatType Book = new BookFormatType(0, "Book");
        public static readonly BookFormatType AudioBook = new BookFormatType(1, "AudioBook");
        public static readonly BookFormatType EBook = new BookFormatType(2, "eBook");

        private BookFormatType() { }
        private BookFormatType(int id, string displayName) : base(id, displayName) { }
    }
}

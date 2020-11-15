using App.Domain.Entities.Framework;
using System;

namespace App.Domain.Entities.Enum
{
    public class BookFormatType : Enumeration
    {
        public static readonly BookFormatType Book = new BookFormatType(0, "Book");
        public static readonly BookFormatType AudioBook = new BookFormatType(1, "AudioBook");
        public static readonly BookFormatType EBook = new BookFormatType(2, "eBook");

        private BookFormatType() { }
        private BookFormatType(int id, string displayName) : base(id, displayName) { }

        public static implicit operator BookFormatType(int? id)
        {
            if (id == null)
                return null;

            var list = Enumeration.FindAll<BookFormatType>();

            foreach (var item in list)
            {
                if (item.Id == (int)id)
                    return item;
            }

            throw new ApplicationException($"{nameof(BookFormatType)} does not have id = {id}");
        }
    }
}

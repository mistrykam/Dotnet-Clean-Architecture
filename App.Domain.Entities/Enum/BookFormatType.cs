using App.Domain.Entities.Framework;
using System;

namespace App.Domain.Entities.Enum
{
    public class BookFormatType : Enumeration
    {
        public static readonly BookFormatType Book = new BookFormatType(0, "Book");
        public static readonly BookFormatType AudioBook = new BookFormatType(1, "AudioBook");
        public static readonly BookFormatType EBook = new BookFormatType(2, "eBook");

        private BookFormatType()
        {
        }

        private BookFormatType(int id, string displayName) : base(id, displayName)
        {
        }

        private static BookFormatType FromInt(int? id)
        {
            var list = Enumeration.FindAll<BookFormatType>();

            foreach (var item in list)
            {
                if (item.Id == (int)id)
                    return item;
            }

            return null;
        }

        public static implicit operator BookFormatType(int? id)
        {
            if (id != null)
                return FromInt(id);
            else
                return null;
        }

        public static implicit operator BookFormatType(string idValue)
        {
            int id;

            if (Int32.TryParse(idValue, out id))
                return FromInt(id);
            else
                return null;
        }
    }
}
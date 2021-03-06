﻿using App.Domain.Entities.Framework;
using System;

namespace App.Domain.Entities.Enum
{
    public class BookFormatType : Enumeration
    {
        public static readonly BookFormatType Paperback = new BookFormatType(0, "Paperback");
        public static readonly BookFormatType Hardcover = new BookFormatType(1, "Hardcover");
        public static readonly BookFormatType AudioBook = new BookFormatType(2, "Audio Book");
        public static readonly BookFormatType EBook = new BookFormatType(3, "eBook Kindle");

        public BookFormatType()
        {
        }

        private BookFormatType(int value, string displayName) : base(value, displayName)
        {
        }

        public static implicit operator BookFormatType(int? value)
        {
            if (value == null)
                return null;

            var list = Enumeration.FindAll<BookFormatType>();

            foreach (var item in list)
            {
                if (item.Value == (int)value)
                    return item;
            }

            throw new ApplicationException($"{nameof(BookFormatType)} does not have id = {value}");
        }
    }
}
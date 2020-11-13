using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace App.Domain.Entities.Framework
{
    public abstract class Enumeration : IComparable
    {
        protected Enumeration() {}

        protected Enumeration(int id, string displayName)
        {
            Id = id;
            DisplayName = displayName;
        }

        public int Id { get; }

        public string DisplayName { get; }

        public override string ToString()
        {
            return DisplayName;
        }

        /// <summary>
        /// Key is the full name
        /// Value contains the Enum (Id, DisplayName)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static IReadOnlyDictionary<string, T> GetAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            var list = new Dictionary<string, T>();

            foreach (var item in fields)
            {
                list.Add(typeof(T).FullName + "." + item.Name, (T)item.GetValue(null));
            }

            return list;
        }

        public override bool Equals(object obj)
        {
            var otherValue = obj as Enumeration;

            if (otherValue == null)
            {
                return false;
            }

            var typeMatches = GetType().Equals(obj.GetType());
            var valueMatches = Id.Equals(otherValue.Id);

            return typeMatches && valueMatches;
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static int AbsoluteDifference(Enumeration firstValue, Enumeration secondValue)
        {
            var absoluteDifference = Math.Abs(firstValue.Id - secondValue.Id);
            return absoluteDifference;
        }

        public static T FromValue<T>(int value) where T : Enumeration, new()
        {
            var matchingItem = parse<T, int>(value, "value", item => item.Id == value);
            return matchingItem;
        }

        public static T FromDisplayName<T>(string displayName) where T : Enumeration, new()
        {
            var matchingItem = parse<T, string>(displayName, "display name", item => item.DisplayName == displayName);
            return matchingItem;
        }

        private static IEnumerable<T> FindAll<T>() where T : Enumeration
        {
            var fields = typeof(T).GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly);

            return fields.Select(f => f.GetValue(null)).Cast<T>();
        }

        private static T parse<T, K>(K value, string description, Func<T, bool> predicate) where T : Enumeration, new()
        {
            var matchingItem = FindAll<T>().FirstOrDefault(predicate);

            if (matchingItem == null)
            {
                var message = string.Format("'{0}' is not a valid {1} in {2}", value, description, typeof(T));
                throw new ApplicationException(message);
            }

            return matchingItem;
        }

        public int CompareTo(object other)
        {
            return Id.CompareTo(((Enumeration)other).Id);
        }
    }
}
﻿using CactusGuru.Infrastructure;
using System;
using System.Linq;

namespace CactusGuru.Domain.Greenhouse
{
    public class Genus : DomainEntity
    {
        public Genus()
        {
            _title = string.Empty;
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set { SetString(ref _title, value); }
        }

        public override string ToString()
        {
            return Title;
        }

        public virtual string Format(string format)
        {
            if (format == "GENUS")
                return Title.ToUpper();
            if (format == "Genus")
                return CapitalizeFirstLetter(Title);
            throw new Exception("Invalid format");
        }

        public virtual bool Equals(Genus other)
        {
            if (other == null) return false;
            if (other is NullGenera) return false;
            return other.Id == Id;
        }

        public override bool Equals(object obj)
        {
            return Equals(obj as Genus);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static string CapitalizeFirstLetter(string title)
        {
            if (string.IsNullOrEmpty(title))
                return string.Empty;
            return title.First().ToString().ToUpper() + title.Substring(1).ToLower();
        }

        public static void SetGenus(ref Genus field, Genus value)
        {
            if (value == null)
                field = Empty;
            else
                field = value;
        }

        #region NULL OBJECT

        private static NullGenera _empty;

        public static Genus Empty
        {
            get { return _empty ?? (_empty = new NullGenera()); }
        }

        private class NullGenera : Genus
        {
            public override bool Equals(Genus other)
            {
                return other is NullGenera;
            }

            public override string ToString()
            {
                return "EMPTY Genera";
            }

            public override string Format(string format)
            {
                return string.Empty;
            }
        }

        #endregion
    }
}

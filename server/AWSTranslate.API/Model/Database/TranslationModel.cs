using System.ComponentModel.DataAnnotations;
using System;

namespace AWSTranslate.API.Model.Database
{
    public class TranslationModel
    {
        [Key]
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}


using System;
using System.ComponentModel.DataAnnotations;

namespace AWSTranslate.API.Model.Postgres
{
    public class TeluguTranslated
    {
        [Key]
        public Guid Id { get; set; }
        public string Key { get; set; }
        public string Value { get; set; }
    }
}

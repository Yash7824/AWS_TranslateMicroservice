namespace AWSTranslate.API.Model
{
    public class UpdateKeyValue
    {

        public string pathLanguage { get; set; }  //path for each language/object 
        public string KeyToUpdate { get; set; }   //keys value to change
        public string NewValue { get; set; } //changed value
    }
}

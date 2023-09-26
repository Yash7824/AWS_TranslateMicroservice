namespace AWSTranslate.API.Model
{
    public class TransliterationRequest
    {
        public string Text { get; set; }
        public string SourceLanguage { get; set; }
        public string TargetLanguage { get; set; }
    }
}

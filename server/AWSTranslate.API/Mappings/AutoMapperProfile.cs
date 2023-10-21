using Amazon;
using AutoMapper;
using AWSTranslate.API.Model.Database;

namespace AWSTranslate.API.Mappings
{
    public class AutoMapperProfile : AutoMapper.Profile
    {
        public AutoMapperProfile() 
        {
            CreateMap<TranslationModel, EnglishTranslated>().ReverseMap();
            CreateMap<TranslationModel, HindiTranslated>().ReverseMap();
            CreateMap<TranslationModel, MarathiTranslated>().ReverseMap();
            CreateMap<TranslationModel, TeluguTranslated>().ReverseMap();
            CreateMap<TranslationModel, TamilTranslated>().ReverseMap();
        }
    }
}

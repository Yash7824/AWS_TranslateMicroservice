using AWSTranslate.API.Model.Database;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AWSTranslate.API.Repositories.BL
{
    public interface IAwsRepository
    {
        Task<List<TranslationModel>> TranslateText(string[] translateArray, string targetLanguageCode);
    }
}

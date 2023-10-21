using Amazon.Translate;
using Amazon.Translate.Model;
using AWSTranslate.API.Data;
using AWSTranslate.API.Model.Database;
using AWSTranslate.API.Repositories.DL;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSTranslate.API.Repositories.BL
{
    public class AwsRepository : IAwsRepository
    {
        private readonly AppDbContext dbContext;
        private readonly IAmazonTranslate _translateClient;
        private readonly IConfiguration configuration;
        private readonly IDataRepository dataRepository;

        public AwsRepository(AppDbContext dbContext, IAmazonTranslate translateClient, IConfiguration configuration, IDataRepository dataRepository)
        {
            this.dbContext = dbContext;
            _translateClient = translateClient;
            this.configuration = configuration;
            this.dataRepository = dataRepository;
        }
        public async Task<List<TranslationModel>> TranslateText(string[] translateArray, string targetLanguageCode)
        {
            string[] TranslateArray = translateArray.Distinct().ToArray();

            List<TranslationModel> translatedList = new List<TranslationModel>();

            foreach (var text in TranslateArray)
            {
                var translateRequest = new TranslateTextRequest
                {
                    SourceLanguageCode = "en",
                    TargetLanguageCode = targetLanguageCode,
                    Text = text
                };

                var response = await _translateClient.TranslateTextAsync(translateRequest);

                Guid Id = Guid.NewGuid();
                TranslationModel translationModel = new TranslationModel();
                translationModel.Id = Id;
                translationModel.Key = text;
                translationModel.Value = response.TranslatedText;
                translatedList.Add(translationModel);

                var addObjectTable = await dataRepository.AddObjectToTable(Id, text, response.TranslatedText, targetLanguageCode);

            }

            return translatedList;
        }
            
    }
}

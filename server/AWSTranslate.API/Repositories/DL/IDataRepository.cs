using System.Threading.Tasks;
using System;
using System.Collections.Generic;

namespace AWSTranslate.API.Repositories.DL
{
    public interface IDataRepository
    {
        dynamic getTable(string targetLanguageCode);
        Task<string> AddObjectToTable(Guid Id, string Key, string Value, string targetLanguageCode);
        Task<string> deleteObjectFromTable(Guid Id, string Key, string Value, string targetLanguageCode);
        Task<List<dynamic>> displayTable(string targetLanguageCode);
        Task<dynamic> getDataFromKey(string key, string targetLanguageCode);
        Task<dynamic> updateKeyValue(string key, string value, string targetLanguageCode);
        Task<List<dynamic>> updateAllValues(string existingValue, string newValue, string targetLanguageCode);
        Task<dynamic> deleteKeyValueId(Guid id, string targetLanguageCode);
        Task<List<dynamic>> getAllDataFromKey(string key, string targetLanguageCode);
        Task<List<dynamic>> updateEveryValue(string targetLanguageCode, string value, string newValue);

    }
}

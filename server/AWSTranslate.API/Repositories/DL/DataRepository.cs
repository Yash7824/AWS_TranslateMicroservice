using AutoMapper;
using AWSTranslate.API.Data;
using AWSTranslate.API.Model.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AWSTranslate.API.Repositories.DL
{
    public class DataRepository :IDataRepository
    {
        private readonly IMapper mapper;
        private readonly AppDbContext dbContext;
        public Dictionary<string, dynamic> TableDict;

        public DataRepository(IMapper mapper, AppDbContext dbContext)
        {
            this.mapper = mapper;
            this.dbContext = dbContext;
            TableDict = new Dictionary<string, dynamic>()
            {
                {"en", dbContext.EnglishTranslatedWords},
                {"hi", dbContext.HindiTranslatedWords},
                {"mr", dbContext.MarathiTranslatedWords},
                {"ta", dbContext.TamilTranslatedWords},
                {"te", dbContext.TeluguTranslatedWords},
            };
        }

        public dynamic getTable(string targetLanguageCode)
        {
            IEnumerable<dynamic> table = TableDict[targetLanguageCode];
            return table.ToList();
        }
        public async Task<string> AddObjectToTable(Guid Id, string Key, string Value, string targetLanguageCode)
        {
            // Add More Languages
            TranslationModel translationModel = new TranslationModel();
            translationModel.Id = Id;
            translationModel.Key = Key;
            translationModel.Value = Value;

            if (targetLanguageCode == "en")
            {
                await dbContext.EnglishTranslatedWords.AddAsync(mapper.Map<EnglishTranslated>(translationModel));
                await dbContext.SaveChangesAsync();
            }
            else if (targetLanguageCode == "hi")
            {
                await dbContext.HindiTranslatedWords.AddAsync(mapper.Map<HindiTranslated>(translationModel));
                await dbContext.SaveChangesAsync();
            }
            else if (targetLanguageCode == "mr")
            {
                await dbContext.MarathiTranslatedWords.AddAsync(mapper.Map<MarathiTranslated>(translationModel));
                await dbContext.SaveChangesAsync();
            }
            else if (targetLanguageCode == "ta")
            {
                await dbContext.TamilTranslatedWords.AddAsync(mapper.Map<TamilTranslated>(translationModel));
                await dbContext.SaveChangesAsync();
            }
            else if (targetLanguageCode == "te")
            {
                await dbContext.TeluguTranslatedWords.AddAsync(mapper.Map<TeluguTranslated>(translationModel));
                await dbContext.SaveChangesAsync();
            }

            return "Data has been added to the table";
        }

        public async Task<string> deleteObjectFromTable(Guid Id, string Key, string Value, string targetLanguageCode)
        {
            TranslationModel translationModel = new TranslationModel();
            translationModel.Id = Id;
            translationModel.Key = Key;
            translationModel.Value = Value;

            if (targetLanguageCode == "en")
            {
                dbContext.EnglishTranslatedWords.Remove(mapper.Map<EnglishTranslated>(translationModel));
                await dbContext.SaveChangesAsync();
            }
            else if (targetLanguageCode == "hi")
            {
                dbContext.HindiTranslatedWords.Remove(mapper.Map<HindiTranslated>(translationModel));
                await dbContext.SaveChangesAsync();
            }
            else if (targetLanguageCode == "mr")
            {
                dbContext.MarathiTranslatedWords.Remove(mapper.Map<MarathiTranslated>(translationModel));
                await dbContext.SaveChangesAsync();
            }
            else if (targetLanguageCode == "ta")
            {
                dbContext.TamilTranslatedWords.Remove(mapper.Map<TamilTranslated>(translationModel));
                await dbContext.SaveChangesAsync();
            }
            else if (targetLanguageCode == "te")
            {
                dbContext.TeluguTranslatedWords.Remove(mapper.Map<TeluguTranslated>(translationModel));
                await dbContext.SaveChangesAsync();
            }

            return "Data has been deleted to the table";
        }

        public async Task<List<dynamic>> displayTable(string targetLanguageCode)
        {
            
            IEnumerable<dynamic> table = TableDict[targetLanguageCode];
            return table.ToList();

        }

        public async Task<dynamic> getDataFromKey(string key, string targetLanguageCode)
        {
            IEnumerable<dynamic> table = TableDict[targetLanguageCode];
            var dataTuple = table.First(x => x.Key == key);
            return dataTuple;
 
        }


        public async Task<dynamic> updateKeyValue(string key, string value, string targetLanguageCode)
        {
            IEnumerable<dynamic> table = TableDict[targetLanguageCode];
            var dbObj = table.First(x => x.Key == key);
            if (dbObj == null) { return null; }
            dbObj.Value = value;
            await dbContext.SaveChangesAsync();
            return dbObj;
        }

        public async Task<List<dynamic>> updateAllValues(string existingValue, string newValue, string targetLanguageCode)
        {
            IEnumerable<dynamic> table = TableDict[targetLanguageCode];
            var dbList = table.Where(f => f.Value == existingValue).ToList();
            dbList.ForEach(f => f.Value = newValue);
            await dbContext.SaveChangesAsync();
            return dbList;   
        }

        public async Task<dynamic> deleteKeyValueId(Guid id, string targetLanguageCode)
        {
            IEnumerable<dynamic> table = TableDict[targetLanguageCode];
            var dbObj =  table.First(x => x.Id == id);
            if (dbObj == null) { return null; }
            string removeObj = deleteObjectFromTable(dbObj.Id, dbObj.Key, dbObj.Value, targetLanguageCode);
            await dbContext.SaveChangesAsync();
            return dbObj; 
        }

        public async Task<List<dynamic>> getAllDataFromKey(string key, string targetLanguageCode)
        {
            IEnumerable<dynamic> table = TableDict[targetLanguageCode];
            var dbList = table.Where(x => x.Key == key).ToList();
            return dbList;
        }


        public async Task<List<dynamic>> updateEveryValue(string targetLanguageCode, string value, string newValue)
        {
            var dbList = getTable(targetLanguageCode);

            for (int i = dbList.Count - 1; i >= 0; i--)
            {
                string existingValue = dbList[i].Value;
                string existingKey = dbList[i].Key;
                if (existingValue.Contains(value))
                {
                    string removeStr = await deleteObjectFromTable(dbList[i].Id, dbList[i].Key, dbList[i].Value, targetLanguageCode);
                    string finalValue = existingValue.Replace(value, newValue);
                    Guid id = Guid.NewGuid();
                    string str = await AddObjectToTable(id, existingKey, finalValue, targetLanguageCode);
                    await dbContext.SaveChangesAsync();
                }
            }
            await dbContext.SaveChangesAsync();

            return dbList;
            
        }
    }
}

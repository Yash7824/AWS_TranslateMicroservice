using Amazon.Translate.Model;
using AWSTranslate.API.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;
using System;
using Amazon.Translate;
using Microsoft.Extensions.Configuration;
using AWSTranslate.API.Model.Postgres;
using Microsoft.EntityFrameworkCore;
using Amazon.TranscribeService.Model.Internal.MarshallTransformations;
using AutoMapper;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace AWSTranslate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PgTranslateController : ControllerBase
    {

        private readonly PgAdminContext dbContext;
  
        private readonly IAmazonTranslate _translateClient;
        private readonly IConfiguration _configuration;
        private readonly IMapper mapper;

        public DbSet<EnglishTranslated> englishTable { get; set; }
        public DbSet<HindiTranslated> hindiTable { get; set; }
        public DbSet<MarathiTranslated> marathiTable { get; set; }
        public DbSet<TamilTranslated> tamilTable { get; set; }
        public DbSet<TeluguTranslated> teluguTable { get; set; }

        public Dictionary<string, object> TableDict;

        public PgTranslateController(PgAdminContext dbContext, IAmazonTranslate translateClient, 
            IConfiguration configuration, IMapper mapper)
        {
            this.dbContext = dbContext;
            this._translateClient = translateClient;
            this._configuration = configuration;
            this.mapper = mapper;
            this.englishTable = dbContext.EnglishTranslatedWords;
            this.hindiTable = dbContext.HindiTranslatedWords;
            this.marathiTable = dbContext.MarathiTranslatedWords;
            this.tamilTable = dbContext.TamilTranslatedWords;
            this.teluguTable = dbContext.TeluguTranslatedWords;

            TableDict = new Dictionary<string, object>()
            {
                {"en", this.englishTable},
                {"hi", this.hindiTable},
                {"mr", this.marathiTable},
                {"ta", this.tamilTable},
                {"te", this.teluguTable},
            };

        }

        // Functionalities
        public async Task<string> AddObjectToTable(Guid Id, string Key, string Value, string targetLanguageCode)
        {
            // Add More Languages
            TranslationModel translationModel = new TranslationModel();
            translationModel.Id = Id;
            translationModel.Key = Key;
            translationModel.Value = Value;

            if(targetLanguageCode == "en") 
            {
                await dbContext.EnglishTranslatedWords.AddAsync(mapper.Map<EnglishTranslated>(translationModel));
                await dbContext.SaveChangesAsync();
            } else if(targetLanguageCode == "hi")
            {
                await dbContext.HindiTranslatedWords.AddAsync(mapper.Map<HindiTranslated>(translationModel));
                await dbContext.SaveChangesAsync();
            } else if(targetLanguageCode == "mr")
            {
                await dbContext.MarathiTranslatedWords.AddAsync(mapper.Map<MarathiTranslated>(translationModel));
                await dbContext.SaveChangesAsync();
            } else if(targetLanguageCode == "ta")
            {
                await dbContext.TamilTranslatedWords.AddAsync(mapper.Map<TamilTranslated>(translationModel));
                await dbContext.SaveChangesAsync();
            } else if(targetLanguageCode == "te")
            {
                await dbContext.TeluguTranslatedWords.AddAsync(mapper.Map<TeluguTranslated>(translationModel));
                await dbContext.SaveChangesAsync();
            }

            return "Data has been added to the table";
        }

        public async Task<IActionResult> displayTable (string targetLanguageCode)
        {
            // Add More Languages
            if(targetLanguageCode == "en"){
                return Ok(await dbContext.EnglishTranslatedWords.ToListAsync());
            } else if(targetLanguageCode == "hi") {
                return Ok(await dbContext.HindiTranslatedWords.ToListAsync());
            } else if(targetLanguageCode == "mr") {
                return Ok(await dbContext.MarathiTranslatedWords.ToListAsync());
            } else if(targetLanguageCode == "ta") {
                return Ok(await dbContext.TamilTranslatedWords.ToListAsync());
            } else if(targetLanguageCode == "te") {
                return Ok(await dbContext.TeluguTranslatedWords.ToListAsync());
            } else { 
                return BadRequest();
            }
            
        }

        public async Task<IActionResult> getDataFromKey(string key, string targetLanguageCode)
        {
            if (targetLanguageCode == "en")
            {
                var dataTuple = await dbContext.EnglishTranslatedWords.FirstAsync(x => x.Key == key);
                return Ok(dataTuple);

            }
            else if (targetLanguageCode == "hi")
            {
                var dataTuple = await dbContext.HindiTranslatedWords.FirstAsync(x => x.Key == key);
                return Ok(dataTuple);

            }
            else if (targetLanguageCode == "mr")
            {
                var dataTuple = await dbContext.MarathiTranslatedWords.FirstAsync(x => x.Key == key);
                return Ok(dataTuple);

            }
            else if (targetLanguageCode == "ta")
            {
                var dataTuple = await dbContext.TamilTranslatedWords.FirstAsync(x => x.Key == key);
                return Ok(dataTuple);

            }
            else if (targetLanguageCode == "te")
            {
                var dataTuple = await dbContext.TeluguTranslatedWords.FirstAsync(x => x.Key == key);
                return Ok(dataTuple);
            }
            
            // Add More Languages
            else
            {
                return BadRequest();
            }  
        }


        public async Task<IActionResult> updateKeyValue(string key, string value, string targetLanguageCode)
        {

            if (targetLanguageCode == "en")
            {
                var dbObj = await englishTable.FirstAsync(x => x.Key == key);
                if (dbObj == null) { return NotFound(); }
                dbObj.Value = value;
                await dbContext.SaveChangesAsync();
                return Ok(dbObj);

            }
            else if (targetLanguageCode == "hi")
            {
                var dbObj = await hindiTable.FirstAsync(x => x.Key == key);
                if (dbObj == null) { return NotFound(); }
                dbObj.Value = value;
                await dbContext.SaveChangesAsync();
                return Ok(dbObj);
            }
            else if (targetLanguageCode == "mr")
            {
                var dbObj = await marathiTable.FirstAsync(x => x.Key == key);
                if (dbObj == null) { return NotFound(); }
                dbObj.Value = value;
                await dbContext.SaveChangesAsync();
                return Ok(dbObj);
            }
            else if (targetLanguageCode == "ta")
            {
                var dbObj = await tamilTable.FirstAsync(x => x.Key == key);
                if (dbObj == null) { return NotFound(); }
                dbObj.Value = value;
                await dbContext.SaveChangesAsync();
                return Ok(dbObj);
            }
            else if (targetLanguageCode == "te")
            {
                var dbObj = await teluguTable.FirstAsync(x => x.Key == key);
                if (dbObj == null) { return NotFound(); }
                dbObj.Value = value;
                await dbContext.SaveChangesAsync();
                return Ok(dbObj);
            }

            // Add More
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> updateAllValues(string existingValue, string newValue, string targetLanguageCode)
        {
            if (targetLanguageCode == "en")
            {
                var dbList = await englishTable.Where(f => f.Value == existingValue).ToListAsync();
                dbList.ForEach(f => f.Value = newValue);
                await dbContext.SaveChangesAsync();
                return Ok(dbList);

            } else if(targetLanguageCode == "hi")
            {
                var dbList = await hindiTable.Where(f => f.Value == existingValue).ToListAsync();
                dbList.ForEach(f => f.Value = newValue);
                await dbContext.SaveChangesAsync();
                return Ok(dbList);
            }
            else if (targetLanguageCode == "mr")
            {
                var dbList = await marathiTable.Where(f => f.Value == existingValue).ToListAsync();
                dbList.ForEach(f => f.Value = newValue);
                await dbContext.SaveChangesAsync();
                return Ok(dbList);
            }
            else if (targetLanguageCode == "ta")
            {
                var dbList = await tamilTable.Where(f => f.Value == existingValue).ToListAsync();
                dbList.ForEach(f => f.Value = newValue);
                await dbContext.SaveChangesAsync();
                return Ok(dbList);
            }
            else if (targetLanguageCode == "te")
            {
                var dbList = await teluguTable.Where(f => f.Value == existingValue).ToListAsync();
                dbList.ForEach(f => f.Value = newValue);
                await dbContext.SaveChangesAsync();
                return Ok(dbList);
            }
            else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> deleteKeyValueId(Guid id, string targetLanguageCode)
        {
            if(targetLanguageCode == "en")
            {
                var dbObj = await englishTable.FindAsync(id);
                if(dbObj == null) { return NotFound(); }
                englishTable.Remove(dbObj);
                await dbContext.SaveChangesAsync();
                return Ok(dbObj);
                
            } else if(targetLanguageCode == "hi")
            {
                var dbObj = await hindiTable.FindAsync(id);
                if (dbObj == null) { return NotFound(); }
                hindiTable.Remove(dbObj);
                await dbContext.SaveChangesAsync();
                return Ok(dbObj);

            }else if(targetLanguageCode == "mr")
            {
                var dbObj = await marathiTable.FindAsync(id);
                if (dbObj == null) { return NotFound(); }
                marathiTable.Remove(dbObj);
                await dbContext.SaveChangesAsync();
                return Ok(dbObj);

            }else if(targetLanguageCode == "ta")
            {
                var dbObj = await tamilTable.FindAsync(id);
                if (dbObj == null) { return NotFound(); }
                tamilTable.Remove(dbObj);
                await dbContext.SaveChangesAsync();
                return Ok(dbObj);

            }else if(targetLanguageCode == "te")
            {
                var dbObj = await teluguTable.FindAsync(id);
                if (dbObj == null) { return NotFound(); }
                teluguTable.Remove(dbObj);
                await dbContext.SaveChangesAsync();
                return Ok(dbObj);

            }else
            {
                return BadRequest();
            }
        }

        public async Task<IActionResult> getAllDataFromKey(string key, string targetLanguageCode)
        {
            if(targetLanguageCode == "en")
            {
                var dbList = await englishTable.Where(x => x.Key == key).ToListAsync();
                return Ok(dbList);

            } else if(targetLanguageCode == "hi")
            {
                var dbList = await hindiTable.Where(x => x.Key == key).ToListAsync();
                return Ok(dbList);

            }else if (targetLanguageCode == "mr")
            {
                var dbList = await marathiTable.Where(x => x.Key == key).ToListAsync();
                return Ok(dbList);

            }else if (targetLanguageCode == "ta")
            {
                var dbList = await tamilTable.Where(x => x.Key == key).ToListAsync();
                return Ok(dbList);

            }else if (targetLanguageCode == "te")
            {
                var dbList = await teluguTable.Where(x => x.Key == key).ToListAsync();
                return Ok(dbList);

            }
            else
            {
                return BadRequest();
            }
        }





        // Rest APIs

        [HttpPost]
        [Route("TextArray")]
        public async Task<IActionResult> TranslateArray([FromBody] string[] translateArray,
            [FromQuery] string targetLanguageCode)                                       
        {

            string[] TranslateArray = translateArray.Distinct().ToArray();
  
            List<TranslationModel> translatedList = new List<TranslationModel>();
            try
            {
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

                    var addObjectTable = await AddObjectToTable(Id, text, response.TranslatedText, targetLanguageCode);

                }

                return Ok(translatedList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        [HttpPost]
        [Route("addKeyValuePairs")]
        public async Task<IActionResult> addKeyValuePairs([FromBody] List<TranslationModel> toAddKeyValuePairs,
            [FromQuery] string targetLanguageCode)
        {

            foreach (var item in toAddKeyValuePairs)
            {
                Guid Id = Guid.NewGuid() ;
                var response = await AddObjectToTable(Id, item.Key, item.Value, targetLanguageCode);
            }

            var output = await displayTable(targetLanguageCode);
            return output;
        }

        [HttpGet]
        [Route("Get-All-Data")]
        public async Task<IActionResult> GetAllData([FromQuery] string targetLanguageCode)
        {
            var response = await displayTable(targetLanguageCode);
            return response;
        }

        [HttpGet]
        [Route("Get-Data-FromKey")]
        public async Task<IActionResult> GetDataFromKey([FromQuery] string key, [FromQuery] string targetLanguageCode)
        {
            var dataTuple = await getDataFromKey(key, targetLanguageCode); 

            return dataTuple;
        }

        [HttpGet]
        [Route("Get-AllData-FromKey")]
        public async Task<IActionResult> GetAllDataFromKey([FromQuery] string key, [FromQuery] string targetLanguageCode)
        {
            var dbList = await getAllDataFromKey(key, targetLanguageCode);
            return dbList;
        }

        [HttpPut]
        [Route("Update-keyValue")]
        public async Task<IActionResult> UpdateKeyValue([FromBody] TranslationModel translateObj,
            [FromQuery] string targetLanguageCode)
        {
            var dbObj = await updateKeyValue(translateObj.Key, translateObj.Value, targetLanguageCode);
            return dbObj;
        }

        [HttpPut]
        [Route("Update-AllValues")]
        public async Task<IActionResult> UpdateAllValues([FromQuery] string existingValue,
            [FromQuery] string updatedValue, [FromQuery] string targetLanguageCode)
        {
            var dbList = await updateAllValues(existingValue, updatedValue, targetLanguageCode);
            return dbList;
        }

        [HttpDelete]
        [Route("Delete-KeyValueById")]
        public async Task<IActionResult> DeleteKeyValueById([FromQuery] Guid id, [FromQuery] string targetLanguageCode)
        {
            var dbObj = await deleteKeyValueId(id, targetLanguageCode);
            return dbObj;
        }



    }
}

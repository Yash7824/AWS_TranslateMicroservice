using Amazon.Translate.Model;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using AWSTranslate.API.Model.Database;
using AWSTranslate.API.Repositories.BL;
using AWSTranslate.API.Repositories.DL;
namespace AWSTranslate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TranslateController : ControllerBase
    {
        private readonly IAwsRepository awsRepository;
        private readonly IDataRepository dataRepository;

        public TranslateController(IAwsRepository awsRepository, IDataRepository dataRepository)
        {
            this.awsRepository = awsRepository;
            this.dataRepository = dataRepository;
        }


        [HttpPost]
        [Route("TextArray")]
        public async Task<IActionResult> TranslateArray([FromBody] string[] translateArray,
            [FromQuery] string targetLanguageCode)                                       
        {
            try
            {
                var translatedList = await awsRepository.TranslateText(translateArray, targetLanguageCode);
                return Ok(translatedList);
            }catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }


        [HttpPost]
        [Route("addKeyValuePairs")]
        public async Task<IActionResult> addKeyValuePairs([FromBody] List<TranslationModel> toAddKeyValuePairs,
            [FromQuery] string targetLanguageCode)
        {
            try
            {
                foreach (var item in toAddKeyValuePairs)
                {
                    Guid Id = Guid.NewGuid();
                    var response = await dataRepository.AddObjectToTable(Id, item.Key, item.Value, targetLanguageCode);
                }

                var output = await dataRepository.displayTable(targetLanguageCode);
                return Ok(output);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }  
            
        }

        [HttpGet]
        [Route("Get-All-Data")]
        public async Task<IActionResult> GetAllData([FromQuery] string targetLanguageCode)
        {
            try
            {
                var response = await dataRepository.displayTable(targetLanguageCode);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            
        }

        [HttpGet]
        [Route("Get-Data-FromKey")]
        public async Task<IActionResult> GetDataFromKey([FromQuery] string key, [FromQuery] string targetLanguageCode)
        {
            try
            {
                var dataTuple = await dataRepository.getDataFromKey(key, targetLanguageCode);
                return Ok(dataTuple);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            
        }

        [HttpGet]
        [Route("Get-AllData-FromKey")]
        public async Task<IActionResult> GetAllDataFromKey([FromQuery] string key, [FromQuery] string targetLanguageCode)
        {
            try
            {
                var dbList = await dataRepository.getAllDataFromKey(key, targetLanguageCode);
                return Ok(dbList);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            
        }

        [HttpPut]
        [Route("Update-keyValue")]
        public async Task<IActionResult> UpdateKeyValue([FromBody] TranslationModel translateObj,
            [FromQuery] string targetLanguageCode)
        {
            try
            {
                var dbObj = await dataRepository.updateKeyValue(translateObj.Key, translateObj.Value, targetLanguageCode);
                return Ok(dbObj);
            }
            catch(Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            
        }

        [HttpPut]
        [Route("Update-AllValues")]
        public async Task<IActionResult> UpdateAllValues([FromQuery] string existingValue,
            [FromQuery] string updatedValue, [FromQuery] string targetLanguageCode)
        {
            try
            {
                var dbList = await dataRepository.updateAllValues(existingValue, updatedValue, targetLanguageCode);
                return Ok(dbList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            
        }

        [HttpDelete]
        [Route("Delete-KeyValueById")]
        public async Task<IActionResult> DeleteKeyValueById([FromQuery] Guid id, [FromQuery] string targetLanguageCode)
        {
            try
            {
                var dbObj = await dataRepository.deleteKeyValueId(id, targetLanguageCode);
                return Ok(dbObj);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            
        }

        [HttpPut]
        [Route("Update-Every-Value")]
        public async Task<IActionResult> UpdateValueEverywhere([FromQuery] string targetLanguageCode,
            [FromQuery] string value, [FromQuery] string newValue)
        {
            try
            {
                var dbList = await dataRepository.updateEveryValue(targetLanguageCode, value, newValue);
                return Ok(dbList);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
            
        }
    }
}

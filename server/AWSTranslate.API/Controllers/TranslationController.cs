using System;
using System.Threading.Tasks;
using Amazon;
using Amazon.Translate;
using Amazon.Translate.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using AWSTranslate.API.Model;
using System.Collections.Generic;
using System.Collections;
using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp;
using System.Linq;
using Newtonsoft.Json;
using Amazon.Runtime.Internal;
using Microsoft.AspNetCore.Http;
using RestSharp;
using AngleSharp.Io;
using Grpc.Core;

namespace AWSTranslate.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    public class TranslationController : ControllerBase   
    {

        private readonly IAmazonTranslate _translateClient;   
        private readonly IConfiguration _configuration;

        string authSecret;
        string basePath;
        IFirebaseClient client;

        public TranslationController(IAmazonTranslate translateClient, IConfiguration configuration)
        {
            authSecret = "CqROITZrKVqDppwuvNX7JkIQwnCQ5zTtor1tIKRR";
            basePath = "https://multilingual-microservic-b5f1b-default-rtdb.firebaseio.com/";
            _translateClient = translateClient;
            _configuration = configuration;

            
            IFirebaseConfig config = new FirebaseConfig
            {
                AuthSecret = authSecret,
                BasePath = basePath,
            };

            client = new FireSharp.FirebaseClient(config);

        }

        

        [HttpGet("Text")]
        public async Task<IActionResult> TranslateText([FromQuery] string text, [FromQuery] string targetLanguageCode)
        {
            try
            {
                var translateRequest = new TranslateTextRequest
                {
                    SourceLanguageCode = "en",
                    TargetLanguageCode = targetLanguageCode,
                    Text = text
                };

                var response = await _translateClient.TranslateTextAsync(translateRequest);

                return Ok(response.TranslatedText);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



        [HttpPut]
        [Route("Update-data")]
        public async Task<IActionResult> UpdateData([FromBody] UpdateKeyValue updateKeyValue)
        {
            var res = await client.GetAsync(updateKeyValue.pathLanguage); //passing pathlanguage variable from class updatekeyvalue
            Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(res.Body.ToString());

            if (data.ContainsKey(updateKeyValue.KeyToUpdate))
            {
                data[updateKeyValue.KeyToUpdate] = updateKeyValue.NewValue;
            }
        

            var res2 = await client.UpdateAsync(updateKeyValue.pathLanguage, data);

            return Ok(data);
        }


        // Updates all the key's values
        // Updates the word which is present in all the pairs of the dictionary
        [HttpPut]
        [Route("Update-value")]
        public async Task<IActionResult> UpdateValue([FromBody] UpdateValue updateValue)
        {
            //passing pathlanguage variable from updatevalue class and retrieving/getting all the object 
            var res = await client.GetAsync(updateValue.pathLanguage);  
            Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(res.Body.ToString());
            Dictionary<string, string> UpdatedData = new Dictionary<string, string>();

            string newStr;
            foreach(KeyValuePair<string, string> entry in data)
            {
                string val = entry.Value;
                if (val.Contains(updateValue.existingValue))
                {

                    // replacing the existing value with the new value and adding the pair in updated dictionary
                   newStr = val.Replace(updateValue.existingValue, updateValue.newValue);
                    UpdatedData.Add(entry.Key, newStr);
                }
                else
                {
                    // pair with no changes must also be added in the updated dictionary
                    UpdatedData.Add(entry.Key, val);
                }
            }

            var res2 = await client.UpdateAsync(updateValue.pathLanguage, UpdatedData);
            return Ok(UpdatedData);
        }

        [HttpPost]
        [Route("GetData")]
        public async Task<IActionResult> GetData([FromBody] string pathLanguage)
        {
            var res = await client.GetAsync(pathLanguage);
            Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(res.Body.ToString());
            
            return Ok(data);
            
        }


        [HttpGet("GetHindiData")]
        public async Task<IActionResult> GetHindiData()
        {
            var res = await client.GetAsync("doc/hindi/-NenZxVH-3Ly9qkTYjL1");
            Dictionary<string, string> data = JsonConvert.DeserializeObject<Dictionary<string, string>>(res.Body.ToString());
            return Ok(data);
        }




        [HttpPost]
        [Route("TextArray")]
        public async Task<IActionResult> TranslateArray([FromBody] string[] translateArray,
                                            [FromQuery] string targetLanguage,
                                            [FromQuery] string targetLanguageCode
                                            )
        {

            string[] TranslateArray = translateArray.Distinct().ToArray();
            ArrayList translatedArray = new ArrayList();
            Dictionary<string, string> translatedDict = new Dictionary<string, string>();

            try
            {
                foreach(var text in TranslateArray)
                {
                    var translateRequest = new TranslateTextRequest
                    {
                        SourceLanguageCode = "en",
                        TargetLanguageCode = targetLanguageCode,
                        Text = text
                    };

                    var response = await _translateClient.TranslateTextAsync(translateRequest);
                    translatedDict.Add(text, response.TranslatedText);

                    translatedArray.Add(response.TranslatedText);
                }

                var responseFirebase = client.Push("doc/" + targetLanguage, translatedDict);
                return Ok(translatedDict);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }



    }
}

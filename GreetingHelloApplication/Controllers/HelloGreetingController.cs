using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using ModelLayer.Model;
using System.Collections.Generic;

namespace HelloGreetingApplication.Controllers
{
    /// <summary>
    /// Class providing API for hitting endpoints
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    public class HelloGreetingController : ControllerBase
    {
        private readonly ILogger<HelloGreetingController> _logger;
        private static Dictionary<string, string> KeyValueStore = new Dictionary<string, string>();
        
        public HelloGreetingController(ILogger<HelloGreetingController> logger)
        {
            _logger = logger;
        }
        /// <summary>: Get method to get Greeting Message
        /// </summary>
        /// <returns> Hello To Greeting App API endpoint</returns>
        [HttpGet]
        public IActionResult Get()
        {
            _logger.LogInformation("Get Request From HelloGReeting");
            ResponseModel<string> response = new ResponseModel<String>
            {
                Data = "Hello To Greeting App API endpoint",
                Success = true,
                Message = "Get Request From HelloGreeting"
            };
            return Ok(response);
        }
        ///<summary>:Add method to add the new key-value pair</summary>
        ///<param name="requestModel">key-value pair</param>
        ///<returns>Request received successfully and stored</returns>
        [HttpPost]
        public IActionResult Post(RequestModel requestModel)
        {
            _logger.LogInformation($"Post request received: key={requestModel.key},Value = {requestModel.value}");
            KeyValueStore[requestModel.key] = requestModel.value;
            return Ok(
                new ResponseModel<string>
                {
                    Data = "Request received successfully and stored",
                    Success = true,
                    Message = $"Post request received: key={requestModel.key},Value = {requestModel.value}"
                });
        }
        ///<summary>:Update an existing value</summary>
        ///<param name="requestModel">key</param>
        ///<returns>Value of the key</returns>
        [HttpPut]
        public IActionResult Put(RequestModel requestModel)
        {
            if (KeyValueStore.ContainsKey(requestModel.key))
            {
                _logger.LogInformation($"Put request received : key={requestModel.key}");
                KeyValueStore[requestModel.key] = requestModel.value;
                return Ok(new ResponseModel<string>
                {
                    Data = KeyValueStore[requestModel.key],
                    Success = true,
                    Message = $"Put request received : key={requestModel.key}"
                });

            }
            _logger.LogInformation($"Put request failed : key={requestModel.key} not found");
            return NotFound(new ResponseModel<string>
            {
                Data = "Key not found",
                Success = false,
                Message = $"Put request failed : key={requestModel.key} not found"
            });
        }
        ///<summary>:Patch method to update the value of the key</summary>
        ///<param name="requestModel">key</param>
        ///<returns>Value of the key</returns>
        [HttpPatch]
        public IActionResult Patch(RequestModel requestModel)
        {
            if (KeyValueStore.ContainsKey(requestModel.key))
            {
                _logger.LogInformation($"Patch request received : key={requestModel.key}");
                KeyValueStore[requestModel.key] = requestModel.value;
                return Ok(new ResponseModel<string>
                {
                    Data = KeyValueStore[requestModel.key],
                    Success = true,
                    Message = $"Patch request received : key={requestModel.key}"
                });
            }
            _logger.LogInformation($"Patch request failed : key={requestModel.key} not found");
            return NotFound(new ResponseModel<string>
            {
                Data = "Key not found",
                Success = false,
                Message = $"Patch request failed : key={requestModel.key} not found"
            });
        }
        [HttpDelete("{key}")]
        public IActionResult Delete(string key)
        {
            if (KeyValueStore.ContainsKey(key))
            {
                _logger.LogInformation($"Delete request received : key={key}");
                KeyValueStore.Remove(key);
                return Ok(new ResponseModel<string>
                {
                    Data = "Key deleted successfully",
                    Success = true,
                    Message = $"Delete request received : key={key}"
                });
            }
            _logger.LogInformation($"Delete request failed : key={key} not found");
            return NotFound(new ResponseModel<string>
            {
                Data = "Key not found",
                Success = false,
                Message = $"Delete request failed : key={key} not found"
            });
        } 

    }
}

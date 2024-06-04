using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace ConsumingAPI.Controllers
{
   
    public class UserFetchController : ControllerBase
    {
        //[HttpGet]
        //[Route("Getalldata")]
        //public IActionResult GetData()
        //{
        //    HttpClient client = new HttpClient();
        //    client.BaseAddress = new Uri("https://localhost:7102/");
        //    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        //    HttpResponseMessage responseMessage = client.GetAsync("api/User/Get").Result;
        //    if (responseMessage.IsSuccessStatusCode)
        //    {
        //        string jsonResponse =  await responseMessage.Content.ReadAsStringAsync();

        //        // Deserialize JSON response
        //        var responseObject = JsonConvert.DeserializeObject<Response>(jsonResponse);

        //        // Find the Content-Type header and return its value
        //        foreach (var header in responseObject.Headers)
        //        {
        //            if (header.Key == "Content-Type")
        //            {
        //                return Ok(header.Value[0]); // Return the Content-Type header value
        //            }
        //        }
        //    }
        //    else
        //    {
        //        return BadRequest("Something went wrong");
        //    }
        //}

        //[HttpGet]
        //[Route("Getalldata")]
        //public async Task<IActionResult> GetData()
        //{
        //    using (HttpClient client = new HttpClient())
        //    {
        //        client.BaseAddress = new Uri("https://localhost:7102/");
        //        client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

        //        HttpResponseMessage responseMessage = await client.GetAsync("api/User/Get");
        //        if (responseMessage.IsSuccessStatusCode)
        //        {
        //            string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

        //            // Deserialize JSON response into a list of headers
        //            var headers = JsonConvert.DeserializeObject<List<Header>>(jsonResponse);

        //            // Find the Content-Type header and return its value
        //            foreach (var header in headers)
        //            {
        //                if (header.Key == "Content-Type")
        //                {
        //                    return Ok(header.Value[0]); // Return the Content-Type header value
        //                }
        //            }
        //            return NotFound("Content-Type header not found in response.");
        //        }
        //        else
        //        {
        //            return BadRequest("Something went wrong");
        //        }
        //    }
        [HttpGet]
        [Route("Getalldata")]
        public async Task<IActionResult> GetData()
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7102/");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                HttpResponseMessage responseMessage = await client.GetAsync("api/User/Get");
                if (responseMessage.IsSuccessStatusCode)
                {
                    string jsonResponse = await responseMessage.Content.ReadAsStringAsync();

                    // Deserialize JSON response into a list of UserEntity objects
                    var users = JsonConvert.DeserializeObject<List<UserEntity>>(jsonResponse);

                    // Return the list of UserEntity objects
                    return Ok(users);
                }
                else
                {
                    return BadRequest("Something went wrong");
                }
            }
        }
            [HttpPost]
            [Route("AddUser")]
            public async Task<IActionResult> AddUser(UserEntity user)
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("https://localhost:7102/");
                    client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                    // Serialize the user object to JSON
                    string json = JsonConvert.SerializeObject(user);

                    // Create the HTTP request content
                    var content = new StringContent(json, Encoding.UTF8, "application/json");

                    // Send the HTTP POST request to the "add" endpoint
                    HttpResponseMessage responseMessage = await client.PostAsync("api/User/add", content);

                    // Check if the request was successful
                    if (responseMessage.IsSuccessStatusCode)
                    {
                        // Read the response content
                        string responseData = await responseMessage.Content.ReadAsStringAsync();

                        // Return the response data
                        return Ok(responseData);
                    }
                    else
                    {
                        // If the request was not successful, return a bad request
                        return BadRequest("Failed to add user");
                    }
                }
            }

        [HttpPut]
        [Route("EditUser/{userId}")]
        public async Task<IActionResult> EditUser(int userId, UserEntity user)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7102/");
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                // Serialize the user object to JSON
                string json = JsonConvert.SerializeObject(user);

                // Create the HTTP request content
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                // Send the HTTP PUT request to the "edit" endpoint
                HttpResponseMessage responseMessage = await client.PutAsync($"api/User/edit/{userId}", content);

                // Check if the request was successful
                if (responseMessage.IsSuccessStatusCode)
                {
                    // Read the response content
                    string responseData = await responseMessage.Content.ReadAsStringAsync();

                    // Return the response data
                    return Ok(responseData);
                }
                else
                {
                    // If the request was not successful, return a bad request
                    return BadRequest("Failed to edit user");
                }
            }
        }

        [HttpDelete]
        [Route("DeleteUser/{userId}")]
        public async Task<IActionResult> DeleteUser(int userId)
        {
            using (HttpClient client = new HttpClient())
            {
                client.BaseAddress = new Uri("https://localhost:7102/");

                // Send the HTTP DELETE request to the "delete" endpoint
                HttpResponseMessage responseMessage = await client.DeleteAsync($"api/User/delete/{userId}");

                // Check if the request was successful
                if (responseMessage.IsSuccessStatusCode)
                {
                    // Read the response content
                    string responseData = await responseMessage.Content.ReadAsStringAsync();

                    // Return the response data
                    return Ok(responseData);
                }
                else
                {
                    // If the request was not successful, return a bad request
                    return BadRequest("Failed to delete user");
                }
            }
        }
    }
    }

    // Define your response models
    public class Response
    {
        public List<Header> Headers { get; set; }
    }

    public class Header
    {
        public string Key { get; set; }
        public List<string> Value { get; set; }
    }

    

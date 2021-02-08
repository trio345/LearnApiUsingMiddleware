using LearnApiUsingMiddleware.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace LearnApiUsingMiddleware.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TodosController : ControllerBase
    {
        private HttpClient client;
        private List<TodosModel> dtTodo;
        string url;

        public TodosController()
        {
            client = new HttpClient();
            dtTodo = new List<TodosModel>();
            url = "https://jsonplaceholder.typicode.com";
        }

        // GET: api/Todos
        [HttpGet]
        public List<TodosModel> GetTodosModel()
        {

            using (client)
            {
                var uri = new Uri($"{url}/todos");
                var response = client.GetAsync(uri).Result;

                if (!response.IsSuccessStatusCode)
                {
                    throw new Exception(response.ToString());
                }

                var responseContent = response.Content;
                var responseString = responseContent.ReadAsStringAsync().Result;

                dynamic todos = JArray.Parse(responseString) as JArray;

                foreach (var todo in todos)
                {
                    TodosModel tm = todo.ToObject<TodosModel>();
                    dtTodo.Add(tm);
                }
            }

            return dtTodo;
        }

        [HttpGet("{id}")]
        public TodosModel GetTodo(int id)
        {
            TodosModel dto;
            var uri = new Uri($"{url}/todos/{id}");

            var httpContent = client.GetAsync(uri).Result;
            if (!httpContent.IsSuccessStatusCode)
            {
                throw new Exception("data not available");
            }

            var jsonContent = httpContent.Content.ReadAsStringAsync().Result;
            dynamic todo = JsonConvert.DeserializeObject(jsonContent);
            dto = todo.ToObject<TodosModel>();
            return dto;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TodosModel todos)
        {
            var uri = new Uri($"{url}/todos");
            var json = JsonConvert.SerializeObject(todos);
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var httpContext = await client.PostAsync(uri, data);

            var response = httpContext.Content.ReadAsStringAsync().Result;

            return Ok(response);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] TodosModel todos)
        {
            var uri = new Uri($"{url}/todos/{id}");

            var json = JsonConvert.SerializeObject(todos.Equals(id));
            var data = new StringContent(json, Encoding.UTF8, "application/json");

            var req = await client.PutAsync(uri, data);

            var response = req.Content.ReadAsStringAsync().Result;

            return Ok(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int id)
        {
            var uri = new Uri($"{url}/todos/{id}");

            var req = await client.DeleteAsync(uri);
            if (!req.IsSuccessStatusCode)
            {
                return BadRequest("Please try again!");
            }

            return Ok("Success delete!");
        }

    }
}

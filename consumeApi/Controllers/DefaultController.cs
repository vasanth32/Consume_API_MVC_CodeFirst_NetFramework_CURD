using consumeApi.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace consumeApi.Controllers
{
    public class DefaultController : Controller
    {
       public  string  Baseurl = "https://localhost:44368/";

        // GET: Default
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Create(Product product)
        {
            
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    var response = await client.PostAsJsonAsync("api/Products", product);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
            }
            return View();
        }



        public async Task<ActionResult> Index()
        {
            List<Product> ProdInfo = new List<Product>();
   
            using (var client = new HttpClient())
            {
                //Passing service base url
                client.BaseAddress = new Uri(Baseurl);
                client.DefaultRequestHeaders.Clear();
                //Define request data format
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //Sending request to find web api REST service resource GetAllEmployees using HttpClient
                HttpResponseMessage Res = await client.GetAsync("api/Products");
                //Checking the response is successful or not which is sent using HttpClient
                if (Res.IsSuccessStatusCode)
                {
                    //Storing the response details recieved from web api
                    var ProdResponse = Res.Content.ReadAsStringAsync().Result;
                    //Deserializing the response recieved from web api and storing into the Employee list
                    ProdInfo = JsonConvert.DeserializeObject<List<Product>>(ProdResponse);
                }

                //returning the employee list to view
                return View(ProdInfo);
            }
        }


        
        //public async Task<ActionResult> Delete(int id)
        //{
        //    Product prod = new Product();

        //    if (ModelState.IsValid)
        //    {
        //        using (var client = new HttpClient())
        //        {
        //            client.BaseAddress = new Uri(Baseurl);

        //            HttpResponseMessage response = await client.GetAsync("api/Products/" + id);

        //            if (response.IsSuccessStatusCode)
        //            {
        //                var prodResponse = response.Content.ReadAsStringAsync().Result;
        //                prod = JsonConvert.DeserializeObject<Product>(prodResponse);
        //            }
        //            else
        //            {
        //                ModelState.AddModelError(string.Empty, "Server error try after some time.");
        //            }
        //        }
        //    }
        //    return View(prod);

        //}

        //[HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
               if (ModelState.IsValid)
                {
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(Baseurl);

                        var response = await client.DeleteAsync("api/Products/" + id);
                        if (response.IsSuccessStatusCode)
                        {
                            return RedirectToAction("Index");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Server error try after some time.");
                        }
                    }
                }
            
                return View();
           
          
        }

        public async Task<ActionResult> Details(int id)
        {
            Product prod = new Product();

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    HttpResponseMessage response = await client.GetAsync("api/Products/" + id);

                    if (response.IsSuccessStatusCode)
                    {
                        var prodResponse = response.Content.ReadAsStringAsync().Result;
                        prod = JsonConvert.DeserializeObject<Product>(prodResponse);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
            }
            return View(prod);
        }
        public async Task<ActionResult> Edit(int id)
        {
            Product prod = new Product();

            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    HttpResponseMessage response = await client.GetAsync("api/Products/"+ id);
                    
                    if (response.IsSuccessStatusCode)
                    {
                        //prod.ProductId = response.
                        var prodResponse = response.Content.ReadAsStringAsync().Result;
                        prod =  JsonConvert.DeserializeObject<Product>(prodResponse);
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
            }
            return View(prod);

        }

        [HttpPost]
        public async  Task<ActionResult> Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(Baseurl);

                    var response = await client.PutAsJsonAsync("api/Products", product);
                    if (response.IsSuccessStatusCode)
                    {
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Server error try after some time.");
                    }
                }
            }
            return View();
        }


    }
}
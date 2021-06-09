using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using FraktonProject.Dtos;
using FraktonProject.Helpers;
using FraktonProject.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace FraktonProject.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CryptoController : ControllerBase
    {

        private readonly string apiUrl = "https://api.coincap.io/v2/assets";

		private readonly ApplicationDbContext _context;
        public CryptoController(ApplicationDbContext context)
        {
			_context = context;
        }

        [Authorize]
        [HttpPost("MarkOrUnmark/{id}/{marked}")]
        public async Task<IActionResult> MarkOrUnmark(string id, bool marked)
        {
            try
            {

                var specificCoin = _context.Cryptos.FirstOrDefault(x => x.Id == id);
                specificCoin.Marked = marked;
				_context.Entry(specificCoin).State = EntityState.Modified;
				await _context.SaveChangesAsync();
				return Ok(specificCoin);

            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

        [Authorize]
        [HttpGet("GetList")]
        public async Task<IActionResult> GetList()
        {
			try
			{
				var client = new HttpClient
				{
					BaseAddress = new Uri(apiUrl)
				};

				client.DefaultRequestHeaders.Accept.Add(
				new MediaTypeWithQualityHeaderValue("application/json"));

				var response = await client.GetAsync(apiUrl);
				var jsonString = await response.Content.ReadAsStringAsync();
				var allResponses = JsonConvert.DeserializeObject<ApiResponse<FraktonProject.Models.Crypto>>(jsonString);

                foreach (var item in allResponses.Data.ToList())
                {
					item.Marked = false;
					_context.Cryptos.Add(item);
					await _context.SaveChangesAsync();
                }

				return Ok(allResponses);


			}
			catch (Exception error)
			{
				return BadRequest(error);
			}
        }

        [Authorize]
        [HttpGet("GetFavouriteCoins")]
        public async Task<IActionResult> GetFavouriteCoins()
        {
            try
            {
                var favouritesCoins = _context.Cryptos.Where(x => x.Marked == true).ToList();

                return Ok(favouritesCoins);

            }
            catch (Exception error)
            {
                return BadRequest(error);
            }
        }

    }
}

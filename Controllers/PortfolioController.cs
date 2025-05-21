using api.Extensions;
using api.Interfaces;
using api.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [Route("/api/portfolio")]
    [ApiController]
    public class PortfolioController : ControllerBase
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly IStockRepository _stockRepo;
        private readonly IPortfolioRepository _portfolioRepo;
        public PortfolioController(UserManager<AppUser> userManager, IStockRepository stockRepo, IPortfolioRepository portfolioRepo)
        {
            _userManager = userManager;
            _stockRepo = stockRepo;
            _portfolioRepo = portfolioRepo;
        }

        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetUserPortfolio()
        {
            string? username = User.GetUserName();

            if (username == null)
            {
                return BadRequest("User not found.");
            }

            AppUser? appUser = await _userManager.FindByNameAsync(username);
            List<Stock> userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);
            return Ok(userPortfolio);
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> AddPortfolio(string symbol)
        {
            string? username = User.GetUserName();
            AppUser? appUser = await _userManager.FindByNameAsync(username);
            Stock? stockModel = await _stockRepo.GetBySymbolAsync(symbol);

            if (stockModel == null)
            {
                return NotFound("No stock found associated with this symbol.");
            }

            List<Stock> userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

            if (userPortfolio.Any(p => p.Symbol.ToLower() == symbol.ToLower()))
            {
                return BadRequest("Stock already exists on this user portfolio.");
            }

            Portfolio portfolioModel = new Portfolio
            {
                AppUserId = appUser.Id,
                StockId = stockModel.Id
            };

            await _portfolioRepo.CreatePortfolio(portfolioModel);

            if (portfolioModel == null)
            {
                return StatusCode(500, "Internal Server Error");
            }
            return Created();
        }

        [HttpDelete]
        [Authorize]
        public async Task<IActionResult> DeletePortfolio(string symbol)
        {
            string? username = User.GetUserName();
            AppUser? appUser = await _userManager.FindByNameAsync(username);
            List<Stock> userPortfolio = await _portfolioRepo.GetUserPortfolio(appUser);

            List<Stock> filteredStocks = userPortfolio.Where(s => s.Symbol.ToLower() == symbol.ToLower()).ToList();

            if (filteredStocks.Count() == 1)
            {
                await _portfolioRepo.DeletePortfolio(appUser, symbol);
            }
            else
            {
                return BadRequest("Stock does not exist in this user portfolio.");
            }

            return NoContent();
        }
    }
}
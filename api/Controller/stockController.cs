using System;
using System.Collections.Generics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace API.Contollers
{
    [Route("api/stock")]
    [APIController]
    public class stockController: ControllerBase
    {
        private readonly DataContext _context;
        private readonly IStockRepository _stockrepo;

        public stockController(DataContext context, IStockRepository stockrepo)
        {
            _stockrepo = stockrepo;
            _context = context;
        }

        [HttpGet]
        public async Task<IactionResult> Getall()
        {
            var stocks = await _stockrepo.GetAllAsync();

            var stockDto = stocks.Select(s => s.ToStockDto());

            return Ok(stocks);
        }

        [HttpGet("{id: int}")]
        public async Task<IactionResult> Get([FromRoute] int id)
        {
            var stock = await _stockrepo.GetByIdAsync(id);

            if(stock == null)
            {
                return NotFound();
            }
             return Ok(stock.ToStockDto());
        }

        [HttpPost]
        public async Task<IactionResult> Create([FromBody] CreateStockRequestDto StockDto)
        {
            var stockModel = stockDto.ToStockFromCreateDto();
            await _stockrepo.stock.CreateAsync(stockModel);
            return CreatedAtAction(nameof(GetById), new {id = stockModel.Id}, stockModel.ToStockDto())
        }

        [HttpPut]
        [Route("{id: int}")]
        public async Task<IactionResult> Update([FromRoute] int id, [FromBody] UpdateStockRequestDto UpdateDto)
        {
            var stockModel = await _stockrepo.stocks.UpdateAsync(id, UpdateDto);

            if(stockModel == null)
            {
                return NotFound();
            }

            return Ok(stockModel.ToStockDto)
        }

        [HttpDelete]
        [Route("{id: int}")]
        public async Task<IactionResult> Delete([FromRoute] int id)
        {
            var stockModel = await _stockrepo.stocks.DeleteAsync(id);

            if(stockModel == null)
            {
                return NotFound();
            }
            
            return NoContent();
        }
    }
}
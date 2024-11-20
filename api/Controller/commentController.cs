using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace api.Controller
{
    [Route("api/comment")]
    [ApiController]
    public class commentController: ControllerBase
    {
        
        private readonly ICommentRepository _CommentRepo
        private readonly IStockRepository _stockRepo

        public commentController(ICommentRepository commentRepo, IStockRepository stockRepo)
        {
            _CommentRepo = commentRepo; 
            _stockRepo = stockRepo  
        }

        [HttpGet]
        public async Task<IactionResult> Getall([FromQuery] commentQueryObject queryObject)
        {
            if(!ModelState.IsValid)
            return BadRequest(ModelState);

            var comments = await _CommentRepo.GetAllAsync(queryObject);
            var commentDto = comments.Select(s => s.ToCommentDto());

            return Ok(commentDto);
        }

        [HttpGet("{id: int}")]
        public async Task<IactionResult> GetById([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var comment = await _CommentRepo.GetByIdAsync(id);

            if(comment == null)
            {
                return NotFound();
            }

            return Ok(comment.ToCommentDto());
        }

        // [HttpPost]
        // [Route("{symbol:alpha}")]
        // public async Task<IactionResult> Create([FromRoute] string symbol, CreateCommnetDto commentDto)
        // {
        //     if(!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     var stock = await _stockRepo.GetBySymbolAsync(symbol);

        //     if(stock == null)
        //     {
        //         stock = await _fmpService.FindStockBySymbolAsync(symbol);
        //         if(stock == null)
        //         {
        //             return BadRequest("Stock does not exists");
        //         }
        //         else
        //         {
        //             await _stockRepo.CreateAsync(stock);
        //         }
        //     }

        // }


        [HttpPost("{stockId: int}")]// One to Many Relationship
        public async Task<IactionResult> Create([FromRoute] int stockId, CreateCommentDto commentDto)
        {
            if(!await _stockRepo.StockExist(stockId))
            {
                return BadRequest("Stock does not exists");
            }
             var commentModel = commentDto.ToCommentFromCreate(stockId);
             await _CommentRepo.CreateAsync(commnetModel);
             return CreatedAtAction(nameof(GetById), new{id = commentModel.Id}, commentModel.ToCommentDto())
        }


        [HttpPut]
        [Route("{id:int}")]
        public async Task<IactionResult> Update([FromRoute] int id, [FromBody] UpdateCommentRequestDto UpdateDto )
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commnet = _CommentRepo.UpdateAsync(id, UpdateDto.ToCommnetFromUpdate(id));

            if(comment == null)
            {
                return NotFound("Comment not found")
            }

            return Ok(comment.ToCommentDto());
        }

        [HttpDelete]
        [FromRoute("{id:int}")]
        public async Task<IactionResult> Delete([FromRoute] int id)
        {
            if(!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var commentModel = await _CommentRepo.DeleteAsync(id);

            if(comment == null)
            {
                return NotFound("Comment not found")
            }

            return Ok(commentModel);
        }
    }
}
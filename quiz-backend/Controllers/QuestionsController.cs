using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using quiz_backend.Models;
using Microsoft.EntityFrameworkCore;

namespace quiz_backend.Controllers
{
    [Produces("application/json")]
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionsController : ControllerBase
    {
        readonly QuizContext context;
        public QuestionsController(QuizContext context) 
        {
            this.context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody]Question question)
        {
            context.Add(question);
            await context.SaveChangesAsync();
            return Ok(question);
        }

        [HttpGet]
        public IEnumerable<Question> Get()
        {
            return context.Questions;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody]Question question)
        {
            // Question question = await context.Questions.SingleOrDefaultAsync(q => q.ID == id);

            //context.Entry needs ID in the body data, so to ensure id's are matching use a simple if statement to check
            if(id != question.ID)
            {
                return BadRequest();
            }

            context.Entry(question).State = EntityState.Modified;

            await context.SaveChangesAsync();

            return Ok(question);
        }
    }
}
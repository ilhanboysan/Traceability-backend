using Api_React_CRUD.Models.cfgErrors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class cfgErrorsController : ControllerBase
{
    private readonly ILogger<cfgErrorsController> _logger;
    private readonly cfgErrorsContext _cfgerrorsContext;

    public cfgErrorsController(cfgErrorsContext cfgerrorscontext)
    {
        _cfgerrorsContext = cfgerrorscontext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<cfgErrors>>> GetErrors()
    {
        var errors = await _cfgerrorsContext.cfgErrorss.ToListAsync();

        if (errors == null || errors.Count == 0)
        {
            return NotFound("No errors found");
        }

        return Ok(errors);
    }

    [HttpPost]
    public async Task<ActionResult> PostErrors(cfgErrors newError)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var existingError = await _cfgerrorsContext.cfgErrorss
                    .FirstOrDefaultAsync(e => e.ErrorNo == newError.ErrorNo && e.ErrorText == newError.ErrorText);

                if (existingError != null)
                {
                    return Conflict("Error already exists");
                }

                _cfgerrorsContext.cfgErrorss.Add(newError);
                await _cfgerrorsContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetErrors), new { errorNo = newError.ErrorNo, errorText = newError.ErrorText }, newError);
            }
            catch (Exception)
            {
                return StatusCode(500, "An error occurred while processing your request");
            }
        }
        else
        {
            return BadRequest(ModelState);
        }
    }



    [HttpPut("{errorNo}")]
    public async Task<ActionResult> PutErrors(int errorNo, [FromBody] cfgErrors updatedError)
    {
        try
        {
            var existingError = await _cfgerrorsContext.cfgErrorss
                .FirstOrDefaultAsync(e => e.ErrorNo == errorNo);

            if (existingError == null)
            {
                return NotFound();
            }

            existingError.ErrorText = updatedError.ErrorText;

            _cfgerrorsContext.Entry(existingError).State = EntityState.Modified;
            await _cfgerrorsContext.SaveChangesAsync();

            return Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Concurrency error: The record you attempted to update was modified by another user. Please refresh and try again.");
        }
        catch (Exception)
        {
            return StatusCode(500, "An error occurred while processing your request.");
        }
    }



    [HttpDelete("{errorNo}/{errorText}")]
    public async Task<ActionResult> DeleteErrors(int errorNo, string errorText)
    {
        var cfgerrors = await _cfgerrorsContext.cfgErrorss
            .FirstOrDefaultAsync(e => e.ErrorNo == errorNo && e.ErrorText == errorText);

        if (cfgerrors == null)
        {
            return NotFound();
        }

        _cfgerrorsContext.cfgErrorss.Remove(cfgerrors);
        await _cfgerrorsContext.SaveChangesAsync();

        return Ok();
    }


}

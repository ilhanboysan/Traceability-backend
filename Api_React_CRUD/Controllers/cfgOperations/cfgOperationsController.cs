using Api_React_CRUD.Models.cfgOperations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class cfgOperationsController : ControllerBase
{
    private readonly ILogger<cfgMachinesController> _logger;
    private readonly cfgOperationsContext _cfgOperationsContext;

    public cfgOperationsController(cfgOperationsContext cfgOperationsContext)
    {
        _cfgOperationsContext = cfgOperationsContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<cfgOperations>>> GetOperations()
    {
        var operations = await _cfgOperationsContext.cfgOperationss.ToListAsync();

        if (operations == null || operations.Count == 0)
        {
            return NotFound("No errors found");
        }

        return Ok(operations);
    }
    [HttpPost]
    public async Task<ActionResult<cfgOperations>> CreateOperation(cfgOperations newOperation)
    {


        if (ModelState.IsValid)
        {
            try
            {
                // Veritabanına yeni bir öğe ekleyin
                _cfgOperationsContext.cfgOperationss.Add(newOperation);

                _cfgOperationsContext.cfgOperationss.Add(newOperation);
                await _cfgOperationsContext.SaveChangesAsync();
    
                // Başarıyla eklenen öğeyi gönderin
                return CreatedAtAction(nameof(GetOperations), new { operationID = newOperation.OperationNo }, newOperation);
            }
            catch (Exception ex)
            {
                // Hata durumunda uygun bir yanıt gönderin


                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }
        else
        {
            // Model doğrulama hatası durumunda uygun bir yanıt gönderin
            return BadRequest(ModelState);
        }
    }

    [HttpPut("{OperationNo}")]
    public async Task<ActionResult> UpdateOperation(int OperationNo, [FromBody] cfgOperations updatedOperation)
    {
        try
        {
            // Veritabanından mevcut öğeyi alın
            var existingOperation = await _cfgOperationsContext.cfgOperationss
                .FirstOrDefaultAsync(o => o.OperationNo == OperationNo);

            if (existingOperation == null)
            {
                // Belirtilen ID ile öğe bulunamazsa NotFound (404) döndürün
                return NotFound();
            }

            // Mevcut öğeyi güncelleyin
            existingOperation.OperationDescription = updatedOperation.OperationDescription;
            existingOperation.OperationNotes = updatedOperation.OperationNotes;
            existingOperation.AssemblyOperation = updatedOperation.AssemblyOperation;

            // Veritabanında değişiklikleri kaydedin
            await _cfgOperationsContext.SaveChangesAsync();

            // Başarı durumunda uygun bir yanıt gönderin
            return Ok();
        }
        catch (Exception ex)
        {
            // Hata durumunda uygun bir yanıt gönderin
            return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
        }
    }

    [HttpDelete("{OperationNo}")]
    public async Task<ActionResult> DeleteOperation(int OperationNo)
    {
        try
        {
            // Veritabanından mevcut öğeyi alın
            var existingOperation = await _cfgOperationsContext.cfgOperationss
                .FirstOrDefaultAsync(o => o.OperationNo == OperationNo);

            if (existingOperation == null)
            {
                // Belirtilen ID ile öğe bulunamazsa NotFound (404) döndürün
                return NotFound();
            }

            // Veritabanından öğeyi kaldırın
            _cfgOperationsContext.cfgOperationss.Remove(existingOperation);
            await _cfgOperationsContext.SaveChangesAsync();

            // Başarı durumunda uygun bir yanıt gönderin
            return Ok();
        }
        catch (Exception ex)
        {
            // Hata durumunda uygun bir yanıt gönderin
            return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
        }
    }


}

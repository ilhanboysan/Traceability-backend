using Api_React_CRUD.Models.cfgMachineOperation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class cfgMachineOpeartionController : ControllerBase
{
    private readonly ILogger<cfgMachineOpeartionController> _logger;
    private readonly cfgMachineOperationContext _cfgmachineoperationcontext;

    public cfgMachineOpeartionController(cfgMachineOperationContext cfgMachineOperationContext)
    {
        _cfgmachineoperationcontext = cfgMachineOperationContext;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<cfgMachineOperation>>> GetMachineOperation()
    {
        var machineoperations = await _cfgmachineoperationcontext.cfgMachineOperation.ToListAsync();

        if (machineoperations == null || machineoperations.Count == 0)
        {
            return NotFound("No errors found");
        }

        return Ok(machineoperations);
    }
    [HttpPost]
    public async Task<ActionResult> AddMachineOperation(cfgMachineOperation newMachineOperation)
    {
        if (ModelState.IsValid)
        {
            try
            {
                _cfgmachineoperationcontext.cfgMachineOperation.Add(newMachineOperation);
                await _cfgmachineoperationcontext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMachineOperation), new { MachineID = newMachineOperation.MachineID, OperationNo = newMachineOperation.OperationNo }, newMachineOperation);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
            }
        }
        else
        {
            return BadRequest(ModelState);
        }
    }

    [HttpDelete("{machineID}/{operationNo}")]
    public async Task<ActionResult> DeleteMachineOperation(int machineID, int operationNo)
    {
        try
        {
            var machineOperation = await _cfgmachineoperationcontext.cfgMachineOperation
                .FirstOrDefaultAsync(m => m.MachineID == machineID && m.OperationNo == operationNo);

            if (machineOperation == null)
            {
                return NotFound();
            }

            _cfgmachineoperationcontext.cfgMachineOperation.Remove(machineOperation);
            await _cfgmachineoperationcontext.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
        }
    }

}
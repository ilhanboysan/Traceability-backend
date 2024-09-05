    using Api_React_CRUD.Models.cfgMachines;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    [Route("api/[controller]")]
    [ApiController]
    public class cfgMachinesController : ControllerBase
    {
        private readonly ILogger<cfgMachinesController> _logger;
        private readonly cfgMachinesContext _cfgMachinesContext;

        public cfgMachinesController(cfgMachinesContext cfgmachinescontext)
        {
            _cfgMachinesContext = cfgmachinescontext;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<cfgMachines>>> GetMachines()
        {
            var machines = await _cfgMachinesContext.cfgMachines.ToListAsync();

            if (machines == null || machines.Count == 0)
            {
                return NotFound("No errors found");
            }

            return Ok(machines);
        }

    [HttpPost]
    public async Task<ActionResult> PostMachines(cfgMachines newMachine)
    {
        if (ModelState.IsValid)
        {
            try
            {
                var existingMachine = await _cfgMachinesContext.cfgMachines
                    .FirstOrDefaultAsync(e => e.MachineName == newMachine.MachineName);

                if (existingMachine != null)
                {
                    return Conflict("Machine already exists");
                }

                _cfgMachinesContext.cfgMachines.Add(newMachine);
                await _cfgMachinesContext.SaveChangesAsync();

                return CreatedAtAction(nameof(GetMachines), new { machineID = newMachine.MachineID }, newMachine);
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



    [HttpPut("{machineID}")]
    public async Task<ActionResult> PutMachine(int machineID, [FromBody] cfgMachines updatedMachine)
    {
        try
        {
            var existingMachine = await _cfgMachinesContext.cfgMachines
                .FirstOrDefaultAsync(m => m.MachineID == machineID);

            if (existingMachine == null)
            {
                return NotFound();
            }

            existingMachine.MachineName = updatedMachine.MachineName;
           

            _cfgMachinesContext.Entry(existingMachine).State = EntityState.Modified;
            await _cfgMachinesContext.SaveChangesAsync();

            return Ok();
        }
        catch (DbUpdateConcurrencyException)
        {
            return Conflict("Concurrency error: The record you attempted to update was modified by another user. Please refresh and try again.");
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
        }
    }



    [HttpDelete("{machineID}")]
    public async Task<ActionResult> DeleteMachine(int machineID)
    {
        try
        {
            var cfgMachine = await _cfgMachinesContext.cfgMachines
                .FirstOrDefaultAsync(m => m.MachineID == machineID);

            if (cfgMachine == null)
            {
                return NotFound();
            }

            _cfgMachinesContext.cfgMachines.Remove(cfgMachine);
            await _cfgMachinesContext.SaveChangesAsync();

            return Ok();
        }
        catch (Exception ex)
        {
            return StatusCode(500, $"An error occurred while processing your request: {ex.Message}");
        }
    }




}

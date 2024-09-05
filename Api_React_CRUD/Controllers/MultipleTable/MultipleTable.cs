using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Api_React_CRUD.Models.MultipleTable;
using Api_React_CRUD.Models.cfgMachines;
using Api_React_CRUD.Models.cfgOperations;
using Api_React_CRUD.Models.cfgMachineOperation;

namespace Api_React_CRUD.Controllers.MultipleTable
{
    [Route("api/[controller]")]
    [ApiController]
    public class MultipleTableController : ControllerBase
    {
        private readonly MultipleTableContext _context;

        public MultipleTableController(MultipleTableContext context)
        {
            _context = context;
        }

        [HttpGet]
        public ActionResult<IEnumerable<object>> GetMultipleTables()
        {
            var result = (
                from operation in _context.cfgOperations
                join machineOperation in _context.cfgMachineOperations on operation.OperationNo equals machineOperation.OperationNo
                join machine in _context.cfgMachines on machineOperation.MachineID equals machine.MachineID
                select new
                {
                    operation.OperationNo,
                    operation.OperationDescription,
                    operation.OperationNotes,
                    operation.AssemblyOperation,
                    machineOperation.MachineID,
                    machine.MachineName
                }
            ).ToList();

            return Ok(result);
        }
    }
}

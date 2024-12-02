using Microsoft.AspNetCore.Mvc;
using MSMS.Data.Interfaces;
using MSMS.Models.Procedures;

namespace MSMS.API;

[Route("api/[controller]")]
[ApiController]
public class ActiveProcedureController : ControllerBase
{
    private readonly IActiveProcedureDatabaseRepository _activeProcedureDb;

    public ActiveProcedureController(IActiveProcedureDatabaseRepository activeProcedureDb)
    {
        _activeProcedureDb = activeProcedureDb;
    }

    [HttpGet("getall")]
    public ActionResult<IEnumerable<ActiveProcedure>> GetAllActiveProcedures()
    {
        var activeProcedures = _activeProcedureDb.GetAll();
        return Ok(activeProcedures);
    }

    [HttpGet("get")]
    public ActionResult<ActiveProcedure?> GetActiveProcedure(int id)
    {
        var activeProcedure = _activeProcedureDb.GetById(id);
        if (activeProcedure is null)
        {
            return NotFound();
        }
        return Ok(activeProcedure);
    }

    [HttpDelete("delete/{id}")]
    public ActionResult DeleteActiveProcedure(int id)
    {
        var activeProcedure = _activeProcedureDb.GetById(id);
        if (activeProcedure is null)
        {
            return NotFound();
        }
        _activeProcedureDb.Delete(activeProcedure);
        return Ok();
    }
}

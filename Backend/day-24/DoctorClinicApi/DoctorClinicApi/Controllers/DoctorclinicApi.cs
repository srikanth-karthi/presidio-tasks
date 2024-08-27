using DoctorClinicApi.Interfaces;
using DoctorClinicApi.Model;
using DoctorClinicApi.Exceptions;
using Microsoft.AspNetCore.Mvc;


[Route("api/[controller]")]
[ApiController]
public class DoctorController : ControllerBase
{

    private readonly IDoctorService _doctorService;

    public DoctorController(IDoctorService doctorService)
    {
        _doctorService = doctorService;
    }

    [HttpGet]
    public async Task<ActionResult<IList<Doctor>>> Get()
    {
        try
        {
            var doctors = await _doctorService.GetDoctors();
            return Ok(doctors.ToList());
        }
        catch (DoctorNotFoundExceptions nefe)
        {
            return NotFound(nefe.Message);
        }
    }

    [HttpGet("speciality/{speciality}")]
    public async Task<ActionResult<Doctor>> GetDoctorBySpeciality(string speciality)
    {
        try
        {
            var doctors = await _doctorService.GetDoctorBySpeciality(speciality);

            return Ok(doctors);

        }
        catch (DoctorNotFoundExceptions nefe)
        {
            return NotFound(nefe.Message);
        }
    }

    [HttpPut("{id}/experience")]
    public async Task<ActionResult<Doctor>> Put(int id, [FromBody]float experience)
    {
        try
        {
            var doctor = await _doctorService.UpdateDoctorExperience(id, experience);
            return Ok(doctor);
        }
        catch (DoctorNotFoundExceptions nsee)
        {
            return NotFound(nsee.Message);
        }
    }
}

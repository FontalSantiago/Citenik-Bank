using Application.DTO;
using Application.Entities;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public class PlanesServices : ControllerBase, IPlanesServices
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public PlanesServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<ActionResult<List<PlanesConsulta>>> listarPlanes()
        {
            try
            {
                var query = await (
                from Planes in _context.Planes
                where Planes.vigenciaDesde <= DateTime.Now && (Planes.vigenciaHasta >= DateTime.Now || Planes.vigenciaHasta == null)
                select new PlanesConsulta
                {
                    id = Planes.id,
                    nombre = Planes.nombre,
                    TNA = Planes.TNA,
                    cuotasMax = Planes.cuotasMax,
                    cuotasMin = Planes.cuotasMin,
                    montoMax = Planes.montoMax,
                    montoMin = Planes.montoMin,
                    edadMax = Planes.edadMax == null ? 0 : Planes.edadMax,
                    precanCuota = Planes.precanCuota == null ? 0 : Planes.precanCuota,
                    precanMulta = Planes.precanMulta == null ? 0 : Planes.precanMulta,
                    costoOtorgamiento = Planes.costoOtorgamiento == null ? 0 : Planes.costoOtorgamiento,
                    vigenciaDesde = Planes.vigenciaDesde,
                    vigenciaHasta = Planes.vigenciaHasta
                }).ToListAsync();

                return Ok(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los prestamos", ex);
            }
        }

        public async Task<PlanDTO> obtenerPlan(int id)
        {
            try
            {
                var plan = await _context.Planes.FirstOrDefaultAsync(x => x.id == id);
                return _mapper.Map<PlanDTO>(plan);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el plan", ex);
            }
        }

        public async Task<ActionResult<List<PlanesConsulta>>> obtenerPlanNombre(string nombre)
        {
            try
            {
                var plan = await _context.Planes.Where(x => x.nombre == nombre).ToListAsync();
                return _mapper.Map<List<PlanesConsulta>>(plan);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el plan por nombre", ex);
            }

        }
        public async Task<ActionResult> guardarPlan(Plan plan)
        {
            try
            {
                var existePlan = await _context.Planes.AnyAsync(x => x.nombre == plan.nombre);
                if (existePlan)
                {
                    return BadRequest($"El plan {plan.nombre} ya existe");
                }
                _context.Add(plan);
                await _context.SaveChangesAsync();
                return Created("/api/planes", plan);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar agregar un plan", ex);
            }
        }

        public async Task<ActionResult> modificarPlan(int id, Plan plan)
        {
            try
            {
                if (plan.id != id)
                {
                    return NotFound("El Id del plan no esta registrado en el sistema");
                }
                _context.Update(plan);
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar actualizar los datos de un plan", ex);
            }
        }
    }
}

using Application.DTO;
using Application.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace Application.Services
{
    public class PrestamosServices : ControllerBase, IPrestamosServices
    {
        private readonly ApplicationDbContext _context;
        private readonly ICuotaServices _servicioCuota;
        private readonly IClienteServices _servicioCliente;
        private readonly IPlanesServices _servicioPlan;

        public PrestamosServices(ApplicationDbContext context, ICuotaServices servicioCuota, IClienteServices servicioCliente, IPlanesServices servicioPlan)
        {
            _context = context;
            _servicioCuota = servicioCuota;
            _servicioCliente = servicioCliente;
            _servicioPlan = servicioPlan;
        }

        public async Task<ActionResult<IList<PrestamoConsulta>>> listarPrestamos()
        {
            try
            {
                var query = await (
                from Prestamos in _context.Prestamos
                join Cliente in _context.Clientes on Prestamos.idCliente equals Cliente.id
                join Plan in _context.Planes on Prestamos.idPlan equals Plan.id
                join Cuota in _context.Cuotas on Prestamos.id equals Cuota.idPrestamo
                group new { Prestamos } by new { Prestamos.id, Cliente.CUIT, Plan.nombre, Prestamos.capital, Prestamos.cantidadCuotas, Prestamos.fechaOtorgamiento, Prestamos.estado } into g
                select new PrestamoConsulta
                {
                    id = g.Key.id,
                    CUIT = g.Key.CUIT,
                    nombre = g.Key.nombre,
                    capital = g.Key.capital,
                    cantidadCuotas = g.Key.cantidadCuotas,
                    cuotasPagas = _context.Cuotas.Where(x => x.idPrestamo == g.Key.id && x.fechaPago != null).Count(),
                    cuotasVencidasImpagas = _context.Cuotas.Where(x => x.fechaPago == null && x.fechaVencimiento < DateTime.Now.Date && x.idPrestamo == g.Key.id).Count(),
                    fechaOtorgamiento = g.Key.fechaOtorgamiento,
                    estado = g.Key.estado
                }).OrderBy(x => x.fechaOtorgamiento).ToListAsync();

                return Ok(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los prestamos", ex);
            }
        }

        public async Task<ActionResult<Prestamo>> obtenerPrestamo(int id)
        {
            try
            {
                var prestamo = await _context.Prestamos.FirstOrDefaultAsync(x => x.id == id);
                return Ok(prestamo);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar obtener el prestamo otorgado", ex);
            }
        }

        public async Task<ActionResult> guardarPrestamo(Prestamo prestamo)
        {
            try
            {
                var clienteDB = await _servicioCliente.obtenerCliente(prestamo.idCliente);
                var planDB = await _servicioPlan.obtenerPlan(prestamo.idPlan);
                var verificacionEdad = verificarEdadCliente(clienteDB, planDB, prestamo);
                var verificacionEntreMonto = verificarEntreMontoPlan(planDB, prestamo);
                var verificacionEntreCuota = verificarEntreCuotaPlan(planDB, prestamo);
                var verificacionDiaVencimiento = verificarDiaVencimientoPlan(prestamo);
                if (verificacionEdad && verificacionEntreMonto && verificacionEntreCuota && verificacionDiaVencimiento)
                {
                    prestamo.estado = "pendiente";
                    _context.Add(prestamo);
                    await _context.SaveChangesAsync();

                    var plan = await _context.Planes.FirstOrDefaultAsync(x => x.id == prestamo.idPlan);
                    var cantidadCuotas = prestamo.cantidadCuotas;

                    for (var i = 0; i < cantidadCuotas; i++)
                    {
                        Cuota cuota = new Cuota();
                        cuota.id = 0;
                        cuota.idPrestamo = prestamo.id;
                        cuota.nroCuota = i + 1;
                        cuota.fechaPago = null;
                        cuota.fechaVencimiento = new DateTime(prestamo.fechaOtorgamiento.Year, prestamo.fechaOtorgamiento.Month, (int)prestamo.diaVencimiento).AddMonths(cuota.nroCuota);
                        _context.Add(cuota);
                        await _context.SaveChangesAsync();
                        await _servicioCuota.guardarComposicion(cuota, plan, prestamo);
                    }

                    return Created("/api/prestamos", prestamo);
                }
                else
                {
                    var mensajeError = "";
                    if (verificacionEdad == false)
                    {
                        mensajeError += "El cliente ingresado no cumple con los requisitos de edad solicitados. ";
                    }
                    if (verificacionEntreMonto == false)
                    {
                        mensajeError += "El monto ingresado no se encuentra dentro de los rangos establecidos para el plan seleccionado. ";
                    }
                    if (verificacionEntreCuota == false)
                    {
                        mensajeError += "Las cuotas ingresadas no se encuentran dentro de los rangos establecidos para el plan seleccionado. ";
                    }
                    if (verificacionDiaVencimiento == false)
                    {
                        mensajeError += "El dia de vencimiento debe ser 10, 15 o 20";
                    }
                    return BadRequest(mensajeError);
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar agregar un prestamo", ex);
            }
        }
        public async Task<ActionResult> actualizarPrestamo(int id)
        {
            try
            {
                var existePrestamo = await _context.Prestamos.FirstOrDefaultAsync<Prestamo>(x => x.id == id);

                if (existePrestamo == null)
                {
                    return NotFound();
                }

                existePrestamo.estado = "vigente";
                await _context.SaveChangesAsync();
                return Ok();
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar actualizar un prestamo", ex);
            }
        }
        private Boolean verificarEdadCliente(ClienteDTO clienteDB, PlanDTO planDB, Prestamo prestamo)
        {
            var fecha = (prestamo.fechaOtorgamiento - clienteDB.fecha_nacimiento);
            var edadCliente = new DateTime(fecha.Ticks).Year - 1;
            if (edadCliente >= 18)
            {
                if (planDB.EdadMax != null && planDB.EdadMax > 0)
                {
                    var meses = prestamo.cantidadCuotas;
                    var edadAlFinalizar = new DateTime(fecha.Ticks).AddMonths(meses).Year - 1;
                    if (planDB.EdadMax > edadAlFinalizar)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                else
                {
                    return true;
                }
            }
            else
            {
                return false;
            }
        }

        private Boolean verificarEntreMontoPlan(PlanDTO plan, Prestamo prestamo)
        {
            if ((plan.MontoMin <= prestamo.capital && prestamo.capital <= plan.MontoMax))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Boolean verificarEntreCuotaPlan(PlanDTO plan, Prestamo prestamo)
        {
            if ((plan.CuotasMin <= prestamo.cantidadCuotas && prestamo.cantidadCuotas <= plan.CuotasMax))
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private Boolean verificarDiaVencimientoPlan(Prestamo prestamo)
        {
            if (((int)prestamo.diaVencimiento == 10 || (int)prestamo.diaVencimiento == 15 || (int)prestamo.diaVencimiento == 20))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
using Application.DTO;
using Application.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Application.Services
{
    public class PagosServices : ControllerBase, IPagosServices
    {
        private readonly ApplicationDbContext _context;
        private readonly ICuotaServices _servicioCuota;

        public PagosServices(ApplicationDbContext context, ICuotaServices servicioCuota)
        {
            _context = context;
            _servicioCuota = servicioCuota;
        }
        [HttpGet]
        public async Task<ActionResult<CuotasVencidas>> obtenerCuotaPagar(int idPrestamo, DateTime fechaPago)
        {
            try
            {
                var existePrestamo = await _context.Prestamos.AnyAsync(x => x.id == idPrestamo);
                if (!existePrestamo)
                {
                    return NotFound("No existe el préstamo");
                }

                var cuotaPagar = await crearComposicionInteres(idPrestamo, fechaPago, false);

                return Ok(cuotaPagar);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar realizar el pago de la cuota", ex);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Double>> pagarCuota(int idPrestamo, DateTime fechaPago)
        {
            try
            {
                var existePrestamo = await _context.Prestamos.AnyAsync(x => x.id == idPrestamo);
                if (!existePrestamo)
                {
                    return NotFound("No existe el préstamo");
                }
                var totalPagar = await crearComposicionInteres(idPrestamo, fechaPago, true);
                return Ok(totalPagar);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar realizar el pago de la cuota", ex);
            }
        }
        public async Task<ActionResult<CuotasVencidas>> crearComposicionInteres(int idPrestamo, DateTime fechaPago, Boolean guardar)
        {
            var query =
            from Cuotas in _context.Cuotas
            join Composicion in _context.Composiciones on Cuotas.id equals Composicion.idCuota
            join Concepto in _context.Conceptos on Composicion.idConcepto equals Concepto.id
            where (Cuotas.idPrestamo == idPrestamo)
            group new { Cuotas, Composicion } by new { Cuotas.id, Cuotas.nroCuota, Cuotas.fechaPago, Cuotas.fechaVencimiento } into g
            select new CuotasVencidas
            {
                id = g.Key.id,
                nroCuota = g.Key.nroCuota,
                monto = g.Sum(x => x.Composicion.monto),
                fechaPago = g.Key.fechaPago,
                fechaVen = g.Key.fechaVencimiento,
                interesPunitorio = null,
                montoPagar = null
           };

            CuotasVencidas cuotaPagar = query.Where(x => x.fechaPago == null).OrderBy(x => x.fechaVen).FirstOrDefault();

            Cuota cuota;
            cuota = _context.Cuotas.FirstOrDefault<Cuota>(x => x.id == cuotaPagar.id);
            var cantidadDias = (fechaPago - cuotaPagar.fechaVen).Days;
            double formulaInteresPunitorio = 0;
            cuotaPagar.montoPagar = cuotaPagar.monto;
            if (cantidadDias > 0)
            {
                formulaInteresPunitorio = 0.0065 * cantidadDias * cuotaPagar.monto;
                cuotaPagar.montoPagar = cuotaPagar.monto + formulaInteresPunitorio;
                cuotaPagar.interesPunitorio = formulaInteresPunitorio;

                if (guardar == true)
                {
                    cuota.fechaPago = fechaPago;
                    cuotaPagar.fechaPago = fechaPago;
                    await _servicioCuota.crearComposicion(cuotaPagar.id, 4, formulaInteresPunitorio);
                    _context.Update(cuota);
                    await _context.SaveChangesAsync();
                }
            }
            else
            {
                if (guardar == true)
                {
                    cuota.fechaPago = fechaPago;
                    cuotaPagar.fechaPago = fechaPago;
                    _context.Update(cuota);
                    await _context.SaveChangesAsync();
                }
            }
            return Ok(cuotaPagar);
        }
    }
}

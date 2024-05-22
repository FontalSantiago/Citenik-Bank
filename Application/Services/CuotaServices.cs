using Application.DTO;
using Application.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CuotaServices : ControllerBase, ICuotaServices
    {
        private readonly ApplicationDbContext _context;

        public CuotaServices(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<ActionResult<IList<CuotasComposiciones>>> listarCuotas(int idPrestamo)
        {
            try
            {
                var query = await (
                from Cuotas in _context.Cuotas
                join Composicion in _context.Composiciones on Cuotas.id equals Composicion.idCuota
                join Concepto in _context.Conceptos on Composicion.idConcepto equals Concepto.id
                where (Cuotas.idPrestamo == idPrestamo)
                group new { Cuotas, Composicion } by new { Cuotas.id, Cuotas.nroCuota, Cuotas.fechaPago, Cuotas.fechaVencimiento } into g
                select new CuotasComposiciones
                {
                    id = g.Key.id,
                    nroCuota = g.Key.nroCuota,
                    monto = g.Sum(x => x.Composicion.monto),
                    fechaPago = g.Key.fechaPago,
                    fechaVen = g.Key.fechaVencimiento
                }).ToListAsync();

                return Ok(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las composiciones", ex);
            }
        }

        public async Task<ActionResult<IList<ComposicionDetallada>>> listarComposicionesCuota(int idPrestamo, int idCuota)
        {
            try
            {
                var query = await (
                from Composicion in _context.Composiciones
                join Concepto in _context.Conceptos on Composicion.idConcepto equals Concepto.id
                join Cuota in _context.Cuotas on Composicion.idCuota equals Cuota.id
                where (Cuota.idPrestamo == idPrestamo) && (Composicion.idCuota == idCuota)
                group new { Composicion } by new { Composicion.id, Concepto.nombre, Composicion.monto } into g
                select new ComposicionDetallada
                {
                    id = g.Key.id,
                    nombre = g.Key.nombre,
                    monto = g.Key.monto,
                }).ToListAsync();

                return Ok(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar las composiciones", ex);
            }
        }
        public async Task<ActionResult> guardarComposicion(Cuota cuota, Plan plan, Prestamo prestamo)
        {
            try
            {
                if (cuota.nroCuota == 1 && plan.costoOtorgamiento != null)
                {
                    var formulaCostoOtorgamiento = (double)plan.costoOtorgamiento;
                    await crearComposicion(cuota.id, 3, formulaCostoOtorgamiento);
                }
                var formulaCapital = prestamo.capital / prestamo.cantidadCuotas;
                await crearComposicion(cuota.id, 1, formulaCapital);

                var formulaInteresFinanciero = (prestamo.capital * ((plan.TNA / 100) * ((double)prestamo.cantidadCuotas / 12))) / (double)prestamo.cantidadCuotas;
                await crearComposicion(cuota.id, 2, formulaInteresFinanciero);

                return Created("/api/cuotas", cuota);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al intentar agregar una composicion", ex);
            }
        }

        public async Task crearComposicion(int idCuota, int idConcepto, double monto)
        {
            Composicion composicion = new Composicion();
            composicion.id = 0;
            composicion.idCuota = idCuota;
            composicion.idConcepto = idConcepto;
            composicion.monto = monto;
            _context.Add(composicion);
            await _context.SaveChangesAsync();
        }
    }
}
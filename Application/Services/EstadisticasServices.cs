using Application.DTO;
using Application.Entities;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class EstadisticasServices : ControllerBase, IEstadisticasServices

    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;

        public EstadisticasServices(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }
        public async Task<ActionResult<IList<Object>>> consultaPrestamosActivosPlan()
        {
            try
            {
                var query = await (
                from p in _context.Prestamos
                join pla in _context.Planes on p.idPlan equals pla.id
                where p.estado == "vigente" && pla.id == p.idPlan
                group pla by pla.nombre into g
                select new { Nombre = g.Key, Cantidad = g.Count() }
                ).ToListAsync();

                return Ok(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar estadísticas, PrestamosActivosPlan", ex);
            }

        }

        public async Task<ActionResult<IList<Object>>> consultaPrestamosActivosCapital()
        {
            try
            {
                var query = from v in (
                from p in _context.Prestamos
                where p.estado == "vigente"
                select new
                {
                    rango =
                    p.capital < 10000 ? "Menores a $10.000" :
                    p.capital >= 10000 && p.capital <= 49999 ? "$10.000 - $49.999" :
                    p.capital >= 50000 && p.capital <= 199999 ? "$50.000 - $199.999" :
                    p.capital >= 200000 && p.capital <= 999999 ? "$200.000 - $999.999" :
                    p.capital >= 1000000 && p.capital <= 5000000 ? "$1.000.000 - $5.000.000" :
                    "Mayores a $5.000.000"
                })
                            group v by v.rango into g
                            select new { rango = g.Key, cantPrestamos = g.Count() };

                query = query.OrderBy(x => x.cantPrestamos);
                return Ok(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar estadísticas, consultaPrestamosActivosCapital", ex);
            }

        }

        public async Task<ActionResult<IList<Object>>> consultaTotalPorConcepto(int idConcepto)
        {
            try
            {
                var query = (from p in _context.Prestamos
                             join c in _context.Cuotas on p.id equals c.idPrestamo
                             join c2 in _context.Composiciones on c.id equals c2.idCuota
                             where p.estado == "vigente" && c.fechaPago == null && c2.idConcepto == idConcepto
                             select c2.monto).Sum();


                return Ok(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar estadísticas, consultaConceptoARecuperar", ex);
            }

        }

        public async Task<ActionResult<IList<Object>>> consultaTotalPorConceptoInteresPunitorio()
        {
            try
            {
                var query = (from p in _context.Prestamos
                             join c in _context.Cuotas on p.id equals c.idPrestamo
                             join c2 in _context.Composiciones on c.id equals c2.idCuota
                             where p.estado == "vigente" && c2.idConcepto == 4
                             select c2.monto).Sum();

                return Ok(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar estadísticas, consultaInteresPunitorioHistorico", ex);
            }

        }

        public async Task<ActionResult<IList<Object>>> consultaClientesMayorDeuda()
        {
            try
            {
                var query = (from p in _context.Prestamos
                             join c in _context.Cuotas on p.id equals c.idPrestamo
                             join c2 in _context.Composiciones on c.id equals c2.idCuota
                             join cli in _context.Clientes on p.idCliente equals cli.id
                             join con in _context.Conceptos on c2.idConcepto equals con.id
                             where p.estado == "vigente" && c.fechaPago == null && (con.id == 1 || con.id == 2)
                             group c2.monto by cli.nombre into g
                             orderby g.Sum() descending
                             select new
                             {                                 
                                 Nombre = g.Key,
                                 Deuda = g.Sum()
                             }).Take(5);

                return Ok(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al consultar estadísticas, consultaClienteMayorDeuda", ex);
            }

        }

        public async Task<ActionResult<IList<Object>>> consultaPrestamosConMayorRentabilidad()
        {
            try
            {
                var query = (from p in _context.Prestamos
                             join c in _context.Cuotas on p.id equals c.idPrestamo
                             join c2 in _context.Composiciones on c.id equals c2.idCuota
                             join cli in _context.Clientes on p.idCliente equals cli.id
                             join con in _context.Conceptos on c2.idConcepto equals con.id
                             join pla in _context.Planes on p.idPlan equals pla.id
                             where p.estado == "vigente" && c.fechaPago != null && (con.id == 2 || con.id == 4)
                             group new { p.id, cli.CUIT, pla.nombre, p.fechaOtorgamiento, c2.monto } by p.id into g
                             orderby g.Sum(x => x.monto) descending
                             select new
                             {
                                 id = g.Key,
                                 g.FirstOrDefault().CUIT,
                                 g.FirstOrDefault().nombre,
                                 g.FirstOrDefault().fechaOtorgamiento,
                                 rentabilidad = g.Sum(x => x.monto)
                             }).Take(5);

                return Ok(query);
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los prestamos", ex);
            }

        }
    }
}

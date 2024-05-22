using Application.DTO;
using Application.Entities;
using Microsoft.AspNetCore.Mvc;

namespace Application.Services
{
    public interface IPlanesServices
    {
        public Task<ActionResult<List<PlanesConsulta>>> listarPlanes();
        public Task<PlanDTO> obtenerPlan(int id);
        public Task<ActionResult<List<PlanesConsulta>>> obtenerPlanNombre(string nombre);
        public Task<ActionResult> guardarPlan(Plan plan);
        public Task<ActionResult> modificarPlan(int id, Plan plan);
    }
}

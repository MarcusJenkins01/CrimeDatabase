using CrimeDatabase.Application.Interfaces.Services;
using CrimeDatabase.Web.Mappings;
using CrimeDatabase.Web.ViewModels.NotesAudits;
using Microsoft.AspNetCore.Mvc;

namespace CrimeDatabase.Web.Controllers
{
    public class NotesAuditsController(INotesAuditService notesService) : Controller
    {
        private readonly INotesAuditService _notesService = notesService;

        // GET: /NotesAudits
        public async Task<IActionResult> Index()
        {
            var audits = await _notesService.GetAllAsync();
            var vm = audits.Select(a => a.ToIndexViewModel()).ToList();

            return View(vm);
        }

        // GET: /NotesAudits/Crime/{crime id}
        [HttpGet]
        public async Task<IActionResult> Crime(Guid id)
        {
            var audits = await _notesService.GetByCrimeIdAsync(id);
            var auditsVm = audits.Select(a => a.ToIndexViewModel()).ToList();
            var vm = new CrimeAuditsViewModel {
                Id = id,
                NotesAudits = auditsVm
            };
            
            return View(vm);
        }
    }
}

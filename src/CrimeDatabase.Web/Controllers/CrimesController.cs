using CrimeDatabase.Application.Interfaces.Services;
using CrimeDatabase.Domain.Entities;
using CrimeDatabase.Domain.Enums;
using CrimeDatabase.Web.Mappings;
using CrimeDatabase.Web.ViewModels.Crimes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CrimeDatabase.Web.Controllers
{
    public class CrimesController(ICrimeService crimeService) : Controller
    {
        private readonly ICrimeService _crimeService = crimeService;

        // GET: /Crimes
        public async Task<IActionResult> Index()
        {
            var crimes = await _crimeService.GetAllAsync();
            var vm = crimes.Select(c => c.ToIndexViewModel()).ToList();

            return View(vm);
        }


        // GET: /Crimes/Create
        public IActionResult Create()
        {
            PopulateCrimeTypes();
            return View();
        }

        // POST: /Crimes/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCrimeViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                PopulateCrimeTypes();
                return View(vm);
            }

            var crime = new Crime
            {
                Id = Guid.NewGuid(),
                CrimeDate = vm.CrimeDate,
                Location = vm.Location,
                VictimName = vm.VictimName,
                CrimeType = vm.CrimeType,
                Notes = vm.Notes
            };

            await _crimeService.CreateAsync(crime);
            return RedirectToAction(nameof(Index));
        }

        // GET: /Crimes/Edit/{id}
        public async Task<IActionResult> Edit(Guid id)
        {
            var crime = await _crimeService.GetByIdAsync(id);
            if (crime == null)
                return NotFound();

            var vm = new EditCrimeViewModel
            {
                Id = crime.Id,
                Notes = crime.Notes
            };

            return View(vm);
        }

        // POST: /Crimes/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(EditCrimeViewModel vm)
        {
            if (!ModelState.IsValid)
                return View(vm);

            await _crimeService.UpdateNotesAsync(vm.Id, vm.Notes);
            return RedirectToAction(nameof(Index));
        }

        // I used ViewBag to provide display names for some of the crime types that are not properly formatted.
        private void PopulateCrimeTypes()
        {
            var items = Enum.GetValues<CrimeType>()
                .Select(ct => new SelectListItem
                {
                    Value = ct.ToString(),
                    Text = ct switch
                    {
                        CrimeType.AntiSocialBehaviour => "Anti-Social Behaviour",
                        CrimeType.CriminalDamage => "Criminal Damage",
                        _ => ct.ToString()
                    }
                });

            ViewBag.CrimeTypes = items;
        }
    }
}

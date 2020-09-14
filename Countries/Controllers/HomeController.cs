using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using RESTCountries.Services;
using Countries.Models;
using System.Data.Entity;

namespace Countries.Controllers
{
    public class HomeController : Controller 
    {
        private readonly AppDbContext context = new AppDbContext();

        public ActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Index(string countryName)
        {
            var countries = await RESTCountriesAPI.GetAllCountriesAsync();
            if (countries.Any(x => x.Name == countryName))
            {
                return RedirectToAction(nameof(Search), "Home", new { countryName });
            }
            return View();
        }

        public async Task<ActionResult> Search(string countryName)
        {
            var c = await RESTCountriesAPI.GetCountryByFullNameAsync(countryName);
            return View(c);
        }

        [HttpPost]
        public ActionResult Search(RESTCountries.Models.Country c)
        {
            if (ModelState.IsValid)
            {
                bool modify = context.Countries.Any(x => x.CountryCode == c.NumericCode);
                Country country;

                if (modify)
                {
                    var id = context.Countries.FirstOrDefault(x => x.CountryCode == c.NumericCode).Id;
                    country = context.Countries.Find(id);
                }
                else
                {
                    country = new Country { };
                }

                country.Area = c.Area;
                country.Name = c.Name;
                country.Population = c.Population;
                country.CountryCode = c.NumericCode;

                if (context.Cities.Any(x => x.Name == c.Capital))
                {
                    country.Capital = context.Cities.FirstOrDefault(x => x.Name == c.Capital);
                }
                else
                {
                    City city = new City
                    {
                        Name = c.Capital
                    };

                    country.Capital = city;
                }

                if (context.Regions.Any(x => x.Name == c.Region))
                {
                    country.Region = context.Regions.FirstOrDefault(x => x.Name == c.Region);
                }
                else
                {
                    Region region = new Region
                    {
                        Name = c.Region
                    };

                    country.Region = region;
                }

                if (modify)
                {
                    context.Entry(country).State = EntityState.Modified;
                }
                else
                {
                    context.Entry(country).State = EntityState.Added;
                }

                context.SaveChanges();
            }

            return View(c);
        }

        public ActionResult All()
        {
            return View(context.Countries.Include(c => c.Capital).Include(r => r.Region));
        }
    }
}
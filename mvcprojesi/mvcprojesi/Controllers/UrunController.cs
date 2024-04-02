using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using mvcprojesi.Models.Entity;

namespace mvcprojesi.Controllers
{
    public class UrunController : Controller
    {
        MvcDbStokEntities3 db = new MvcDbStokEntities3();
        public ActionResult Index()
        {
            var degerler = db.Urunler.ToList();
            return View(degerler);
        }

        [HttpGet]
        public ActionResult Urunekle()
        {
            List<SelectListItem> degerler = (from i in db.Kategoriler.ToList()
                                             select new SelectListItem
                                             {
                                                 Text = i.Kategoriad,
                                                 Value = i.Kategoriid.ToString()
                                             }).ToList();
            ViewBag.dgr = degerler;
            return View();
        }

        [HttpPost]
        public ActionResult Urunekle(Urunler p1)
        {

            var ktg = db.Kategoriler.Where(m => m.Kategoriid == p1.Kategoriler.Kategoriid).FirstOrDefault();
            p1.Kategoriler = ktg;
            db.Urunler.Add(p1);
            
            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SIL(int id)
        {
            var kategori = db.Urunler.Find(id);
            db.Urunler.Remove(kategori);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}

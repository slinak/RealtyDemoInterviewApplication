using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using RealtyDemo.Models;

namespace RealtyDemo.Controllers
{
    public class RealtyListingModelsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: RealtyListingModels
        public ActionResult Index()
        {
            return View(db.RealtyListingModels.ToList());
        }

        //GET: RealtyListingModel/Search?searchBy=&searchPhrase=
        public ActionResult Search(string searchBy, string searchPhrase)
        {
            IEnumerable<RealtyListingModel> results;

            if (searchBy == "All" && searchPhrase != null)
                results = RealtyListingHelper.SearchAllAttributes(db.RealtyListingModels, searchPhrase);
            else if (searchBy != "" && searchPhrase != null)
                results = RealtyListingHelper.SearchSpecificAttribute(db.RealtyListingModels, searchBy, searchPhrase);
            else
                return RedirectToAction("Index");

            if (results.ToList().Count == 0)
            {
                ViewBag.Message = "No Results Found";
                return View();
            }
            else
            {
                ViewBag.Message = "Search Results:";
                return View(results);
            }
        }        

        // GET: RealtyListingModels/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealtyListingModel realtyListingModel = db.RealtyListingModels.Find(id);
            if (realtyListingModel == null)
            {
                return HttpNotFound();
            }
            return View(realtyListingModel);
        }

        // GET: RealtyListingModels/Create
        [Authorize]
        public ActionResult Create()
        {
            return View();
        }

        // POST: RealtyListingModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "MLSID,StreetAddressLine1,StreetAddressLine2,StreetAddressCity,StreetAddressState,StreetAddressZipCode,StreetAddressNeighborhood,SalePrice,InitialListingDate,BedroomCount,Pictures,GarageSizeSquareFeet,HouseSizeSquareFeet,LotSizeSquareFeet,Description")] RealtyListingModel realtyListingModel)
        {
            if (ModelState.IsValid)
            {
                db.RealtyListingModels.Add(realtyListingModel);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(realtyListingModel);
        }

        // GET: RealtyListingModels/Edit/5
        [Authorize]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealtyListingModel realtyListingModel = db.RealtyListingModels.Find(id);
            if (realtyListingModel == null)
            {
                return HttpNotFound();
            }
            return View(realtyListingModel);
        }

        // POST: RealtyListingModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "MLSID,StreetAddressLine1,StreetAddressLine2,StreetAddressCity,StreetAddressState,StreetAddressZipCode,StreetAddressNeighborhood,SalePrice,InitialListingDate,BedroomCount,Pictures,GarageSizeSquareFeet,HouseSizeSquareFeet,LotSizeSquareFeet,Description")] RealtyListingModel realtyListingModel)
        {
            if (ModelState.IsValid)
            {
                db.Entry(realtyListingModel).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(realtyListingModel);
        }

        // GET: RealtyListingModels/Delete/5
        [Authorize]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            RealtyListingModel realtyListingModel = db.RealtyListingModels.Find(id);
            if (realtyListingModel == null)
            {
                return HttpNotFound();
            }
            return View(realtyListingModel);
        }

        // POST: RealtyListingModels/Delete/5
        [Authorize]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            RealtyListingModel realtyListingModel = db.RealtyListingModels.Find(id);
            db.RealtyListingModels.Remove(realtyListingModel);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}

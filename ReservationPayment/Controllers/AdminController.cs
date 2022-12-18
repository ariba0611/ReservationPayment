using ReservationPayment.Models.DAL;
using ReservationPayment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ReservationPayment.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        private IRailwayRepository<TrainDetails> interfaceObj;
        public AdminController()
        {
            this.interfaceObj = new RailwayRepository<TrainDetails>();

        }
        // GET: Admin
        public ActionResult Index()
        {
            var data = interfaceObj.GetModel().ToList();
            return View(data);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(TrainDetails train)

        {

            interfaceObj.InsertModel(train);

            interfaceObj.Save();
            return RedirectToAction("Index");
            //return View();
        }

        public ActionResult Edit(int id)
        {
            TrainDetails td = interfaceObj.GetModelByID(id);
            return View(td);


        }
        [HttpPost]
        public ActionResult Edit(int id, TrainDetails train)
        {


            interfaceObj.UpdateModel(train);
            interfaceObj.Save();

            return RedirectToAction("Index");


        }

        public ActionResult Delete(int id)
        {
            TrainDetails td = interfaceObj.GetModelByID(id);
            return View(td);
        }
        [HttpPost]
        public ActionResult Delete(int id, TrainDetails train)
        {

            interfaceObj.DeleteModel(id);
            interfaceObj.Save();
            return RedirectToAction("Index");
        }

    }
}

using ReservationPayment.Models;
using ReservationPayment.Models.DAL;
using System.Collections.Generic;

using System.Diagnostics;
using System.Linq;
using System.Web.Mvc;

namespace ReservationPayment.Controllers
{
    public class ResController : Controller
    {
        List<Reservation> resList = new List<Reservation>();
        private IRailwayRepository<TrainDetails> TrainObj;
        public ResController()
        {
            this.TrainObj = new RailwayRepository<TrainDetails>();

        }
        public ActionResult Index()
        {
              List<Reservation> resList = new List<Reservation>();
            List<TrainDetails> trainDetails = TrainObj.GetModel().ToList();

            ViewData["TrainDetailS"] = new SelectList(trainDetails, dataValueField: "SourceStation", dataTextField: "SourceStation");
            ViewData["TrainDetailDesti"] = new SelectList(trainDetails, dataValueField: "DestinationStation", dataTextField: "DestinationStation");

            return View();

        }
        [HttpPost]
        public ActionResult Index(Reservation res)

        {
           
                 var data  = res ;
                 resList.Add(data);
                //Debug.WriteLine(resList[1]);

                for(int i = 0; i < resList.Count(); i++)
               {
                Debug.WriteLine("Aditya" + resList[i]);
               }

         
             Session["ResGender"] = res.Res_Gender;
            
                List<TrainDetails> trainDetails = TrainObj.GetModel().ToList();

               ViewData["TrainDetailS"] = new SelectList(trainDetails, dataValueField: "SourceStation", dataTextField:  "SourceStation");
               ViewData["TrainDetailDesti"] = new SelectList(trainDetails,dataValueField: "DestinationStation",dataTextField: "DestinationStation");

                Session["SourceStation"] = res.TrainDetails.SourceStation;

               Session["DestinationStation"] = res.TrainDetails.DestinationStation;





            return View(resList);

        }

        public ActionResult ListRes() {

            return View();
        }

    }
}
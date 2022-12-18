using ReservationPayment.Models;
using ReservationPayment.Models.DAL;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace ReservationPayment.Controllers
{
    public class ResController : Controller
    {
        private IRailwayRepository<TrainDetails> TrainObj;
        public ResController()
        {
            this.TrainObj = new RailwayRepository<TrainDetails>();

        }
        public ActionResult Index()
        {

            List<TrainDetails> trainDetails = TrainObj.GetModel().ToList();

            ViewData["TrainDetailS"] = new SelectList(trainDetails, dataValueField: "SourceStation", dataTextField: "SourceStation");
            ViewData["TrainDetailDesti"] = new SelectList(trainDetails, dataValueField: "DestinationStation", dataTextField: "DestinationStation");

            return View();

        }
        [HttpPost]
        public ActionResult Index(Reservation res)

        {

            Session["ResName"] = res.Res_Name;
            Session["ResGender"] = res.Res_Gender;

            List<TrainDetails> trainDetails = TrainObj.GetModel().ToList();

            ViewData["TrainDetailS"] = new SelectList(trainDetails, dataValueField: "SourceStation", dataTextField: "SourceStation");
            ViewData["TrainDetailDesti"] = new SelectList(trainDetails, dataValueField: "DestinationStation", dataTextField: "DestinationStation");

            Session["SourceStation"] = res.TrainDetails.SourceStation;

            Session["DestinationStation"] = res.TrainDetails.DestinationStation;





            return View();

        }

    }
}
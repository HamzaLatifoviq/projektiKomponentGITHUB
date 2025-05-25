using projektiKomponentGITHUB.Models;
using System;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace projektiKomponentGITHUB.Controllers
{
    public class HoteletViewController : Controller
    {
        public ActionResult Hotel1()
        {
            return View();
        }

        public ActionResult Hotel2()
        {
            return View();
        }

        public ActionResult Hotel3()
        {
            return View();
        }

        public ActionResult Hotel4()
        {
            return View();
        }

        public ActionResult Hotel5()
        {
            return View();
        }
    }



    namespace projektiKomponentGITHUB.Controllers
    {
        public class ReservationController : Controller
        {
            private readonly string connectionString = "Data Source=NIKI\\SQLEXPRESS;Initial Catalog=komponentDatabase;Integrated Security=True;Encrypt=False";

            // Merr çmimin për natë nga tabela Rooms për hotelin përkatës
            private decimal GetPricePerNight(int hotelId)
            {
                decimal pricePerNight = 0m;
                string query = "SELECT TOP 1 price_per_night FROM Rooms WHERE hotel_id = @HotelId";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand cmd = new SqlCommand(query, connection))
                {
                    cmd.Parameters.AddWithValue("@HotelId", hotelId);
                    connection.Open();
                    var result = cmd.ExecuteScalar();
                    if (result != null)
                        pricePerNight = Convert.ToDecimal(result);
                }
                return pricePerNight;
            }

            [HttpPost]
            public JsonResult CheckAvailability(int hotelId, string reservationRange, int adultsCount, int childrenCount, int roomsCount)
            {
                // Këtu mund ta implementosh kontrollin e disponueshmërisë, për momentin e kthen gjithmonë true
                return Json(new { available = true });
            }

            private int CalculateNights(string reservationRange)
            {
                int nightsCount = 1;

                if (!string.IsNullOrEmpty(reservationRange))
                {
                    var dates = reservationRange.Split(new string[] { " to " }, StringSplitOptions.None);
                    if (dates.Length == 2 &&
                        DateTime.TryParse(dates[0], out DateTime start) &&
                        DateTime.TryParse(dates[1], out DateTime end))
                    {
                        nightsCount = (end - start).Days;
                        if (nightsCount <= 0) nightsCount = 1;
                    }
                }

                return nightsCount;
            }

            public bool InsertReservation(int hotelId, string reservationRange, int adultsCount, int childrenCount, int roomsCount)
            {
                decimal pricePerNight = GetPricePerNight(hotelId);
                if (pricePerNight == 0m)
                {
                    Console.WriteLine("Çmimi për natë nuk është i vlefshëm.");
                    return false;
                }

                int nightsCount = CalculateNights(reservationRange);

                decimal totalPrice = pricePerNight * roomsCount * nightsCount;

                string query = @"INSERT INTO Reservations 
                            (HotelId, ReservationRange, AdultsCount, ChildrenCount, RoomsCount, Price) 
                            VALUES (@HotelId, @ReservationRange, @AdultsCount, @ChildrenCount, @RoomsCount, @Price)";

                using (SqlConnection connection = new SqlConnection(connectionString))
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HotelId", hotelId);
                    command.Parameters.AddWithValue("@ReservationRange", reservationRange);
                    command.Parameters.AddWithValue("@AdultsCount", adultsCount);
                    command.Parameters.AddWithValue("@ChildrenCount", (object)childrenCount ?? DBNull.Value);
                    command.Parameters.AddWithValue("@RoomsCount", roomsCount);
                    command.Parameters.AddWithValue("@Price", totalPrice);
                    //  command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    try
                    {
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        if (result > 0)
                        {
                            Console.WriteLine("Rezervimi u shtua me sukses.");
                            return true;
                        }
                        else
                        {
                            Console.WriteLine("Nuk u shtua asnjë rresht.");
                            return false;
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Gabim gjatë futjes së rezervimit: " + ex.Message);
                        return false;
                    }
                }
            }

            [HttpPost]
            public JsonResult MakeReservation(int hotelId, string reservationRange, int adultsCount, int childrenCount, int roomsCount)
            {
                try
                {
                    bool success = InsertReservation(hotelId, reservationRange, adultsCount, childrenCount, roomsCount);
                    return Json(new { success = success });
                }
                catch (Exception ex)
                {
                    return Json(new { success = false, message = ex.Message });
                }
            }
        }
    }
}
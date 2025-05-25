
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace projektiKomponentGITHUB.Controllers
{
    public class HoteletViewController : Controller
    {
        // GET: HoteletView
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
        static void Main()
        {
            string connectionString = "Data Source=NIKI\\SQLEXPRESS;Initial Catalog=komponentDatabase;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Lidhja me bazën e të dhënave u realizua me sukses!");

                    // Ekzekuto një pyetje të thjeshtë
                    string query = "SELECT COUNT(*) FROM Reservations";
                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        int count = (int)command.ExecuteScalar();
                        Console.WriteLine($"Numri i rezervimeve: {count}");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Gabim: {ex.Message}");
                }
            }

        }

    }
}
namespace YourNamespace.Controllers
{
    public class ReservationController : Controller
    {
        [HttpPost]
        public JsonResult CheckAvailability(int hotelId, string reservationRange, int adultsCount, int childrenCount, int roomsCount)
        {
            // Logjika për të kontrolluar nëse dhomat janë të lira
            bool isAvailable = true; // Zëvendësoje me query në bazën e të dhënave

            return Json(new { available = isAvailable });
        }
        public void TestConnection()
        {
            string connectionString = "Data Source=NIKI\\SQLEXPRESS;Initial Catalog=HotelBooking;Integrated Security=True;Trust Server Certificate=True;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                try
                {
                    connection.Open();
                    Console.WriteLine("Lidhja me bazën e të dhënave funksionon.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Gabim në lidhje: " + ex.Message);
                }
            }
        }
        public bool InsertReservation(int hotelId, string reservationRange, int adultsCount, int childrenCount, int roomsCount)
        {
            string query = "INSERT INTO Reservations (HotelId, ReservationRange, AdultsCount, ChildrenCount, RoomsCount) VALUES (@HotelId, @ReservationRange, @AdultsCount, @ChildrenCount, @RoomsCount)";
            string connectionString = "Data Source=NIKI\\SQLEXPRESS;Initial Catalog=HotelBooking;Integrated Security=True;Trust Server Certificate=True;";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HotelId", hotelId);
                    command.Parameters.AddWithValue("@ReservationRange", reservationRange);
                    command.Parameters.AddWithValue("@AdultsCount", adultsCount);
                    command.Parameters.AddWithValue("@ChildrenCount", childrenCount);
                    command.Parameters.AddWithValue("@RoomsCount", roomsCount);

                    try
                    {
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (Exception ex)
                    {
                        // Logoni gabimin për debug
                        Console.WriteLine("Gabim gjatë ruajtjes: " + ex.Message);
                        return false;
                    }
                }
            }
        }
        [HttpPost]
        public JsonResult MakeReservation(int hotelId, string reservationRange, int adultsCount, int childrenCount, int roomsCount)
        {
            // Logjika për të bërë rezervimin në bazën e të dhënave
            try
            {
                // Shembull i thjeshtë për të ruajtur rezervimin
                bool success = true; // Zëvendësoje me query për ruajtjen në bazën e të dhënave

                return Json(new { success = success });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, message = ex.Message });
            }
        }
    }
}


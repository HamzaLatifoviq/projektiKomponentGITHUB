using System;
using System.Data.SqlClient;
using System.Web.Mvc;

namespace projektiKomponentGITHUB.Controllers
{
    public class HoteletViewController : Controller
    {
        // Veprimet për shfaqjen e faqeve për hotele
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

        // Metodë për testimin e lidhjes me bazën e të dhënave
        public ActionResult TestDatabaseConnection()
        {
            string connectionString = "Data Source=NIKI\\SQLEXPRESS;Initial Catalog=komponentDatabase;Integrated Security=True;Encrypt=False";

            try
            {
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    connection.Open();
                    ViewBag.Message = "Lidhja me bazën e të dhënave u realizua me sukses!";
                }
            }
            catch (Exception ex)
            {
                ViewBag.Message = $"Gabim gjatë lidhjes: {ex.Message}";
            }

            return View();
        }
    }
}

namespace projektiKomponentGITHUB.Controllers
{
    public class ReservationController : Controller
    {
        [HttpPost]
        public JsonResult CheckAvailability(int hotelId, string reservationRange, int adultsCount, int childrenCount, int roomsCount)
        {
            // Logjika për kontrollin e disponueshmërisë (duhet të zëvendësohet me query në bazën e të dhënave)
            bool isAvailable = true;

            return Json(new { available = isAvailable });
        }

        public bool InsertReservation(int hotelId, string reservationRange, int adultsCount, int childrenCount, int roomsCount)
        {
            string query = "INSERT INTO Reservations (HotelId, ReservationRange, AdultsCount, ChildrenCount, RoomsCount, CreatedAt) " +
                           "VALUES (@HotelId, @ReservationRange, @AdultsCount, @ChildrenCount, @RoomsCount, @CreatedAt)";
            string connectionString = "Data Source=NIKI\\SQLEXPRESS;Initial Catalog=komponentDatabase;Integrated Security=True;Encrypt=False";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                using (SqlCommand command = new SqlCommand(query, connection))
                {
                    command.Parameters.AddWithValue("@HotelId", hotelId);
                    command.Parameters.AddWithValue("@ReservationRange", reservationRange);
                    command.Parameters.AddWithValue("@AdultsCount", adultsCount);
                    command.Parameters.AddWithValue("@ChildrenCount", childrenCount);
                    command.Parameters.AddWithValue("@RoomsCount", roomsCount);
                    command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

                    try
                    {
                        connection.Open();
                        int result = command.ExecuteNonQuery();
                        return result > 0;
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine("Gabim gjatë ruajtjes: " + ex.Message);
                        return false;
                    }
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

namespace projektiKomponentGITHUB.Models
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("Reservations")]
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int HotelId { get; set; }

        [Required]
        [StringLength(50)]
        public string ReservationRange { get; set; }

        [Required]
        public int AdultsCount { get; set; }

        public int? ChildrenCount { get; set; }

        [Required]
        public int RoomsCount { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}

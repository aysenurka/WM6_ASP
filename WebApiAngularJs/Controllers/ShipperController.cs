using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web.Http;

using WebApiAngularJs.Models;

namespace WebApiAngularJs.Controllers
{
    public class ShipperController : ApiController
    {
        NorthwindEntities db = new NorthwindEntities();

        [HttpGet]
        public IHttpActionResult GetAll()
        {
            try
            {
                return Ok(new
                {
                    success = true,
                    data = db.Shippers.Select(x => new ShipperViewModel()
                    {
                        ShipperID = x.ShipperID,
                        CompanyName = x.CompanyName,
                        Phone = x.Phone
                    }).ToList()
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Bir hata oluştu {ex.Message}");
            }
        }

        public IHttpActionResult GetById(int id = 0)
        {
            try
            {
                var ship = db.Shippers.Find(id);
                if (ship == null)
                    return NotFound();

                var data = new ShipperViewModel()
                {
                    ShipperID = ship.ShipperID,
                    CompanyName = ship.CompanyName,
                    Phone = ship.Phone
                };
                return Ok(new
                {
                    success = true,
                    data = data
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Bir hata oluştur {ex.Message}");
            }
        }

        [HttpPost]
        public IHttpActionResult Add([FromBody] ShipperViewModel model)
        {
            try
            {
                db.Shippers.Add(new Shipper()
                {
                    CompanyName = model.CompanyName,
                    Phone = model.Phone
                });
                db.SaveChanges();

                return Ok(new
                {
                    success = true,
                    message = "Shipper eklendi"
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Bir hata oluştu {ex.Message}");
            }
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id = 0)
        {
            try
            {
                var ship = db.Shippers.Find(id);
                db.Shippers.Remove(ship);
                db.SaveChanges();

                return Ok(new
                {
                    success = true,
                    message = "Silme işlemi başarılı"
                });
            }
            catch (Exception ex)
            {
                return BadRequest($"Bir hata oluştu {ex.Message}");
            }
        }

        [HttpPut]
        public IHttpActionResult Put(int id, ShipperViewModel model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (id != model.ShipperID)
                return BadRequest();

            db.Entry(model).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
                return Ok(new
                {
                    success = true,
                    message = "Güncelleme işlemi başarılı"
                });
            }
            catch (DbUpdateConcurrencyException ex)
            {
                if (!db.Categories.Any(x => x.CategoryID == id))
                {
                    return NotFound();
                }
                else
                {
                    return BadRequest($"Bir hata oluştu {ex.Message}");
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }
    }
}

public class ShipperViewModel
{
    public int ShipperID { get; set; }
    public string CompanyName { get; set; }
    public string Phone { get; set; }
}
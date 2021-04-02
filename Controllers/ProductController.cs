using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using WebAPI_ShopBridge.Models;

namespace WebAPI_ShopBridge.Controllers
{
    public class ProductController : ApiController
    {
        //GET : Retrieve data

        public async Task<IHttpActionResult> GetAllProduct()
        {
            await Task.Delay(3000);

            IList<ProductViewModel> products = null;
            using (var x = new InventoryDBEntities())
            {
                products = x.Products
                            .Select(c => new ProductViewModel()
                            {
                                ProductID = c.ProductID,
                                Name=c.Name,
                                Description=c.Description,
                                Price=c.Price
                               
                               
                            }).ToList<ProductViewModel>() ;
            }
            if (products.Count == 0)
            {
                throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            
            return Ok(products);
        }
        //POST : Insert new record

        public async Task<IHttpActionResult> PostNewProduct(ProductViewModel product)
        {
            await Task.Delay(1);

            if (!ModelState.IsValid)
                return BadRequest("INVALID! Pls check");
            using (var x = new InventoryDBEntities())
            {
                x.Products.Add(new Product()
                {
                    ProductID = product.ProductID,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price
                });
                x.SaveChanges();
            }
            return Ok();
        }

        //PUT : Update the record based on id

        public async Task<IHttpActionResult> PutProduct(ProductViewModel product)
        {
            await Task.Delay(1);

            if (!ModelState.IsValid)
                return BadRequest("Invalid data, please check again");
            using (var x=new InventoryDBEntities())
            {
                var checkExistingProduct = x.Products.Where(c => c.ProductID == product.ProductID)
                                                    .FirstOrDefault<Product>();
                if (checkExistingProduct != null)
                {
                    checkExistingProduct.Name = product.Name;
                    checkExistingProduct.Description = product.Description;
                    checkExistingProduct.Price = product.Price;

                    x.SaveChanges();
                }
                else
                    throw new HttpResponseException(HttpStatusCode.NotFound);
            }
            return Ok();
        }

        //DELETE : Delete the record based on id

        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            await Task.Delay(1);
            if (id <= 0)
                return BadRequest("Invalid Product ID, pls enter correct ID");

            using (var x = new InventoryDBEntities())
            {
                var product = x.Products
                    .Where(c => c.ProductID == id )
                    .FirstOrDefault();

                x.Entry(product).State = System.Data.Entity.EntityState.Deleted;
                x.SaveChanges();
            }
            return Ok();
        }

    }
}

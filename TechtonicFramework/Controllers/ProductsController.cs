using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using TechtonicFramework.Data;
using TechtonicFramework.Data.Enums;
using TechtonicFramework.Dtos;
using TechtonicFramework.Dtos.Product;
using TechtonicFramework.Mappers;
using TechtonicFramework.Repository;

namespace TechtonicFramework.Controllers
{
    [RoutePrefix("api/Products")]
    public class ProductsController : ApiController
    {
        private readonly ProductRepository _productRepository;

        public ProductsController()
        {
            _productRepository = new ProductRepository(new ApplicationDbContext());
        }

        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetProducts(
            string orderBy = null,
            string filters = null,
            int? pageNumber = 1,
            int? pageSize = 5,
            string search = null,
            string priceRange = null)
        {
            try
            {
                var result = await _productRepository.GetProducts(orderBy, filters, pageNumber, pageSize, search, priceRange);

                if (result?.productCardDtos == null || result.productCardDtos.Count == 0)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"[GetProducts] Error: {ex.Message}", ex));
            }
        }

        [HttpGet]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> GetProduct(int id)
        {
            try
            {
                var productWithRelatedProducts = await _productRepository.GetProductById(id);

                if (productWithRelatedProducts == null)
                    return NotFound();

                return Ok(productWithRelatedProducts);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"[GetProduct] Error fetching product ID {id}: {ex.Message}", ex));
            }
        }

        [HttpGet]
        [Route("filters")]
        public async Task<IHttpActionResult> GetFiltersForCategory([FromUri] string categorySlug = "laptop")
        {
            try
            {
                var filters = await _productRepository.GetFiltersAttributesAsync(categorySlug);
                var prices = await _productRepository.GetPriceRangeAsync(categorySlug);

                return Ok(new TotalFilterDto
                {
                    filterDtos = filters ?? new List<FilterDto>(),
                    priceRangeDto = prices
                });
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"[GetFiltersForCategory] Error: {ex.Message}", ex));
            }
        }

        [HttpGet]
        [Route("DealOfTheDay")]
        public async Task<IHttpActionResult> GetDealOfTheDay()
        {
            try
            {
                var dealProduct = await _productRepository.GetDealOfTheDayAsync();
                if (dealProduct == null)
                    return NotFound();

                return Ok(dealProduct.toProductCardDto());
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"[GetDealOfTheDay] Error: {ex.Message}", ex));
            }
        }

        [HttpGet]
        [Route("MostVisited")]
        public async Task<IHttpActionResult> GetMostVisitedProducts(
            [FromUri] TimePeriod period = TimePeriod.All,
            [FromUri] int count = 5)
        {
            try
            {
                DateTime fromTime;
                switch (period)
                {
                    case TimePeriod.Day:
                        fromTime = DateTime.UtcNow.Date;
                        break;
                    case TimePeriod.Week:
                        fromTime = DateTime.UtcNow.Date.AddDays(-(int)DateTime.UtcNow.DayOfWeek);
                        break;
                    case TimePeriod.Month:
                        fromTime = new DateTime(DateTime.UtcNow.Year, DateTime.UtcNow.Month, 1);
                        break;
                    case TimePeriod.Year:
                        fromTime = new DateTime(DateTime.UtcNow.Year, 1, 1);
                        break;
                    default:
                        fromTime = DateTime.MinValue;
                        break;
                }

                var products = await _productRepository.GetMostVisitedProductsAsync(count, fromTime);
                if (products == null || products.Count == 0)
                    return NotFound();

                return Ok(products.Select(p => p.toProductCardDto()).ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"[GetMostVisitedProducts] Error: {ex.Message}", ex));
            }
        }

        [HttpPost]
        [Route("question/{id:int}")]
        public async Task<IHttpActionResult> AskQuestion(int id, [FromBody] ProductQuestionDto pqDto)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(pqDto?.Question))
                    return BadRequest("Question cannot be empty.");

                var result = await _productRepository.AskQuestionAsync(id, pqDto.Question);
                if (result == null)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"[AskQuestion] Error submitting question: {ex.Message}", ex));
            }
        }

        [HttpPost]
        [Route("review/{id:int}")]
        public async Task<IHttpActionResult> AddReview(int id, [FromBody] ProductReviewPostDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Comment) || dto.Rating < 1 || dto.Rating > 5)
                return BadRequest("Invalid review data.");

            try
            {
                var email = HttpContext.Current.User.Identity.Name;
                await _productRepository.AddReviewAsync(id, email, dto);
                return Ok(); // Or StatusCode(HttpStatusCode.NoContent) if you want 204
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("[AddReview] " + ex.Message, ex));
            }
        }

        [HttpGet]
        [Route("TopDiscounted")]
        public async Task<IHttpActionResult> GetTopDiscountedProducts([FromUri] int count = 4)
        {
            try
            {
                var products = await _productRepository.GetTopDiscountedProductsAsync(count);

                if (products == null || !products.Any())
                {
                    return NotFound();
                }

                return Ok(products.Select(p => p.toProductCardDto()).ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"[GetTopDiscountedProducts] Error: {ex.Message}", ex));
            }
        }

        [HttpGet]
        [Route("Discounted")]
        public async Task<IHttpActionResult> GetAllDiscountedProducts()
        {
            try
            {
                var products = await _productRepository.GetAllDiscountedProductsAsync();

                if (products == null || !products.Any())
                {
                    return NotFound();
                }

                return Ok(products.Select(p => p.toProductCardDto()).ToList());
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"[GetTopDiscountedProducts] Error: {ex.Message}", ex));
            }
        }

    }
}

using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using TechtonicFramework.Data;
using TechtonicFramework.Dtos;
using TechtonicFramework.Dtos.Management.DashboardRelated;
using TechtonicFramework.Dtos.Management.ProductRelated;
using TechtonicFramework.Dtos.Product;
using TechtonicFramework.Extensions;
using TechtonicFramework.Mappers;
using TechtonicFramework.Repository;

namespace TechtonicFramework.Controllers
{
    [RoutePrefix("api/ProductManagement")]
    public class ProductManagementController : ApiController
    {
        private readonly ProductManagementRepository _productManagementRepository = new ProductManagementRepository();
        private readonly ProductRepository _productRepository = new ProductRepository(new ApplicationDbContext());

        [Authorize(Roles = "Moderator")]
        [HttpGet]
        [Route("")]
        public async Task<IHttpActionResult> GetAdminProducts(
            string sortBy = null,
            string filters = null,
            int? pageNumber = 1,
            int? pageSize = 10,
            string search = null,
            string priceRange = null)
        {
            try
            {
                var result = await _productManagementRepository.GetAdminProducts(sortBy, filters, pageNumber, pageSize, search, priceRange);

                if (result?.productCardDtos == null || result.productCardDtos.Count == 0)
                    return NotFound();

                return Ok(result);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception($"[Admin:GetProducts] Error: {ex.Message}", ex));
            }
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [Route("create")]
        public async Task<IHttpActionResult> CreateProduct()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var provider = await Request.Content.ReadAsMultipartAsync();
            var productDto = await MultipartFormDataHelper.ParseCreateProductDtoAsync(provider); 

            try
            {
                var product = await _productManagementRepository.CreateProductAsync(productDto);
                return Ok(product.toProductDto());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
        [Route("update/{id:int}")]
        public async Task<IHttpActionResult> GetProductForEdit(int id)
        {
            try
            {
                var dto = await _productManagementRepository.GetProductForUpdateAsync(id);
                if (dto == null)
                    return NotFound();

                return Ok(dto);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


        [Authorize(Roles = "Moderator")]
        [HttpPut]
        [Route("update/{id:int}")]
        public async Task<IHttpActionResult> UpdateProduct(int id)
        {
            var provider = await Request.Content.ReadAsMultipartAsync();
            var productDto = await MultipartFormDataHelper.ParseUpdateProductDtoAsync(provider);

            if (id != productDto.Id)
                return BadRequest("ID mismatch between route and body.");

            try
            {
                var updatedProduct = await _productManagementRepository.UpdateProductAsync(productDto);
                if (updatedProduct == null)
                    return NotFound();

                return Ok(updatedProduct.toProductDto());
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [Authorize(Roles = "Moderator")]
        [HttpDelete]
        [Route("{id:int}")]
        public async Task<IHttpActionResult> DeleteProduct(int id)
        {
            try
            {
                var deleted = await _productManagementRepository.DeleteProductAsync(id);
                if (!deleted)
                    return NotFound();

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch
            {
                return InternalServerError();
            }
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
        [Route("brands")]
        public async Task<IHttpActionResult> GetBrands()
        {
            var brands = await _productManagementRepository.GetBrandsAsync();
            return Ok(brands);
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [Route("brands")]
        public async Task<IHttpActionResult> CreateBrand([FromBody] CreateBrandDto dto)
        {
            var brand = await _productManagementRepository.CreateBrandAsync(dto);
            return Created($"api/product-management/brands/{brand.Id}", brand);
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
        [Route("filters")]
        public async Task<IHttpActionResult> GetAdminFilters()
        {
            var categorySlug = "laptop";
            var filters = await _productManagementRepository.GetAdminFiltersAsync(categorySlug);

            if (filters == null || filters.Count == 0)
                return NotFound();

            return Ok(filters);
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [Route("filters")]
        public async Task<IHttpActionResult> CreateFilterAttribute([FromBody] CreateFilterAttributeDto dto)
        {
            try
            {
                var filter = await _productManagementRepository.CreateFilterAttributeAsync(dto);
                return Ok(filter);
            }
            catch (ArgumentException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [Authorize(Roles = "Moderator")]
        [HttpPost]
        [Route("filters/{filterId:int}/values")]
        public async Task<IHttpActionResult> AddFilterValue(int filterId, [FromBody] CreateFilterAttributeValueDto dto)
        {
            try
            {
                var value = await _productManagementRepository.AddFilterValueAsync(filterId, dto);
                return Ok(value);
            }
            catch (ArgumentException ex)
            {
                return NotFound();
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpGet]
        [Route("dashboard-summary")]
        public async Task<IHttpActionResult> GetAdminDashboardSummary()
        {
            try
            {
                var dashboardDto = new DashboardDto
                {
                    DashboardSales = await _productManagementRepository.GetDashboardSalesSummaryAsync(),
                    TopSellingProducts = await _productManagementRepository.GetTopSellingProductsSummaryAsync(),
                    MostVisitedProducts = await _productManagementRepository.GetMostVisitedProductsSummaryAsync(),
                    TopCartedProducts = await _productManagementRepository.GetTopCartedProductsSummaryAsync(),
                    ProductsOverviewDto = await _productManagementRepository.GetProductOverviewSummaryAsync(),
                    OrderSummaryDto = await _productManagementRepository.GetOrderCountSummaryAsync(),
                    TopSellingBrands = await _productManagementRepository.GetTopSellingBrandsSummaryAsync(),
                    LowStockProducts = await _productManagementRepository.GetLowStockProductsSummaryAsync()
                };

                return Ok(dashboardDto);
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.InternalServerError, new
                {
                    error = "An unexpected error occurred while fetching dashboard summary.",
                    detail = ex.Message
                });
            }
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
        [Route("questions")]
        public async Task<IHttpActionResult> GetUnansweredProductQuestions()
        {
            try
            {
                var questions = await _productManagementRepository.GetUnansweredProductQuestionsAsync();
                return Ok(questions);
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("[GetUnansweredProductQuestions] " + ex.Message));
            }
        }

        [Authorize(Roles = "Moderator")]
        [HttpPut]
        [Route("questions/{questionId:int}")]
        public async Task<IHttpActionResult> AnswerProductQuestion(int questionId, [FromBody] string answer)
        {
            if (string.IsNullOrWhiteSpace(answer))
                return BadRequest("Answer cannot be empty.");

            try
            {
                var success = await _productManagementRepository.AnswerProductQuestionAsync(questionId, answer);
                if (!success)
                    return NotFound();

                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(new Exception("[AnswerProductQuestion] " + ex.Message));
            }
        }

        [Authorize(Roles = "Moderator")]
        [HttpGet]
        [Route("reviews")]
        public async Task<IHttpActionResult> GetProductReviews(
            string ReviewerName = null,
            string productId = null,
            string orderBy = null)
        {
            var reviews = await _productManagementRepository.GetProductReviews(
                ReviewerName,
                productId,
                orderBy);

            if (reviews == null)
            {
                return BadRequest("Invalid productId format. Must be an integer.");
            }

            if (reviews.Count == 0)
            {
                return NotFound();
            }

            return Ok(reviews);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("reviews/{reviewId:int}")]
        public async Task<IHttpActionResult> DeleteReview(int reviewId)
        {
            var deleted = await _productManagementRepository.DeleteReviewAsync(reviewId);

            if (!deleted)
            {
                return NotFound();
            }

            return Ok(new { message = "Review deleted successfully." });
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("filters/{filterId:int}")]
        public async Task<IHttpActionResult> DeleteFilter(int filterId)
        {
            try
            {
                var deleted = await _productManagementRepository.DeleteFilterAsync(filterId);
                if (!deleted)
                    return NotFound();

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete]
        [Route("filtervalues/{valueId:int}")]
        public async Task<IHttpActionResult> DeleteFilterValue(int valueId)
        {
            try
            {
                var deleted = await _productManagementRepository.DeleteFilterValueAsync(valueId);
                if (!deleted)
                    return NotFound();

                return StatusCode(HttpStatusCode.NoContent);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }


    }
}

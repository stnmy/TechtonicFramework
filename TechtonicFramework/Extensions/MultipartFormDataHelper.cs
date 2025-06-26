using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using TechtonicFramework.Dtos.Management.ProductRelated;
using System.Web;
using System.Web.Script.Serialization;
using System.Text.RegularExpressions;

namespace TechtonicFramework.Extensions
{
    public static class MultipartFormDataHelper
    {
        public static async Task<UpdateProductDto> ParseUpdateProductDtoAsync(MultipartMemoryStreamProvider provider)
        {
            var dto = new UpdateProductDto();
            var attrMap = new Dictionary<int, ProductAttributeValueCreateDto>();

            foreach (var content in provider.Contents)
            {
                var name = content.Headers.ContentDisposition.Name?.Trim('"')?.ToLowerInvariant();
                if (name == null) continue;

                if (content.Headers.ContentDisposition.FileName != null)
                {
                    var fileName = content.Headers.ContentDisposition.FileName.Trim('"');
                    var bytes = await content.ReadAsByteArrayAsync();
                    var stream = new System.IO.MemoryStream(bytes);
                    var postedFile = new HttpPostedFileMock(fileName, content.Headers.ContentType.MediaType, stream);
                    dto.ProductImages.Add(postedFile);
                }
                else
                {
                    var value = await content.ReadAsStringAsync();

                    if (name.StartsWith("attributevalues["))
                    {
                        var match = Regex.Match(name, @"attributevalues\[(\d+)\]\.(\w+)");
                        if (match.Success)
                        {
                            int index = int.Parse(match.Groups[1].Value);
                            string prop = match.Groups[2].Value;

                            if (!attrMap.ContainsKey(index))
                                attrMap[index] = new ProductAttributeValueCreateDto();

                            var attr = attrMap[index];

                            switch (prop)
                            {
                                case "name": attr.Name = value; break;
                                case "value": attr.Value = value; break;
                                case "specificationcategory": attr.SpecificationCategory = value; break;
                                case "filterattributevalueid":
                                    if (int.TryParse(value, out int idVal))
                                        attr.FilterAttributeValueId = idVal;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (name)
                        {
                            case "id": dto.Id = int.Parse(value); break;
                            case "name": dto.Name = value; break;
                            case "description": dto.Description = value; break;
                            case "price": dto.Price = decimal.Parse(value); break;
                            case "stockquantity": dto.StockQuantity = int.Parse(value); break;
                            case "isfeatured": dto.IsFeatured = bool.Parse(value); break;
                            case "isdealoftheday": dto.IsDealOfTheDay = bool.Parse(value); break;
                            case "discountprice":
                                dto.DiscountPrice = string.IsNullOrWhiteSpace(value) ? (decimal?)null : decimal.Parse(value);
                                break;
                        }
                    }
                }
            }

            dto.AttributeValues = new List<ProductAttributeValueCreateDto>(attrMap.Values);
            return dto;
        }

        public static async Task<CreateProductDto> ParseCreateProductDtoAsync(MultipartMemoryStreamProvider provider)
        {
            var dto = new CreateProductDto();
            var attrMap = new Dictionary<int, ProductAttributeValueCreateDto>();

            foreach (var content in provider.Contents)
            {
                var name = content.Headers.ContentDisposition.Name?.Trim('"')?.ToLowerInvariant();
                if (name == null) continue;

                if (content.Headers.ContentDisposition.FileName != null)
                {
                    var fileName = content.Headers.ContentDisposition.FileName.Trim('"');
                    var bytes = await content.ReadAsByteArrayAsync();
                    var stream = new System.IO.MemoryStream(bytes);
                    var postedFile = new HttpPostedFileMock(fileName, content.Headers.ContentType.MediaType, stream);
                    dto.ProductImages.Add(postedFile);
                }
                else
                {
                    var value = await content.ReadAsStringAsync();

                    if (name.StartsWith("attributevalues["))
                    {
                        var match = Regex.Match(name, @"attributevalues\[(\d+)\]\.(\w+)");
                        if (match.Success)
                        {
                            int index = int.Parse(match.Groups[1].Value);
                            string prop = match.Groups[2].Value;

                            if (!attrMap.ContainsKey(index))
                                attrMap[index] = new ProductAttributeValueCreateDto();

                            var attr = attrMap[index];

                            switch (prop)
                            {
                                case "name": attr.Name = value; break;
                                case "value": attr.Value = value; break;
                                case "specificationcategory": attr.SpecificationCategory = value; break;
                                case "filterattributevalueid":
                                    if (int.TryParse(value, out int idVal))
                                        attr.FilterAttributeValueId = idVal;
                                    break;
                            }
                        }
                    }
                    else
                    {
                        switch (name)
                        {
                            case "name": dto.Name = value; break;
                            case "description": dto.Description = value; break;
                            case "price": dto.Price = decimal.Parse(value); break;
                            case "stockquantity": dto.StockQuantity = int.Parse(value); break;
                            case "isfeatured": dto.IsFeatured = bool.Parse(value); break;
                            case "isdealoftheday": dto.IsDealOfTheDay = bool.Parse(value); break;
                            case "brandid": dto.BrandId = int.Parse(value); break;
                            case "categoryid": dto.CategoryId = int.Parse(value); break;
                            case "subcategoryid":
                                dto.SubCategoryId = string.IsNullOrEmpty(value) ? (int?)null : int.Parse(value);
                                break;
                        }
                    }
                }
            }

            dto.AttributeValues = new List<ProductAttributeValueCreateDto>(attrMap.Values);
            return dto;
        }
    }

    internal class HttpPostedFileMock : HttpPostedFileBase
    {
        private readonly System.IO.Stream _stream;
        private readonly string _fileName;
        private readonly string _contentType;

        public HttpPostedFileMock(string fileName, string contentType, System.IO.Stream stream)
        {
            _fileName = fileName;
            _contentType = contentType;
            _stream = stream;
        }

        public override int ContentLength => (int)_stream.Length;
        public override string FileName => _fileName;
        public override string ContentType => _contentType;
        public override System.IO.Stream InputStream => _stream;
    }
}

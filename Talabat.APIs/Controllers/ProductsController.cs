﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIs.DTOs;
using Talabat.APIs.Errors;
using Talabat.Core.Entities;
using Talabat.Core.Repositories.Contract;
using Talabat.Core.Specifications.ProductSpecs;

namespace Talabat.APIs.Controllers
{
	public class ProductsController : BaseApiController
	{
		private readonly IGenericRepository<Product> _productRepo;
		private readonly IGenericRepository<ProductBrand> _brandsRepo;
		private readonly IGenericRepository<ProductCategory> _categoryRepo;
		private readonly IMapper _mapper;

		public ProductsController(
			IGenericRepository<Product> productRepo,
			IGenericRepository<ProductBrand> brandsRepo,
			IGenericRepository<ProductCategory> CategoryRepo,
			IMapper mapper)
		{
			_productRepo = productRepo;
			_brandsRepo = brandsRepo;
			_categoryRepo = CategoryRepo;
			_mapper = mapper;
		}
		// api/Products
		[HttpGet]
		public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
		{
			var spec = new ProductWithBrandAndCategorySpecifications();
			var products = await _productRepo.GetAllWithSpecAsync(spec);
			//JsonResult result = new JsonResult(products);

			return Ok(_mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
		}

		[ProducesResponseType(typeof(ProductToReturnDto), StatusCodes.Status200OK)]
		[ProducesResponseType(typeof(APIResponse), StatusCodes.Status404NotFound)]
		[HttpGet("{id}")]
		public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
		{
			var spec = new ProductWithBrandAndCategorySpecifications(id);
			var product = await _productRepo.GetWithSpecAsync(spec);

			if (product is null)
			{
				return NotFound(new APIResponse(404));
			}
			return Ok(_mapper.Map<Product, ProductToReturnDto>(product));
		}

		[HttpGet("brands")] // Get : Api/Producs/brands
		public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetBrands()
		{
			var brands = await _brandsRepo.GetAllAsync();
			return Ok(brands);
		}
		
		[HttpGet("category")] // Get : Api/Producs/category
		public async Task<ActionResult<IReadOnlyList<ProductCategory>>> GetCategorys()
		{
			var categories = await _categoryRepo.GetAllAsync();
			return Ok(categories);
		}


	}
}

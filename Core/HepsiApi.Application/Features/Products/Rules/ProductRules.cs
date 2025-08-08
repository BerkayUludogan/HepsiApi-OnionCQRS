using HepsiApi.Application.Bases;
using HepsiApi.Application.Features.Products.Exception;
using HepsiApi.Domain.Entities;

namespace HepsiApi.Application.Features.Products.Rules
{
    public class ProductRules : BaseRules
    {
        public Task ProductTitleMustNotBeSame(IList<Product> products, string requestTitle)
        {
            if (products.Any(p => p.Title == requestTitle)) throw new ProductTitleMustNotBeSameExcepiton();
            return Task.CompletedTask;
        }
    }
}

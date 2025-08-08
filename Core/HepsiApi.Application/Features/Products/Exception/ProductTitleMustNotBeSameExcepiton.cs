using HepsiApi.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Features.Products.Exception
{
    public class ProductTitleMustNotBeSameExcepiton : BaseExceptions
    {
        public ProductTitleMustNotBeSameExcepiton() : base("Ürün başlığı zaten var!") { }
    }
}

using HepsiApi.Application.Bases;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HepsiApi.Application.Features.Auth.Exceptions
{
    public class EmailAddressShouldNotBeValidException : BaseExceptions
    {
        public EmailAddressShouldNotBeValidException() : base("Böyle bir email adresi bulunmamaktadır.")
        { 
        }
    }
}

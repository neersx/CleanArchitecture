using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorHero.CleanArchitecture.Application.Exceptions
{
    public class ForbiddenAccessException : CustomException
    {
        public ForbiddenAccessException(string message) : base(message, null, System.Net.HttpStatusCode.Forbidden) { }
    }
}

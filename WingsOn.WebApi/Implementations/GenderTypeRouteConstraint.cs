using System;
using System.Text.RegularExpressions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using WingsOn.Domain;

namespace WingsOn.WebApi.Implementations
{
    public class GenderTypeRouteConstraint:IRouteConstraint
    {
        public bool Match(HttpContext httpContext, IRouter route, string routeKey, RouteValueDictionary values,
            RouteDirection routeDirection)
        {
            var candidate = values[routeKey]?.ToString();
            return !string.IsNullOrEmpty(candidate) && Regex.IsMatch(candidate, @"^[a-zA-Z]+$") &&
                   Enum.TryParse(candidate, true, out GenderType result);
        }
    }
}
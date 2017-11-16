﻿namespace Tests.Integration.Mvc
{
    using System;
    using System.Web.Mvc;
    using System.Web.Optimization;
    using System.Web.Routing;
    using TestData.Data;
    using Tests.Integration.Mvc.Controllers;

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);

            var dataGenerator = new DataGenerator();
            HomeController.SimpleDataBig = dataGenerator.GenerateSimpleData(500, DateTime.Now.AddYears(-5), DateTime.Now);
            HomeController.ComplexDataBig = dataGenerator.GenerateComplexData(500, DateTime.Now.AddYears(-5), DateTime.Now);
        }
    }
}
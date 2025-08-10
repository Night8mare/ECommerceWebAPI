using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchEcommerce.Tests.Common
{
    public abstract class ControllerTestBase<TController> where TController : ControllerBase, new()
    {
        protected readonly Mock<ISender> MediatorMock;
        protected readonly TController Controller;

        protected ControllerTestBase()
        {
            MediatorMock = new Mock<ISender>();
            Controller = new TController();

            var serviceProvider = new Mock<IServiceProvider>();
            serviceProvider
                .Setup(s => s.GetService(typeof(ISender)))
                .Returns(MediatorMock.Object);

            var httpContext = new DefaultHttpContext
            {
                RequestServices = serviceProvider.Object
            };

            Controller.ControllerContext = new ControllerContext
            {
                HttpContext = httpContext
            };
        }
    }
}

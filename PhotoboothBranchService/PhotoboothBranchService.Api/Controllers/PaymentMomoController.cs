using Microsoft.AspNetCore.Mvc;
using PhotoboothBranchService.Api.Common;
using PhotoboothBranchService.Application.DTOs.MoMoPayment;
using PhotoboothBranchService.Application.Services.MoMoServices;

namespace PhotoboothBranchService.Api.Controllers
{
    public class PaymentMomoController : ControllerBaseApi
    {
        private readonly IMoMoService _moService;

        public PaymentMomoController(IMoMoService moService)
        {
            _moService = moService;
        }

        

    }
}

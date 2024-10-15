using Microsoft.AspNetCore.Mvc;
using Store.Service.Dtos.BasketDtos;
using Store.Service.Dtos.OrderDtos;
using Store.Service.Interfaces;
using Stripe;

namespace Store.Web.Controllers
{
	public class PaymentController : BaseController
	{
		private readonly IPaymentService _paymentService;
		const string endpointSecret = "whsec_2a36b456c66e8d63aa051813d785bbbf283f305c9885442a084ceac21cc1ad78";

		public PaymentController(IPaymentService paymentService)
		{
			_paymentService = paymentService;
		}

		[HttpPost]
		public async Task<ActionResult<CustomerBasketDto>> CreateOrUpdatePaymentIntent(CustomerBasketDto input)
		 => Ok(await _paymentService.CreateOrUpdatePaymentIntent(input));

		[HttpPost]
		public async Task<IActionResult> Webhook()
		{
			var json = await new StreamReader(HttpContext.Request.Body).ReadToEndAsync();

			try
			{
				var stripeEvent = EventUtility.ConstructEvent(json,
					Request.Headers["Stripe-Signature"],endpointSecret);
				PaymentIntent paymentIntent;
				if (stripeEvent.Type == EventTypes.PaymentIntentSucceeded)
				{
					paymentIntent = stripeEvent.Data.Object as PaymentIntent;

					await _paymentService.UpdateOrderPaymentSucceeded(paymentIntent?.Id);
				}
				else
				{
					Console.WriteLine("Unhandled event type: {0}", stripeEvent.Type);
				}
				return Ok();
			}
			catch (StripeException ex)
			{
				return BadRequest();
			}
		}
	}
}

using Jorda.Server.Application.Common.Models.Payment;

namespace Jorda.Server.Application.Common.Interfaces;

public interface IPaymentService
{
    public void CreateCheckoutSession(CreateCheckoutSessionRequest request);
}

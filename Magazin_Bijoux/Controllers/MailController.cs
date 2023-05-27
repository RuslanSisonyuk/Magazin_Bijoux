using Magazin_Bijoux.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

[Route("api/[controller]")]
[ApiController]
public class MailController : ControllerBase
{
    private readonly IMailService mailService;
    public MailController(IMailService mailService)
    {
        this.mailService = mailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> SendMail([FromForm] MailRequest request)
    {
        try
        {
            await mailService.SendEmailAsync(request);
            return Ok();
        }
        catch (Exception ex)
        {
            throw;
        }
    }

    [HttpPost("welcome")]
    public async Task<IActionResult> SendWelcomeMail([FromForm] WelcomeRequest request)
    {
        try
        {
            await mailService.SendWelcomeEmailAsync(request);
            return RedirectToAction("Cart", "CartItems");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
    public async Task<IActionResult> SendPurchaseMail([FromForm] PurchaseRequest request)
    {
        try
        {
            await mailService.SendPurchaseEmailAsync(request);
            return RedirectToAction("Cart", "CartItems");
        }
        catch (Exception ex)
        {
            throw;
        }
    }
}
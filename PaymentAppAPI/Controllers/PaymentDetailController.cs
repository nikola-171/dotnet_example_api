using Microsoft.AspNetCore.Mvc;
using PaymentApp.Models;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore.Design;
using System.Threading.Tasks;
using System.Linq;
using System.Collections.Concurrent;
using Microsoft.EntityFrameworkCore;

namespace PaymentApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PaymentDetailController : ControllerBase
    {

        private readonly PaymentDetailsContext _context;

        public PaymentDetailController(PaymentDetailsContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<PaymentDetails>>> GetPaymentDetails()
        {
            return await _context.PaymentDetails.ToListAsync();
        }

        [HttpPost]
        public async Task<ActionResult<PaymentDetails>> PostPaymentDetails(PaymentDetails paymentDetails)
        {
            _context.PaymentDetails.Add(paymentDetails);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetPaymentDetail", new { id = paymentDetails.PaymentDetailId}, paymentDetails);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<ActionResult<PaymentDetails>> GetPaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);

            if(paymentDetail == null)
            {
                return NotFound();
            }else
            {
                return paymentDetail;
            }
        }

        [HttpPut]
        [Route("{id}")]
        public async Task<ActionResult> PutPaymentDdetail(int id, PaymentDetails paymentDetails)
        {
            if(id != paymentDetails.PaymentDetailId)
            {
                return BadRequest();
            }
            _context.Entry(paymentDetails).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }catch(DbUpdateConcurrencyException)
            {
                if(!PaymentDetailExists(id))
                {
                    return NoContent();
                }else
                {
                    throw;
                }
            }

            return NoContent();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<ActionResult> DeletePaymentDetail(int id)
        {
            var paymentDetail = await _context.PaymentDetails.FindAsync(id);

            if(paymentDetail == null)
            {
                return NotFound();
            }
            else
            {
                _context.PaymentDetails.Remove(paymentDetail);
                await _context.SaveChangesAsync();

                return NoContent();
            }
        }

        private bool PaymentDetailExists(int id)
        {
            return _context.PaymentDetails.Any(e => e.PaymentDetailId == id);
        }

    }
}
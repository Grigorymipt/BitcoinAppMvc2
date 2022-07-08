using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using BitcoinAppMvc.Models;

namespace BitcoinAppMvc.Data
{
    public class TransactionDataController : Controller
    {
        private readonly BitcoinAppMvcContext _context;

        public TransactionDataController(BitcoinAppMvcContext context)
        {
            _context = context;
        }

        // GET: db1
        public async Task<IActionResult> Index()
        {
            return View( _context.TransactionDataRepository.List());
        }

        // GET: db1/Details/5
        public async Task<IActionResult> Details(Guid id)
        {
            if (id == null || _context.TransactionData == null)
            {
                return NotFound();
            }

            var db1 = await _context.TransactionDataRepository.GetAsync(id);
            if (db1 == null)
            {
                return NotFound();
            }

            return View(db1);
        }

        // GET: db1/Create
        public IActionResult Create()
        {
            var td = new TransactionData();
            return View(td);
        }

        // POST: db1/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,UTCCreated,UTCChanged,Entity_Id,System_Id,DocumentType,PublicationStatus")] TransactionData db1)
        {
            if (ModelState.IsValid)
            {
                _context.TransactionDataRepository.Add(db1);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(db1);
        }

        // GET: db1/Edit/5
        public async Task<IActionResult> Edit(Guid id)
        {
            var db1 = await _context.TransactionDataRepository.GetAsync(id);
            if (db1 == null)
            {
                return NotFound();
            }


            return View(db1);
        }

        // POST: db1/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, 
            [Bind("Id,UTCCreated,UTCChanged,Entity_Id,System_Id,DocumentType,PublicationStatus")] TransactionData db1)
        {
            if (id != db1.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.TransactionDataRepository.Update(db1);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!db1Exists(db1.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(db1);
        }

        // GET: db1/Delete/5
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var db1 = await _context.TransactionDataRepository
                .GetAsync(id);
            if (db1 == null)
            {
                return NotFound();
            }

            return View(db1);
        }

        // POST: db1/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var db1 = await _context.TransactionDataRepository.GetAsync(id);
            if (db1 != null)
            {
                _context.TransactionDataRepository.Remove(id);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool db1Exists(Guid id)
        {
          return (_context.TransactionDataRepository.GetAsync(id)!=null);
        }


        //TODO: УДАЛИТЬ

        private void ForJobExecutionCireExample() {

            var transactions = _context.TransactionDataRepository
            .GetByStatus(PublicationStatus.New).OrderBy(t => t.UTCChanged);
                
            foreach(var transaction in transactions)
            {
                transaction.PublicationStatus = PublicationStatus.Penging;

                _context.TransactionDataRepository.Update(transaction);

                try
                {
                    //todo: some blockchain logic
                    transaction.PublicationStatus = PublicationStatus.Commited;
                    transaction.ExternalTransactionId = "Сюда всунуть транзакшн id из битка";
                }
                catch (Exception ex) {
                    transaction.PublicationStatus = PublicationStatus.Failed;
                    _context.TransactionDataRepository.Update(transaction);
                }
            }
                
        }
    }
}


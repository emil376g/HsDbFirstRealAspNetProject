﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using HsDbFirstRealAspNetProject.Models;
using HsDbFirstRealAspNetProject.Models.DbModel;
using System.Reflection;
using unirest_net.http;
using System.Net;
using System.IO;
using Microsoft.AspNetCore.Authorization;

namespace HsDbFirstRealAspNetProject.Controllers
{
    public class CardInfoesController : Controller
    {
        private readonly HsDbFirstRealAspNetProjectContext _context;

        public CardInfoesController(HsDbFirstRealAspNetProjectContext context)
        {
            _context = context;
        }

        // GET: CardInfoes
        public async Task<IActionResult> Index(
            string sortOrder,
            string currentFilter,
            string searchString,
            int? page,
            int? parPage)
        {
            ViewData["CurrentSort"] = sortOrder;
            ViewData["CardSetParm"] = string.IsNullOrEmpty(sortOrder) ? "CardSet_desc" : "";
            ViewData["CurrentFilter"] = searchString;
            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var card = from c in _context.CardInfo.Distinct() select c;
            card = card.OrderBy(c => c.AdditionCard.Cost);
            if (!string.IsNullOrEmpty(searchString))
            {
                card = card.Where(c => c.Name.Contains(searchString) || c.Type.Contains(searchString) || c.Class.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "CardSet_desc":
                    card = card.OrderByDescending(c => c.CardSet);
                    break;
                default:
                    card = card.OrderBy(c => c.Class);
                    break;
            }
            if(parPage == null || parPage == 0)
            {
                parPage = 20;
            }
            return View(await PaginatedList<CardInfo>.CreateAsync(card.AsNoTracking(), page ?? 1, (int)parPage));
        }

        // GET: CardInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardInfo = await _context.CardInfo
                .SingleOrDefaultAsync(m => m.CardInfoId == id);
            if (cardInfo == null)
            {
                return NotFound();
            }

            return View(cardInfo);
        }

        // GET: CardInfoes/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: CardInfoes/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Policy = "Create")]
        public async Task<IActionResult> Create([Bind("CardInfoId,CardId,DbId,Name,Type,Text,Class,CardSet,Img")] CardInfo cardInfo)
        {
            HttpResponse<Hearthstone.Hs> response = Unirest.get("https://omgvamp-hearthstone-v1.p.mashape.com/cards")
.header("X-Mashape-Key", "GBUA6L0J0QmshBqbIxIzTmy4Up8tp1r18WOjsnu52LriUEs890")
.asJson<Hearthstone.Hs>();
            List<Hearthstone.Hs> hearthstoneCards = new List<Hearthstone.Hs>
            {
                response.Body
            };
            PropertyInfo[] property = typeof(Hearthstone.Hs).GetProperties();
            foreach (PropertyInfo info in property)
            {
                if (info.GetValue(response.Body) is List<Hearthstone.Basic> item2)
                {
                    foreach (Hearthstone.Basic item in item2)
                    {
                        using (WebClient client = new WebClient())
                        {
                            if (item.Img != null)
                            {
                                try
                                {
                                    var file = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images", item.Name + ".png");
                                    client.DownloadFile(item.Img, file);
                                    item.Img = item.Name + ".png";
                                    _context.Add((CardInfo)item);
                                }
                                catch (Exception)
                                {

                                }
                            }
                        }

                    }
                }
            }
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: CardInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardInfo = await _context.CardInfo.SingleOrDefaultAsync(m => m.CardInfoId == id);
            if (cardInfo == null)
            {
                return NotFound();
            }
            return View(cardInfo);
        }

        // POST: CardInfoes/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("CardInfoId,CardId,DbId,Name,Type,Text,Class,CardSet,Img")] CardInfo cardInfo)
        {
            if (id != cardInfo.CardInfoId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(cardInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CardInfoExists(cardInfo.CardInfoId))
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
            return View(cardInfo);
        }

        // GET: CardInfoes/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var cardInfo = await _context.CardInfo
                .SingleOrDefaultAsync(m => m.CardInfoId == id);
            if (cardInfo == null)
            {
                return NotFound();
            }

            return View(cardInfo);
        }

        // POST: CardInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var cardInfo = await _context.CardInfo.SingleOrDefaultAsync(m => m.CardInfoId == id);
            _context.CardInfo.Remove(cardInfo);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CardInfoExists(int id)
        {
            return _context.CardInfo.Any(e => e.CardInfoId == id);
        }
    }
}

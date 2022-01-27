#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BackEnd.Context;
using BackEnd.Models;
using BackEnd.Pages;

namespace BackEnd.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    
    public class ProdutosController : Controller
    {
        private const string V = "Não incidente";
        private readonly AppDbContext _context;

        public ProdutosController(AppDbContext context)
        {
            _context = context;
        }

        // GET: Produtos
        [HttpGet(Name = "Getprodutos")]
        public async Task<IActionResult> Index(int pg=1)
        {
            List<Produto> produtos = await _context.Produtos.ToListAsync();
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            int recsCount = produtos.Count();

            var pages = new Pages.Pages(recsCount,
                                  pg,
                                  pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = produtos.Skip(recSkip).Take(pageSize).ToList();
            this.ViewBag.Pages = pages;

            return View(data);
        }
        // GET: ProdutosFiltro
        [HttpGet(Name = "GetprodutosFiltrados")]
        public async Task<IActionResult> IndexFilterName(string nome,int pg = 1)
        {
            List<Produto> produtos = await _context.Produtos.ToListAsync();
            List<Produto> produtosNome = null;

            foreach (Produto produto in produtos)
            {
                if (produto.Nome == nome)
                {
                 produtosNome.Add(produto);   
                }
            }
            const int pageSize = 10;
            if (pg < 1)
                pg = 1;
            int recsCount = produtos.Count();

            var pages = new Pages.Pages(recsCount,
                                  pg,
                                  pageSize);
            int recSkip = (pg - 1) * pageSize;
            var data = produtos.Skip(recSkip).Take(pageSize).First();
            this.ViewBag.Pages = pages;

            return View(data);
        }
        [HttpGet(Name = "GetprodutosById")]
        // GET: Produtos/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }

        // GET: Produtos/Create
        [HttpGet(Name = "create")]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Produtoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Nome,Preco")] Produto produto)
        {
            if (ModelState.IsValid)
            {
                _context.Add(produto);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(produto);
        }

        // GET: Produtos/Edit/5
        [HttpGet(Name = "EditProdutos")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos.FindAsync(id);
            if (produto == null)
            {
                return NotFound();
            }
            return View(produto);
        }

        // POST: Produtos/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost(Name = "EditarProdutos")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Nome,Preco")] Produto produto)
        {
            if (id != produto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(produto);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ProdutoExists(produto.Id))
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
            return View(produto);
        }

        // GET: Produtos/Delete/5
        [HttpGet(Name = "DeleteProdutos")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var produto = await _context.Produtos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (produto == null)
            {
                return NotFound();
            }

            return View(produto);
        }
        // GET: Produtos incidentes
        [HttpGet(Name = "ProdutosIncidente")]
        public async Task<IActionResult> IdIncidente(int id)
        {
            if (ProdutoExists(id)) {
                return await Details(id);
            }
            else
            {
                return NotFound();
            }
            
                
            
        }


        // POST: Produtos/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var produto = await _context.Produtos.FindAsync(id);
            _context.Produtos.Remove(produto);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ProdutoExists(int id)
        {
            return _context.Produtos.Any(e => e.Id == id);
        }
    }
}

using Microsoft.AspNetCore.Mvc;

namespace Test3_08_04_2024_Ordinamento_Paginazione.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class ProdottiController : ControllerBase
    {
        private static readonly List<Prodotto> prodotti = new List<Prodotto>()
        {
            new Prodotto { Id = 1, Nome = "prodotto1", Categoria = "robe", Prezzo = 5.00},
            new Prodotto { Id = 2, Nome = "prodotto2", Categoria = "robe", Prezzo = 5.00},
            new Prodotto { Id = 3, Nome = "prodotto3", Categoria = "robe", Prezzo = 5.00},
            new Prodotto { Id = 4, Nome = "prodotto4", Categoria = "robe", Prezzo = 5.00},
            new Prodotto { Id = 5, Nome = "prodotto5", Categoria = "robe", Prezzo = 5.00},
            new Prodotto { Id = 6, Nome = "prodotto6", Categoria = "cose", Prezzo = 2.00},
            new Prodotto { Id = 7, Nome = "prodotto7", Categoria = "cose", Prezzo = 2.00},
            new Prodotto { Id = 8, Nome = "prodotto8", Categoria = "cose", Prezzo = 2.00},
            new Prodotto { Id = 9, Nome = "prodotto9", Categoria = "cose", Prezzo = 2.00},
            new Prodotto { Id = 10, Nome = "prodotto10", Categoria = "cose", Prezzo = 2.00},
            new Prodotto { Id = 11, Nome = "prodotto11", Categoria = "affari", Prezzo = 4.50},
            new Prodotto { Id = 12, Nome = "prodotto12", Categoria = "affari", Prezzo = 4.50},
            new Prodotto { Id = 13, Nome = "prodotto13", Categoria = "affari", Prezzo = 4.50},
            new Prodotto { Id = 14, Nome = "prodotto14", Categoria = "affari", Prezzo = 4.50},
            new Prodotto { Id = 15, Nome = "prodotto15", Categoria = "affari", Prezzo = 4.50},
            new Prodotto { Id = 16, Nome = "prodotto16", Categoria = "situazioni", Prezzo = 1.00},
            new Prodotto { Id = 17, Nome = "prodotto17", Categoria = "situazioni", Prezzo = 1.00},
            new Prodotto { Id = 18, Nome = "prodotto18", Categoria = "situazioni", Prezzo = 1.00},
            new Prodotto { Id = 19, Nome = "prodotto19", Categoria = "situazioni", Prezzo = 1.00},
            new Prodotto { Id = 20, Nome = "prodotto20", Categoria = "situazioni", Prezzo = 1.00},
            new Prodotto { Id = 21, Nome = "prodotto21", Categoria = "cianfrusaglie", Prezzo = 7.00},
            new Prodotto { Id = 22, Nome = "prodotto22", Categoria = "cianfrusaglie", Prezzo = 7.00},
            new Prodotto { Id = 23, Nome = "prodotto23", Categoria = "cianfrusaglie", Prezzo = 7.00},
            new Prodotto { Id = 24, Nome = "prodotto24", Categoria = "cianfrusaglie", Prezzo = 7.00},
            new Prodotto { Id = 25, Nome = "prodotto25", Categoria = "cianfrusaglie", Prezzo = 7.00},
            new Prodotto { Id = 26, Nome = "prodotto26", Categoria = "scarti", Prezzo = 30.00},
            new Prodotto { Id = 27, Nome = "prodotto27", Categoria = "scarti", Prezzo = 30.00},
            new Prodotto { Id = 28, Nome = "prodotto28", Categoria = "scarti", Prezzo = 30.00},
            new Prodotto { Id = 29, Nome = "prodotto29", Categoria = "scarti", Prezzo = 30.00},
            new Prodotto { Id = 30, Nome = "prodotto30", Categoria = "scarti", Prezzo = 30.00},
            new Prodotto { Id = 31, Nome = "prodotto98774958732", Categoria = "easterEgg", Prezzo = 0.99},
            new Prodotto { Id = 32, Nome = "prodottoTrentadue", Categoria = "easterEgg", Prezzo = 87.98},
            new Prodotto { Id = 33, Nome = "prodottoInesistente", Categoria = "easterEgg", Prezzo = 0.01},
        };

        [HttpGet("Ricerca", Name = "Ricerca")]
        public IActionResult GetProdottiRicerca([FromQuery] String nome, [FromQuery] String categoria, [FromQuery] double? prezzo)
        {
            var risultato = prodotti.AsQueryable();

            if (!String.IsNullOrEmpty(nome))
            {
                risultato = risultato.Where(p => p.Nome.Contains(nome));
            }

            if (!String.IsNullOrEmpty(categoria))
            {
                risultato = risultato.Where(p => p.Categoria == categoria);
            }

            if (prezzo != null)
            {
                risultato = risultato.Where(p => p.Prezzo == prezzo);
            }

            return Ok(risultato);
        }

        [HttpGet("Ordinamento", Name = "Ordinamento")]
        public IActionResult GetProdottiOrdine([FromQuery] int pagina = 1, [FromQuery] int limit = 10, [FromQuery] String ordinaPer = "Id", [FromQuery] bool ordineAsc = true)
        {
            if (pagina < 1) pagina = 1;

            // ORDINAMENTO
            var risultato = prodotti.AsQueryable();
            risultato = ordineAsc ? risultato.OrderBy(p => p.GetType().GetProperty(ordinaPer).GetValue(p)) : risultato.OrderBy(p => p.GetType().GetProperty(ordinaPer).GetValue(p));

            // PAGINAZIONE
            var prodottiPaginati = risultato.Skip((pagina - 1) * limit).Take(limit);

            return Ok(prodottiPaginati);
        }

        [HttpGet("RicercaOrdinamento", Name = "RicercaOrdinamento")]
        public IActionResult GetProdottiTutto([FromQuery] String nome, [FromQuery] String categoria, [FromQuery] double? prezzo, [FromQuery] int pagina = 1, [FromQuery] int limit = 10, [FromQuery] String ordinaPer = "Id", [FromQuery] bool ordineAsc = true)
        {
            var risultato = prodotti.AsQueryable();

            if (pagina < 1) pagina = 1;

            // RICERCA
            if (!String.IsNullOrEmpty(nome))
            {
                risultato = risultato.Where(p => p.Nome.Contains(nome));
            }

            if (!String.IsNullOrEmpty(categoria))
            {
                risultato = risultato.Where(p => p.Categoria == categoria);
            }

            if (prezzo != null)
            {
                risultato = risultato.Where(p => p.Prezzo == prezzo);
            }

            // ORDINAMENTO
            risultato = ordineAsc ? risultato.OrderBy(p => p.GetType().GetProperty(ordinaPer).GetValue(p)) : risultato.OrderBy(p => p.GetType().GetProperty(ordinaPer).GetValue(p));

            // PAGINAZIONE
            var prodottiPaginati = risultato.Skip((pagina - 1) * limit).Take(limit);

            return Ok(risultato);
        }
    }
}

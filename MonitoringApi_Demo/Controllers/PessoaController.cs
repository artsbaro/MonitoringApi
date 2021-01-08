using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MonitoringApi_Demo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class PessoaController : ControllerBase
    {
        private static readonly List<Pessoa> _pessoas = new List<Pessoa>
        {
            new Pessoa{Id = 1, Nome = "Antonio Raulande", Cpf = "11111111111", DataNascimento = new DateTime(1983,6,12)},
            new Pessoa{Id = 2, Nome = "Mario Jorge", Cpf = "22222222222", DataNascimento = new DateTime(1984,10,12)},
            new Pessoa{Id = 3, Nome = "João Paes", Cpf = "33333333333", DataNascimento = new DateTime(1985,7,12)},
            new Pessoa{Id = 4, Nome = "Daniel Jesus", Cpf = "44444444444", DataNascimento = new DateTime(1986,3,12)}
        };

        private readonly ILogger<PessoaController> _logger;

        public PessoaController(ILogger<PessoaController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Task.FromResult(_pessoas));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var pessoa = await Task.FromResult(_pessoas.FirstOrDefault(p => p.Id == id));

            if (pessoa == null) return NotFound();

            return Ok(pessoa);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pessoa  pessoa)
        {
            var pes = await Task.FromResult(_pessoas.FirstOrDefault(p => p.Id == pessoa.Id));

            if (pes == null) return NotFound();

            return Ok(pessoa);
        }
    }
}

using App.Metrics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MonitoringApi_Demo.AppMetrics;
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
        private readonly IMetrics _metrics;

        public PessoaController(ILogger<PessoaController> logger, 
            IMetrics metrics)
        {
            _logger = logger;
            _metrics = metrics;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await Task.FromResult(GetPessoas()));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            //var pessoa = await Task.FromResult(_pessoas.FirstOrDefault(p => p.Id == id));
            var pessoa = GetPessoa(id);

            if (pessoa == null) return NotFound();

            return Ok(pessoa);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pessoa  pessoa)
        {
            //var pes = await Task.FromResult(_pessoas.FirstOrDefault(p => p.Id == pessoa.Id));
            var pes = GetPessoa(pessoa.Id);

            if (pes != null) return BadRequest();

            AddPessoa(pessoa);

            return Ok();
        }

        private Pessoa GetPessoa(int id)
        {
            _metrics.Measure.Counter.Increment(MetricsRegistry.QuantidadeConexoesDBCriadas);
            return _pessoas.FirstOrDefault(p => p.Id == id);
        }

        private List<Pessoa> GetPessoas()
        {
            _metrics.Measure.Counter.Increment(MetricsRegistry.QuantidadeConexoesDBCriadas);
            return _pessoas;
        }

        private void AddPessoa(Pessoa pessoa)
        {
            _metrics.Measure.Counter.Increment(MetricsRegistry.QuantidadeConexoesDBCriadas);
            _pessoas.Add(pessoa);
            _metrics.Measure.Counter.Increment(MetricsRegistry.QuantidadePessoasCriadas);
        }
    }
}

using APIAppLivros.Models;
using LivrosAPI.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using MySqlX.XDevAPI;
using Swashbuckle.AspNetCore.Annotations;

namespace LivrosAPI.Controllers
{
    [Route("api/livro")]
    [ApiController]
    public class LivrosController : ControllerBase
    {
        private readonly LivrosRepository _livrosRepository;

        public LivrosController(LivrosRepository livrosRepository)
        {
            _livrosRepository = livrosRepository;
        }

   

        // GET api/<PessoaController>/5
        [HttpGet("detalhes/{id}")]
        [SwaggerOperation(
            Summary = "Obtém um livro pelo ID",
            Description = "Este endpoint retorna todos os dados de um livro cadastrado filtrando pelo ID.")]
        public async Task<Livros> BuscarPorId(int id)
        {
            return await _livrosRepository.BuscarPorId(id);
        }

        // POST api/<PessoaController>
        [HttpPost]
        [SwaggerOperation(
            Summary = "Adiciona um novo Livro",
            Description = "Adiciona um novo livro com validação para verificar títulos duplicados..")]
        public async void Criar([FromBody] Livros dados)
        {
            await _livrosRepository.Salvar(dados);
        }

        // PUT api/<PessoaController>/5
        [HttpPut("{id}")]
        [SwaggerOperation(
            Summary = "Atualiza um livro existente.",
            Description = "Este endpoint é responsável por atualizar os dados de um livro no banco.")]
        public async Task<IActionResult> Atualizar(int id, [FromBody] Livros dados)
        {
            dados.Id = id;
            await _livrosRepository.Atualizar(dados);
            return Ok();
        }

        // DELETE api/<PessoaController>/5
        [HttpDelete("{id}")]
        [SwaggerOperation(
            Summary = "Exclui um livro pelo ID.",
            Description = "Este endpoint é responsável por remover os dados de um livro no banco.")]
        public async Task<IActionResult> Delete(int id)
        {
            await _livrosRepository.DeletarPorId(id);
            return Ok();
        }
        // GET: api/<PessoaController>
        [HttpGet]
        [Route("listar")]
        [SwaggerOperation(Summary = "Listar todos os livros", Description = "Este endpoint retorna um listagem de livros cadastrados.")]
        public async Task<IEnumerable<Livros>> Listar([FromQuery] bool? ativo = null)
        {
            return await _livrosRepository.ListarTodosLivros(ativo);
        }
    }

}

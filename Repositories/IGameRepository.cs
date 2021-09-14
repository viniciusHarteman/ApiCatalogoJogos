using ApiCatalogoJogos.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Repositories
{
    public interface IGameRepository : IDisposable
    {
        Task<List<Game>> Obter(int pagina, int quantidade);
        Task<Game> Obter(Guid id);
        Task<List<Game>> Obter(string nome, string produtora);
        Task Inserir(Game game);
        Task Atualizar(Game game);
        Task Remover(Guid id);
    }
}

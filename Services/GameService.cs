using ApiCatalogoJogos.Entities;
using ApiCatalogoJogos.Exceptions;
using ApiCatalogoJogos.InputModel;
using ApiCatalogoJogos.Repositories;
using ApiCatalogoJogos.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Services
{
    public class GameService : IGameService
    {
        private readonly IGameRepository _jogoRepository;

        public GameService(IGameRepository jogoRepository)
        {
            _jogoRepository = jogoRepository;
        }

        public async Task<List<GameViewModel>> Obter(int pagina, int quantidade)
        {
            var games = await _jogoRepository.Obter(pagina, quantidade);

            return games.Select(game => new GameViewModel
            {
                Id = game.Id,
                Nome = game.Nome, 
                Produtora = game.Produtora,
                Preco = game.Preco
            }).ToList();
        }

        public async Task<GameViewModel> Obter(Guid id)
        {
            var game = await _jogoRepository.Obter(id);

            if (game == null)
                return null;

            return new GameViewModel
            {
                Id = game.Id,
                Nome = game.Nome,
                Produtora = game.Produtora,
                Preco = game.Preco
            };
        }

        public async Task<GameViewModel> Inserir(GameInputModel game)
        {
            var entidadeGame = await _jogoRepository.Obter(game.Nome, game.Produtora);

            if (entidadeGame.Count > 0)
                throw new JogoJaCadastradoException();

            var gameInsert = new Game
            {
                Id = Guid.NewGuid(),
                Nome = game.Nome,
                Produtora = game.Produtora,
                Preco = game.Preco
            };

            await _jogoRepository.Inserir(gameInsert);

            return new GameViewModel
            {
                Id = gameInsert.Id,
                Nome = game.Nome,
                Produtora = game.Produtora,
                Preco = game.Preco
            };
        }

        public async Task Atualizar(Guid id, GameInputModel game)
        {
            var entidadeGame = await _jogoRepository.Obter(id);

            if (entidadeGame == null)
                throw new JogoNaoCadastradoException();

            entidadeGame.Nome = game.Nome;
            entidadeGame.Produtora = game.Produtora;
            entidadeGame.Preco = game.Preco;

            await _jogoRepository.Atualizar(entidadeGame);
        }

        public async Task Atualizar(Guid id, double preco)
        {
            var entidadeGame = await _jogoRepository.Obter(id);

            if (entidadeGame == null)
                throw new JogoNaoCadastradoException();

            entidadeGame.Preco = preco;

            await _jogoRepository.Atualizar(entidadeGame);
        }

        public async Task Remover(Guid id)
        {
            var game = await _jogoRepository.Obter(id);

            if (game == null)
                throw new JogoNaoCadastradoException();

            await _jogoRepository.Remover(id);
        }

        public void Dispose()
        {
            _jogoRepository?.Dispose();
        }
    }
}

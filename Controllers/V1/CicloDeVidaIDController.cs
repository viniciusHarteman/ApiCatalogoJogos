using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;

namespace ApiCatalogoJogos.Controllers.V1
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class CicloDeVidaIDController : ControllerBase
    {
        public readonly ISingleton _Singleton1;
        public readonly ISingleton _Singleton2;

        public readonly IScoped _Scoped1;
        public readonly IScoped _Scoped2;

        public readonly ITransient _Transient1;
        public readonly ITransient _Transient2;

        public CicloDeVidaIDController(ISingleton Singleton1,
                                       ISingleton Singleton2,
                                       IScoped Scoped1,
                                       IScoped Scoped2,
                                       ITransient Transient1,
                                       ITransient Transient2)
        {
            _Singleton1 = Singleton1;
            _Singleton2 = Singleton2;
            _Scoped1 = Scoped1;
            _Scoped2 = Scoped2;
            _Transient1 = Transient1;
            _Transient2 = Transient2;
        }

        [HttpGet]
        public Task<string> Get()
        {
            StringBuilder stringBuilder = new StringBuilder();

            stringBuilder.AppendLine($"Singleton 1: {_Singleton1.Id}");
            stringBuilder.AppendLine($"Singleton 2: {_Singleton2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Scoped 1: {_Scoped1.Id}");
            stringBuilder.AppendLine($"Scoped 2: {_Scoped2.Id}");
            stringBuilder.AppendLine();
            stringBuilder.AppendLine($"Transient 1: {_Transient1.Id}");
            stringBuilder.AppendLine($"Transient 2: {_Transient2.Id}");

            return Task.FromResult(stringBuilder.ToString());
        }

    }

    public interface IGeral
    {
        public Guid Id { get; }
    }

    public interface ISingleton : IGeral
    { }

    public interface IScoped : IGeral
    { }

    public interface ITransient : IGeral
    { }

    public class CicloDeVida : ISingleton, IScoped, ITransient
    {
        private readonly Guid _guid;

        public CicloDeVida()
        {
            _guid = Guid.NewGuid();
        }

        public Guid Id => _guid;
    }

}

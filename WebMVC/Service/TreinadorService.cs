using WebMVC.Data;
using WebMVC.Models;

namespace WebMVC.Service
{
    public class TreinadorService
    {
        private readonly PokemonContext _context;
        public TreinadorService(PokemonContext context)
        {
            _context = context;
        }

        internal void AddTreinador(Treinador treinadorModel)
        {
            throw new NotImplementedException();
        }
    }
}

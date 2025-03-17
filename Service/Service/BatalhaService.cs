using ApiModelsResponse.ViewModels;
using DB.Data;
using Microsoft.EntityFrameworkCore;
using ModelsResponse.ViewModels;

namespace Service.Service
{
    public class BatalhaService
    {
        readonly private PokemonContext _context;
        public BatalhaService(PokemonContext context)
        {
            _context = context;
        }

        public List<TreinadorViewModel> GetAllTrainers()
        {
            return _context.Treinador.ToList().Select(e => new TreinadorViewModel
            {
                Id = e.Id,
                ImagePath = e.ImagePath,
                Name = e.Name,
                Location = e.Location
            }).ToList();
        }

        public List<PokemonCampoBatalhaViewModel> GetPokemonsFromTrainerId(int treinadorId)
        {
            var pokemonsStatDetails = _context.PokemonStatsDetails.ToList();
            return _context.PokemonTreinador.Include(e => e.Type).Include(e => e.PokemonApiId).ToList().Where(e => e.TreinadorId == treinadorId).Select(e =>
                new PokemonCampoBatalhaViewModel
                {
                    Id = e.Id,
                    ImageUrl = e.PokemonApiId.Url,
                    Vida = pokemonsStatDetails.First(p => p.Id == e.PokemonApiId.Id).HP,
                    Level = e.Level,
                    Move1 = e.Move1Id,
                    Move2 = e.Move2Id,
                    Move3 = e.Move3Id,
                    Move4 = e.Move4Id,
                    ATTACK = pokemonsStatDetails.First(p => p.Id == e.PokemonApiId.Id).ATTACK,
                    DEFENSE = pokemonsStatDetails.First(p => p.Id == e.PokemonApiId.Id).DEFENSE,
                    SP_ATTACK = pokemonsStatDetails.First(p => p.Id == e.PokemonApiId.Id).SP_ATTACK,
                    SP_DEFENSE = pokemonsStatDetails.First(p => p.Id == e.PokemonApiId.Id).SP_DEFENSE,
                    SPEED = pokemonsStatDetails.First(p => p.Id == e.PokemonApiId.Id).SPEED,
                    HP = pokemonsStatDetails.First(p => p.Id == e.PokemonApiId.Id).HP,
                    Name = e.PokemonName,
                    PokemonType1 = new PokemonTypeViewModel { Id = e.TypeId, Name = e.Type.Name },
                    PokemonType2 = new PokemonTypeViewModel { Id = e.SecondTypeId.Value }
                }
            ).ToList();
        }
    }
}

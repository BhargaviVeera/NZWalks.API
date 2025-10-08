using AutoMapper;
using NZWalks.Models.Domain;
using NZWalks.Models.DTO;

namespace NZWalks.Mappings
{
    public class AutoMapperProfiles : Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<Walk, WalkDTO>().ReverseMap();
            CreateMap<AddWalkDTO, Walk>();
            CreateMap<UpdateWalkDTO, Walk>();

            CreateMap<Region, RegionDTO>().ReverseMap();
            CreateMap<AddRegionDTO, Walk>();
            CreateMap<UpdateRegionDTO, Walk>();

            CreateMap<Difficulty, DifficultyDTO>().ReverseMap();
            CreateMap<AddDifficultyDTO, Difficulty>();
            CreateMap<UpdateDifficultyDTO, Difficulty>();
        }
    }
}

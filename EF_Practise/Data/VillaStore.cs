using EF_Practise.Modals;

namespace EF_Practise.Data
{
    public static class VillaStore
    {
        public static List<VillaDto> villas = new List<VillaDto>{ 
        new VillaDto{Id=1,Name="FSD",Sqft=100,Ocupancy=3},
        new VillaDto{Id=2,Name="LHR",Sqft=101,Ocupancy=4},
        new VillaDto{Id=3,Name="ISL",Sqft=300,Ocupancy=6},
        new VillaDto{Id=4,Name="KHI",Sqft=400,Ocupancy=8},
        };
    }
}

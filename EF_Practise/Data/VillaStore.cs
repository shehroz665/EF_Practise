using EF_Practise.Modals;

namespace EF_Practise.Data
{
    public static class VillaStore
    {
        public static List<Villa> villas = new List<Villa>{ 
        new Villa{Id=1,Name="FSD",Sqft=100,Ocupancy=3},
        new Villa{Id=2,Name="LHR",Sqft=101,Ocupancy=4},
        new Villa{Id=3,Name="ISL",Sqft=300,Ocupancy=6},
        new Villa{Id=4,Name="KHI",Sqft=400,Ocupancy=8},
        };
    }
}

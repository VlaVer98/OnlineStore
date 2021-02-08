using AutoMapper;

namespace Shop.WEB.Core.AutoMapper
{
    public static class AutoMapperConfig
    {
        public static IMapper Initialize()
        {
            var mapperConfig = new MapperConfiguration(mc =>
            {
                mc.AddProfile(new AutoMapperProfile());
            });

            return mapperConfig.CreateMapper();
        }
    }
}

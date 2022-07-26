using AutoMapper;
using System;

namespace MJ_CAIS.Tests.Helpers
{
    public static class InitObjectHelper
    {
        internal static IMapper GetMapper<T>()
        {
            var tInstance = (T)Activator.CreateInstance(typeof(T));
            var profile = tInstance as Profile;
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(profile));
            var mapper = new Mapper(configuration);

            return mapper;
        }
    }
}

namespace MainApi.Helpers
{
    public static class ServiceRegistrationExtensions
    {
        public static IServiceCollection AddAllImplementationsAsTransient<T>(this IServiceCollection services) where T : class
        {
            if (typeof(T).IsInterface == false)
            {
                throw new TypeAccessException("Can not add implementations of non-interface type.");
            }

            var interfaceImplementations = AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(x => x.GetTypes())
                .Where(x => typeof(T).IsAssignableFrom(x) && !x.IsInterface && !x.IsAbstract);

            foreach (var item in interfaceImplementations)
            {
                if (!item.IsAbstract)
                {
                    services.AddTransient(item);
                }
            }

            return services;
        } 
    }
}

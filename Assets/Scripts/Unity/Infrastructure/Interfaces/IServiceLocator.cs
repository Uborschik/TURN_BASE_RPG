namespace GameUnity.Infrastructure.Interfaces
{
    public interface IServiceLocator
    {
        T Get<T>() where T : class;
        void Register<T>(T service) where T : class;
    }
}
using PortableIoC;

namespace MusicLogicExtendedWp7.ViewModels
{
    public class ServiceLocator
    {
        private static IPortableIoC ioc = new PortableIoc();

        public static void Set<T>(T t)
        {
            ioc.Register<T>(x => t);
        }

        public static T Get<T>()
        {
            var bar = ioc.Resolve<T>();
            return bar;
        }
    }
}
using PortableIoC;

namespace DatabaseSearchLogic
{
    public class ServiceLocator2
    {
        private static readonly IPortableIoC ioc = new PortableIoc();

        public static void Set<T>(T t)
        {
            ioc.Register(x => t);
        }

        public static T Get<T>()
        {
            var bar = ioc.Resolve<T>();
            return bar;
        }
    }
}
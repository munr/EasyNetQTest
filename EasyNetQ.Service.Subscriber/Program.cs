using Castle.Windsor;
using Castle.Windsor.Installer;
using Topshelf;

namespace EasyNetQTest.Service.Subscriber
{
    class Program
    {
        static void Main(string[] args)
        {
            // create the container and run any installers in this assembly
            var container = new WindsorContainer().Install(FromAssembly.This());
            
            // start of the TopShelf configuration
            HostFactory.Run(x =>
            {
                x.Service<ISubscriberService>(s =>
                {
                    s.ConstructUsing(name => container.Resolve<ISubscriberService>());
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc =>
                    {
                        tc.Stop();
                        container.Release(tc);
                        container.Dispose();
                    });
                });

                x.RunAsLocalSystem();

                x.SetDescription("EasyNetQ Test Subscriber service.");
                x.SetDisplayName("EasyNetQTestSubscriberService");
                x.SetServiceName("EasyNetQTestSubscriberService");
            });
        }
    }
}

using AppAutoFac;
using Autofac.Core;
using Autofac;
using System.Net.WebSockets;

var container = BuildContainer();

using (var scope = container.BeginLifetimeScope())
{
    var service1 = scope.Resolve<PostalServiceSender>();
    service1.Send("a", "a");

    var service2 = scope.Resolve<EmailNotifier>();
    service2.Send("a", "a");

    var service3 = scope.ResolveKeyed<IWorker>("a");
    service3.Go();

    var service4 = scope.ResolveKeyed<IWorker>("b");
    service4.Go();
}


static IContainer BuildContainer()
{
    var builder = new ContainerBuilder();
    builder.RegisterType<PostalServiceSender>()
                   .As<ISender>()
                   .AsSelf();
    builder.RegisterType<EmailNotifier>()
           .As<ISender>()
           .AsSelf();

    builder.RegisterType<ShippingProcessor>()
           .WithParameter(
             new ResolvedParameter(
               (pi, ctx) => pi.ParameterType == typeof(ISender),
               (pi, ctx) => ctx.Resolve<PostalServiceSender>()));

    builder.RegisterType<CustomerNotifier>()
           .WithParameter(
             new ResolvedParameter(
               (pi, ctx) => pi.ParameterType == typeof(ISender),
               (pi, ctx) => ctx.Resolve<EmailNotifier>()));

    builder.RegisterType<Worker>()
           .As<IWorker>()
           .Keyed<IWorker>("a")
           .WithParameter(
             new ResolvedParameter(
               (pi, ctx) => pi.ParameterType == typeof(ISender),
               (pi, ctx) => ctx.Resolve<PostalServiceSender>()));

    builder.RegisterType<NewWorker>()
           .As<IWorker>()
           .Keyed<IWorker>("b")
           .WithParameter(
             new ResolvedParameter(
               (pi, ctx) => pi.ParameterType == typeof(ISender),
               (pi, ctx) => ctx.Resolve<EmailNotifier>()));

    return builder.Build();
}

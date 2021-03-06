﻿using System.Linq;
using Bender.Reflection;
using Castle.MicroKernel.Registration;
using Castle.MicroKernel.SubSystems.Configuration;
using Castle.Windsor;
using RKE.IOC.Common.Attributes;

namespace RKE.IOC.Manager.Core.WinsdorsInstallers
{
    public class SingletonInstaller : BaseInstaller, IWindsorInstaller
    {
        public void Install(IWindsorContainer container, IConfigurationStore store)
        {
            var allTypes = base.GetTypesFromThisApplication();
            var filteredTypes = allTypes.Where(t => t.HasAttribute<SingletonScopeAttribute>()).ToArray();
            foreach (var type in filteredTypes)
            {
                var interfaces = GetServiceTypes(type);
                foreach (var @interface in interfaces)
                {
                    Register(container, @interface, type, x => x.LifestyleSingleton());
                }
            }
        }
    }
}
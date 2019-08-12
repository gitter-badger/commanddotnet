﻿using System;
using System.Collections.Generic;
using System.Reflection;
using CommandDotNet.Execution;

namespace CommandDotNet.ClassModeling.Definitions
{
    internal class MethodCommandDef : ICommandDef
    {
        private readonly MethodBase _method;

        public string Name { get; }
        public Type CommandHostClassType { get; }
        public ICustomAttributeProvider CustomAttributeProvider => _method;
        public bool IsExecutable => true;
        public IReadOnlyCollection<IArgumentDef> Arguments { get; }
        public IReadOnlyCollection<ICommandDef> SubCommands { get; } = new List<ICommandDef>().AsReadOnly();
        public IMethodDef MiddlewareMethodDef { get; }
        public IMethodDef InvokeMethodDef { get; }
        public Command Command { get; set; }
        
        public MethodCommandDef(MethodInfo method, Type commandHostClassType, IMethodDef middlewareMethodDef, AppConfig appConfig)
        {
            _method = method;

            Name = method.BuildName(appConfig);
            CommandHostClassType = commandHostClassType;
            MiddlewareMethodDef = middlewareMethodDef;
            InvokeMethodDef = new MethodDef(method, appConfig);
            Arguments = InvokeMethodDef.ArgumentDefs;
        }
    }
}
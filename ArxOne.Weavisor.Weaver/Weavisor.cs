﻿#region Weavisor
// Arx One Aspects
// A simple post build weaving package
// https://github.com/ArxOne/Weavisor
// Release under MIT license http://opensource.org/licenses/mit-license.php
#endregion

using System;
using ArxOne.Weavisor.Weaver;
using Mono.Cecil;

/// <summary>
/// This is the entry point, automatically found by Fody
/// </summary>
// ReSharper disable once UnusedMember.Global
// ReSharper disable once CheckNamespace
public class Weavisor
{
    /// <summary>
    /// Gets or sets the module definition (injected by Fody).
    /// </summary>
    /// <value>
    /// The module definition.
    /// </value>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public ModuleDefinition ModuleDefinition { get; set; }

    /// <summary>
    /// Gets or sets the logger at information level (injected by Fody).
    /// </summary>
    /// <value>
    /// The log information.
    /// </value>
    // ReSharper disable once MemberCanBePrivate.Global
    public Action<string> LogInfo { get; set; }
    /// <summary>
    /// Gets or sets the log at warning level (injected by Fody).
    /// </summary>
    /// <value>
    /// The log warning.
    /// </value>
    // ReSharper disable once MemberCanBePrivate.Global
    public Action<string> LogWarning { get; set; }

    /// <summary>
    /// Gets or sets the assembly resolver (injected by Fody).
    /// </summary>
    /// <value>
    /// The assembly resolver.
    /// </value>
    // ReSharper disable once UnusedAutoPropertyAccessor.Global
    // ReSharper disable once MemberCanBePrivate.Global
    public IAssemblyResolver AssemblyResolver { get; set; }

    public Weavisor()
    {
        LogInfo = m => { };
        LogWarning = m => { };
    }

    /// <summary>
    /// Executes this instance.
    /// </summary>
    // ReSharper disable once UnusedMember.Global
    public void Execute()
    {
        // instances are created here
        // please also note poor man's dependency injection (which is enough for us here)
        var logger = new Logger { LogInfo = LogInfo, LogWarning = LogWarning };
        var typeResolver = new TypeResolver { AssemblyResolver = AssemblyResolver, Logger = logger };
        var aspectWeaver = new AspectWeaver { Logger = logger, TypeResolver = typeResolver };
        aspectWeaver.Weave(ModuleDefinition);
    }
}
namespace DotNetCoreAppBuilder
{
    using System;

    /// <summary>
    ///     Enum to indicate type of Visual Studio discovery.
    /// </summary>
    [Flags]
    public enum DiscoveryType
    {
        /// <summary>
        ///     Discovery via the current environment. This indicates the caller originated
        ///     from a Visual Studio Developer Command Prompt.
        /// </summary>
        DeveloperConsole = 1,

        /// <summary>
        ///     Discovery via Visual Studio Setup API.
        /// </summary>
        VisualStudioSetup = 2
    }
}
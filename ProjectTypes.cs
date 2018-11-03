﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Xml.Serialization;

/// <summary>
/// Custom class for mapping enumeration values to premake configuration tag.
/// </summary>
public class FunctionNameAttribute : Attribute
{
    /// <summary>
    /// function name itself.
    /// </summary>
    public String tag;

    /// <summary>
    /// function name attribute
    /// </summary>
    public FunctionNameAttribute(String s)
    {
        tag = s;
    }
}


/// <summary>
/// Specifies whether or not to use precompiled headers
/// </summary>
public enum EPrecompiledHeaderUse
{
    /// <summary>
    /// Create precompiled headers
    /// </summary>
    Create,
    /// <summary>
    /// Use precompiled headers
    /// </summary>
    Use,

    /// <summary>
    /// Default value (not initialized)
    /// </summary>
    NotUsing,

    /// <summary>
    /// Not available in project file, but this is something we indicate that we haven't set value for precompiled headers.
    /// </summary>
    ProjectDefault
}


/// <summary>
/// Exception Handling Model
/// </summary>
public enum EExceptionHandling
{
    /// <summary>
    /// The exception-handling model that catches both asynchronous (structured) and synchronous (C++) exceptions. (/EHa)
    /// </summary>
    Async,

    /// <summary>
    /// Both C++ and C functions can throw exceptions (/EHs)
    /// </summary>
    SyncCThrow,

    /// <summary>
    /// C++ functions can throw exceptions, C functions don't throw exceptions (/EHsc)
    /// </summary>
    Sync,

    /// <summary>
    /// Functions assumed not to throw exceptions (/EH-)
    /// </summary>
    NoExceptionHandling,

    /// <summary>
    /// Functions assumed not to throw exceptions (-fexceptions)
    /// </summary>
    Enabled = SyncCThrow,

    /// <summary>
    /// Functions assumed not to throw exceptions (-fno-exceptions)
    /// </summary>
    Disabled = NoExceptionHandling,

    /// <summary>
    /// -funwind-tables
    /// </summary>
    UnwindTables,

    /// <summary>
    /// Default value, not saved in .vcxproj project file
    /// </summary>
    ProjectDefault

}


/// <summary>
/// Run-Time Error Checks
/// </summary>
public enum EBasicRuntimeChecks
{
    /// <summary>
    /// Stack Frames, /RTCs - Enables stack frame run-time error checking.
    /// </summary>
    StackFrameRuntimeCheck,

    /// <summary>
    /// Uninitialized Variables, /RTCu - Reports when a variable is used without having been initialized.
    /// </summary>
    UninitializedLocalUsageCheck,

    /// <summary>
    /// Both, /RTC1 - Equivalent of StackFrameRuntimeCheck + UninitializedLocalUsageCheck.
    /// </summary>
    EnableFastChecks,

    /// <summary>
    /// No Run-time checks.
    /// </summary>
    Default,

    /// <summary>
    /// Not present in .vcxproj, sets to project default value
    /// </summary>
    ProjectDefault
}



/// <summary>
/// Specifies the level of warning to be generated by the compiler.
/// </summary>
public enum EWarningLevel
{
    /// <summary>
    /// Level 0 disables all warnings.
    /// </summary>
    TurnOffAllWarnings,
    /// <summary>
    /// Level 1 displays severe warnings. Level 1 is the default setting.
    /// </summary>
    Level1,
    /// <summary>
    /// Level 2 displays all level 1 warnings and warnings that are less severe than level 1.
    /// </summary>
    Level2,
    /// <summary>
    /// Level 3 displays all level 2 warnings and all other warnings that are recommended for production purposes.
    /// </summary>
    Level3,
    /// <summary>
    /// Level 4 displays all level 3 warnings and informational warnings. We recommend that you use this option only to provide lint-like warnings. 
    /// However, for a new project, it may be best to use /W4 in all compilations; this will ensure the fewest possible hard-to-find code defects.
    /// </summary>
    Level4,
    /// <summary>
    /// Displays all /W4 warnings and any other warnings that are not included in /W4—for example, warnings that are off by default.
    /// </summary>
    EnableAllWarnings
}

/// <summary>
/// Defines what needs to be done with given item. Not all project types support all enumerations - for example
/// packaging projects / C# projects does not support CustomBuild.
/// </summary>
public enum IncludeType
{
    /// <summary>
    /// Header file (.h)
    /// </summary>
    ClInclude,

    /// <summary>
    /// Any custom file with custom build step
    /// </summary>
    CustomBuild,

    /// <summary>
    /// Source codes (.cpp) files
    /// </summary>
    ClCompile,

    /// <summary>
    /// .def / .bat
    /// </summary>
    None,

    /// <summary>
    /// .txt files.
    /// </summary>
    Text,

    /// <summary>
    /// .rc / resource files.
    /// </summary>
    ResourceCompile,

    /// <summary>
    /// .ico files.
    /// </summary>
    Image,

    // Following enumerations are used in android packaging project (.androidproj)
    Content,
    AntBuildXml,
    AndroidManifest,
    AntProjectPropertiesFile,

    /// <summary>
    /// For Android package project: Reference to another project, which needs to be included into package.
    /// </summary>
    ProjectReference,

    /// <summary>
    /// Intentionally not valid value, so can be replaced with correct one. (Visual studio does not supports one)
    /// </summary>
    Invalid,

    /// <summary>
    /// C# references to .net assemblies
    /// </summary>
    Reference,

    /// <summary>
    /// C# - source codes to compile
    /// </summary>
    Compile,

    /// <summary>
    /// Android / Gradle project, *.template files.
    /// </summary>
    GradleTemplate,

    /// <summary>
    /// .java - source codes to compile
    /// </summary>
    JavaCompile
}

/// <summary>
/// Defines debug information format.
/// </summary>
public enum EDebugInformationFormat
{
    /// <summary>
    /// Applicable for windows projects only. /ZI compiler flag.
    /// </summary>
    EditAndContinue,

    /// <summary>
    /// Applicable for windows and android projects
    /// </summary>
    None,

    /// <summary>
    /// Applicable for windows projects only
    /// </summary>
    OldStyle,

    /// <summary>
    /// Applicable for windows projects only. /Zi compiler flag.
    /// </summary>
    ProgramDatabase,

    /// <summary>
    /// Applicable for android projects only.
    /// </summary>
    LineNumber,

    /// <summary>
    /// Applicable for android projects only.
    /// </summary>
    FullDebug,

    /// <summary>
    /// Just some value, just to indicate that enumeration value is invalid.
    /// </summary>
    Invalid
}

/// <summary>
/// C Language standard
/// </summary>
public enum ECLanguageStandard
{
    ProjectDefault,
    c89,
    c99,
    c11,
    gnu99,
    gnu11
}

/// <summary>
/// C++ Language standard
/// </summary>
public enum ECppLanguageStandard
{ 
    ProjectDefault,
    cpp98,
    cpp11,
    cpp1y,
    gnupp98,
    gnupp11,
    gnupp1y
}

/// <summary>
/// Configuration class which configures project as well as individual file entries.
/// </summary>
[DebuggerDisplay("Configuration( confName:'{confName}' )")]
public class FileConfigurationInfo
{
    /// <summary>
    /// For debugging purposes - specifies configuration name (Debug|Win32) with which given configuration is accosiated with.
    /// </summary>
    public String confName;
    //--------------------------------------------------------------------------------------------
    // Following fields are located under following XML nodes
    // ItemDefinitionGroup\
    //                     ClCompile
    //                     Link
    // ItemGroup\
    //                     ClCompile
    //--------------------------------------------------------------------------------------------

    /// <summary>
    /// Precompile header - use or create.
    /// </summary>
    public EPrecompiledHeaderUse PrecompiledHeader = EPrecompiledHeaderUse.ProjectDefault;

    /// <summary>
    /// When set to true - disabled from build.
    /// </summary>
    public bool ExcludedFromBuild = false;

    /// <summary>
    /// Defines, ';' separated list.
    /// </summary>
    public String PreprocessorDefinitions = "";

    /// <summary>
    /// Additional Include Directories, ';' separated list.
    /// </summary>
    public String AdditionalIncludeDirectories = "";

    /// <summary>
    /// List of warning to disable, ';' separated list.
    /// </summary>
    public String DisableSpecificWarnings = "";

    /// <summary>
    /// Exception Handling Model
    /// </summary>
    public EExceptionHandling ExceptionHandling = EExceptionHandling.ProjectDefault;

    /// <summary>
    /// Gets xml tag for .vcxproj
    /// </summary>
    public String getExceptionHandlingValue(EKeyword keyword)
    {
        bool isCLangGccCompiler = keyword == EKeyword.Android;

        switch (ExceptionHandling)
        {
            default:
            case EExceptionHandling.Enabled:        // == SyncCThrow
                return (isCLangGccCompiler) ? "Enabled" : "SyncCThrow";

            case EExceptionHandling.Async:
                return (isCLangGccCompiler) ? "Enabled": "Async";

            case EExceptionHandling.Disabled:       // == NoExceptionHandling
                return (isCLangGccCompiler) ? "Disabled" : "false";

            case EExceptionHandling.Sync:
                return (isCLangGccCompiler) ? "Enabled" : "Sync";

            case EExceptionHandling.UnwindTables:
                return (isCLangGccCompiler) ? "UnwindTables" : "SyncCThrow";
        }
    }


    /// <summary>
    /// Run-Time Error Checks
    /// </summary>
    public EBasicRuntimeChecks BasicRuntimeChecks = EBasicRuntimeChecks.ProjectDefault;

    /// <summary>
    /// In windows projects only: Set to true if includes needs to be shown. Used for debug purposes, not loaded by script as configuration parameter.
    /// </summary>
    public bool ShowIncludes = false;

    /// <summary>
    /// obj / lib files, ';' separated list.
    /// On windows platform can include also libraries, on android 'LibraryDependencies' specifies library files.
    /// </summary>
    public String AdditionalDependencies = "";

    /// <summary>
    /// Android specific: Additional libraries to link
    /// </summary>
    public String LibraryDependencies = "";

    /// <summary>
    /// Additional directory from where to search obj / lib files, ';' separated list.
    /// </summary>
    public String AdditionalLibraryDirectories = "";

    /// <summary>
    /// Output filename (.obj file)
    /// </summary>
    public String ObjectFileName;
    public String XMLDocumentationFileName;

    /// <summary>
    /// Precompiled header file
    /// </summary>
    public String PrecompiledHeaderFile = "stdafx.h";

    /// <summary>
    /// Android specific.
    /// </summary>
    public ECompileAs CompileAs = ECompileAs.Default;

    /// <summary>
    /// Optimization level. (MaxSpeed is default value for each project configuration, for each file configuration - it's ProjectDefault)
    /// </summary>
    public EOptimization Optimization = EOptimization.MaxSpeed;

    /// <summary>
    /// Gets optimization level, set for specific project type.
    /// </summary>
    /// <param name="p">Project for which to query</param>
    public EOptimization getOptimization( Project p )
    {
        if( p.Keyword == EKeyword.Android && Optimization == EOptimization.MinSpace )
            return EOptimization.MinSize;

        if( p.Keyword != EKeyword.Android && Optimization == EOptimization.MinSize )
            return EOptimization.MinSpace;

        return Optimization;
    }


    /// <summary>
    /// Run-time library
    /// </summary>
    public ERuntimeLibrary RuntimeLibrary = ERuntimeLibrary.NotSet;

    /// <summary>
    /// Allows the compiler to package individual functions in the form of packaged functions (COMDATs).
    /// </summary>
    public bool FunctionLevelLinking = false;

    /// <summary>
    /// Enables minimal rebuild, which determines whether C++ source files that include changed C++ class definitions (stored in header (.h) files) need to be recompiled.
    /// (/Gm option)
    /// </summary>
    public bool? MinimalRebuild = null;

    /// <summary>
    /// Replaces some function calls with intrinsic or otherwise special forms of the function that help your application run faster.
    /// </summary>
    public bool IntrinsicFunctions = false;

    /// <summary>
    /// Some sort of linker optimization flag: COMDAT folding
    /// </summary>
    public bool EnableCOMDATFolding = false;

    /// <summary>
    /// Eliminates functions and data that are never referenced
    /// </summary>
    public bool OptimizeReferences = false;

    /// <summary>
    /// Set to true to enable profiling (/PROFILE linker flag)
    /// </summary>
    public bool Profile = false;

    /// <summary>
    /// Format of debug information.
    /// </summary>
    public EDebugInformationFormat DebugInformationFormat = EDebugInformationFormat.Invalid;

    /// <summary>
    /// Gets visual studio default format for specific configuration.
    /// </summary>
    /// <param name="confName">configuration name (E.g. Debug|Win32) for which to query, null if use this configuration</param>
    /// <returns>Default value</returns>
    public EDebugInformationFormat getDebugInformationFormatDefault( String confName )
    {
        String platform;
        
        if( confName != null )
            platform = confName.Split('|')[1];
        else
            platform = this.confName.Split('|')[1];

        if (platform == "Win32" || platform == "x86")
            return EDebugInformationFormat.EditAndContinue;

        if (platform == "x64")
            return EDebugInformationFormat.ProgramDatabase;

        // Android projects does not have "default" configuration, so they needs to be specified anyway.
        if (platform == "ARM" || platform == "ARM64")
            return EDebugInformationFormat.Invalid;

        if (SolutionProjectBuilder.isDeveloper())
        {
            // Default needs to be checked from Visual studio.
            Debugger.Break();
        }

        return EDebugInformationFormat.ProgramDatabase;
    }

    /// <summary>
    ///  Build with Multiple Processes -
    ///     Windows: "/MP" - can be specified on file level, not sure why
    ///     Android: "UseMultiToolTask" - only on project level
    /// </summary>
    public bool MultiProcessorCompilation = false;

    /// <summary>
    /// Custom build step for includeType.CustomBuild specification. Can be null if not defined.
    /// </summary>
    public CustomBuildRule customBuildRule;

    /// <summary>
    /// Additional compiler options
    /// </summary>
    public String ClCompile_AdditionalOptions = "";

    /// <summary>
    /// Additional linker options
    /// </summary>
    public String Link_AdditionalOptions = "";

    /// <summary>
    /// Android: Enable run-time type information
    /// </summary>
    public bool RuntimeTypeInfo = false;

    /// <summary>
    /// Android: C Language Standard
    /// </summary>
    public ECLanguageStandard CLanguageStandard = ECLanguageStandard.ProjectDefault;

    /// <summary>
    /// Android: C++ Language Standard
    /// </summary>
    public ECppLanguageStandard CppLanguageStandard = ECppLanguageStandard.ProjectDefault;

}


/// <summary>
/// Information about that particular file.
/// </summary>
[DebuggerDisplay("{relativePath} ({includeType})")]
public class FileInfo
{
    /// <summary>
    /// Include type, same as specified in .vcxproj / .androidproj.
    /// </summary>
    public IncludeType includeType;

    /// <summary>
    /// Relative path to file (from project path perspective)
    /// </summary>
    public String relativePath;

    /// <summary>
    /// When includeType == ProjectReference - specifies referenced project guid. Includes guid brackets - '{'/'}'
    /// </summary>
    public String Project;

    /// <summary>
    /// C# - location of .dll assembly
    /// </summary>
    public String HintPath;

    /// <summary>
    /// Per configuration specific file configuration. It's acceptable for this list to have 0 entries if no file specific configuration
    /// is introduced.
    /// </summary>
    public List<FileConfigurationInfo> fileConfig = new List<FileConfigurationInfo>();
}

/// <summary>
/// Custom build tool properties.
/// </summary>
[DebuggerDisplay("Custom Build Tool '{Message}'")]
public class CustomBuildRule
{
    /// <summary>
    /// Visual studio: Command line
    /// </summary>
    [XmlElementAttribute( Order = 1 )]
    public String Command = "";
    /// <summary>
    /// Visual studio: description. Use empty string to supress message printing.
    /// </summary>
    [XmlElementAttribute( Order = 2 )]
    public String Message = "Performing Custom Build Tools";
    /// <summary>
    /// Visual studio: outputs
    /// </summary>
    [XmlElementAttribute( Order = 3 )]
    public String Outputs = "";
    /// <summary>
    /// Visual studio: additional dependencies
    /// </summary>
    [XmlElementAttribute( Order = 4 )]
    public String AdditionalInputs = "";
    /// <summary>
    /// Specify whether the inputs and outputs files with specific extension are passed to linker.
    /// </summary>
    [XmlElementAttribute( Order = 5 )]
    public bool LinkObjects = true;
    /// <summary>
    /// Probably unused field. Added to satisfy code when loading project.
    /// </summary>
    [XmlElementAttribute( Order = 6 )]
    public bool ExcludedFromBuild = false;

    /// <summary>
    /// Gets class instance as one xml string.
    /// </summary>
    public override string ToString()
    {
        XmlSerializer ser = new XmlSerializer(typeof(CustomBuildRule), typeof(CustomBuildRule).GetNestedTypes());
        using (var ms = new MemoryStream())
        {
            ser.Serialize(ms, this);
            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }

    /// <summary>
    /// Decodes class from string
    /// </summary>
    /// <param name="inS">xml string to deserialize</param>
    /// <returns>CustomBuildRule class instance</returns>
    public static CustomBuildRule FromString(String inS)
    {
        XmlSerializer ser = new XmlSerializer(typeof(CustomBuildRule), typeof(CustomBuildRule).GetNestedTypes());

        using (TextReader reader = new StringReader(inS))
        {
            return (CustomBuildRule)ser.Deserialize(reader);
        }
    }
}

/// <summary>
/// Project type
/// </summary>
public enum EConfigurationType
{
    /// <summary>
    /// .exe
    /// </summary>
    [FunctionName("Application")]
    Application = 0,

    /// <summary>
    /// .dll
    /// </summary>
    [FunctionName("SharedLib")]
    DynamicLibrary,

    /// <summary>
    /// .lib or .a
    /// </summary>
    [FunctionName("StaticLib")]
    StaticLibrary,

    /// <summary>
    /// Android gradle project: Library (.aar/.jar)
    /// </summary>
    [FunctionName("Library")]
    Library,

    /// <summary>
    /// Utility project
    /// </summary>
    [FunctionName("Utility")]
    Utility,

    /// <summary>
    /// This value does not physically exists in serialized form in .vcxproj, used only for generation of C# script.
    /// </summary>
    [FunctionName("ConsoleApp")]
    ConsoleApplication
};

/// <summary>
/// Character set - unicode MBCS.
/// </summary>
public enum ECharacterSet
{
    /// <summary>
    /// Character set is not specified
    /// </summary>
    [FunctionName("NotSet")]
    NotSet = 0,

    /// <summary>
    /// Unicode
    /// </summary>
    [FunctionName("Unicode")]
    Unicode = 0,

    /// <summary>
    /// Ansi
    /// </summary>
    [FunctionName("MBCS")]
    MultiByte
}

/// <summary>
/// Enables cross-module optimizations by delaying code generation to link-time; requires that linker option 'Link Time Code Generation' be turned on.
/// </summary>
[Description("")]   // Marker to switch Enum value / Description when parsing
public enum EWholeProgramOptimization
{
    /// <summary>
    /// Visual studio default.
    /// </summary>
    [Description("false")]
    NoWholeProgramOptimization = 0,

    /// <summary>
    /// Yes / /GL compiler option.
    /// </summary>
    [Description("true")]
    UseLinkTimeCodeGeneration,

    [Description("PGInstrument")]
    ProfileGuidedOptimization_Instrument,

    [Description("PGOptimize")]
    ProfileGuidedOptimization_Optimize,

    [Description("PGUpdate")]
    ProfileGuidedOptimization_Update
}


/// <summary>
/// Binary image format / target
/// </summary>
public enum ESubSystem
{
    /// <summary>
    /// Not specified
    /// </summary>
    NotSet,
    /// <summary>
    /// Windows application
    /// </summary>
    Windows,
    /// <summary>
    /// Console application
    /// </summary>
    Console,
    Native,
    EFI_Application,
    EFI_Boot_Service_Driver,
    EFI_ROM,
    EFI_Runtime,
    POSIX
}

/// <summary>
/// How to optimize code ?
/// </summary>
public enum EOptimization
{
    [FunctionName("custom")]
    Custom,

    /// <summary>
    /// No optimizations
    /// </summary>
    [FunctionName("off")]
    Disabled,

    /// <summary>
    /// Minimize Size, in Windows projects
    /// </summary>
    [FunctionName("size")]
    MinSpace,

    /// <summary>
    /// Minimize Size, In Android projects
    /// </summary>
    [FunctionName( "size" )]
    MinSize,

    /// <summary>
    /// Maximize Speed
    /// </summary>
    [FunctionName("speed")]
    MaxSpeed,

    /// <summary>
    /// Full Optimization
    /// </summary>
    [FunctionName("on")]
    Full,

    /// <summary>
    /// Not available in project file, but this is something we indicate that we haven't set value
    /// </summary>
    [FunctionName("default")]
    ProjectDefault
}

/// <summary>
/// Run-time library
/// </summary>
public enum ERuntimeLibrary
{
    /// <summary>
    /// Just artificial value to tell that value was not initialized.
    /// </summary>
    NotSet,

    /// <summary>
    /// Multi-threaded (/MT)
    /// </summary>
    MultiThreaded,

    /// <summary>
    /// Multi-threaded Debug (/MTd)
    /// </summary>
    MultiThreadedDebug,

    /// <summary>
    /// Multi-threaded (/MT)
    /// </summary>
    MultiThreadedDLL,

    /// <summary>
    /// Multi-threaded Debug DLL (/MDd)
    /// </summary>
    MultiThreadedDebugDLL
}


/// <summary>
/// Generate debug information
/// </summary>
[Description("")]   // Marker to switch Enum value / Description when parsing
public enum EGenerateDebugInformation
{
    /// <summary>
    /// No
    /// </summary>
    [Description("false"), FunctionName("off")]
    No = 0,

    /// <summary>
    /// Optimize for debugging
    /// </summary>
    [Description("true"), FunctionName("on")]
    OptimizeForDebugging,

    /// <summary>
    /// Use fast linking
    /// </summary>
    [Description("DebugFastLink"), FunctionName("fastlink")]
    OptimizeForFasterLinking
}

/// <summary>
/// Compile As option.
/// </summary>
public enum ECompileAs
{
    /// <summary>
    /// Compile as 'Default'
    /// </summary>
    [FunctionName("default")]
    Default,

    /// <summary>
    /// Compile as C++ Code (-x c++)
    /// </summary>
    [FunctionName("cpp")]
    CompileAsCpp,

    /// <summary>
    /// Compile as C Code (-x c)
    /// </summary>
    [FunctionName("c")]
    CompileAsC
}

/// <summary>
/// Use of STL library
/// </summary>
[Description("")]   // Marker to switch Enum value / Description when parsing
public enum EUseOfStl
{
    /// <summary>
    /// Minimal C++ runtime library (system)
    /// </summary>
    [Description("system")]
    system,

    /// <summary>
    /// C++ runtime static library (gabi++_static)
    /// </summary>
    [Description("gabi++_static")]
    gabi_cpp_static,

    /// <summary>
    /// C++ runtime shared library (gabi++_shared)
    /// </summary>
    [Description("gabi++_shared")]
    gabi_cpp_shared,

    /// <summary>
    /// STLport runtime static library (stlport_static)
    /// </summary>
    [Description("stlport_static")]
    stlport_static,

    /// <summary>
    /// STLport runtime shared library (stlport_shared)
    /// </summary>
    [Description("stlport_shared")]
    stlport_shared,

    /// <summary>
    /// GNU STL static library (gnustl_static)
    /// </summary>
    [Description("gnustl_static")]
    gnustl_static,

    /// <summary>
    /// GNU STL shared library (gnustl_shared)
    /// </summary>
    [Description("gnustl_shared")]
    gnustl_shared,

    /// <summary>
    /// LLVM libc++ static library (c++_static)
    /// </summary>
    [Description("c++_static")]
    cpp_static,

    /// <summary>
    /// LLVM libc++ shared library (c++_shared)
    /// </summary>
    [Description("c++_shared")]
    cpp_shared
}

/// <summary>
/// Arm architecture only
/// </summary>
public enum EThumbMode
{ 
    /// <summary>
    /// Default for ARM64 architecture
    /// </summary>
    Disabled,

    /// <summary>
    /// Default for ARM architecture
    /// </summary>
    Thumb,

    /// <summary>
    /// ARM
    /// </summary>
    ARM,

    /// <summary>
    /// Not specified
    /// </summary>
    NotSpecified
}

/// <summary>
/// Just a helper class for serializing class instances
/// </summary>
public class XmlSerializer2
{
    /// <summary>
    /// Serializes any custom type to string.
    /// </summary>
    /// <returns>Serialized xml string</returns>
    static public string ToString(object o)
    {
        Type type = o.GetType();
        XmlSerializer ser = new XmlSerializer(type, type.GetNestedTypes());
        using (var ms = new MemoryStream())
        {
            ser.Serialize(ms, o);
            return Encoding.UTF8.GetString(ms.ToArray());
        }
    }

    /// <summary>
    /// Deserializes class instance from string s.
    /// </summary>
    static public T FromString<T>( String s )
    {
        XmlSerializer ser = new XmlSerializer(typeof(T), typeof(T).GetNestedTypes());

        using (TextReader reader = new StringReader(s))
        {
            return (T)ser.Deserialize(reader);
        }
    }
}


/// <summary>
/// Pre-link / pre-build / post-build item events.
/// </summary>
public class BuildEvent
{
    /// <summary>
    /// Command to be executed
    /// </summary>
    public String Command = "";

    /// <summary>
    /// We don't care about Message currently since it's not used in pre/postbuildcommands
    /// </summary>
    [XmlIgnore]
    /// <summary>
    /// Command description (is not used)
    /// </summary>
    public String Message = "";

    /// <summary>
    /// class instance to string.
    /// </summary>
    public override string ToString()
    {
        return XmlSerializer2.ToString(this);
    }
}

/// <summary>
/// All values set by default are Visual Studio default.
/// 
/// Configuration class includes everything what cannot be configured on individual file level.
/// If configuration option can be configured on file level, it should be declared in FileConfigurationInfo.
/// </summary>
public class Configuration : FileConfigurationInfo
{
    /// <summary>
    /// Project type
    /// </summary>
    public EConfigurationType ConfigurationType = EConfigurationType.Application;

    /// <summary>
    /// Called when ConfigurationType has changed.
    /// </summary>
    public void ConfigurationTypeUpdated()
    {
        // ConfigurationType changes, maybe makes sense to check TargetExt later on ?
    } //ConfigurationTypeUpdated

    /// <summary>
    /// Mysterious flag, which cannot be set from Visual studio properties, but it affects to some parameter's default values.
    /// </summary>
    public bool UseDebugLibraries = false;

    /// <summary>
    /// Android api level, for example "android-22". If null - uses Visual studio default.
    /// 
    /// </summary>
    public String AndroidAPILevel;

    /// <summary>
    /// Use of STL
    /// </summary>
    public EUseOfStl UseOfStl = EUseOfStl.gnustl_static;

    /// <summary>
    /// ARM or Thumb execution mode
    /// </summary>
    public EThumbMode ThumbMode = EThumbMode.NotSpecified;

    /// <summary>
    /// Gets default for ThumbMode
    /// </summary>
    /// <param name="confName">Configuration name</param>
    /// <returns>Visual studio default for Thumb mode</returns>
    static public EThumbMode getThumbModeDefault( String confName )
    {
        if( confName.Contains( "64" ) )
            return EThumbMode.NotSpecified;

        return EThumbMode.Thumb;
    }

    /// <summary>
    /// Get list of supported UseOfSTL values
    /// </summary>
    public static List<String> UseOfStl_getSupportedValues()
    {
        List<String> values = typeof(EUseOfStl).GetEnumNames().Select(x => typeof(EUseOfStl).GetMember(x)[0].GetCustomAttribute<DescriptionAttribute>().Description).ToList();
        return values;
    }

    /// <summary>
    /// Visual studio default depends on cpu architecture - android-19 is default for ARM, android-21 for ARM64.
    /// </summary>
    /// <param name="confName"></param>
    /// <returns>android api level default</returns>
    static public String getAndroidAPILevelDefault(String confName)
    {
        if (confName.Contains("64"))
            return "android-21";

        return "android-19";
    }

    /// <summary>
    /// For example:
    ///     null - default
    ///     'Clang_3_8'     - Clang 3.8
    ///     'v141'          - for Visual Studio 2017.
    ///     'v140'          - for Visual Studio 2015.
    ///     'v120'          - for Visual Studio 2013.
    /// </summary>
    public String PlatformToolset;

    /// <summary>
    /// Queries default value for PlatformToolset.
    /// </summary>
    /// <param name="p">Project against which to query</param>
    /// <returns>Default value</returns>
    public String getPlatformToolsetDefault(Project p)
    {
        switch (p.Keyword)
        {
            case EKeyword.Android:
                return "Clang_3_8";
            default:
            case EKeyword.Win32Proj:
                return "v140";
        }
    }

    /// <summary>
    /// Specifies project character set
    /// </summary>
    public ECharacterSet CharacterSet = ECharacterSet.Unicode;

    /// <summary>
    /// Defines how MFC is linked in
    /// </summary>
    public EUseOfMfc UseOfMfc = EUseOfMfc.None;


    public bool LinkIncremental = true;

    /// <summary>
    /// Enables cross-module optimizations by delaying code generation to link-time; requires that linker option 'Link Time Code Generation' be turned on.
    /// </summary>
    public EWholeProgramOptimization WholeProgramOptimization;

    /// <summary>
    /// Output Directory. 
    ///     Visual studio default:  can be queried using getOutDirDefault()
    ///     premake default:        bin\$(Platform)\$(Configuration)\
    /// </summary>
    public String OutDir;

    /// <summary>
    /// Gets default value for OutDir field.
    /// </summary>
    /// <param name="p">Project against which to query</param>
    /// <returns>Default value</returns>
    public String getOutDirDefault(Project p)
    {
        switch (p.Keyword)
        {
            case EKeyword.Android:
                return @"$(SolutionDir)$(Platform)\$(Configuration)\";
            default:
            case EKeyword.Win32Proj:
                return "$(SolutionDir)$(Configuration)\\";
        }
    }

    /// <summary>
    /// Intermediate Directory.
    ///     Visual studio default:  $(Configuration)\
    ///     premake default:        obj\$(Platform)\$(Configuration)\
    /// </summary>
    public String IntDir;

    /// <summary>
    /// Gets intermediate directory default.
    /// </summary>
    /// <param name="p">project</param>
    /// <returns>Default value of IntDir</returns>
    public String getIntDirDefault(Project p)
    {
        switch (p.Keyword)
        {
            case EKeyword.Android:
                return @"$(Platform)\$(Configuration)\";
            default:
            case EKeyword.Win32Proj:
                return @"$(Configuration)\";
        }
    }

    /// <summary>
    /// Target Name.
    /// Visual studio default: $(ProjectName)
    /// </summary>
    public String TargetName;

    /// <summary>
    /// Gets default value for TargetName
    /// </summary>
    /// <param name="p">Project against which to query</param>
    /// <returns>Default value</returns>
    public String getTargetNameDefault(Project p)
    {
        switch (p.Keyword)
        {
            case EKeyword.Android:
                return "lib$(RootNamespace)";
            default:
            case EKeyword.Win32Proj:
                return "$(ProjectName)";
        }
    }

    /// <summary>
    /// Target Extension (.exe, .dll, ...).
    /// If set to default - must be null.
    /// </summary>
    public String TargetExt;

    /// <summary>
    /// Gets default value for target ext.
    /// </summary>
    /// <param name="p">Project against which to query</param>
    /// <returns>Default value</returns>
    public String getTargetExtDefault(Project p)
    {
        switch (p.Keyword)
        {
            case EKeyword.Android:
                return ".so";
            default:
            case EKeyword.Win32Proj:
                return ".dll";
        }
    }

    //-------------------------------------------------------------
    // VC++ Directories tab - starts
    //-------------------------------------------------------------
    /// <summary>
    /// Include Directories - Semicolon (';') separated list of pathes
    /// </summary>
    public String IncludePath = "";

    /// <summary>
    /// Executable Directories - Semicolon (';') separated list of pathes
    /// </summary>
    public String ExecutablePath = "";

    /// <summary>
    /// Reference Directories - Semicolon (';') separated list of pathes
    /// </summary>
    public String ReferencePath = "";

    /// <summary>
    /// Library Directories - Semicolon (';') separated list of pathes
    /// </summary>
    public String LibraryPath = "";

    /// <summary>
    /// Source Directories - Semicolon (';') separated list of pathes
    /// </summary>
    public String SourcePath = "";

    /// <summary>
    /// Exclude Directories - Semicolon (';') separated list of pathes
    /// </summary>
    public String ExcludePath = "";

    //-------------------------------------------------------------
    // VC++ Directories tab ends
    //-------------------------------------------------------------

    public EWarningLevel WarningLevel = EWarningLevel.Level1;

    /// <summary>
    /// Typically Windows or Console.
    /// </summary>
    public ESubSystem SubSystem;

    /// <summary>
    /// Visual studio defaults: OptimizeForDebugging for release, OptimizeForFasterLinking for debug.
    /// </summary>
    public EGenerateDebugInformation GenerateDebugInformation;

    /// <summary>
    /// .def file location
    /// </summary>
    public String ModuleDefinitionFile = "";

    /// <summary>
    /// Android specific.
    /// </summary>
    public String AndroidAppLibName;

    //-------------------------------------------------------------
    // Pre-build / pre-link / post build events
    //-------------------------------------------------------------
    /// <summary>
    /// When disabled - pre build event is not used.
    /// </summary>
    public bool PreBuildEventUseInBuild = true;

    /// <summary>
    /// Pre-build event
    /// </summary>
    public BuildEvent PreBuildEvent = new BuildEvent();

    /// <summary>
    /// When disabled - post build event is not used.
    /// </summary>
    public bool PostBuildEventUseInBuild = true;

    /// <summary>
    /// Post-build event
    /// </summary>
    public BuildEvent PostBuildEvent = new BuildEvent();

    /// <summary>
    /// When disabled - pre link build event is not used.
    /// </summary>
    public bool PreLinkEventUseInBuild = true;

    /// <summary>
    /// Prelink-build event
    /// </summary>
    public BuildEvent PreLinkEvent = new BuildEvent();

    /// <summary>
    /// gradle package .apk path
    /// </summary>
    public String ApkFileName;

    /// <summary>
    /// gradle package options.
    /// </summary>
    public String AdditionalOptions;
}

/// <summary>
/// Tags platform
/// </summary>
public enum EKeyword
{
    /// <summary>
    /// For sub-folders for example (Also default value)
    /// </summary>
    None = 0,

    /// <summary>
    /// Windows project (32 or 64 bit)
    /// </summary>
    Win32Proj,

    /// <summary>
    /// Android project
    /// </summary>
    Android,

    /// <summary>
    /// Windows application with MFC support
    /// </summary>
    MFCProj,

    /// <summary>
    /// Android packaging project (does not exists on file format level)
    /// </summary>
    AntPackage,

    /// <summary>
    /// Typically set for Android packaging project. (does not exists on file format level)
    /// </summary>
    GradlePackage
}

/// <summary>
/// Defines how MFC is linked in
/// </summary>
public enum EUseOfMfc
{
    /// <summary>
    /// Use Standard Windows Libraries (No MFC)
    /// </summary>
    None,

    /// <summary>
    /// Use MFC in a Static Library
    /// </summary>
    Static,

    /// <summary>
    /// Use MFC in a Shared DLL
    /// </summary>
    Dynamic,

    /// <summary>
    /// Use Standard Windows Libraries (No MFC), can be serialized for Utility project type.
    /// </summary>
    _false
}

public class GradlePackage
{
    public String ProjectDirectory;

    public bool IsProjectDirectoryDefault()
    {
        if( ProjectDirectory == null || ProjectDirectory == "$(ProjectDir)app\\" )
            return true;

        return false;
    }

    /// <summary>
    /// Gradle version
    /// </summary>
    public String GradleVersion;

    /// <summary>
    /// Gradle batch to execute, normally gradlew.bat.
    /// </summary>
    public String ToolName;
    public String GradlePlugin;
    public String AndroidAppLibName;
    public String ApplicationName;
}


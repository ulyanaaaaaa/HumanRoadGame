using UnityEditor;
using UnityEngine;

public static class CompileFactory 
{
    [MenuItem("AutoBuild/CompileForWindow")]
    private static void CompileForWindow()
    {
        ICompileFactory compileFactory = new SimpleWinCompileFactory();
        compileFactory.Compile();
    }
    
    [MenuItem("AutoBuild/CompileForAndroid")]
    private static void CompileForAndroid()
    {
        ICompileFactory compileFactory = new SimpleWinCompileFactory();
        compileFactory.Compile();
    }

    [MenuItem("AutoBuild/CompileAab")]
    private static void CompileAab()
    {
        EditorUserBuildSettings.buildAppBundle = true;
        ICompileFactory compileFactory = new SimpleAabCompileFactory();
        compileFactory.Compile();
        EditorUserBuildSettings.buildAppBundle = false;
    }
    
    [MenuItem("AutoBuild/CompileAll")]
    private static void Compile()
    {
        CompileForAndroid();
        CompileForWindow();
        CompileAab();
    }
}

public interface ICompileFactory
{
    public void Compile();
}

public class SimpleWinCompileFactory : ICompileFactory
{
    public void Compile()
    {
        BuildPipeline.BuildPlayer(Scenes(),
            Application.persistentDataPath + "/GameWin.exe",
            BuildTarget.StandaloneWindows,
            BuildOptions.Development);
    }

    private EditorBuildSettingsScene[] Scenes()
    {
        return EditorBuildSettings.scenes;
    }
}

public class SimpleAndroidCompileFactory : ICompileFactory
{
    public void Compile()
    {
        BuildPipeline.BuildPlayer(Scenes(),
            Application.persistentDataPath + "/GameAndroid.apk",
            BuildTarget.StandaloneWindows,
            BuildOptions.None);
    }

    private EditorBuildSettingsScene[] Scenes()
    {
        return EditorBuildSettings.scenes;
    }
}

public class SimpleAabCompileFactory : ICompileFactory
{
    public void Compile()
    {
        BuildPipeline.BuildPlayer(Scenes(),
            Application.persistentDataPath + "/GameAab.aab",
            BuildTarget.StandaloneWindows,
            BuildOptions.None);
    }

    private EditorBuildSettingsScene[] Scenes()
    {
        return EditorBuildSettings.scenes;
    }
}

namespace DailyLogger;

using UnityEngine;
using System.Collections.Generic;
public class BaseLogger
{
    public string Name => GetType().Name;
    private string GroupTag => NameTag ? $"[{Name}] " : "";

    public bool NameTag { get; set; } = true;
    public bool IsEnable { get; set; } = true;
    public bool Colorful { get; set; } = true;
    private string ColorTag => Colorful ? $"<color=#{GetColor(Name)}>" : "";

    public virtual void Log(string message)
    {
        if (!IsEnable) return;

        if (Colorful)
            Debug.Log($"{ColorTag}{GroupTag}{message}</color>");
        else
            Debug.Log($"{GroupTag}{message}");
    }

    public virtual void LogWarning(string message)
    {
        if (!IsEnable) return;
        if (Colorful)
            Debug.LogWarning($"{ColorTag}{GroupTag}{message}</color>");
        else
            Debug.LogWarning($"{GroupTag}{message}");
    }

    public virtual void LogError(string message)
    {
        if (!IsEnable) return;
        if (Colorful)
            Debug.LogError($"{ColorTag}{GroupTag}{message}</color>");
        else
            Debug.LogError($"{GroupTag}{message}");
    }

    public virtual void Log(string message, Object context)
    {
        if (!IsEnable) return;
        if (Colorful)
            Debug.Log($"{ColorTag}{GroupTag}{message}</color>", context);
        else
            Debug.Log($"{GroupTag}{message}", context);
    }

    public virtual void LogWarning(string message, Object context)
    {
        if (!IsEnable) return;
        if (Colorful)
            Debug.LogWarning($"{ColorTag}{GroupTag}{message}</color>", context);
        else
            Debug.LogWarning($"{GroupTag}{message}", context);
    }

    public virtual void LogError(string message, Object context)
    {
        if (!IsEnable) return;
        if (Colorful)
            Debug.LogError($"{ColorTag}{GroupTag}{message}</color>", context);
        else
            Debug.LogError($"{GroupTag}{message}", context);
    }

    private static string GetColor(string txt)
    {
        var hash = txt.GetHashCode();
        var r = 128 + (hash & 0xFF) % 73f;
        var g = 128 + ((hash >> 8) & 0xFF) % 73f;
        var b = 128 + ((hash >> 16) & 0xFF) % 73f;
        var color = new Color(r / 255f, g / 255f, b / 255f);
        return ColorUtility.ToHtmlStringRGB(color);
    }
}
public class DevLogger : BaseLogger
{
}
public class SysLogger : BaseLogger
{
}
public class FrameWorkLogger : BaseLogger
{
}
public class GameLogger : BaseLogger
{
}
public class EditorLogger : BaseLogger
{
    public override void Log(string message)
    {
        if (Application.isEditor)
            base.Log(message);
    }
    public override void LogWarning(string message)
    {
        if (Application.isEditor)
            base.LogWarning(message);
    }
    public override void LogError(string message)
    {
        if (Application.isEditor)
            base.LogError(message);
    }

    public override void Log(string message, Object context)
    {
        if (Application.isEditor)
            base.Log(message, context);
    }

    public override void LogWarning(string message, Object context)
    {
        if (Application.isEditor)
            base.LogWarning(message, context);

    }

    public override void LogError(string message, Object context)
    {
        if (Application.isEditor)
            base.LogError(message, context);
    }


}
public static class DLogger
{
    private static BaseLogger _defaultLogger = new BaseLogger();
    private static Dictionary<System.Type, BaseLogger> _loggers = new();

    static DLogger()
    {
        _defaultLogger.Colorful = false;
        _defaultLogger.NameTag = false;

        var loggers = new BaseLogger[]
        {
            new DevLogger(),
            new SysLogger(),
            new FrameWorkLogger(),
            new GameLogger(),
            new EditorLogger()
        };

        foreach (var logger in loggers)
        {
            _loggers.TryAdd(logger.GetType(), logger);
        }
    }
    public static void EnableAll(bool enable)
    {
        _defaultLogger.IsEnable = enable;
        foreach (var logger in _loggers.Values)
        {
            logger.IsEnable = enable;
        }
    }
    public static void SetEnable<T>(bool enable) where T : BaseLogger => Get<T>().IsEnable = enable;
    public static void SetColorful<T>(bool colorful) where T : BaseLogger => Get<T>().Colorful = colorful;
    public static BaseLogger Get<T>() where T : BaseLogger => _loggers.GetValueOrDefault(typeof(T), _defaultLogger);

    public static void Register<T>(BaseLogger logger) => _loggers.TryAdd(typeof(T), logger);

    public static void Log(string message) => _defaultLogger.Log(message);
    public static void LogWarning(string message) => _defaultLogger.LogWarning(message);
    public static void LogError(string message) => _defaultLogger.LogError(message);

    public static void Log(string message, Object context) => _defaultLogger.Log(message, context);
    public static void LogWarning(string message, Object context) => _defaultLogger.LogWarning(message, context);
    public static void LogError(string message, Object context) => _defaultLogger.LogError(message, context);

    public static void Log<T>(string message) where T : BaseLogger => Get<T>().Log(message);
    public static void LogWarning<T>(string message) where T : BaseLogger => Get<T>().LogWarning(message);
    public static void LogError<T>(string message) where T : BaseLogger => Get<T>().LogError(message);

    public static void Log<T>(string message, Object context) where T : BaseLogger => Get<T>().Log(message, context);
    public static void LogWarning<T>(string message, Object context) where T : BaseLogger => Get<T>().LogWarning(message, context);
    public static void LogError<T>(string message, Object context) where T : BaseLogger => Get<T>().LogError(message, context);


    public static void DevLog(string message) => Log<DevLogger>(message);
    public static void DevLogWarning(string message) => LogWarning<DevLogger>(message);
    public static void DevLogError(string message) => LogError<DevLogger>(message);

    public static void DevLog(string message, Object context) => Log<DevLogger>(message, context);
    public static void DevLogWarning(string message, Object context) => LogWarning<DevLogger>(message, context);
    public static void DevLogError(string message, Object context) => LogError<DevLogger>(message, context);

    public static void EditorLog(string message) => Log<EditorLogger>(message);
    public static void EditorLogWarning(string message) => LogWarning<EditorLogger>(message);
    public static void EditorLogError(string message) => LogError<EditorLogger>(message);

    public static void EditorLog(string message, Object context) => Log<EditorLogger>(message, context);
    public static void EditorLogWarning(string message, Object context) => LogWarning<EditorLogger>(message, context);
    public static void EditorLogError(string message, Object context) => LogError<EditorLogger>(message, context);

    public static void SysLog(string message) => Log<SysLogger>(message);
    public static void SysLogWarning(string message) => LogWarning<SysLogger>(message);
    public static void SysLogError(string message) => LogError<SysLogger>(message);

    public static void SysLog(string message, Object context) => Log<SysLogger>(message, context);
    public static void SysLogWarning(string message, Object context) => LogWarning<SysLogger>(message, context);
    public static void SysLogError(string message, Object context) => LogError<SysLogger>(message, context);

    public static void FwLog(string message) => Log<FrameWorkLogger>(message);
    public static void FwLogWarning(string message) => LogWarning<FrameWorkLogger>(message);
    public static void FwLogError(string message) => LogError<FrameWorkLogger>(message);

    public static void FwLog(string message, Object context) => Log<FrameWorkLogger>(message, context);
    public static void FwLogWarning(string message, Object context) => LogWarning<FrameWorkLogger>(message, context);
    public static void FwLogError(string message, Object context) => LogError<FrameWorkLogger>(message, context);

    public static void GameLog(string message) => Log<GameLogger>(message);
    public static void GameLogWarning(string message) => LogWarning<GameLogger>(message);
    public static void GameLogError(string message) => LogError<GameLogger>(message);

    public static void GameLog(string message, Object context) => Log<GameLogger>(message, context);
    public static void GameLogWarning(string message, Object context) => LogWarning<GameLogger>(message, context);
    public static void GameLogError(string message, Object context) => LogError<GameLogger>(message, context);

}
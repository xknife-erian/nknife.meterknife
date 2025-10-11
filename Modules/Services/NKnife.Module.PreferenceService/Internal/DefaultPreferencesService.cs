using NLog;
using RAY.Common;
using RAY.Common.Services;
using RAY.Library;
using RAY.Library.Concepts.XML;
using System.Collections.Immutable;
using NKnife.Circe.Base.Modules.Services;

namespace NKnife.Module.PreferenceService.Internal
{
    /// <summary>
    ///     用户选项管理器
    /// </summary>
    internal sealed class DefaultPreferencesService(IAppWorkspaceService __appWorkspace)
        : QuickXml(Path.Combine(__appWorkspace.PreferencesPath, OPTION_FILE)), IPreferencesService
    {
        private const string OPTION_FILE = "Preferences.conf";
        private static readonly ILogger s_logger = LogManager.GetCurrentClassLogger();

        /// <inheritdoc />
        public IImmutableList<string> GetPreferenceKeys()
        {
            return GetKeys();
        }

        /// <inheritdoc />
        public object? GetPreference(string key, Type valueType)
        {
            try
            {
                return GetValueByType(key, valueType);
            }
            catch (Exception e)
            {
                s_logger.Error(e, $"获取选项值失败。Key:{key}; Type:{valueType.Name}\r\n{e.Message}");

                return null;
            }
        }

        /// <inheritdoc />
        public T GetPreference<T>(string key, T? defaultValue)
        {
            return GetValue(key, defaultValue);
        }

        /// <inheritdoc />
        public bool TryGetPreference<T>(string key, out T? value)
        {
            return TryGetValue(key, out value);
        }

        /// <inheritdoc />
        public void SetPreference(string key, object value)
        {
            SetValue(key, value);
        }

        /// <inheritdoc />
        public void ForcedUpdatePreference(string key, object value, Func<bool> cond)
        {
            var isNeed = cond.Invoke();
            if(isNeed)
                SetPreference(key, value);
        }

        /// <inheritdoc />
        public event EventHandler<DataChangedEventArgs<(string PreferenceKey, object PreferenceValue)>>? PreferenceUpdated;

        #region Implementation of IDisposable
        /// <summary>Performs application-defined tasks associated with freeing, releasing, or resetting unmanaged resources.</summary>
        public void Dispose()
        {
        }
        #endregion

        #region Implementation of IService
        /// <inheritdoc />
        public Guid Id { get; } = Guid.NewGuid();

        /// <inheritdoc />
        public string? Description => "用户首选项服务";

        /// <inheritdoc />
        public bool Initialize(params object[] args)
        {
            return true;
        }
        #endregion
    }
}
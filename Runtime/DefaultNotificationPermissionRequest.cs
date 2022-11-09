#if UNITY_EDITOR || (!UNITY_IOS && !UNITY_ANDROID)

using System;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;

namespace Kogane.Internal
{
    /// <summary>
    /// iOS / Android プラットフォーム以外の通知許可ダイアログの結果を保持する構造体
    /// </summary>
    [Serializable]
    internal readonly struct DefaultNotificationPermissionRequestResult : INotificationPermissionRequestResult
    {
    }

    /// <summary>
    /// iOS / Android プラットフォーム以外で通知許可ダイアログを表示するクラス
    /// </summary>
    [UsedImplicitly]
    internal sealed class DefaultNotificationPermissionRequest : INotificationPermissionRequest
    {
        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// 通知許可ダイアログを表示します
        /// </summary>
        UniTask<INotificationPermissionRequestResult> INotificationPermissionRequest.RequestAsync()
        {
            return UniTask.FromResult( ( INotificationPermissionRequestResult )new DefaultNotificationPermissionRequestResult() );
        }
    }
}

#endif
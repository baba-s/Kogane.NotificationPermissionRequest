using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Kogane.Internal
{
    /// <summary>
    /// iOS / Android プラットフォーム以外の通知許可ダイアログの結果を保持する構造体
    /// </summary>
    [Serializable]
    internal readonly struct DefaultNotificationPermissionRequestResult : INotificationPermissionRequestResult
    {
        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// JSON 形式の文字列に変換して返します
        /// </summary>
        public override string ToString()
        {
            return JsonUtility.ToJson( this, true );
        }
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
        UniTask<INotificationPermissionRequestResult> INotificationPermissionRequest.RequestAsync( CancellationToken cancellationToken )
        {
            return UniTask.FromResult( ( INotificationPermissionRequestResult )new DefaultNotificationPermissionRequestResult() );
        }
    }
}
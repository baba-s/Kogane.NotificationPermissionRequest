#if UNITY_EDITOR || UNITY_ANDROID
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Unity.Notifications.Android;
using UnityEngine;

namespace Kogane.Internal
{
    /// <summary>
    /// Android の通知許可ダイアログの結果を保持する構造体
    /// </summary>
    [Serializable]
    [SuppressMessage( "ReSharper", "NotAccessedField.Local" )]
    internal struct AndroidNotificationPermissionRequestResult : INotificationPermissionRequestResult
    {
        //================================================================================
        // 変数(SerializeField)
        //================================================================================
        [SerializeField] private string m_status;

        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal AndroidNotificationPermissionRequestResult( string status )
        {
            m_status = status;
        }
    }

    /// <summary>
    /// Android で通知許可ダイアログを表示するクラス
    /// </summary>
    [UsedImplicitly]
    internal sealed class AndroidNotificationPermissionRequest : INotificationPermissionRequest
    {
        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// 通知許可ダイアログを表示します
        /// </summary>
        async UniTask<INotificationPermissionRequestResult> INotificationPermissionRequest.RequestAsync( CancellationToken cancellationToken )
        {
            // Android 13 以降なら通知許可ダイアログが表示されます
            // そうでない場合は通知許可ダイアログは表示されず
            // request.Status が Allowed で返ってきます
            var request = new PermissionRequest();

            while ( request.Status == PermissionStatus.RequestPending )
            {
                await UniTask.NextFrame( cancellationToken );
            }

            return new AndroidNotificationPermissionRequestResult( request.Status.ToString() );
        }
    }
}

#endif
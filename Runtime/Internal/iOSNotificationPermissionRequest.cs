#if UNITY_EDITOR || UNITY_IOS
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Unity.Notifications.iOS;
using UnityEngine;

namespace Kogane.Internal
{
    /// <summary>
    /// iOS の通知許可ダイアログの結果を保持する構造体
    /// </summary>
    [Serializable]
    [SuppressMessage( "ReSharper", "NotAccessedField.Local" )]
    internal struct iOSNotificationPermissionRequestResult : INotificationPermissionRequestResult
    {
        //================================================================================
        // 変数(SerializeField)
        //================================================================================
        [SerializeField] private bool   m_isFinished;
        [SerializeField] private bool   m_granted;
        [SerializeField] private string m_error;
        [SerializeField] private string m_deviceToken;

        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal iOSNotificationPermissionRequestResult
        (
            bool   isFinished,
            bool   granted,
            string error,
            string deviceToken
        )
        {
            m_isFinished  = isFinished;
            m_granted     = granted;
            m_error       = error;
            m_deviceToken = deviceToken;
        }
    }

    /// <summary>
    /// iOS で通知許可ダイアログを表示するクラス
    /// </summary>
    [UsedImplicitly]
    internal sealed class iOSNotificationPermissionRequest : INotificationPermissionRequest
    {
        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// 通知許可ダイアログを表示します
        /// </summary>
        async UniTask<INotificationPermissionRequestResult> INotificationPermissionRequest.RequestAsync( CancellationToken cancellationToken )
        {
            var option = AuthorizationOption.Alert | AuthorizationOption.Badge;

            using var request = new AuthorizationRequest( option, true );

            while ( !request.IsFinished )
            {
                await UniTask.NextFrame( cancellationToken );
            }

            return new iOSNotificationPermissionRequestResult
            (
                isFinished: request.IsFinished,
                granted: request.Granted,
                error: request.Error,
                deviceToken: request.DeviceToken
            );
        }
    }
}

#endif
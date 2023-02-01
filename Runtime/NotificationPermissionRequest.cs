using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Kogane.Internal;
using UnityEngine;

namespace Kogane
{
    /// <summary>
    /// 通知許可ダイアログを表示するクラス
    /// </summary>
    public static class NotificationPermissionRequest
    {
        //================================================================================
        // 変数(static readonly)
        //================================================================================
        private static readonly INotificationPermissionRequest m_request =
#if !UNITY_EDITOR && UNITY_ANDROID
            new AndroidNotificationPermissionRequest();
#else
            new DefaultNotificationPermissionRequest();
#endif

        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// 通知許可ダイアログを表示します
        /// </summary>
        public static async UniTask<INotificationPermissionRequestResult> RequestAsync( GameObject gameObject )
        {
            if ( gameObject == null ) throw new OperationCanceledException();

            return await m_request.RequestAsync( gameObject.GetCancellationTokenOnDestroy() );
        }

        /// <summary>
        /// 通知許可ダイアログを表示します
        /// </summary>
        public static async UniTask<INotificationPermissionRequestResult> RequestAsync<T>( T component ) where T : Component
        {
            if ( component == null ) throw new OperationCanceledException();

            return await m_request.RequestAsync( component.GetCancellationTokenOnDestroy() );
        }

        /// <summary>
        /// 通知許可ダイアログを表示します
        /// </summary>
        public static async UniTask<INotificationPermissionRequestResult> RequestAsync( CancellationToken cancellationToken )
        {
            return await m_request.RequestAsync( cancellationToken );
        }
    }
}
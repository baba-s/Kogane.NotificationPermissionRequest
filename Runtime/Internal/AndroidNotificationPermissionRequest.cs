#if UNITY_EDITOR || UNITY_ANDROID
using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.Android;

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
        [SerializeField] private string m_eventName;

        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// コンストラクタ
        /// </summary>
        internal AndroidNotificationPermissionRequestResult( string eventName )
        {
            m_eventName = eventName;
        }

        /// <summary>
        /// JSON 形式の文字列に変換して返します
        /// </summary>
        public override string ToString()
        {
            return JsonUtility.ToJson( this, true );
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
            if ( !AndroidApiLevel.IsAndroidVersion13OrHigher ) return new AndroidNotificationPermissionRequestResult( "None" );

            var tcs       = new UniTaskCompletionSource<string>();
            var callbacks = new PermissionCallbacks();
            callbacks.PermissionGranted               += _ => tcs.TrySetResult( "PermissionGranted" );
            callbacks.PermissionDenied                += _ => tcs.TrySetResult( "PermissionDenied" );
            callbacks.PermissionDeniedAndDontAskAgain += _ => tcs.TrySetResult( "PermissionDeniedAndDontAskAgain" );
            Permission.RequestUserPermission( "android.permission.POST_NOTIFICATIONS", callbacks );

            var eventName = await tcs.Task;

            return new AndroidNotificationPermissionRequestResult( eventName );
        }
    }
}

#endif
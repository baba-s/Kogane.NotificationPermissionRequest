using Cysharp.Threading.Tasks;
using Kogane.Internal;

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
#if !UNITY_EDITOR && UNITY_IOS
            new iOSNotificationPermissionRequest();
#elif !UNITY_EDITOR && UNITY_ANDROID
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
        public static async UniTask<INotificationPermissionRequestResult> RequestAsync()
        {
            return await m_request.RequestAsync();
        }
    }
}
using System.Threading;
using Cysharp.Threading.Tasks;

namespace Kogane
{
    /// <summary>
    /// 通知許可ダイアログの結果を保持する構造体のインターフェイス
    /// </summary>
    public interface INotificationPermissionRequestResult
    {
    }
}

namespace Kogane.Internal
{
    /// <summary>
    /// 通知許可ダイアログを表示するクラスのインターフェイス
    /// </summary>
    internal interface INotificationPermissionRequest
    {
        //================================================================================
        // 関数
        //================================================================================
        /// <summary>
        /// 通知許可ダイアログを表示します
        /// </summary>
        UniTask<INotificationPermissionRequestResult> RequestAsync( CancellationToken cancellationToken );
    }
}
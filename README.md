# Kogane Notification Permission Request

Android で通知許可ダイアログを表示するクラス

## 使用例

```csharp
using Cysharp.Threading.Tasks;
using Kogane;
using UnityEngine;

public sealed class Example : MonoBehaviour
{
    private async UniTaskVoid Start()
    {
        var result = await NotificationPermissionRequest.RequestAsync( this );
        Debug.Log( result );
    }
}
```
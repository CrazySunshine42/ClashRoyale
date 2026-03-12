using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;




public class TestLoad : MonoBehaviour
{
    async void Start()
    {
        var handle = Addressables.LoadAssetAsync<GameObject>("https://raw.githubusercontent.com/CrazySunshine42/ClashRoyale/refs/heads/master/ServerData/[BuildTarget]");
        await handle.Task;
        if (handle.Status == AsyncOperationStatus.Succeeded)
        {
            Instantiate(handle.Result, Vector3.zero, Quaternion.identity);
            Debug.Log("实例化成功");
        }
        else
        {
            Debug.LogError($"错误：{handle.OperationException}");
        }
        Addressables.Release(handle);
    }
}



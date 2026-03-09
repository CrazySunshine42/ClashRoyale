using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
namespace UnityRoyale
{


    public class TestBundleCRC : MonoBehaviour
    {
        // 输入你的 AssetBundle 完整 URL
        public string bundleURL = "https://raw.githubusercontent.com/CrazySunshine42/ClashRoyale/refs/heads/master/ServerData/StandaloneWindows64/myplaceables_assets_all_9278ef498d8d89732b1c0fa56a7ded22.bundle";

        IEnumerator Start()
        {
            // 创建 UnityWebRequest，第四个参数 crc 设为 0 表示禁用 CRC 校验
            UnityWebRequest request = UnityWebRequestAssetBundle.GetAssetBundle(bundleURL, 0);
            yield return request.SendWebRequest();

            if (request.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("下载失败: " + request.error);
            }
            else
            {
                // 成功获取 AssetBundle
                AssetBundle bundle = DownloadHandlerAssetBundle.GetContent(request);
                if (bundle != null)
                {
                    Debug.Log("AssetBundle 加载成功！可以尝试加载资源...");
                    // 这里可以进一步加载资源，如 var asset = bundle.LoadAsset<GameObject>("assetName");
                }
                else
                {
                    Debug.LogError("AssetBundle 加载失败，即使 CRC 已禁用！");
                }
            }
        }
    }
}

using System;
using System.Reflection;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;

namespace UnityRoyale
{
    public class MyDllLoader : MonoBehaviour
    {
        // Start is called before the first frame update
        async Task Start()
        {
            //TextAsset不仅能承载文本数据，也能承载二进制数据
            TextAsset dll = await Addressables.LoadAssetAsync<TextAsset>("HelloDLL.dll").Task;
            TextAsset pdb = await Addressables.LoadAssetAsync<TextAsset>("HelloDLL.pdb").Task;
            //载入到mono虚拟机里面
            var ass = Assembly.Load(dll.bytes, pdb.bytes);
            //打印所有该DLL中的数据类型（结果应该是只有HelloDLL这个类名被打印）
            //foreach (var item in ass.GetTypes())
            //{
            //    print(item);
            //}
            //执行SayHello方法
            Type t = ass.GetType("HelloDLL");
            t.GetMethod("SayHello").Invoke(null,null);
        }
    }
}

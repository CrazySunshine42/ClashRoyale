using UnityEditor;
using System.IO;
using UnityEngine;
namespace UnityRoyale
{
    public static class CopyDlls
    {
        public static string src = "Library\\ScriptAssemblies";//拷贝源
        public static string dest = "Assets/_CALSS_S1/Resources_moved/MyDlls";//拷贝目标
        public static string[] files = new[] { "HelloDLL.dll", "HelloDLL.pdb" };//要拷贝的文件列表
        [MenuItem("Tools/Copy Dlls")]
        public static void DoCopyDlls()
        {
            //在指定路径中创建目录（如果目标路径不存在就创建一个）
            Directory.CreateDirectory(dest);
            //
            foreach (string file in files)
            {
                //源文件逐个拷贝到目标位置，并且改名加上.bytes后缀（只有.bytes后缀的文件会被认为是二进制数据，dll无法被作为数据打包）
                Debug.Log($"{Path.Combine(src, file)} => {Path.Combine(dest, file + ".bytes")}");
                File.Copy(Path.Combine(src,file),Path.Combine(dest,file+".bytes"),true);
            }
            AssetDatabase.Refresh();//拷贝资源后自动刷新
        }
    }
}

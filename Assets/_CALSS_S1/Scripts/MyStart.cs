using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UnityRoyale
{
    public class MyStart : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            UIPage.ShowPageAsync<LogoPage>();
        }
    }
}

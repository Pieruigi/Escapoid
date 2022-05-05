using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Zoca
{
    public class VirtualCameraResetter : MonoBehaviour
    {

        bool changed = false;

        // Start is called before the first frame update
        void Start()
        {
            
        }

        // Update is called once per frame
        void LateUpdate()
        {
            if (changed)
                return;

            changed = true;
            CinemachineVirtualCamera virtualCam = GetComponent<CinemachineVirtualCamera>();
            virtualCam.m_Lens.OrthographicSize = Camera.main.orthographicSize;
            Debug.Log("CameraOrtoSDize:" + Camera.main.orthographicSize);
        }
    }

}

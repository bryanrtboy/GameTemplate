using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CreatorKitCodeInternal;

namespace CreatorKitCode
{

    public class MouseControls : MonoBehaviour
    {
        Camera m_MainCamera;

        public float m_separationAmount = .01f;
        public EventFloat OnCTRLClick;

        // Start is called before the first frame update
        void Awake()
        {
            m_MainCamera = Camera.main;
        }

        // Update is called once per frame
        void Update()
        {
            //float mouseWheel = Input.GetAxis("Mouse ScrollWheel");
            //if (!Mathf.Approximately(mouseWheel, 0.0f))
            //{
            //    Vector3 view = m_MainCamera.ScreenToViewportPoint(Input.mousePosition);
            //    if (view.x > 0f && view.x < 1f && view.y > 0f && view.y < 1f)
            //        CameraController.Instance.Zoom(-mouseWheel * Time.deltaTime * 20.0f);
            //}

            if (Input.GetKey(KeyCode.LeftControl) && Input.GetMouseButton(0))
            {
                OnCTRLClick.Invoke(m_separationAmount);

            }

        }
    }
}

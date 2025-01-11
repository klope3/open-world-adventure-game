using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

//From https://discussions.unity.com/t/how-do-i-turn-off-fog-on-a-specific-camera-using-urp/903128/13
public class DisableCameraFog : MonoBehaviour
{
    // Unity calls this method automatically when it enables this component
    private void OnEnable()
    {
        // Add WriteLogMessage as a delegate of the RenderPipelineManager.beginCameraRendering event
        RenderPipelineManager.beginCameraRendering += BeginRender;
        RenderPipelineManager.endCameraRendering += EndRender;
    }

    // Unity calls this method automatically when it disables this component
    private void OnDisable()
    {
        // Remove WriteLogMessage as a delegate of the  RenderPipelineManager.beginCameraRendering event
        RenderPipelineManager.beginCameraRendering -= BeginRender;
        RenderPipelineManager.endCameraRendering -= EndRender;
    }

    // When this method is a delegate of RenderPipeline.beginCameraRendering event, Unity calls this method every time it raises the beginCameraRendering event
    void BeginRender(ScriptableRenderContext context, Camera camera)
    {
        if (camera.name == gameObject.name)
        {
            RenderSettings.fog = false;
        }

    }

    void EndRender(ScriptableRenderContext context, Camera camera)
    {
        if (camera.name == gameObject.name)
        {
            RenderSettings.fog = true;
        }
    }
}
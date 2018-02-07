using UnityEngine;

[ExecuteInEditMode]
[AddComponentMenu("Image Effects/Pixel Perfect")]
public class PixelPerfect : MonoBehaviour
{
    [SerializeField] private Camera camera;

    private int height;

    [SerializeField] private int width = 384;

    protected void Start()
    {
        camera = GetComponent<Camera>();

        if (!SystemInfo.supportsImageEffects) enabled = false;
    }

    private void Update()
    {
        var ratio = camera.pixelHeight / (float) camera.pixelWidth;
        height = Mathf.RoundToInt(width * ratio);
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        source.filterMode = FilterMode.Point;
        var buffer = RenderTexture.GetTemporary(width, height, -1);
        buffer.filterMode = FilterMode.Point;
        Graphics.Blit(source, buffer);
        Graphics.Blit(buffer, destination);
        RenderTexture.ReleaseTemporary(buffer);
    }
}
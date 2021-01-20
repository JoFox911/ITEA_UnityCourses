using UnityEngine;

public class HiResScreenShots : MonoBehaviour
{
    [SerializeField]
    private Camera _camera;
    [SerializeField]
    private int _resWidth = 2550;
    [SerializeField]
    private int _resHeight = 3300;

    public static string ScreenShotName(int width, int height)
    {
        return string.Format("{0}/screenshots/screen_{1}x{2}_{3}.png",
                             Application.dataPath,
                             width, height,
                             System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown("k"))
        {
            RenderTexture rt = new RenderTexture(_resWidth, _resHeight, 24);
            _camera.targetTexture = rt;
            Texture2D screenShot = new Texture2D(_resWidth, _resHeight, TextureFormat.RGB24, false);
            _camera.Render();
            RenderTexture.active = rt;
            screenShot.ReadPixels(new Rect(0, 0, _resWidth, _resHeight), 0, 0);
            _camera.targetTexture = null;
            RenderTexture.active = null; // JC: added to avoid errors
            Destroy(rt);
            byte[] bytes = screenShot.EncodeToPNG();
            string filename = ScreenShotName(_resWidth, _resHeight);
            System.IO.File.WriteAllBytes(filename, bytes);
            Debug.Log(string.Format("Took screenshot to: {0}", filename));
        }
    }
}
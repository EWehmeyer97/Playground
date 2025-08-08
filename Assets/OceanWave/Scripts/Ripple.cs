using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Ripple : MonoBehaviour
{
    [SerializeField] private Camera rippleCam;
    [SerializeField] private int TextureSize = 512;
    [SerializeField] private RenderTexture ObjectsRT;
    private RenderTexture CurrRT, PrevRT, TempRT;

    [SerializeField] private Shader RippleShader, AddShader;
    private Material RippleMat, AddMat;
    [SerializeField] private Material material;
    
    void Start()
    {
        //Creating render textures and materials
        CurrRT = new RenderTexture(TextureSize, TextureSize, 0, RenderTextureFormat.RFloat);
        PrevRT = new RenderTexture(TextureSize, TextureSize, 0, RenderTextureFormat.RFloat);
        TempRT = new RenderTexture(TextureSize, TextureSize, 0, RenderTextureFormat.RFloat);

        RippleMat = new Material(RippleShader);
        AddMat = new Material(AddShader);

        //Change material of this object to the calculated ripple shader.
        material.SetTexture("_RippleTex", CurrRT);
        material.SetFloat("_cameraSize", 1 / (2 * rippleCam.orthographicSize));
        material.SetVector("_cameraPos", new Vector2(rippleCam.transform.position.x, rippleCam.transform.position.z));

        StartCoroutine(Ripples());
    }

    
    IEnumerator Ripples()
    {
        //Copy the result of blending the render textures to TempRT.
        AddMat.SetTexture("_ObjectsRT", ObjectsRT);
        AddMat.SetTexture("_CurrentRT", CurrRT);
        Graphics.Blit(null, TempRT, AddMat);

        RenderTexture rt0 = TempRT;
        TempRT = CurrRT;
        CurrRT = rt0;

        //Calculate the ripple animation using ripple shader.
        RippleMat.SetTexture("_PrevRT", PrevRT);
        RippleMat.SetTexture("_CurrentRT", CurrRT);
        Graphics.Blit(null, TempRT, RippleMat);
        Graphics.Blit(TempRT, PrevRT);

        //Swap PrevRT and CurrentRT to calculate the result for the next frame.
        RenderTexture rt = PrevRT;
        PrevRT = CurrRT;
        CurrRT = rt;

        //Wait for one frame and then execute again.
        yield return null;
        StartCoroutine(Ripples());
    }
}
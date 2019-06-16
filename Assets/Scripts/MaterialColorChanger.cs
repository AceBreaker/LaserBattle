using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MaterialColorChanger : MonoBehaviour {

    public Color defaultColor, deadColor;

    public Color defaultEmissionColor, deadEmissionColor;

    public float speed = 1;
    public float offset;

    Renderer renderer;
    MaterialPropertyBlock propBlock;

    public bool startLerping = false;
    float timeSinceStart = 0.0f;
    public float duration = 2.0f;
    
    public Color newColor;

    public Color newEmissionColor;

    private void Start()
    {
        renderer = GetComponent<Renderer>();
        propBlock = new MaterialPropertyBlock();
    }

    private void Update()
    {
        if (startLerping)
        {
            renderer.GetPropertyBlock(propBlock);

            timeSinceStart += Time.deltaTime;
            float t = timeSinceStart / duration;
            if (t >= 1.0f)
            {
                t = 1.0f;
                startLerping = false;
            }

            newColor = Color.Lerp(defaultColor, deadColor, t);
            newEmissionColor = Color.Lerp(defaultEmissionColor, deadEmissionColor, t);
            propBlock.SetColor("_Color", newColor);
            propBlock.SetColor("_EmissionColor", newEmissionColor);

            renderer.SetPropertyBlock(propBlock);
        }
    }

    public void BeginColorChanging()
    {
        startLerping = true;
    }
}

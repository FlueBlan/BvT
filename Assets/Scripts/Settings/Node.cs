using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Color hoverColor = new Color(1f, 1f, 0f, 0.5f); // Semi-transparent yellow
    public Color notEnoughMoneyColor = new Color(1f, 0f, 0f, 0.5f); // Semi-transparent red

    [Header("Optional")]
    public GameObject turret;

    private Renderer rend;
    private Color startColor;

    private BuildManager buildManager;
    private CursorManager cursorManager;

    void Start()
    {
        cursorManager = FindFirstObjectByType<CursorManager>();
        rend = GetComponent<Renderer>();
        buildManager = BuildManager.instance;

        if (rend != null)
        {
            // Cache original color
            startColor = rend.material.color;

            // Force transparent setup
            SetMaterialToTransparent(rend.material);

            // Start fully transparent
            rend.material.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        }
        else
        {
            Debug.LogWarning("No Renderer found on Node: " + gameObject.name);
        }

        if (buildManager == null)
        {
            Debug.LogWarning("BuildManager instance not found in scene!");
        }
    }

    public Vector3 GetBuildPosition()
    {
        return transform.position;
    }

    void OnMouseDown()
    {
        if (EventSystem.current.IsPointerOverGameObject()) return;
        if (buildManager == null || !buildManager.CanBuild) return;
        if (turret != null) return;

        buildManager.BuildTurretOn(this);
    }

    void OnMouseEnter()
    {
        if (buildManager == null || rend == null) return;

        rend.material.color = buildManager.HasMoney ? hoverColor : notEnoughMoneyColor;
        cursorManager?.OnHoverEnter();
    }

    void OnMouseExit()
    {
        if (rend != null)
        {
            rend.material.color = new Color(startColor.r, startColor.g, startColor.b, 0f);
        }
        cursorManager?.OnHoverExit();
    }

    void SetMaterialToTransparent(Material mat)
    {
        mat.SetFloat("_Mode", 3); // Transparent
        mat.SetInt("_SrcBlend", (int)UnityEngine.Rendering.BlendMode.SrcAlpha);
        mat.SetInt("_DstBlend", (int)UnityEngine.Rendering.BlendMode.OneMinusSrcAlpha);
        mat.SetInt("_ZWrite", 0);
        mat.DisableKeyword("_ALPHATEST_ON");
        mat.EnableKeyword("_ALPHABLEND_ON");
        mat.DisableKeyword("_ALPHAPREMULTIPLY_ON");
        mat.renderQueue = 3000;
    }
}

using UnityEngine;

public class BuildManager : MonoBehaviour
{
    public static BuildManager instance;
    public bool canPlaceBeans = true;

    [Header("Bean Prefabs")]
    public BeanShooterBlueprint wizardBeanPrefab;
    public BeanShooterBlueprint buffbeanPrefab;
    public BeanShooterBlueprint greenBeanPrefab;
    public BeanShooterBlueprint popcornBombPrefab;

    [Header("Build Effects & SFX")]
    public GameObject buildEffect;
    public AudioSource audioSource;
    public AudioClip placeBeanSFX;

    private BeanShooterBlueprint turretToBuild;

    public bool CanBuild => turretToBuild != null;
    public bool HasMoney => turretToBuild != null && PlayerStats.Money >= turretToBuild.cost;

    void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("More than one BuildManager in the scene!");
            return;
        }
        instance = this;
    }

    public void BuildTurretOn(Node node)
    {
        if (!canPlaceBeans)
        {
            Debug.Log("Cannot place beans during tutorial.");
            return;
        }

        if (!CanBuild || !HasMoney)
        {
            Debug.LogWarning("No turret selected or not enough money.");
            return;
        }

        PlayerStats.Money -= turretToBuild.cost;
        Vector3 spawnPos = node.GetBuildPosition() + turretToBuild.spawnOffset;
        Quaternion rotation = Quaternion.Euler(turretToBuild.rotationEuler);

        GameObject turret = Instantiate(turretToBuild.prefab, spawnPos, rotation);
        node.turret = turret;

        if (buildEffect != null)
        {
            GameObject effect = Instantiate(buildEffect, spawnPos + Vector3.up * 0.5f, Quaternion.identity);
            Destroy(effect, 5f);
        }

        // Play troop placement SFX
        if (audioSource != null && placeBeanSFX != null)
        {
            audioSource.PlayOneShot(placeBeanSFX);
        }

        Debug.Log("Built turret: " + turret.name);
        ClearTurretToBuild();
    }

    public void SelectBeanToBuild(BeanShooterBlueprint turret) => turretToBuild = turret;
    public void ClearTurretToBuild() => turretToBuild = null;
    public BeanShooterBlueprint GetTurretToBuild() => turretToBuild;
}

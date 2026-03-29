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
    [SerializeField] private HandView handView;

    [Header("Build Effects & SFX")]
    public GameObject buildEffect;
    public AudioSource audioSource;
    public AudioClip placeBeanSFX;

    private BeanShooterBlueprint turretToBuild;
    private CardView currentCard;

    // Reference to check if a turret can be built
    public bool CanBuild => turretToBuild != null;
    public bool HasMoney => turretToBuild != null && PlayerStats.Money >= turretToBuild.cost;

    void Awake()
    {
        //Check if BuilManager already exists
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

        //If both checks pass, build the turret
        PlayerStats.Money -= turretToBuild.cost;
        //this is not where the turret is in blueprint
        Vector3 spawnPos = node.GetBuildPosition() + turretToBuild.spawnOffset;
        Quaternion rotation = Quaternion.Euler(turretToBuild.rotationEuler);

        GameObject turret = Instantiate(turretToBuild.prefab, spawnPos, rotation);
        node.turret = turret;

        //Special effects for building turrets
        if (buildEffect != null)
        {
            GameObject effect = Instantiate(buildEffect, spawnPos + Vector3.up * 0.5f, Quaternion.identity);
            Destroy(effect, 5f);
        }

        // Play troop placement SFX
        if (audioSource != null && placeBeanSFX != null)
            audioSource.PlayOneShot(placeBeanSFX);

        if (currentCard != null)
        {
            handView.RemoveCard(currentCard);
            Destroy(currentCard.gameObject);
            currentCard = null;
        }
        Debug.Log("Built turret: " + turret.name);
        ClearTurretToBuild();
    }

    public void SelectBeanToBuild(BeanShooterBlueprint turret, CardView cardView)
    {
        turretToBuild = turret;
        currentCard = cardView;
    }
    public void ClearTurretToBuild() => turretToBuild = null;
    public BeanShooterBlueprint GetTurretToBuild() => turretToBuild;
}

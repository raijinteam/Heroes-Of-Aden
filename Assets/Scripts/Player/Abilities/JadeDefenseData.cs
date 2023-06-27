using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JadeDefenseData : PowerUpHandler
{
    [SerializeField] private JadeDefenseMovement jadeDefense;
    [SerializeField] private JadeController obj_Jade;
    [SerializeField] private Transform jadeParent;
    [SerializeField] private int numberOfObjects; // The number of objects to be created
    [SerializeField] private float radius;        // The radius of your circle
    [SerializeField] private int damage;
    [SerializeField] private List<JadeController> list_JadeTransforms = new List<JadeController>();

    private void Start()
    {
        jadeParent.gameObject.SetActive(true);
        SetData();      
    }

	

	private void DestroyAndSpawnJades()
	{
     
        JadeController obj = Instantiate(obj_Jade, jadeParent.position + new Vector3(radius, 0, 0), Quaternion.identity);
        obj.transform.SetParent(jadeParent, true);

        list_JadeTransforms.Add(obj);

        for(int i = 0; i < list_JadeTransforms.Count; i++)
		{
            list_JadeTransforms[i].body.SetActive(false);
		}

        // spawn new ones
        float angleIncrement = 360f / numberOfObjects;

        jadeDefense.enabled = false;
        jadeDefense.transform.localEulerAngles = Vector3.zero;

        for (int i = 0; i < numberOfObjects; i++)
        {
            list_JadeTransforms[i].transform.position = jadeParent.position + new Vector3(radius, 0, 0);
            list_JadeTransforms[i].transform.rotation = Quaternion.identity;
            list_JadeTransforms[i].SetDamage(damage);

            // Rotate the parent object
            jadeParent.rotation = Quaternion.Euler(0f, 0f, -angleIncrement * (i + 1));
        }


        for (int i = 0; i < list_JadeTransforms.Count; i++)
        {
            list_JadeTransforms[i].body.SetActive(true);
        }

        jadeDefense.enabled = true;
    }

    private void SetData()
	{
        damage = AbilityManager.Instance.jadeDefenseData.all_Damage[currentLevel];
        
        if(numberOfObjects != AbilityManager.Instance.jadeDefenseData.all_JadeCount[currentLevel])
		{
            numberOfObjects = AbilityManager.Instance.jadeDefenseData.all_JadeCount[currentLevel];
            DestroyAndSpawnJades();
        }
		else
		{
            for (int i = 0; i < numberOfObjects; i++)
            {   
                list_JadeTransforms[i].SetDamage(damage);
            }
        }

        
        jadeDefense.SetSpeed(AbilityManager.Instance.jadeDefenseData.all_RotationSpeed[currentLevel]);
        
    }

    public override void LevelUp()
    {
        currentLevel += 1;
        SetData();
    }

    public override void UpdateCooldownTime()
    {

    }


    public override string GetMyPowerName()
    {
        return AbilityManager.Instance.jadeDefenseData.powerUpName;
    }

    public override int GetMyCurrentLevel()
    {
        return currentLevel;
    }

    public override Sprite GetMyIcon()
    {
        return AbilityManager.Instance.jadeDefenseData.powerUpIcon;
    }

    public override string GetMyPowerInfo()
    {
        return AbilityManager.Instance.jadeDefenseData.powerUpInfo;
    }

    public override void SetUpdateInfoPanel(int _panelIndex)
    {
        int count = 0;

        int damageNew = AbilityManager.Instance.jadeDefenseData.all_Damage[currentLevel + 1];
        int damageOld = AbilityManager.Instance.jadeDefenseData.all_Damage[currentLevel];
        int numberOfObjectsNew = AbilityManager.Instance.jadeDefenseData.all_JadeCount[currentLevel + 1];

        if (damageOld != damageNew)
		{
            AbilityManager.Instance.HandleDamageIncrease(_panelIndex, count, damageOld, damageNew);
            count += 1;
        }

        if (numberOfObjects != numberOfObjectsNew)
        {
            AbilityManager.Instance.HandleProjectileIncrease(_panelIndex, count, numberOfObjects, numberOfObjectsNew);
            count += 1;
        }

    }
}

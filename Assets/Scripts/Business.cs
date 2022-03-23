using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using GIKIT;

// This section is for Business Ids in enum form
public enum BusinessID { Lemon, Egg, Bakery, Computer, Oil, Beer, Car, Diamond, Stone }

public class Business : MonoBehaviour
{
    public BusinessID ID;
    public string Name;
    public Sprite Sprite = null;
    public Sprite Art = null;
    public Sprite ProductSprite = null;
    public double Price;
    public double Earning;
    public double ExpandPrice;
    public int EarningMultiplier = 1;
    public int ExpandMultiplier = 1;
    public int Level;
    public float EarnTime;
    public bool IsPurchased = false;
    public UnityEvent OnEnableEvent;

    public int FreeSlot { get; private set; } = 0;
    public float ExpandCurve { get { return expandCurve; } }
    public float EarningCurve { get { return earningCurve; } }

    [Header("Curve Data")]
    public float earningCurve;
    public float expandCurve;

    [Header("UI Elements")]
    [SerializeField] TextMeshProUGUI levelText;
    [SerializeField] TextMeshProUGUI earningText;
    [SerializeField] TextMeshProUGUI expandPriceText;
    [SerializeField] TextMeshProUGUI priceText;
    [SerializeField] TextMeshProUGUI timeText;
    [SerializeField] Image progressImage;
    [SerializeField] Transform lockPanel;
    [SerializeField] Button expandButton;
    [SerializeField] TextMeshProUGUI multiplierText;
    [SerializeField] Color expandEnabledTextColor;
    [SerializeField] Color expandDisabledTextColor;
    [SerializeField] Button managerButton1;
    [SerializeField] Button managerButton2;
    [SerializeField] Button managerButton3;
    [SerializeField] Rail rail;
    [SerializeField] Image artImage;
    [SerializeField] Color inActiveColor;
    [SerializeField] Image railImage;
    [SerializeField] Image productImage;
    [SerializeField] Slider levelSlider = null;
    [SerializeField] Transform handIcon = null;
    [SerializeField] Image businessImage = null;
    [SerializeField] Image lockButtonImage = null;
    [SerializeField] TextMeshProUGUI businessNameText = null;
    [SerializeField] GameObject expandParticles = null;
    [SerializeField] GameObject constructionParticles = null;
    [SerializeField] TextMeshProUGUI nameText = null;
    [SerializeField] Transform slotGlow = null;
    [SerializeField] Transform glowParticles = null;

    [Header("Sound Effects")]
    [SerializeField] AudioClip constructionSFX = null;
    [SerializeField] AudioClip expandSoundSFX = null;




    bool isEarning = false;
    bool hasManager = false;
    Manager[] managers = new Manager[3];
    int levelUpTarget = 0;
    double earning;
    Animator animator = null;
    bool isAutomated = false;
    float timeRequiredForStaticEarningbar = 0.5f;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        BusinessSaveData loadedData = SaveManager.Instance.LoadBusinessData(ID);

        if (loadedData != null)
        {
            Price = loadedData.Price;
            Earning = loadedData.Earning;
            ExpandPrice = loadedData.ExpandPrice;
            EarningMultiplier = loadedData.EarningMultiplier;
            Level = loadedData.Level;
            EarnTime = loadedData.EarnTime;
            IsPurchased = loadedData.IsPurchased;
            hasManager = loadedData.HasManager;
            isAutomated = loadedData.IsAutomated;
        }

        SetData(Name, Sprite, EarnTime, Earning, ExpandPrice, Level, Art, ProductSprite, Price, IsPurchased);
    }

    private void Update()
    {

        levelText.text = Level.ToString();

        //Earning
        earning = GetMultipliedEarning(EarningMultiplier);
        earningText.text = MoneyConvertor.Convert(earning);

        //Expand
        double expandPrice = GetMultipliedExpandPrice(ExpandMultiplier);
        expandPriceText.text = MoneyConvertor.Convert(expandPrice);

        //Time
        timeText.text = TimeConvertor.ConvertFromSeconds(EarnTime);
        multiplierText.text = "x" + (ExpandMultiplier);

        //Enabling and disabling button based on player money
        if (Player.Instance.Coins >= expandPrice)
        {
            expandButton.interactable = true;
            expandPriceText.color = expandEnabledTextColor;
        }
        else
        {
            expandButton.interactable = false;
            expandPriceText.color = expandDisabledTextColor;
        }

        //Checking if Business has been automated
        if (isAutomated)
        {
            Earn();
        }
    }



    public void Buy()
    {
        StartCoroutine(BuyWithDelay(0.1f));
    }

    public void Earn()
    {
        if (!isEarning)
        {
            StartCoroutine(EarnCoroutine());
        }
    }


    public void Expand()
    {

        Earning = GetMultipliedEarning(EarningMultiplier);

        ExpandPrice = GetMultipliedExpandPrice(ExpandMultiplier);

        Player.Instance.Coins -= ExpandPrice;

        Level += ExpandMultiplier;

        float levelSliderValue = Mathf.Lerp(0, 1, (float)Level / (float)levelUpTarget);

        if (levelSliderValue == 1)
        {
            levelSliderValue = 0;
        }

        if (levelSlider)
        {
            levelSlider.value = levelSliderValue;
        }

        if (MultiplierChanger.Instance.IsMaxed())
        {
            MultiplierChanger.Instance.ApplyMultiplier();
        }

        //Show Coin particles
        Vector3 offset = new Vector3(0f, 0f, -0.5f);
        GameObject newParticles = Instantiate(expandParticles, expandButton.transform.position + offset, Quaternion.identity);
        float particleDuration = 1f;
        Destroy(newParticles, particleDuration);
        MusicManager.Instance.PlaySFX(expandSoundSFX);


    }

    public double GetMultipliedExpandPrice(int multiplier)
    {
        double ret = ExpandPrice;
        double sum = 0;

        for (int i = 0; i < multiplier; i++)
        {
            ret += ret * expandCurve;
            sum += ret;
        }

        return sum;
    }

    public double GetMultipliedEarning(int multiplier)
    {
        double ret = Earning;

        for (int i = 0; i < multiplier; i++)
        {
            ret += ret * earningCurve;
        }

        return ret;
    }

    public void SetManager(int freeSlot, Manager manager)
    {
        if (freeSlot < 0 || freeSlot > 2)
        {
            return;
        }

        managers[freeSlot] = manager;
        hasManager = true;

        for (int i = 0; i < manager.Actions.Length; i++)
        {
            manager.Actions[i].Act(this);
        }


        switch (freeSlot)
        {
            case 0:
                managerButton1.image.sprite = manager.Sprite;
                break;
            case 1:
                managerButton2.image.sprite = manager.Sprite;
                break;
            case 2:
                managerButton3.image.sprite = manager.Sprite;
                break;
        }

        FreeSlot++;

        if (freeSlot > 2)
        {
            freeSlot = -1;
        }
    }


    public void OrderManagers()
    {
        ManagerOderer.Instance.Order(ID);
    }

    public void SetLevelUpTarget(int target)
    {
        levelUpTarget = target;
    }

    public void TapAnimation()
    {
        if (animator)
        {
            animator.SetTrigger("Tap");
        }
    }




    //---------------Helpers-------------------

    public void SetData(string name, Sprite sprite, float earnTime, double earning, double expandPrice, int level, Sprite art, Sprite product, double price, bool isPurchased)
    {
        gameObject.name = "Business (" + name + ")";
        businessImage.sprite = sprite;
        timeText.text = TimeConvertor.ConvertFromSeconds(earnTime);
        earningText.text = MoneyConvertor.Convert(earning);
        expandPriceText.text = MoneyConvertor.Convert(expandPrice);
        levelText.text = level.ToString();
        artImage.sprite = art;
        productImage.sprite = product;
        lockButtonImage.sprite = sprite;
        priceText.text = MoneyConvertor.Convert(Price);
        lockPanel.gameObject.SetActive(!isPurchased);
        businessNameText.text = name.ToString();
        nameText.text = name.ToString();
        if (!isPurchased)
        {
            nameText.gameObject.SetActive(false);
        }

        // this is called when business is purchased and enabled
        if (isPurchased)
        {
            if (OnEnableEvent != null)
            {
                OnEnableEvent.Invoke();
            }
        }


    }

    IEnumerator EarnCoroutine()
    {
        float fill = 0;
        isEarning = true;
        //Make art image birght when earning
        SetSlotColors(Color.white);

        // hide hand icon when earning
        if (handIcon)
        {
            handIcon.gameObject.SetActive(false);
        }

        //Start rail movement;
        if (rail)
        {
            float railSpeed = 0.1f;

            // rail speed are set here according to earnTime
            if (EarnTime >= 3)
            {
                railSpeed = 0.08f;
            }
            else if (EarnTime >= 2)
            {
                railSpeed = 0.1f;
            }
            else if (EarnTime >= 1)
            {
                railSpeed = 0.15f;
            }
            else if (EarnTime >= 0.5)
            {
                railSpeed = 0.2f;
            }
            else if (EarnTime < 0.5)
            {
                railSpeed = 0.25f;
            }
           
            rail.Speed = railSpeed;
            rail.IsRunning = true;
        }

        //show glowParticles while earning
        if (glowParticles)
        {
            glowParticles.gameObject.SetActive(true);
        }
        //show slot glow
        if (slotGlow)
        {
            slotGlow.gameObject.SetActive(true);
        }

        // this loop fills earning progress bar over time
        while (fill < 1)
        {
            fill += Time.deltaTime / EarnTime;
            fill = Mathf.Clamp(fill, 0, 1);

            

            if (EarnTime <= timeRequiredForStaticEarningbar)
            {
                progressImage.fillAmount = 1f;
            }
            else
            {
                progressImage.fillAmount = fill;
            }

            
            yield return null;
        }
        // when earning stops, this happens when progress bar becomes full, when 'fill' value becomes '1'

        Player.Instance.Coins += Mathf.Floor((float)earning);

        // reset earning progress to empty only when earn time is greater than timeRequired to make bar static

        if (EarnTime > timeRequiredForStaticEarningbar)
        {
            progressImage.fillAmount = 0f;
        }
       
        isEarning = false;

        //making art darker again
        if (!hasManager)
        {
            SetSlotColors(inActiveColor);
        }

        //displaying hand icon again
        if (handIcon && !hasManager)
        {
            handIcon.gameObject.SetActive(true);
        }


        if (rail)
        {
            rail.IsRunning = false;
        }

        //hiding glow particles
        if (glowParticles && !isAutomated)
        {
            glowParticles.gameObject.SetActive(false);
        }

        //hiding slot glow
        if (slotGlow && !isAutomated)
        {
            slotGlow.gameObject.SetActive(false);
        }
    }

    private void SetSlotColors(Color color)
    {
        if (artImage)
        {
            artImage.color = color;
        }
        if (railImage)
        {
            railImage.color = color;
        }
        if (productImage)
        {
            productImage.color = color;
        }
    }

    public void Automate()
    {
        isAutomated = true;
    }

    public BusinessSaveData CreateSaveData()
    {
        BusinessSaveData saveData = new BusinessSaveData(Price,Earning,ExpandPrice,EarningMultiplier,Level,EarnTime,IsPurchased,hasManager,isAutomated);
        return saveData;
    }

    IEnumerator BuyWithDelay(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        if (Player.Instance.Coins >= Price)
        {
            IsPurchased = true;
            Player.Instance.Coins -= Price;
            lockPanel.gameObject.SetActive(false);

            //onEnable event is triggered when business is purchased for the first time
            if (OnEnableEvent != null)
            {
                OnEnableEvent.Invoke();
            }
        }

        Vector3 offset = new Vector3(0f, 0f, -0.5f);
        GameObject newConstructionVFX = Instantiate(constructionParticles, transform.position, Quaternion.identity, transform);
        float lifeTime = 3.5f;
        Destroy(newConstructionVFX, lifeTime);
        handIcon.gameObject.SetActive(true);
        nameText.gameObject.SetActive(true);
        MusicManager.Instance.PlaySFX(constructionSFX);

    }


    
  

}
    
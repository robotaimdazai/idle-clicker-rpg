using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Graph : MonoBehaviour
{
    public bool ShowNodes = false;
    public float SpeedMultiplier = 1f;
    public Vector2 CurrentNodePosition { get { return currentNodePosition; } }
    public TextMeshProUGUI RateText { set { rateText = value; } get { return rateText; } }

    [Header ("Graph Settings")]
    [SerializeField] RectTransform background = null;
    [SerializeField] RectTransform origin;
    [SerializeField] Sprite nodeSprite;
    [SerializeField] TextMeshProUGUI rateText;
    [SerializeField] Vector2 rateTextOffset = Vector2.zero;

    [SerializeField] float lineThickness = 7f;
    [SerializeField] float maxHeight = 527f;
    [SerializeField] float floorHeight = 162f;
    [SerializeField] float nodesPerSecond = 1f;
    [SerializeField] float xStepsWidth = 50f;
    [SerializeField] int minNodesRequiredForScrolling = 9;

    

    [Header("Probability Settings")]
    [Range(0,1)]
    [SerializeField] float aboveBidLineProbability = 0.1f;
    [Range(0, 1)]
    public float BidLinePercentage = 1f;
    Vector2 previousNodePosition= Vector2.zero;

    float t = 0f;
    float xTime = 0f;
    bool isEnabled = false;
    int nodesCount = 0;

    int garbageCount = 0;
    int garbageCountToStartCollection = 40;
    float garbageCollectorAggresiveness = 0.9f;
    Vector2 currentNodePosition = Vector2.zero;


    public float Rate { get; private set; } = 0f;

    private void Start()
    {
        //StartGraph();
        rateText.gameObject.SetActive(false);

      
    }

    public void StartGraph()
    {
        isEnabled = true;
    }

    public void StopGraph()
    {
        
        isEnabled = false;
        t = 0f;
        xTime = 0f;
        nodesCount = 0;
        garbageCount = 0;
        previousNodePosition = Vector2.zero;
        origin.anchoredPosition = new Vector3(0f,0f,0f);
        rateText.gameObject.SetActive(false);
        


        foreach (Transform item in origin)
        {

            TextMeshProUGUI rateTextGameObject = item.GetComponent<TextMeshProUGUI>();
            if (!rateTextGameObject)
            {
                Destroy(item.gameObject);
            }
        }
    }

    private void Update()
    {
        if (!isEnabled)
        {
            return;
        }

        t += Time.deltaTime * SpeedMultiplier;
        xTime += Time.deltaTime * xStepsWidth * SpeedMultiplier;

        if (t >= 1/nodesPerSecond)
        {
            float randomPercentage = Random.Range(0f , BidLinePercentage);
            float randomHeight = GetHeightfromPercentage(randomPercentage);

            float probability = Random.value;

            if (probability <= aboveBidLineProbability)
            {
                randomPercentage = Random.Range(BidLinePercentage , 1);
                randomHeight = GetHeightfromPercentage(randomPercentage);
            }

            Rate = randomPercentage;

            float xValue = xTime;

            if (nodesCount == 0)
            {
                xValue = 0f;
            }

            Vector2 newNodePosition = new Vector2(xValue,randomHeight);

            CreateNode(newNodePosition);
            t = 0f;
        }

        if (nodesCount >= minNodesRequiredForScrolling)
        {
            origin.anchoredPosition = new Vector2(origin.anchoredPosition.x - xStepsWidth * Time.deltaTime * SpeedMultiplier, 0);
        }
        if (garbageCount >= garbageCountToStartCollection)
        {
            RunGarbageCollector(garbageCollectorAggresiveness);
            garbageCount = 0;
        }


    }

    private void CreateNode(Vector2 position)
    {
        GameObject newNode = new GameObject("new node", typeof(RectTransform));
        garbageCount++;
        newNode.GetComponent<RectTransform>().SetParent(origin,false);
        if (ShowNodes)
        {
            newNode.AddComponent<Image>().sprite = nodeSprite;
        }
        RectTransform nodeRectTransform = newNode.GetComponent<RectTransform>();
        nodeRectTransform.anchorMax = new Vector2(0,0);
        nodeRectTransform.anchorMin = new Vector2(0,0);
        nodeRectTransform.anchoredPosition = position;

        // saving position for bid line
        currentNodePosition = newNode.transform.position;

        // rate text settings
        
        rateText.gameObject.SetActive(true);
        
        rateText.rectTransform.anchoredPosition = position + rateTextOffset;
        

        if (previousNodePosition == Vector2.zero)
        {
            previousNodePosition = position;
        }
        else
        {
            DrawConnection(previousNodePosition,position);
            previousNodePosition = position;
        }

        nodesCount++;
    }

    private void DrawConnection(Vector2 nodeA, Vector2 nodeB)
    {
        GameObject newLine = new GameObject("connection", typeof(Image));
        garbageCount++;
        newLine.transform.SetParent(origin, false);
        RectTransform connectionRectTransform = newLine.GetComponent<RectTransform>();

        Vector2 direction = (nodeB - nodeA).normalized;
        float distance = Vector2.Distance(nodeB, nodeA);
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        connectionRectTransform.anchorMin = new Vector2(0, 0);
        connectionRectTransform.anchorMax = new Vector2(0, 0);
        connectionRectTransform.sizeDelta = new Vector2(distance, lineThickness);

        connectionRectTransform.anchoredPosition = nodeA + (direction ) * distance * 0.5f;
        connectionRectTransform.eulerAngles = new Vector3(0f,0f,angle);
        
        

    }

    private void RunGarbageCollector(float thresholdNormalized)
    {
        int numOfObjectsToDestroy = (int)thresholdNormalized * origin.childCount;
        for (int i = 0; i < numOfObjectsToDestroy; i++)
        {
            GameObject garbageObject = origin.GetChild(i).gameObject;
            TextMeshProUGUI rateTextGameObject = garbageObject.GetComponent<TextMeshProUGUI>();
            if (!rateTextGameObject)
            {
                Destroy(garbageObject);
            }
           
        }
        garbageCount -= numOfObjectsToDestroy;
    }

    private float GetHeightfromPercentage(float percentage)
    {
        float height = 0f;
        percentage = Mathf.Clamp01(percentage);
        height = Mathf.Lerp(floorHeight,maxHeight,percentage);
        return height;
    }


    
}

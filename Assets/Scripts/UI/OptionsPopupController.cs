using UnityEngine;
using UnityEngine.UI;

public class OptionsPopupController : MonoBehaviour
{
    [SerializeField] private InputField gridSizeInput;
    [SerializeField] private InputField totalObstaclesInput;
    [SerializeField] private XYInput startPositionInput;
    [SerializeField] private XYInput endPositionInput;
    [SerializeField] private Button buttonConfirm;
    [SerializeField] private Button buttonOptions;
    [SerializeField] private GameObject pupup;

    private GameOptions options;

    // Start is called before the first frame update
    void Start()
    {
        options = GameOptions.Options;
        PopulateFieldsFromOptions();

        // TODO: on enable/desible should register/unregister callbacks
        RegisterCallbacks();
    }

    private void PopulateFieldsFromOptions()
    {
        gridSizeInput.text = options.GridSize.ToString();
        totalObstaclesInput.text = options.TotalObstacles.ToString();
        startPositionInput.SetValue(options.StartPosition);
        endPositionInput.SetValue(options.EndPosition);
    }

    private void RegisterCallbacks()
    {
        gridSizeInput.onValueChanged.AddListener(OnGridSizeChanged);
        totalObstaclesInput.onValueChanged.AddListener(OnTotalObstaclesChanged);
        startPositionInput.onValueChanged = OnStartPositionChanged;
        endPositionInput.onValueChanged = OnEndPositionChanged;
        buttonConfirm.onClick.AddListener(OnConfirmButtonClicked);
        buttonOptions.onClick.AddListener(OnOptionsButtonClicked);
    }

    public void OnGridSizeChanged(string newSize)
    {
        int parsedSize;
        if(int.TryParse(newSize, out parsedSize))
        {
            options.GridSize = parsedSize;
        }
    }

    public void OnTotalObstaclesChanged(string newTotal)
    {
        int parsedTotal;
        if(int.TryParse(newTotal, out parsedTotal))
        {
            options.TotalObstacles = parsedTotal;
        }
    }
    public void OnStartPositionChanged(Vector2Int newValue)
    {
        options.StartPosition = newValue;
    }

    public void OnEndPositionChanged(Vector2Int newValue)
    {
        options.EndPosition = newValue;
    }

    public void OnConfirmButtonClicked()
    {
        options.SaveStateToDisk();
        AnimatePopup(up: false);
    }

    public void OnOptionsButtonClicked()
    {
        AnimatePopup(up: true);
    }

    private void AnimatePopup(bool up)
    {
        // options button is always hidden on the beggining of animation
        buttonOptions.gameObject.SetActive(false);

        // TODO: launch animation coroutine
        pupup.SetActive(up);

        // preview option button if pop-up goes down
        buttonOptions.gameObject.SetActive(!up);
    }
}

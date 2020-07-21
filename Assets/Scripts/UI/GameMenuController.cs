﻿using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// TODO: this should be separated in multiple controllers
public class GameMenuController : MonoBehaviour
{
    [SerializeField] private Button buttonNext;
    [SerializeField] private Button buttonHome;
    [SerializeField] private Button buttonHome1;
    [SerializeField] Button buttonFinish;
    [SerializeField] private Button buttonGo;
    [SerializeField] private PopupAnimator pupupAnimator;
    [SerializeField] private GameManager gameManager;
    [SerializeField] private Scoreboard scoreboard;
    [SerializeField] private ScrollRect resultList;
    [SerializeField] private GameObject resultPopup;
    [SerializeField] private PathfinderResultViewholder resultViewholderTemplate;



    private void Start()
    {
        RegisterUICallbacks();

        gameManager.RunFinishedEvent.AddListener(OnNewResult);
        gameManager.NewStateGeneratedEvent.AddListener(() => { buttonGo.gameObject.SetActive(true); });
    }

    private void RegisterUICallbacks()
    {
        buttonHome?.onClick.AddListener(OnButtonHomeClicked);
        buttonHome1?.onClick.AddListener(OnButtonHomeClicked);
        buttonNext?.onClick.AddListener(OnButtonNextClicked);
        buttonFinish?.onClick.AddListener(OnButtonFinishClicked);
        buttonGo?.onClick.AddListener(OnButtonGoClicked);
    }
    
    private void OnButtonHomeClicked()
    {
        // load home, quick and dirty
        SceneManager.LoadScene(0);
    }

    private void OnButtonNextClicked()
    {
        gameManager.GenerateNewState();
        pupupAnimator.Animate(up: false);
    }
    
    private void OnButtonFinishClicked()
    {
        pupupAnimator.Animate(up: false);
        resultPopup.SetActive(true);

        // TODO: extract this in separate class
        float viewholderHeight = ((RectTransform)resultViewholderTemplate.transform).rect.height;
        Vector3 currentElementPosition = ((RectTransform)resultViewholderTemplate.transform).anchoredPosition;
        for (int i = 0; i < scoreboard.Results.Count; ++i)
        {
            currentElementPosition.y = viewholderHeight / 2 + i * viewholderHeight;
            var resultView = Instantiate(resultViewholderTemplate, resultList.content.transform);
            ((RectTransform)resultView.transform).anchoredPosition = currentElementPosition;
            resultView.PopulateView(scoreboard.Results[i]);
        }

        ((RectTransform)resultList.content.transform).sizeDelta = new Vector2(0, viewholderHeight * scoreboard.Results.Count);
    }

    private void OnNewResult(PathfinderResult result)
    {
        // TODO: clean this mess

        scoreboard.AddResult(result);
        pupupAnimator.Animate(up: true);
    }

    public void OnButtonGoClicked()
    {
        buttonGo.gameObject.SetActive(false);
        gameManager.StartPathfinders();
    }
}


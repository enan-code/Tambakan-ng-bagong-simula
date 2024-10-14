using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;


public class SwipeController : MonoBehaviour, IEndDragHandler
{
    [SerializeField] int maxPage; // Total number of pages
    int currentPage;
    Vector3 targetPos;
    [SerializeField] Vector3 pageStep; // The distance to move for each page
    [SerializeField] RectTransform LevelPagesRect; // The UI element containing pages

    [SerializeField] float tweenTime;
    [SerializeField] LeanTweenType tweenType;
    float dragThreshould;

    [SerializeField] Image[] barImage;
    [SerializeField] Button prevbtn, nxtBtn; 
    [SerializeField] Sprite barClosed, BarOpen;

    public void Awake()
    {
        currentPage = 1;
        targetPos = LevelPagesRect.localPosition;
        dragThreshould = Screen.width / 15;
        UpdateArrowButton();
    }

    public void Next()
    {
        if (currentPage < maxPage)
        {
            currentPage++;
            targetPos += pageStep;
            MovePage();
        }
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (Math.Abs(eventData.position.x - eventData.pressPosition.x) < dragThreshould)
        {
            if (eventData.position.x > eventData.pressPosition.x) Previous();
            else Next();
        }
        else
        {
            MovePage();
        }
    }

    public void Previous()
    {
        if (currentPage > 1) // Ensure we don’t go below the first page
        {
            currentPage--;
            targetPos -= pageStep;
            MovePage();
        }
    }

    void MovePage()
    {
        LevelPagesRect.LeanMoveLocal(targetPos, tweenTime).setEase(tweenType);
        UpdateBar();
        UpdateArrowButton();
    }
    void UpdateBar()
    {
        foreach (var item in barImage)
        {
            item.sprite = barClosed;
        }
        barImage[currentPage - 1].sprite = BarOpen;
    }
    public void UpdateArrowButton()
    {
        nxtBtn.interactable = true;
        prevbtn.interactable = true;
        if (currentPage == 1) prevbtn.interactable = false;
        else if (currentPage==maxPage) nxtBtn.interactable = false;

    }
}

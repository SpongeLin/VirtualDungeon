using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class GameEventSelection {
    public string selectionContent;

    public abstract void SelectionExcite();
}
namespace nGameEventSelection
{
    public class DeleteCard : GameEventSelection
    {
        public DeleteCard()
        {
            selectionContent = "刪除一張卡";
        }
        public override void SelectionExcite()
        {
            LobbyManager.instance.deal.StartPickCard("Delete");
        }
    }
    public class CopyeCard : GameEventSelection
    {
        public CopyeCard()
        {
            selectionContent = "複製一張卡";
        }
        public override void SelectionExcite()
        {
            LobbyManager.instance.deal.StartPickCard("Copy");
        }
    }
}
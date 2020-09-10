using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class CharacterCreator
{

    public static void TestCreat(CharView view , string charName)
    {
        CharData data = new CharData();
        data.charName = charName;
        data.charShowName = charName;
        data.charView = view;
        view.character = data;
    }

}

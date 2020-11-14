using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemCreator 
{
    public static Item CreateItem(string itemName)
    {
        switch (itemName)
        {
            case "Heart":
                return new nItem.Heart();
            case "Money":
                return new nItem.Money();
            case "Gloves":
                return new nItem.Gloves();
            case "MagicBook":
                return new nItem.MagicBook();
            case "Battery":
                return new nItem.Battery();
            case "Armor":
                return new nItem.Armor();
        }

        return null;
    }
}
public abstract class Item
{
    public string itemName;
    public string itemShowName;
    public string itemDescription;

    public abstract void Equip(CharacterDataPack cdp);
    public abstract void Battle(CharData chara);
}

namespace nItem
{
    public class Heart : Item
    {
        public Heart()
        {
            itemName = "Heart";
            itemShowName = "心臟";
            itemDescription = "角色最大生命值+15";
        }

        public override void Battle(CharData chara)
        {
            
        }

        public override void Equip(CharacterDataPack cdp)
        {
            cdp.maxHealth += 15;
            cdp.currentHealth += 15;
        }
    }

    public class Money : Item
    {
        public Money()
        {
            itemName = "Money";
            itemShowName = "錢包";
            itemDescription = "獲得金錢";
        }

        public override void Battle(CharData chara)
        {

        }

        public override void Equip(CharacterDataPack cdp)
        {

        }
    }
    public class  Gloves: Item
    {
        public Gloves()
        {
            itemName = "Gloves";
            itemShowName = "力量手套";
            itemDescription = "力量+5";
        }

        public override void Battle(CharData chara)
        {

        }

        public override void Equip(CharacterDataPack cdp)
        {
            cdp.power += 5;
        }
    }
    public class MagicBook : Item
    {
        public MagicBook()
        {
            itemName = "MagicBook";
            itemShowName = "魔導書";
            itemDescription = "角色基礎魔力+2點";
        }

        public override void Battle(CharData chara)
        {

        }

        public override void Equip(CharacterDataPack cdp)
        {
            cdp.magic += 2;
        }
    }
    public class Battery : Item
    {
        public Battery()
        {
            itemName = "Battery";
            itemShowName = "電池";
            itemDescription = "角色最大能量+1";
        }

        public override void Battle(CharData chara)
        {

        }

        public override void Equip(CharacterDataPack cdp)
        {
            cdp.maxEnergy += 1;
        }
    }
    public class Armor : Item
    {
        public Armor()
        {
            itemName = "Armor";
            itemShowName = "盔甲";
            itemDescription = "角色基礎護盾+10";
        }

        public override void Battle(CharData chara)
        {

        }

        public override void Equip(CharacterDataPack cdp)
        {
            cdp.armor += 10;
        }
    }

}
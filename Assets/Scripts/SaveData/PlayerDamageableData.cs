using System.Collections.Generic;

[System.Serializable]
public class PlayerDamageableData
{
    public int maxHealth;
    public int health; 
    public List<int> damageModifiers;

    public List<int> armorModifiers;

    public PlayerDamageableData(PlayerDamageable playerDamageable)
    {
        maxHealth = playerDamageable.MaxHealth;
        health = playerDamageable.health;
        
        

        // Damage stat
        
        damageModifiers = new List<int>(playerDamageable.damage.GetModifiers());

        // Armor stat
        
        armorModifiers = new List<int>(playerDamageable.armor.GetModifiers());
    }
}

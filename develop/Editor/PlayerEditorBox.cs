using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Player))]
public class PlayerEditorBox : Editor
{
    SerializedProperty damage;
    SerializedProperty heal;
    SerializedProperty stamina;
    SerializedProperty maxhpup;
    SerializedProperty maxhpdown;

    private void OnEnable()
    {
        damage = serializedObject.FindProperty("debugTest_damage");
        heal = serializedObject.FindProperty("debugTest_heal");
        stamina = serializedObject.FindProperty("debugTest_stamina");
        maxhpup = serializedObject.FindProperty("debugTest_maxhpup");
        maxhpdown = serializedObject.FindProperty("debugTest_maxhpdown");
    }

    public override void OnInspectorGUI()
    {
        float damage = this.damage.floatValue;
        float heal = this.heal.floatValue;
        float stamina = this.stamina.floatValue;
        int maxhpup = this.maxhpup.intValue;
        int maxhpdown = this.maxhpdown.intValue;

        base.OnInspectorGUI();

        Player player = (Player)target;
        GUILayout.Label("- Tool Box -");

        if(GUILayout.Button("Damage Player"))
        {
            player.DamagePlayer(damage);
        }

        if (GUILayout.Button("Heal Player"))
        {
            player.HealPlayer(heal);
        }


        if (GUILayout.Button("Use Stamina"))
        {
            player.UseStamina(stamina);
        }

        if (GUILayout.Button("Max HP Up"))
        {
            player.Status.MaxHPup(maxhpup);
        }

        if (GUILayout.Button("Max HP Down"))
        {
            player.Status.MaxHPdown(maxhpdown);
        }


    }
}

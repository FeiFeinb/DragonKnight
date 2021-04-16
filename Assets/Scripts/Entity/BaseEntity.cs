using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace RPG.Entity
{

    public class BaseEntity : MonoBehaviour
    {
        public string EntityID => entityID;
        [SerializeField] private string entityID;
    }
}

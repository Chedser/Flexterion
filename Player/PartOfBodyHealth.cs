using UnityEngine;
public class PartOfBodyHealth : MonoBehaviour
{

 public   Health health;

    public enum PartOfBody {Body, Head, Limb }

    public PartOfBody partOfBody;

    private void Awake()
    {

        health = GetComponentInParent<Health>();

    }

}

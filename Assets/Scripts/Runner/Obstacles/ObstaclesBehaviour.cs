using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesBehaviour : MonoBehaviour {

    public enum TypeObstacle { down = 0, up = 1, nothing = 2 }
    public enum SpriteObstacle { aviator, bird, cactus, rock, nothing }

    public TypeObstacle typeObstacle;
    public SpriteObstacle spriteObstacle;

    [HideInInspector] public BoxCollider2D[] collidersObstacles;
    public Sprite[] spriteGroup;

    public List<string> keySequence, complementarySequence;
    public string KeyPlusComplementary;

    public float levelDificuty;

    private void Awake() {

        this.transform.localRotation = Quaternion.Euler(Vector3.zero);

    }

    private void Update() {

        if (RunnerController.instace.currentState == RunnerController.State.beforeRunner) {

            levelDificuty = 0;

        }

        RandomSprite(this.typeObstacle);

    }

    private void CollidersActivate() {

        if (typeObstacle == TypeObstacle.down) { collidersObstacles[0].enabled = true; collidersObstacles[1].enabled = false; }
        else if (typeObstacle == TypeObstacle.up) { collidersObstacles[0].enabled = false; collidersObstacles[1].enabled = true; }

    }

    private void RandomType(string nameObstacle) {

        string nameOfObstacle = nameObstacle;

        if (nameObstacle != "Slot (01)" && nameObstacle != "Slot (13)" && nameObstacle != "Slot (25)") return;

            else {

                int indexOStartbstacle = int.Parse(nameObstacle.Substring(nameObstacle.Length - 3, 2));
                int indexLastObstacle = indexOStartbstacle + 11;

                KeyPlusComplementary = keySequence[Random.Range(0, keySequence.Capacity - 1)] + complementarySequence[Random.Range(0, complementarySequence.Capacity - 1)];

                if (ObstaclesController.instace.sequence.Contains(KeyPlusComplementary)) {

                    RandomType(nameOfObstacle);
                    return;
                
                }
                else {
                    
                    if(ObstaclesController.instace.sequence != "") ObstaclesController.instace.sequence += ", " + KeyPlusComplementary;
                    else ObstaclesController.instace.sequence = KeyPlusComplementary;

                }

                int counter = 0; TypeObstacle type = TypeObstacle.nothing;
                for (int i = indexOStartbstacle; i < indexLastObstacle + 1; i++) {

                    counter++;

                    string typeString = ObstaclesController.instace.sequence.Substring(ObstaclesController.instace.sequence.Length - (13 - counter), 1);

                    if (typeString == "0") type = TypeObstacle.down;
                        else if(typeString == "1") type = TypeObstacle.up;
                            else type = TypeObstacle.nothing;

                    ObstaclesController.instace.pointsObstacles[i - 1].transform.GetChild(1).gameObject
                    .GetComponent<ObstaclesBehaviour>().typeObstacle = type;

                    if (levelDificuty == 0) {

                            ObstaclesController.instace.pointsObstacles[i - 1].SetActive(true);

                        if (int.Parse(ObstaclesController.instace.pointsObstacles[i - 1].transform.name.Substring(6, 2)) % 2 == 0) {

                            ObstaclesController.instace.pointsObstacles[i - 1].SetActive(false);

                        }

                    }
                        else if (levelDificuty < 3) {

                                ObstaclesController.instace.pointsObstacles[i - 1].SetActive(true);

                            if (ObstaclesController.instace.pointsObstacles[i - 1].transform.name.Contains("2)") ||
                                ObstaclesController.instace.pointsObstacles[i - 1].transform.name.Contains("6)") ||
                                ObstaclesController.instace.pointsObstacles[i - 1].transform.name.Contains("9)")) {

                                ObstaclesController.instace.pointsObstacles[i - 1].SetActive(false);

                            }

                        }
                            else ObstaclesController.instace.pointsObstacles[i - 1].SetActive(true);

            }

            }

        levelDificuty++;

    }

    private void RandomSprite(TypeObstacle thisTypeObstacle) {

        switch (thisTypeObstacle) {

            case TypeObstacle.down:

                this.spriteObstacle = (SpriteObstacle)Random.Range(2, 4);
                this.GetComponent<SpriteRenderer>().sprite = spriteGroup[1];

                break;

            case TypeObstacle.up:

                this.spriteObstacle = (SpriteObstacle)Random.Range(0, 2);
                this.GetComponent<SpriteRenderer>().sprite = spriteGroup[0];

                break;

            case TypeObstacle.nothing:

                this.spriteObstacle = SpriteObstacle.nothing;
                this.GetComponent<SpriteRenderer>().sprite = null;

                break;

        }

        CollidersActivate();

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.name == "RunnerController") {

            print("Troquei!");
            RandomType(this.gameObject.transform.parent.name);

        }

        if (Progress.instance.endGame) {

            this.gameObject.SetActive(false);

        }

    }

}

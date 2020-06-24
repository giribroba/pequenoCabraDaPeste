using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstaclesBehaviour : MonoBehaviour {

    public enum TypeObstacle { down, up, nothing }

    public enum SpriteObstacle { aviator, bird, cactus, rock, nothing }

    public static GameObject lastObstaclesType, beforeLastObstaclesType;

    public GameObject a, b;

    public TypeObstacle typeObstacle;
    public SpriteObstacle spriteObstacle;

    [HideInInspector] public BoxCollider2D[] collidersObstacles;

    private void Awake() {

        this.transform.localRotation = Quaternion.Euler(Vector3.zero);

    }

    private void Update() {

        a = beforeLastObstaclesType;
        b = lastObstaclesType;

    }

    static float countStartRandom = 0;
    private void RandomType() {

        countStartRandom++;

        beforeLastObstaclesType = lastObstaclesType;
        lastObstaclesType = this.gameObject;

        float random = Random.Range(1, 11);

        if (countStartRandom > 1) {

            if (beforeLastObstaclesType.GetComponent<ObstaclesBehaviour>().typeObstacle == TypeObstacle.nothing) {

                if (lastObstaclesType.GetComponent<ObstaclesBehaviour>().typeObstacle == TypeObstacle.down) {
                    if (random > 0 && random < 6) { this.typeObstacle = TypeObstacle.up; }
                        else if (random > 5 && random < 9) { this.typeObstacle = TypeObstacle.down; }
                            else this.typeObstacle = TypeObstacle.nothing;

                }
                    else if (lastObstaclesType.GetComponent<ObstaclesBehaviour>().typeObstacle == TypeObstacle.up) {

                    if (random > 0 && random < 4) { this.typeObstacle = TypeObstacle.up; }
                        else if (random > 3 && random < 9) { this.typeObstacle = TypeObstacle.down; }
                            else this.typeObstacle = TypeObstacle.nothing;

                    }
                        else {

                            if (random > 0 && random < 6) { this.typeObstacle = TypeObstacle.down; }
                            else this.typeObstacle = TypeObstacle.up;

                        }

            }
                else if (beforeLastObstaclesType.GetComponent<ObstaclesBehaviour>().typeObstacle == TypeObstacle.up) {

                    if (lastObstaclesType.GetComponent<ObstaclesBehaviour>().typeObstacle == TypeObstacle.down) {
                        if (random > 0 && random < 4) { this.typeObstacle = TypeObstacle.up; }
                            else if (random > 3 && random < 8) { this.typeObstacle = TypeObstacle.down; }
                                else this.typeObstacle = TypeObstacle.nothing;

                    }
                        else if (lastObstaclesType.GetComponent<ObstaclesBehaviour>().typeObstacle == TypeObstacle.up) {

                            if (random > 0 && random < 6) { this.typeObstacle = TypeObstacle.down; }
                                else this.typeObstacle = TypeObstacle.nothing;

                        }
                            else {

                                if (random > 0 && random < 5) { this.typeObstacle = TypeObstacle.up; }
                                    else if (random > 4 && random < 10) { this.typeObstacle = TypeObstacle.down; }
                                        else this.typeObstacle = TypeObstacle.nothing;

                            }

                }
                    else if (beforeLastObstaclesType.GetComponent<ObstaclesBehaviour>().typeObstacle == TypeObstacle.down) {

                        if (lastObstaclesType.GetComponent<ObstaclesBehaviour>().typeObstacle == TypeObstacle.down) {
                            if (random > 0 && random < 6) { this.typeObstacle = TypeObstacle.up; }
                                else this.typeObstacle = TypeObstacle.nothing;

                        }
                            else if (lastObstaclesType.GetComponent<ObstaclesBehaviour>().typeObstacle == TypeObstacle.up) {

                                if (random > 0 && random < 4) { this.typeObstacle = TypeObstacle.up; }
                                    else if (random > 3 && random < 8) { this.typeObstacle = TypeObstacle.down; }
                                        else this.typeObstacle = TypeObstacle.nothing;

                            }
                                else {

                                    if (random > 0 && random < 6) { this.typeObstacle = TypeObstacle.up; }
                                        else if (random > 5 && random < 10) { this.typeObstacle = TypeObstacle.down; }
                                            else this.typeObstacle = TypeObstacle.nothing;

                                }

                    }

            RandomSprite(this.typeObstacle);

        }

    }

    private void RandomSprite(TypeObstacle thisTypeObstacle) {

        switch (thisTypeObstacle) {

            case TypeObstacle.down:

                this.spriteObstacle = (SpriteObstacle)Random.Range(2, 4);

                break;

            case TypeObstacle.up:

                this.spriteObstacle = (SpriteObstacle)Random.Range(0, 2);

                break;

            case TypeObstacle.nothing:

                this.spriteObstacle = SpriteObstacle.nothing;

                break;

        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {

        if (collision.gameObject.name == "RunnerController") {

            print("Troquei!");
            RandomType();

        }

    }

}

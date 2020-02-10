using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

namespace VRArmIK
{
    public class VRArmIK_UI : MonoBehaviour
    {
        public ShoulderTransforms _ShoulderTransforms; 
        public Transform _UI;
        private string copyPath;
        public bool recorder;
        private StreamWriter writer;
        string w,wl,wr;
        private float Timer;
         void Awake()
        {
            int i = PlayerPrefs.GetInt("getCount", 0);
            Timer = 0;
            copyPath = Application.dataPath + "/" + DateTime.Now.ToString("yyyy-MM-dd") + "_Data_" + i + ".csv";
            i++;
            PlayerPrefs.SetInt("getCount", i);
            var file = File.Open(copyPath, FileMode.OpenOrCreate, FileAccess.ReadWrite);
            writer = new StreamWriter(file);
            w = "time" + "," + "leftArm_upperArmRotation_x" + "," + "leftArm_upperArmRotation_y" + "," + "leftArm_upperArmRotation_z" + "," +
                
                
                "leftArm_lowerArmRotation_x" + "," + "leftArm_lowerArmRotation_y" + "," + "leftArm_lowerArmRotation_z" + "," +


                "leftArm_handRotation_x" + "," + "leftArm_handRotation_y" + "," + "leftArm_handRotation_z" + "," +

                "rightArm_upperArmRotation_x" + "," +"rightArm_upperArmRotation_y" + "," +  "rightArm_upperArmRotation_z" + "," + 
                
                "rightArm_lowerArmRotation_x" + "," + "rightArm_lowerArmRotation_y" + "," + "rightArm_lowerArmRotation_z" + "," + 
                
                "rightArm_handRotation_x" + "," + "rightArm_handRotation_y" + "," + "rightArm_handRotation_z";

            writer.WriteLine(w);
            Debug.Log("Creating Classfile: " + copyPath);

        }

        void Update()
        {
            if (_ShoulderTransforms.leftArm != null)
            {
                _UI.GetChild(0).GetComponent<Text>().text = "leftArm - upperArmRotation " + _ShoulderTransforms.leftArm.GetComponent<VRArmIK>().upperArmRotation.eulerAngles;
                _UI.GetChild(1).GetComponent<Text>().text = "leftArm - lowerArmRotation " + _ShoulderTransforms.leftArm.GetComponent<VRArmIK>().lowerArmRotation.eulerAngles;
                _UI.GetChild(2).GetComponent<Text>().text = "leftArm - handRotation " + _ShoulderTransforms.leftArm.GetComponent<VRArmIK>().handRotation.eulerAngles;

            }

            if (_ShoulderTransforms.rightArm != null)
            {
                _UI.GetChild(3).GetComponent<Text>().text = "rightArm - upperArmRotation " + _ShoulderTransforms.rightArm.GetComponent<VRArmIK>().upperArmRotation.eulerAngles;
                _UI.GetChild(4).GetComponent<Text>().text = "rightArm - lowerArmRotation " + _ShoulderTransforms.rightArm.GetComponent<VRArmIK>().lowerArmRotation.eulerAngles;
                _UI.GetChild(5).GetComponent<Text>().text = "rightArm - handRotation " + _ShoulderTransforms.rightArm.GetComponent<VRArmIK>().handRotation.eulerAngles;
            }
            if (recorder)
            {
                Timer += Time.deltaTime;

                if (_ShoulderTransforms.leftArm != null)
                {
                    wl = _ShoulderTransforms.leftArm.GetComponent<VRArmIK>().upperArmRotation.eulerAngles.x.ToString() + "," + _ShoulderTransforms.leftArm.GetComponent<VRArmIK>().upperArmRotation.eulerAngles.y.ToString() + "," + _ShoulderTransforms.leftArm.GetComponent<VRArmIK>().upperArmRotation.eulerAngles.z.ToString();


                    wl += "," + _ShoulderTransforms.leftArm.GetComponent<VRArmIK>().lowerArmRotation.eulerAngles.x + "," + _ShoulderTransforms.leftArm.GetComponent<VRArmIK>().lowerArmRotation.eulerAngles.y
                        + "," + _ShoulderTransforms.leftArm.GetComponent<VRArmIK>().lowerArmRotation.eulerAngles.z;


                     wl += "," + _ShoulderTransforms.leftArm.GetComponent<VRArmIK>().handRotation.eulerAngles.x + "," + _ShoulderTransforms.leftArm.GetComponent<VRArmIK>().handRotation.eulerAngles.y + "," + _ShoulderTransforms.leftArm.GetComponent<VRArmIK>().handRotation.eulerAngles.z; 
                }
                else
                {
                    wl = "0" + "0" + "0";
                }

                if (_ShoulderTransforms.rightArm != null)
                {


                    wr = _ShoulderTransforms.rightArm.GetComponent<VRArmIK>().upperArmRotation.eulerAngles.x.ToString() + "," + _ShoulderTransforms.rightArm.GetComponent<VRArmIK>().upperArmRotation.eulerAngles.y.ToString() + "," + _ShoulderTransforms.rightArm.GetComponent<VRArmIK>().upperArmRotation.eulerAngles.z.ToString();


                    wr += "," + _ShoulderTransforms.rightArm.GetComponent<VRArmIK>().lowerArmRotation.eulerAngles.x + "," + _ShoulderTransforms.rightArm.GetComponent<VRArmIK>().lowerArmRotation.eulerAngles.y
                        + "," + _ShoulderTransforms.rightArm.GetComponent<VRArmIK>().lowerArmRotation.eulerAngles.z;


                    wr += "," + _ShoulderTransforms.rightArm.GetComponent<VRArmIK>().handRotation.eulerAngles.x + "," + _ShoulderTransforms.rightArm.GetComponent<VRArmIK>().handRotation.eulerAngles.y + "," + _ShoulderTransforms.rightArm.GetComponent<VRArmIK>().handRotation.eulerAngles.z;
                }
                else
                {
                    wr = "0" + "0" + "0";
                }

                writer.WriteLine(Timer + "," + wl + "," + wr);

            }

        }

        private void OnApplicationQuit()
        {
            writer.Close();
        }

        public void recorderActivator(Toggle t) {

            print("Recording " + t.isOn);
            if(t.isOn)
                recorder = true;
            else
                recorder = false;
        }

    }
}

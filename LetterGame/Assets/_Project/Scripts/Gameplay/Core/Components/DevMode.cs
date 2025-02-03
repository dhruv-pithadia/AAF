
using System;
using System.Text;
using UnityEngine;
using UnityEngine.UI;
using Leap.HandsModule;
using System.Collections.Generic;
using System.IO;

namespace LetterQuest.Core
{
    public class DevMode : MonoBehaviour
    {
        [SerializeField] private Material opaqueHands;
        [SerializeField] private Material transparentHands;
        [SerializeField] private Toggle saveHandPositionsToggle;
        private SkinnedMeshRenderer rightHandRenderer;
        private SkinnedMeshRenderer leftHandRenderer;
        private Transform rightHandTransform;
        private Transform leftHandTransform;

        private List<Vector3> _rightHandPositions;
        private List<Vector3> _leftHandPositions;
        private StringBuilder _stringBuilder;
        private bool _savePositions;
        private float _timer;

        #region Unity Methods

        private void Start()
        {
            _timer = 5f;
            _savePositions = false;
            saveHandPositionsToggle.gameObject.SetActive(false);
            var hands = FindObjectsOfType<HandBinder>(true);
            leftHandRenderer = hands[0].gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            rightHandRenderer = hands[1].gameObject.GetComponentInChildren<SkinnedMeshRenderer>();
            rightHandTransform = rightHandRenderer.transform.parent.GetChild(0).GetChild(0);
            leftHandTransform = leftHandRenderer.transform.parent.GetChild(0).GetChild(0);
        }

        private void LateUpdate()
        {
            if (_savePositions == false) return;
            _timer -= Time.deltaTime;
            if (_timer > 0f) return;

            _timer = 0.1f;
            _leftHandPositions.Add(leftHandTransform.position);
            _rightHandPositions.Add(rightHandTransform.position);
        }

        private void OnDisable()
        {
            _stringBuilder?.Clear();
            _stringBuilder = null;
            ClearData();
        }

        #endregion

        #region Public Methods

        public void ToggleDevMode(bool value)
        {
            if (value) EnableDevMode();
            else DisableDevMode();
        }

        public void ToggleHandPositionSave(bool value)
        {
            if (value) EnablePositionSave();
            else DisablePositionSave();
        }

        public void SavePositionData()
        {
            if (_savePositions == false) return;
            _stringBuilder = new StringBuilder();
            SaveHandPositions(_leftHandPositions);
            SaveHandPositions(_rightHandPositions, true);
            _savePositions = false;
            ClearData();
        }

        #endregion

        #region Private Methods

        private void EnableDevMode()
        {
            leftHandRenderer.sharedMaterial = opaqueHands;
            rightHandRenderer.sharedMaterial = opaqueHands;
            saveHandPositionsToggle.gameObject.SetActive(true);
        }

        private void DisableDevMode()
        {
            DisablePositionSave();
            leftHandRenderer.sharedMaterial = transparentHands;
            rightHandRenderer.sharedMaterial = transparentHands;
            saveHandPositionsToggle.gameObject.SetActive(false);
        }

        private void EnablePositionSave()
        {
            _savePositions = true;
            _leftHandPositions ??= new List<Vector3>();
            _rightHandPositions ??= new List<Vector3>();
        }

        private void DisablePositionSave()
        {
            saveHandPositionsToggle.isOn = false;
            _savePositions = false;
            ClearData();
        }

        private void SaveHandPositions(List<Vector3> input, bool rightHand = false)
        {
            var count = input.Count;
            if (count == 0) return;
            _stringBuilder.Clear();
            _stringBuilder.AppendJoin(',', input);
            SaveData(_stringBuilder.ToString(), rightHand);
        }

        private void SaveData(string data, bool rightHand)
        {
            var directory = Path.Combine(Application.persistentDataPath, "Data");
            if (Directory.Exists(directory) == false) Directory.CreateDirectory(directory);

            var time = DateTime.Now;
            _stringBuilder.Clear();
            _stringBuilder.Append(rightHand ? "RightHand" : "LeftHand");
            _stringBuilder.Append("_");
            _stringBuilder.Append(time.ToString("M-d-yyyy"));
            _stringBuilder.Append("_");
            _stringBuilder.Append(time.ToString("hh;mm;ss tt"));
            _stringBuilder.Append(".txt");

            var saveFilePath = Path.Combine(directory, _stringBuilder.ToString());
            File.WriteAllText(saveFilePath, data);
        }

        private void ClearData()
        {
            _rightHandPositions?.Clear();
            _leftHandPositions?.Clear();
        }

        #endregion
    }
}

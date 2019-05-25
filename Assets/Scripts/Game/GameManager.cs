﻿using Gfen.Game.Config;
using Gfen.Game.Logic;
using Gfen.Game.Manager;
using Gfen.Game.Presentation;
using Gfen.Game.UI;
using UnityEngine;

namespace Gfen.Game
{
    public class GameManager : MonoBehaviour
    {
        public ConfigSerializableSet configSet;

        public Camera gameCamera;

        public UIManager uiManager;

        private LevelManager m_levelManager;

        public LevelManager LevelManager { get { return m_levelManager; } }

        private LogicGameManager m_logicGameManager;

        private PresentationGameManager m_presentationGameManager;

        private bool m_isInGame;

        private void Start() 
        {
            configSet.Init();

            m_levelManager = new LevelManager();
            m_levelManager.Init();

            uiManager.Init(this);

            m_isInGame = false;
            m_logicGameManager = new LogicGameManager(this);
            m_presentationGameManager = new PresentationGameManager(this, m_logicGameManager);

            uiManager.ShowPage<LevelPage>();
        }

        private void Update() 
        {
            if (m_isInGame)
            {
                var operationType = OperationType.None;
                if (Input.GetKeyUp(KeyCode.UpArrow))
                {
                    operationType = OperationType.Up;
                }
                else if (Input.GetKeyUp(KeyCode.DownArrow))
                {
                    operationType = OperationType.Down;
                }
                else if (Input.GetKeyUp(KeyCode.LeftArrow))
                {
                    operationType = OperationType.Left;
                }
                else if (Input.GetKeyUp(KeyCode.RightArrow))
                {
                    operationType = OperationType.Right;
                }
                else if (Input.GetKeyUp(KeyCode.Space))
                {
                    operationType = OperationType.Wait;
                }

                if (operationType != OperationType.None)
                {
                    m_logicGameManager.Tick(operationType);
                    m_presentationGameManager.RefreshPresentation();
                }

                if (Input.GetKeyUp(KeyCode.Z))
                {
                    m_logicGameManager.Undo();
                    m_presentationGameManager.RefreshPresentation();
                }
                else if (Input.GetKeyUp(KeyCode.R))
                {
                    m_logicGameManager.Redo();
                    m_presentationGameManager.RefreshPresentation();
                }
                else if (Input.GetKeyUp(KeyCode.Q))
                {
                    m_logicGameManager.RestartGame();
                    m_presentationGameManager.RefreshPresentation();
                }
            }
        }

        public void StartGame(int levelId)
        {
            m_logicGameManager.StartGame(configSet.GetLevelConfig(levelId).map);
            m_presentationGameManager.StartPresent();
            m_isInGame = true;
        }
    }
}

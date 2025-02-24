using System.Collections.Generic;
using UnityEngine;

namespace Gfen.Game.UI
{
    public class ChapterPage : UIPage
    {
        public Transform chapterListRootTransform;

        public ChapterCell templateChapterCell;

        private List<ChapterCell> m_chapterCells = new List<ChapterCell>();

        protected override void OnShow()
        {
            base.OnShow();

            m_gameManager.LevelManager.SetStayChapterIndex(-1);

            var chapterConfigs = m_gameManager.gameConfig.chapterConfigs;

            // Save chapters into a list
            while (m_chapterCells.Count < chapterConfigs.Length)
            {
                var chapterCell = UIUtils.InstantiateUICell(chapterListRootTransform, templateChapterCell);
                m_chapterCells.Add(chapterCell);
            }

            // Show and set button content
            // Hide excess instantiations of cells
            for (var i = 0; i < chapterConfigs.Length; i++)
            {
                m_chapterCells[i].Show(m_gameManager);
                m_chapterCells[i].SetContent(i);
            }
            for (var i = chapterConfigs.Length; i < m_chapterCells.Count; i++)
            {
                m_chapterCells[i].Hide();
            }
        }
    }
}

using GameLauncher.Models.Commons;
using UnityEngine;

namespace GameLauncher.Models.DomainObjects
{
    public class GameData
    {
        public GameData(string title, string genre, string summary, Executable exeAsExecutable, Sprite sprite, Executable descriptionAsExecutable)
        {
            Title = title;
            Genre = genre;
            Summary = summary;
            ExeAsExecutable = exeAsExecutable;
            Sprite = sprite;
            DescriptionAsExecutable = descriptionAsExecutable;
        }

        /// <summary>
        /// タイトル名
        /// </summary>
        public string Title { get; private set; }

        /// <summary>
        /// ジャンル名
        /// </summary>
        public string Genre { get; private set; }

        /// <summary>
        /// 選択されている際に表示される簡易説明文
        /// </summary>
        public string Summary { get; private set; }

        /// <summary>
        /// 実行ファイルのショートカットを起動するExecutableクラス
        /// </summary>
        /// <remarks>
        /// ショートカットでないと動かない場合があります
        /// </remarks>
        public Executable ExeAsExecutable { get; private set; }

        /// <summary>
        /// アイコンのテクスチャ
        /// </summary>
        public Sprite Sprite { get; private set; }

        /// <summary>
        /// 詳細説明が書いてあるファイルを起動するExecutableクラス
        /// </summary>
        public Executable DescriptionAsExecutable { get; private set; }
    }
}

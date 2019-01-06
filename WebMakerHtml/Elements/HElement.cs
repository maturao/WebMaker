using WebMaker.Html.General;

namespace WebMaker.Html.Elements
{
    /// <summary>
    /// Třída reprezentující HTML "h" element
    /// </summary>
    public class HElement : OpenElement
    {
        private const int levelMax = 6;
        private const int levelMin = 1;
        private int _level;

        /// <summary>
        /// Vytvoří instanci třídy HElement
        /// </summary>
        /// <param name="level">Header level</param>
        public HElement(int level) => Level = level;

        /// <summary>
        /// Vytvoří instanci třídy HElement s nejnižším levelem
        /// </summary>
        public HElement() : this(levelMin)
        {
        }

        /// <summary>
        /// Header level
        /// </summary>
        public int Level
        {
            get => _level;
            set
            {
                if (value < levelMin)
                {
                    _level = levelMin;
                }
                else if (value > levelMax)
                {
                    _level = levelMax;
                }
                else
                {
                    _level = value;
                }
            }
        }

        /// <summary>
        /// Název elementy uvnitř HTML tagu
        /// </summary>
        public override string TagName => base.TagName + Level;
    }
}
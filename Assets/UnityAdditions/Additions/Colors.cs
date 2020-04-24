using System;
using UnityEngine;
using UnityAdditions;

namespace UnityAdditions.Colors
{

    /// <summary>
    /// Have you ever scoffed at the lack of color choices found within UnityEngine.Color? Don't worry, friend, this class has
    /// every color from A to Z listed on wikipedia. Every instance is implicitly synonymous to UnityEnine.Color, meaning you
    /// can bounce between the two at your leisure. To find a list of the colors contained in this struct at
    /// en.wikipedia.org/wiki/List_of_colors:[_A-F|_G-M|N-Z]
    /// </summary>
    public struct Colors : IEquatable<Colors>, IEquatable<Color>
    {
        #region Fields
        /// <summary>
        /// Red component of this color object
        /// </summary>
        public float r;
        /// <summary>
        /// Green component of this color object
        /// </summary>
        public float g;
        /// <summary>
        /// Blue component of this color object
        /// </summary>
        public float b;
        /// <summary>
        /// Transparency of this color
        /// </summary>
        public float a;
        #endregion

        #region Constructors
        /// <summary>
        /// Create a custom color with a default opacity of 1
        /// </summary>
        /// <param name="r">Red component of this color</param>
        /// <param name="g">Green component of this color</param>
        /// <param name="b">Blue component of this color</param>
        public Colors(float r, float g, float b) { this.r = r; this.g = g; this.b = b; this.a = 1; }
        /// <summary>
        /// Create a custom color with a custom opacity
        /// </summary>
        /// <param name="r">Red component of this color</param>
        /// <param name="g">Green component of this color</param>
        /// <param name="b">Blue component of this color</param>
        /// <param name="a">Alpha comonent of this color</param>
        public Colors(float r, float g, float b, float a) { this.r = r; this.g = g; this.b = b; this.a = a; }
        #endregion

        #region Properties
        /// <summary>
        /// Access or mutate each individual color component by index
        /// </summary>
        /// <param name="i">Index of correspondng component [0..3]</param>
        /// <returns>Value of the component at the requested indes</returns>
        public float this[int i]
        {
            get
            {
                if (i < 0 || i > 3) throw new IndexOutOfRangeException($"Cannot retrieve value from invalid color component at index {i}");

                if (i == 0) return r;
                else if (i == 1) return g;
                else if (i == 2) return b;
                else return a;
            }
            set
            {
                if (i < 0 || i > 3) throw new IndexOutOfRangeException($"Cannot assign value to invalid color component at index {i}");

                value = Mathf.Clamp01(value);

                if (i == 0) r = value;
                else if (i == 1) g = value;
                else if (i == 2) b = value;
                else a = value;
            }
        }
        /// <summary>
        /// Check for color equality using the indexor
        /// </summary>
        /// <param name="c">Colors to compare to</param>
        /// <returns>True of colors are equal</returns>
        public bool this[Colors c] { get => this == c; }
        /// <summary>
        /// Check for color equality using the indexor
        /// </summary>
        /// <param name="c">Color to compare to</param>
        /// <returns>True of colors are equal</returns>
        public bool this[Color c] { get => this == c; }
        #endregion

        #region Colors
        //TODO Add a comment to each... and... every... property........ have fun...
        //TODO Complete List (Left off at C; Completed AB)
        public static Colors AbsoluteZero { get => new Colors(.00f, .28f, .73f); }
        public static Colors AcidGreen { get => new Colors(.69f, .75f, .10f); }
        public static Colors Aero { get => new Colors(.49f, .73f, .91f); }
        public static Colors AeroBlue { get => new Colors(.79f, 1.0f, .90f); }
        public static Colors AfricanViolet { get => new Colors(.70f, .52f, .75f); }
        public static Colors AirSuperiorityBlue { get => new Colors(.45f, .63f, .76f); }
        public static Colors Alabaster { get => new Colors(.93f, .92f, .88f); }
        public static Colors AliceBlue { get => new Colors(.94f, .87f, .80f); }
        public static Colors AlloyOrange { get => new Colors(.77f, .38f, .60f); }
        public static Colors Almond { get => new Colors(.94f, .87f, .80f); }
        public static Colors Amaranth { get => new Colors(.90f, .17f, .31f); }
        public static Colors AmaranthMP { get => new Colors(.62f, .17f, .41f); }
        public static Colors AmaranthPink { get => new Colors(.95f, .61f, .73f); }
        public static Colors AmaranthPurple { get => new Colors(.67f, .15f, .31f); }
        public static Colors AmaranthRed { get => new Colors(.83f, .13f, .18f); }
        public static Colors Amazon { get => new Colors(.23f, .48f, .34f); }
        public static Colors Amber { get => new Colors(1.0f, .75f, .00f); }
        public static Colors AmberOrange { get => new Colors(1.0f, .49f, .00f); }
        public static Colors Amethyst { get => new Colors(.60f, .40f, .80f); }
        public static Colors AndroidGreen { get => new Colors(.64f, .78f, .22f); }
        public static Colors AntiqueBrass { get => new Colors(.80f, .58f, .46f); }
        public static Colors AntiqueBronze { get => new Colors(.40f, .36f, .12f); }
        public static Colors AntiqueFuchsia { get => new Colors(.57f, .36f, .51f); }
        public static Colors AntiqueRuby { get => new Colors(.52f, .11f, .18f); }
        public static Colors AntiqueWhite { get => new Colors(.98f, .92f, .84f); }
        public static Colors Ao { get => new Colors(.00f, .50f, .00f); }
        public static Colors AppleGreen { get => new Colors(.55f, .71f, .00f); }
        public static Colors Apricot { get => new Colors(.98f, .81f, .69f); }
        public static Colors Aqua { get => new Colors(.00f, .50f, .00f); }
        public static Colors Aquamarine { get => new Colors(.50f, 1.0f, .83f); }
        public static Colors ArcticLime { get => new Colors(.82f, 1.0f, .08f); }
        public static Colors ArmyGreen { get => new Colors(.29f, .33f, .13f); }
        public static Colors Artichoke { get => new Colors(.56f, .59f, .47f); }
        public static Colors ArylideYellow { get => new Colors(.91f, .84f, .42f); }
        public static Colors AshGray { get => new Colors(.70f, .75f, .71f); }
        public static Colors AshGrey { get => new Colors(.70f, .75f, .71f); }
        public static Colors Asparagus { get => new Colors(.53f, .66f, .42f); }
        public static Colors AtomicTangerine { get => new Colors(1.0f, .60f, .40f); }
        public static Colors Auburn { get => new Colors(.65f, .16f, .16f); }
        public static Colors Aureolin { get => new Colors(.99f, .93f, .00f); }
        public static Colors Avocado { get => new Colors(.34f, .51f, .01f); }
        public static Colors Azure { get => new Colors(.00f, .50f, 1.0f); }
        public static Colors AzureX11 { get => new Colors(.94f, 1.0f, 1.0f); }
        public static Colors BabyBlue { get => new Colors(.54f, .81f, .94f); }
        public static Colors BabyBlueEyes { get => new Colors(.63f, .79f, .95f); }
        public static Colors BabyPink { get => new Colors(.96f, .76f, .76f); }
        public static Colors BabyPowder { get => new Colors(1.0f, 1.0f, .98f); }
        public static Colors BakerMillerPink { get => new Colors(1.0f, .57f, .69f); }
        public static Colors BananaMania { get => new Colors(.98f, .91f, .71f); }
        public static Colors BarbiePink { get => new Colors(.85f, .09f, .52f); }
        public static Colors BarnRed { get => new Colors(.49f, .04f, .01f); }
        public static Colors BattleshipGray { get => new Colors(.52f, .52f, .51f); }
        public static Colors BattleshipGrey { get => new Colors(.52f, .52f, .51f); }
        public static Colors BeauBlue { get => new Colors(.74f, .83f, .90f); }
        public static Colors Beaver { get => new Colors(.62f, .51f, .44f); }
        public static Colors Beige { get => new Colors(.96f, .96f, .86f); }
        public static Colors BdazzledBlue { get => new Colors(.18f, .35f, .58f); }
        public static Colors BigDipORuby { get => new Colors(.61f, .15f, .26f); }
        public static Colors Bisque { get => new Colors(1.0f, .89f, .77f); }
        public static Colors Bistre { get => new Colors(.24f, .17f, .12f); }
        public static Colors BistreBrown { get => new Colors(.59f, .44f, .09f); }
        public static Colors BitterLemon { get => new Colors(.79f, .88f, .05f); }
        public static Colors BitterLime { get => new Colors(.75f, 1.0f, .00f); }
        public static Colors Bittersweet { get => new Colors(1.0f, .44f, .37f); }
        public static Colors BittersweetShimmer { get => new Colors(.75f, .31f, .32f); }
        public static Colors Black { get => new Colors(.00f, .00f, .00f); }
        public static Colors BlackBean { get => new Colors(.24f, .05f, .01f); }
        public static Colors BlackChocolate { get => new Colors(.11f, .09f, .07f); }
        public static Colors BlackCoffee { get => new Colors(.23f, .18f, .18f); }
        public static Colors BlackCoral { get => new Colors(.33f, .38f, .44f); }
        public static Colors BlackOlive { get => new Colors(.23f, .24f, .21f); }
        public static Colors BlackShadows { get => new Colors(.75f, .69f, .70f); }
        public static Colors BlanchedAlmond { get => new Colors(1.0f, .92f, .80f); }
        public static Colors BlastOffBronze { get => new Colors(.65f, .44f, .39f); }
        public static Colors BleuDeFrance { get => new Colors(.19f, .55f, .93f); }
        public static Colors BlizzardBlue { get => new Colors(.67f, .90f, .93f); }
        public static Colors Blond { get => new Colors(.98f, .94f, .75f); }
        public static Colors Blonde { get => new Colors(.98f, .94f, .75f); }
        public static Colors BloodRed { get => new Colors(.40f, .00f, 1.0f); }
        public static Colors Blue { get => new Colors(.00f, .00f, 1.0f); }
        public static Colors CrayolaBlue { get => new Colors(.12f, .46f, 1.0f); }
        public static Colors MunsellBlue { get => new Colors(.00f, .58f, .69f); }
        public static Colors NCSBlue { get => new Colors(.00f, .53f, .74f); }
        public static Colors PantoneBlue { get => new Colors(.00f, .09f, .66f); }
        public static Colors PigmentBlue { get => new Colors(.20f, .20f, .60f); }
        public static Colors RYBBlue { get => new Colors(.01f, .28f, 1.0f); }
        public static Colors BlueBell { get => new Colors(.64f, .64f, .82f); }
        public static Colors BlueGray { get => new Colors(.40f, .60f, .80f); }
        public static Colors BlueGrey { get => new Colors(.40f, .60f, .80f); }
        public static Colors BlueGreen { get => new Colors(.05f, .60f, .73f); }
        public static Colors ColorWheelBlueGreen { get => new Colors(.02f, .31f, .25f); }
        public static Colors BlueJeans { get => new Colors(.36f, .68f, .93f); }
        public static Colors BlueSapphire { get => new Colors(.07f, .38f, .50f); }
        public static Colors BlueViolet { get => new Colors(.54f, .17f, .89f); }
        public static Colors CrayolaBlueViolet { get => new Colors(.45f, .40f, .74f); }
        public static Colors ColorWheelBlueViolet { get => new Colors(.30f, .10f, .50f); }
        public static Colors BlueYonder { get => new Colors(.31f, .45f, .65f); }
        public static Colors Bluetiful { get => new Colors(.24f, .41f, .91f); }
        public static Colors Blush { get => new Colors(.87f, .36f, .51f); }
        public static Colors Bole { get => new Colors(.47f, .27f, .23f); }
        public static Colors Bone { get => new Colors(.89f, .85f, .79f); }
        public static Colors BottleGreen { get => new Colors(.00f, .42f, .31f); }
        public static Colors Brandy { get => new Colors(.53f, .25f, .25f); }
        public static Colors BrickRed { get => new Colors(.80f, .25f, .33f); }
        public static Colors BrightGreen { get => new Colors(.40f, 1.0f, .00f); }
        public static Colors BrightLilac { get => new Colors(.85f, .57f, .94f); }
        public static Colors BrightMaroon { get => new Colors(.76f, .13f, .28f); }
        public static Colors BrightNavyBlue { get => new Colors(.10f, .45f, .82f); }
        public static Colors CrayolaBrightYellow { get => new Colors(1.0f, .67f, .11f); }
        public static Colors BrilliantRose { get => new Colors(1.0f, .33f, .64f); }
        public static Colors BrinkPink { get => new Colors(.98f, .38f, .50f); }
        public static Colors BritishRacingGreen { get => new Colors(.00f, .26f, .15f); }
        public static Colors Bronze { get => new Colors(.80f, .50f, .20f); }
        public static Colors Brown { get => new Colors(.53f, .33f, .04f); }
        public static Colors BrownSugar { get => new Colors(.69f, .43f, .30f); }
        public static Colors BrunswickGreen { get => new Colors(.11f, .30f, .24f); }
        public static Colors BudGreen { get => new Colors(.48f, .71f, .38f); }
        public static Colors Buff { get => new Colors(.94f, .86f, .51f); }
        public static Colors Burgundy { get => new Colors(.50f, .00f, .13f); }
        public static Colors Burlywood { get => new Colors(.87f, .72f, .53f); }
        public static Colors BurnishedBrown { get => new Colors(.63f, .48f, .45f); }
        public static Colors BurntOrange { get => new Colors(.80f, .33f, .00f); }
        public static Colors BurntSienna { get => new Colors(.91f, .45f, .32f); }
        public static Colors BurntAmber { get => new Colors(.54f, .20f, .14f); }
        public static Colors Byzantine { get => new Colors(.74f, .20f, .64f); }
        public static Colors Byzantium { get => new Colors(.44f, .16f, .39f); }

        #endregion

        #region Methods
        //TODO add some functionality or creation methods, i.e. a way to create
        //based on a hex number/string (#AAFE42), or create colors through HSV or another standard
        /// <summary>
        /// Combine and return the average of two or more colors
        /// </summary>
        /// <param name="combine">Collection or comma-separated parameter list of colors to combine. Length must be greater than 2</param>
        /// <returns>Average color based on the provided collection</returns>
        public static Colors Combine(params Colors[] combine)
        {
            if (combine.Length < 2)
            {
                if (!UnityAdditionsSettings.SuppressErrors) Debug.LogError($"{nameof(Colors)}.{nameof(Combine)} was called with a params list containing fewer than two items, the item at index zero or a default color instance has been returned");
                if (combine.Length == 1) return combine[0];
                else return new Colors();
            }

            Colors final = new Colors(0, 0, 0, 0);
            foreach (Colors c in combine) final += c;
            final /= combine.Length;
            return final;
        }
        #endregion

        #region Implementations
        public bool Equals(Color other)
        {
            return r == other.r && g == other.g && b == other.b && a == other.a;
        }
        public bool Equals(Colors other)
        {
            return r == other.r && g == other.g && b == other.b && a == other.a;
        }
        #endregion

        #region Overrides
        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            else if (obj is Colors) return Equals((Colors)obj);
            else if (obj is Color) return Equals((Color)obj);
            else return false;
        }
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }
        public override string ToString()
        {
            return $"Colors(r: {r}, g: {g}, b: {b}, a: {a})";
        }
        #endregion

        #region Operators
        public static implicit operator Color(Colors c) => new Color(c.r, c.g, c.b, c.a);
        public static implicit operator Colors(Color c) => new Colors(c.r, c.g, c.b, c.a);
        public static implicit operator Vector4(Colors c) => new Vector4(c.r, c.g, c.b, c.a);
        public static implicit operator Colors(Vector4 v) => new Colors(v.x, v.y, v.z, v.w);

        //arithmetic operators should be overloaded
        public static Colors operator +(Colors a, Colors b) => new Colors(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
        public static Colors operator +(Color a, Colors b) => new Colors(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
        public static Colors operator +(Colors a, Color b) => new Colors(a.r + b.r, a.g + b.g, a.b + b.b, a.a + b.a);
        public static Colors operator -(Colors a, Colors b) => new Colors(a.r - a.r, b.g - b.g, a.b - b.b, a.a - b.a);
        public static Colors operator -(Color a, Colors b) => new Colors(a.r - a.r, b.g - b.g, a.b - b.b, a.a - b.a);
        public static Colors operator -(Colors a, Color b) => new Colors(a.r - a.r, b.g - b.g, a.b - b.b, a.a - b.a);
        public static Colors operator *(Colors a, Colors b) => new Colors(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
        public static Colors operator *(Color a, Colors b) => new Colors(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
        public static Colors operator *(Colors a, Color b) => new Colors(a.r * b.r, a.g * b.g, a.b * b.b, a.a * b.a);
        public static Colors operator *(Colors a, float b) => new Colors(a.r * b, a.g * b, a.b * b, a.a * b);
        public static Colors operator *(float a, Colors b) => new Colors(b.r * a, b.g * a, b.b * a, b.a * a);
        public static Colors operator /(Colors a, float b) => new Colors(a.r / b, a.g / b, a.b / b, a.a / b);

        public static bool operator ==(Colors lhs, Colors rhs) { return lhs.Equals(rhs); }
        public static bool operator !=(Colors lhs, Colors rhs) { return !lhs.Equals(rhs); }
        public static bool operator ==(Colors lhs, Color rhs) { return lhs.Equals(rhs); }
        public static bool operator !=(Colors lhs, Color rhs) { return !lhs.Equals(rhs); }
        public static bool operator ==(Color lhs, Colors rhs) { return lhs.Equals(rhs); }
        public static bool operator !=(Color lhs, Colors rhs) { return !lhs.Equals(rhs); }
        #endregion
    }
}
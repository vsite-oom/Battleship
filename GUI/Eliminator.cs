using System.ComponentModel;

namespace GUI
{
    public enum Eliminator
    {
        [Description("Nema dodira brodova")]
        SurroundingSquareEliminator,
        [Description("Brodovi se smiju dodirivati")]
        SimpleSquareEliminator
    }
}

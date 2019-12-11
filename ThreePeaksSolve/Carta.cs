using System;
using System.Collections;
using System.Collections.Generic;

namespace ThreePeaksSolve
{
    public class Carta
    {
        public bool EsPico { get; internal set; }

        public Palo Palo { get; set; }

        public int Numero { get; set; }

        public Carta(int numero, Palo palo)
        {
            this.Numero = numero;
            this.Palo = palo;
        }

        public override string ToString()
        {
            string palo = string.Empty;

            switch (Numero)
            {
                case 1:
                    palo = " A";
                    break;
                case 11:
                    palo = " J";
                    break;
                case 12:
                    palo = " Q";
                    break;
                case 13:
                    palo = " K";
                    break; 
                default:
                    palo = " " + Numero.ToString();
                    break;
            }
            // ♦  ♥  ♠  ♣
            switch (Palo)
            {
                case Palo.Corazon:
                    palo += "♥";
                    break;
                case Palo.Diamante:
                    palo += "♦";
                    break;
                case Palo.Pica:
                    palo += "♠";
                    break;
                case Palo.Trebol:
                    palo += "♣";
                    break;
            }
            if (Numero == 10)
                return $"{palo}";
            else
                return $" {palo}";
        }

    }
}

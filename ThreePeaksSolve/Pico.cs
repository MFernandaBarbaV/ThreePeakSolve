using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;

namespace ThreePeaksSolve
{
    public class Pico : Carta
    {
        #region properties

        public Pico SuperiorIzquierda { get; set; }
        public Pico SuperiorDerecha { get; set; }
        public Pico InferiorIzquierda { get; set; }
        public Pico InferiorDerecha { get; set; }

        public bool Cubierta
        {
            get
            {

                try
                {
                    if (InferiorDerecha == null && InferiorIzquierda == null)
                        return false; // si no tiene cartas abajo, esta descubierta

                    if (InferiorDerecha == null)
                    {
                        return !InferiorIzquierda.Usada; // si no tiene una carta, depende de la otra
                    }

                    if (InferiorIzquierda == null)
                    {
                        return !InferiorDerecha.Usada; // si no tiene una carta, depende de la otra
                    }

                    if (InferiorDerecha.Usada && InferiorIzquierda.Usada)
                    {
                        return false; // si las dos están usadas, no esta cubierta
                    }

                    if (!InferiorDerecha.Usada && !InferiorIzquierda.Usada)
                    {
                        return true; // si las dos están no usadas, la carta esta cubierta
                    }

                    if (!InferiorDerecha.Usada && InferiorIzquierda.Usada)
                    {
                        return true; // con una no usada, esta cubierta la carta
                    }

                    if (InferiorDerecha.Usada && !InferiorIzquierda.Usada)
                    {
                        return true; // con una no usada, esta cubierta la carta
                    }

                    return true; // aqui no debería llegar
                }
                catch (Exception)
                {
                    return true;
                }

            }
        }

        public bool Usada { get; set; }

        #endregion

        public Pico(int numero, Palo palo) : base(numero, palo)
        {
            this.Usada = false;
            this.EsPico = true;
        }

        public Pico(Carta carta, bool esPico) : base(carta.Numero, carta.Palo)
        {
            this.Usada = false;
            this.EsPico = esPico;
        }

        public Pico(Carta carta) : base(carta.Numero, carta.Palo)
        {
            this.Usada = false;
            this.EsPico = true;
        }

        #region 
        
        
        public void ConocerPadres()
        {
            InferiorDerecha.SuperiorIzquierda = this;
            InferiorIzquierda.SuperiorDerecha = this;

            if (InferiorIzquierda.InferiorIzquierda != null)
            {
                InferiorIzquierda.ConocerPadres();
            }

            if (InferiorDerecha.InferiorIzquierda != null)
            {
                InferiorDerecha.ConocerPadres();
            }
        }

        public bool EsConsecuente(Carta b)
        {
            if (this.Numero + 1 == b.Numero)
                return true;

            if (this.Numero - 1 == b.Numero)
                return true;

            if (this.Numero == 13 && b.Numero == 1)
                return true;

            if (this.Numero == 1 && b.Numero == 13)
                return true;

            return false;
        }

        #endregion

       
    }
}
